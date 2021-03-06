﻿using System;
using System.Collections.Generic;
using System.Linq;

using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.SystemModule;

namespace Xenial.FeatureCenter.Module.Model.GeneratorUpdaters
{
    public sealed partial class FeatureCenterNavigationItemNodesUpdater : ModelNodesGeneratorUpdater<NavigationItemNodeGenerator>
    {
        private static readonly Dictionary<string, string> imageNames = new()
        {
            ["ModelBuilders"] = "direction1",
            ["Editors"] = "EditNames"
        };

        public override void UpdateNode(ModelNode node)
        {
            const string defaultGroupName = "Default";

            var nodesDictionary = new Dictionary<string, List<IModelNavigationItem>>();
            nodesDictionary[defaultGroupName] = new List<IModelNavigationItem>();

            if (node is IModelRootNavigationItems rootNavigationItems)
            {
                foreach (var item in rootNavigationItems.Items)
                {
                    CollectNodes(item);
                }

                rootNavigationItems.Items.ClearNodes();

                foreach (var groupName in nodesDictionary.Keys)
                {
                    var newItem = rootNavigationItems.Items.AddNode<IModelNavigationItem>(groupName);
                    newItem.ImageName = imageNames.ContainsKey(newItem.Id) ? imageNames[newItem.Id] : null;

                    var newNavItems = nodesDictionary[groupName];
                    foreach (var navItem in newNavItems)
                    {
                        var newNavItem = newItem.Items.AddNode<IModelNavigationItem>(navItem.Id);

                        CopyPropertiesTo(navItem, newNavItem);
                        if (newNavItem is IModelBaseChoiceActionItem newChoiceActionItem && navItem is IModelBaseChoiceActionItem choiceActionItem)
                        {
                            newChoiceActionItem.Caption = choiceActionItem.Caption;
                        }
                    }
                }
            }

            void AddOrAppendToDictionary(string groupName, IModelNavigationItem item)
            {
                if (!nodesDictionary!.ContainsKey(groupName))
                {
                    nodesDictionary[groupName] = new List<IModelNavigationItem>();
                }

                nodesDictionary[groupName].Add(item);
            }

            void AddToDictionary(IModelNavigationItem item, string? caption)
            {
                if (caption?.Contains("-") == true)
                {
                    var groupName = caption.Split('-')[0].Trim();
                    var newCaption = caption.Split('-')[1].Trim();
                    item.Caption = newCaption;

                    if (item is IModelBaseChoiceActionItem choiceActionItem)
                    {
                        choiceActionItem.Caption = newCaption;
                    }
                    AddOrAppendToDictionary(groupName, item);
                }
                else
                {
                    AddOrAppendToDictionary(defaultGroupName, item);
                }
            }

            void CollectNodes(IModelNavigationItem item)
            {
                if (item.View is IModelObjectView modelObjectView)
                {
                    AddToDictionary(item, item.Caption);
                }
                foreach (var nestedNode in item.Items)
                {
                    CollectNodes(nestedNode);
                }
            }

            static void CopyPropertiesTo<T>(T source, T dest)
            {
                var plist = from prop in typeof(T).GetProperties() where prop.CanRead && prop.CanWrite select prop;

                foreach (var prop in plist)
                {
                    prop.SetValue(dest, prop.GetValue(source, null), null);
                }
            }
        }
    }
}
