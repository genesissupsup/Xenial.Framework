﻿using System;
using System.Linq;
using System.Reflection;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.Xpo;
using DevExpress.XtraBars.Ribbon;

using Xenial.FeatureCenter.Module.BusinessObjects.Editors;
using Xenial.Framework;
using Xenial.Framework.TokenEditors.Win;
using Xenial.Framework.StepProgressEditors.Win;
using Xenial.Framework.WebView.Win;

namespace Xenial.FeatureCenter.Module.Win
{
    public class FeatureCenterWindowsFormsModule : XenialModuleBase
    {
        protected override ModuleTypeList GetRequiredModuleTypesCore()
            => base.GetRequiredModuleTypesCore()
            .AndModuleTypes(
                typeof(FeatureCenterModule),
                typeof(XenialWebViewWindowsFormsModule),

                typeof(XenialTokenEditorsWindowsFormsModule),
                typeof(XenialStepProgressEditorsWindowsFormsModule)
            );

        protected override void RegisterEditorDescriptors(EditorDescriptorsFactory editorDescriptorsFactory)
        {
            base.RegisterEditorDescriptors(editorDescriptorsFactory);

            editorDescriptorsFactory.UseTokenObjectsPropertyEditorsWin<TokenEditorNonPersistentTokens>();
            editorDescriptorsFactory.UseTokenObjectsPropertyEditorsForTypeWin<XPCollection<TokenEditorPersistentTokens>>();
        }
    }
}
