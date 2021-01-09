﻿using System;

using Xenial;
using Xenial.Framework.StepProgressEditors.PubTernal;

namespace DevExpress.Persistent.Base
{
    /// <summary>
    /// Class WebViewUriEditorAttribute.
    /// Implements the <see cref="DevExpress.Persistent.Base.EditorAliasAttribute" />
    /// </summary>
    /// <seealso cref="DevExpress.Persistent.Base.EditorAliasAttribute" />
    /// <autogeneratedoc />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    [XenialCheckLicence]
    public sealed partial class StepProgressEnumEditorAttribute : EditorAliasAttribute
    {
        /// <summary>
        /// <para>Initializes a new instance of the <see cref="StepProgressEnumEditorAttribute"/> class.</para>
        /// </summary>
        public StepProgressEnumEditorAttribute() : base(StepProgressEditorAliases.StepProgressEnumPropertyEditor) { }
    }
}
