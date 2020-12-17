﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading.Tasks;

using Xenial.Delicious.Beer.Recipes;

using static Bullseye.Targets;
using static SimpleExec.Command;
using static Xenial.Delicious.Beer.Recipes.IISRecipe;

namespace Xenial.Build
{
    internal static partial class Program
    {
        internal static async Task Main(string[] args)
        {
            var PublicKey = "MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAE3VFauRJrFzuZveL+J/naEs+CrNLBrc/sSDihdkUTo3Np/o4IoM8fxR6kYHIdH/7LXfXltFRREkv2ceTN8gyZuw==";

            static string logOptions(string target)
                => $"/maxcpucount /nologo /verbosity:minimal /bl:./artifacts/logs/xenial.framework.{target}.binlog";

            const string Configuration = "Release";

            var sln = RuntimeInformation
                .IsOSPlatform(OSPlatform.Windows)
                ? "Xenial.Framework.sln"
                : "Xenial.Framework.CrossPlatform.slnf";

            var demoBlazor = "./demos/FeatureCenter/Xenial.FeatureCenter.Blazor.Server/Xenial.FeatureCenter.Blazor.Server.csproj";

            string GetProperties() => string.Join(" ", new Dictionary<string, string>
            {
                ["Configuration"] = Configuration,
                ["XenialPublicKey"] = PublicKey
            }.Select(p => $"/P:{p.Key}=\"{p.Value}\""));

            Target("ensure-tools", () => EnsureTools());

            Target("clean", DependsOn("ensure-tools"),
                () => RunAsync("dotnet", $"rimraf . -i **/bin/**/*.* -i **/obj/**/*.* -i artifacts/**/*.* -e node_modules/**/*.* -e build/**/*.* -q")
            );

            Target("lint", DependsOn("ensure-tools"),
                () => RunAsync("dotnet", $"format --exclude ext --check --verbosity diagnostic")
            );

            Target("format", DependsOn("ensure-tools"),
                () => RunAsync("dotnet", $"format --exclude ext")
            );

            Target("restore", DependsOn("lint"),
                () => RunAsync("dotnet", $"restore {logOptions("restore")}")
            );

            Target("build", DependsOn("restore"),
                () => RunAsync("dotnet", $"build {sln} --no-restore -c {Configuration} {logOptions("build")} {GetProperties()}")
            );

            Target("test", DependsOn("build"), async () =>
            {
                var (fullFramework, netcore, net5) = FindTfms();

                var tfms = RuntimeInformation
                            .IsOSPlatform(OSPlatform.Windows)
                            ? new[] { fullFramework, netcore, net5 }
                            : new[] { netcore, net5 };

                var tests = tfms
                    .Select(tfm => RunAsync("dotnet", $"run --project test/Xenial.Framework.Tests/Xenial.Framework.Tests.csproj --no-build --no-restore --framework {tfm} -c {Configuration} {GetProperties()}"))
                    .ToArray();

                await Task.WhenAll(tests);
            });

            Target("lic", DependsOn("test"),
                async () =>
                {
                    var files = Directory.EnumerateFiles(@"src", "*.csproj", SearchOption.AllDirectories).Select(file => new
                    {
                        ProjectName = $"src/{Path.GetFileNameWithoutExtension(file)}/{Path.GetFileName(file)}",
                        ThirdPartyName = $"src/{Path.GetFileNameWithoutExtension(file)}/THIRD-PARTY-NOTICES.TXT"
                    });

                    // Filter files that are cross platform
                    if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        using var slnFilter = File.OpenRead(sln);
                        var filter = await JsonDocument.ParseAsync(slnFilter);
                        var solution = filter.RootElement.GetProperty("solution");
                        var projects = solution.GetProperty("projects");

                        var items = projects.EnumerateArray();
                        var srcFilter = items.Select(s => s.GetString()).Where(s => s.StartsWith("src")).ToList();

                        files = files.Where(f => srcFilter.Contains(f.ProjectName));
                    }

                    var tasks = files.Select(proj => RunAsync("dotnet", $"thirdlicense --project {proj.ProjectName} --output {proj.ThirdPartyName}"));

                    await Task.WhenAll(tasks);
                }
            );

            Target("pack", DependsOn("test"), //TODO: generate lic on deployment
                () => RunAsync("dotnet", $"pack {sln} --no-restore --no-build -c {Configuration} {logOptions("pack.nuget")} {GetProperties()}")
            );

            BuildAndDeployIISProject(new IISDeployOptions("Xenial.FeatureCenter.Blazor", "framework.featurecenter.xenial.io")
            {
                PathToCsproj = demoBlazor,
                AssemblyProperties = "/property:XenialDebug=false"
            }, "Xenial.FeatureCenter.Blazor");

            Target("demos", DependsOn("pack", "publish:Xenial.FeatureCenter.Blazor"));

            Target("docs",
                () => RunAsync("dotnet", "wyam docs -o ../artifacts/docs")
            );

            Target("docs.serve", DependsOn("ensure-tools"),
                () => RunAsync("dotnet", "wyam docs -o ../artifacts/docs -w -p")
            );

            Target("deploy.nuget", DependsOn("ensure-tools"), async () =>
            {
                var files = Directory.EnumerateFiles("artifacts/nuget", "*.nupkg");

                foreach (var file in files)
                {
                    await RunAsync("dotnet", $"nuget push {file} --skip-duplicate -s https://api.nuget.org/v3/index.json -k {Environment.GetEnvironmentVariable("NUGET_AUTH_TOKEN")}",
                        noEcho: true
                    );
                }
            });

            Target("release", async () =>
            {
                await Release();
            });

            Target("default", DependsOn("test"));

            await RunTargetsAndExitAsync(args);
        }
    }
}
