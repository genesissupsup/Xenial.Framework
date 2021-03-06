﻿
using System;

namespace DevExpress.ExpressApp.Editors
{
    /// <summary>
    /// Class StepProgressEnumPropertyEditorExtensions.
    /// </summary>
    /// <autogeneratedoc />
    public static class StepProgressEnumPropertyEditorExtensions
    {
        /// <summary>
        /// Uses the token objects property editor.
        /// </summary>
        /// <param name="editorDescriptorsFactory">The editor descriptors factory.</param>
        /// <returns>EditorDescriptorsFactory.</returns>
        /// <exception cref="System.ArgumentNullException">editorDescriptorsFactory</exception>
        /// <autogeneratedoc />
        public static EditorDescriptorsFactory UseStepProgressEnumPropertyEditors(this EditorDescriptorsFactory editorDescriptorsFactory)
        {
            _ = editorDescriptorsFactory ?? throw new ArgumentNullException(nameof(editorDescriptorsFactory));

            editorDescriptorsFactory.RegisterPropertyEditorAlias(
                Xenial.Framework.StepProgressEditors.PubTernal.StepProgressEditorAliases.StepProgressEnumPropertyEditor,
                typeof(Enum),
                true
            );

            return editorDescriptorsFactory;
        }

        /// <summary>
        /// Uses the token objects property editor.
        /// </summary>
        /// <typeparam name="TPropertyType">The type of the t property type.</typeparam>
        /// <param name="editorDescriptorsFactory">The editor descriptors factory.</param>
        /// <returns>EditorDescriptorsFactory.</returns>
        /// <exception cref="ArgumentNullException">editorDescriptorsFactory</exception>
        /// <exception cref="System.ArgumentNullException">editorDescriptorsFactory</exception>
        public static EditorDescriptorsFactory UseStepProgressEnumPropertyEditor<TPropertyType>(this EditorDescriptorsFactory editorDescriptorsFactory)
            where TPropertyType : Enum
        {
            _ = editorDescriptorsFactory ?? throw new ArgumentNullException(nameof(editorDescriptorsFactory));

            editorDescriptorsFactory.RegisterPropertyEditorAlias(
                Xenial.Framework.StepProgressEditors.PubTernal.StepProgressEditorAliases.StepProgressEnumPropertyEditor,
                typeof(TPropertyType),
                true
            );

            return editorDescriptorsFactory;
        }
    }
}
