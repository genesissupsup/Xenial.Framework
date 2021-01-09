﻿using System;
using System.Linq;

using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.Persistent.Base;

namespace Xenial.Framework.StepProgressEditors.Model.GeneratorUpdaters
{
    /// <summary>
    /// Class StepProgressBarEnumModelGeneratorUpdater.
    /// Implements the <see cref="DevExpress.ExpressApp.Model.ModelNodesGeneratorUpdater{DevExpress.ExpressApp.Model.NodeGenerators.ModelBOModelMemberNodesGenerator}" />
    /// </summary>
    /// <seealso cref="DevExpress.ExpressApp.Model.ModelNodesGeneratorUpdater{DevExpress.ExpressApp.Model.NodeGenerators.ModelBOModelMemberNodesGenerator}" />
    /// <autogeneratedoc />
    public class StepProgressBarEnumModelGeneratorUpdater : ModelNodesGeneratorUpdater<ModelBOModelMemberNodesGenerator>
    {
        /// <summary>
        /// Updates the Application Model node content generated by the Nodes Generator, specified by the <see cref="T:DevExpress.ExpressApp.Model.ModelNodesGeneratorUpdater`1" /> class' type parameter.
        /// </summary>
        /// <param name="node">A ModelNode Application Model node to be updated.</param>
        /// <autogeneratedoc />
        public override void UpdateNode(ModelNode node)
        {
            if (node is IModelBOModelClassMembers membersNode)
            {
                var editorType = FindPropertyEditorType(node);
                if (editorType is null)
                {
                    return;
                }

                var members = membersNode
                    .Select(m =>
                    {
                        var (memberInfo, attr) = (m, m.MemberInfo.FindAttribute<EditorAliasAttribute>(true));
                        return (memberInfo, attr);
                    })
                    .Where(info =>
                    {
                        var result = info.attr is not null && info.attr.Alias == EditorAliases.StepProgressEnumPropertyEditor;
                        if (result)
                        {
                            var underlyingType = Nullable.GetUnderlyingType(info.memberInfo.Type);
                            if (underlyingType != null && underlyingType.IsEnum)
                            {
                                return true;
                            }
                        }
                        return false;
                    });

                foreach (var member in members)
                {
                    member.memberInfo.PropertyEditorType = editorType;
                }

                static Type? FindPropertyEditorType(ModelNode n)
                {
                    var editorDescriptors = ((IModelSources)n.Application).EditorDescriptors;
                    if (editorDescriptors != null)
                    {
                        foreach (var typeRegistration in editorDescriptors.PropertyEditorRegistrations)
                        {
                            if (typeRegistration.Alias == EditorAliases.StepProgressEnumPropertyEditor)
                            {
                                return typeRegistration.EditorType;
                            }
                        }
                    }
                    return null;
                }
            }
        }
    }


}
