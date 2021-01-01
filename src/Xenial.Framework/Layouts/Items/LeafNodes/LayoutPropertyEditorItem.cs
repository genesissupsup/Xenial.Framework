﻿#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CA1000 // Don't declare static members in generic types
#pragma warning disable IDE1006 // Naming Styles

using System;
using System.Linq.Expressions;

using DevExpress.ExpressApp.Model;

using Xenial.Data;
using Xenial.Framework.Layouts.Items.Base;

namespace Xenial.Framework.Layouts.Items.LeafNodes
{
    /// <summary>
    /// 
    /// </summary>
    [XenialCheckLicence]
    public partial record LayoutPropertyEditorItem(string PropertyEditorId) : LayoutViewItem(PropertyEditorId)
    {
        /// <summary>
        /// Creates the specified property editor identifier.
        /// </summary>
        /// <param name="propertyEditorId">The property editor identifier.</param>
        /// <returns>Xenial.Framework.Layouts.Items.LeafNodes.LayoutPropertyEditorItem.</returns>
        /// <autogeneratedoc />
        public static new LayoutPropertyEditorItem Create(string propertyEditorId)
            => new(propertyEditorId);

        /// <summary>
        /// Gets or sets the property editor options.
        /// </summary>
        /// <value>The property editor options.</value>
        /// <autogeneratedoc />
        public Action<IModelPropertyEditor>? PropertyEditorOptions { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    [XenialCheckLicence]
    public partial record LayoutPropertyEditorItem<TModelClass>(string ViewItemId) : LayoutPropertyEditorItem(ViewItemId)
        where TModelClass : class
    {
        protected static ExpressionHelper<TModelClass> ExpressionHelper { get; } = Xenial.Utils.ExpressionHelper.Create<TModelClass>();

        public static LayoutPropertyEditorItem<TModelClass> Create<TProperty>(Expression<Func<TModelClass, TProperty>> expression)
            => new(ExpressionHelper.Property(expression));
    }
}