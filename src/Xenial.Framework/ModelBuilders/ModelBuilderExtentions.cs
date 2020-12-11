﻿using System;
using System.Linq;
using System.Linq.Expressions;

using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;

namespace Xenial.Framework.ModelBuilders
{
    /// <summary>
    /// Class ModelBuilderExtentions.
    /// </summary>
    public static class ModelBuilderExtentions
    {
        /// <summary>
        /// Determines whether the specified caption has caption.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="caption">The caption.</param>
        /// <returns></returns>
        public static IModelBuilder<T> HasCaption<T>(this IModelBuilder<T> builder, string caption)
            => builder.WithModelDefault(ModelDefaults.Caption, caption);

        /// <summary>
        /// Withes the model default.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="modelDefaultPropertyName">Name of the model default property.</param>
        /// <param name="modelDefaultPropertyValue">The model default property value.</param>
        /// <returns></returns>
        public static IModelBuilder<T> WithModelDefault<T>(this IModelBuilder<T> builder, string modelDefaultPropertyName, string modelDefaultPropertyValue)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            return builder.WithAttribute(new ModelDefaultAttribute(modelDefaultPropertyName, modelDefaultPropertyValue));
        }

        /// <summary>
        /// Withes the model default.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="modelDefaultPropertyName">Name of the model default property.</param>
        /// <param name="modelDefaultPropertyValue">if set to <c>true</c> [model default property value].</param>
        /// <returns></returns>
        public static IModelBuilder<T> WithModelDefault<T>(this IModelBuilder<T> builder, string modelDefaultPropertyName, bool modelDefaultPropertyValue)
            => builder.WithModelDefault(modelDefaultPropertyName, modelDefaultPropertyValue.ToString());

        /// <summary>
        /// Determines whether the specified image name has image.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="imageName">Name of the image.</param>
        /// <returns></returns>
        public static IModelBuilder<T> HasImage<T>(this IModelBuilder<T> builder, string imageName)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            return builder.WithAttribute(new ImageNameAttribute(imageName));
        }

        /// <summary>
        /// Determines whether [has default property] [the specified property name].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public static IModelBuilder<T> HasDefaultProperty<T>(this IModelBuilder<T> builder, string propertyName)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            return builder
                    .WithAttribute(new System.ComponentModel.DefaultPropertyAttribute(propertyName))
                    .WithAttribute(new DevExpress.ExpressApp.DC.XafDefaultPropertyAttribute(propertyName));
        }

        /// <summary>
        /// Determines whether [has default property] [the specified property].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static IModelBuilder<T> HasDefaultProperty<T, TProp>(this IModelBuilder<T> builder, Expression<Func<T, TProp>> property)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            return builder.HasDefaultProperty(builder.Exp.Property(property));
        }

        /// <summary>
        /// Determines whether [has object caption format] [the specified format string].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="formatString">The format string.</param>
        /// <returns></returns>
        public static IModelBuilder<T> HasObjectCaptionFormat<T>(this IModelBuilder<T> builder, string formatString)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            return builder.WithAttribute(new ObjectCaptionFormatAttribute(formatString));
        }

        /// <summary>
        /// Determines whether [has object caption format] [the specified property].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        public static IModelBuilder<T> HasObjectCaptionFormat<T, TProp>(this IModelBuilder<T> builder, Expression<Func<T, TProp>> property)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            return builder.HasObjectCaptionFormat($"{{0:{builder.Exp.Property(property)}}}");
        }

        /// <summary>
        /// Nots the allowing edit.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IModelBuilder<T> NotAllowingEdit<T>(this IModelBuilder<T> builder)
            => builder.WithModelDefault(ModelDefaults.AllowEdit, false);

        /// <summary>
        /// Allowings the edit.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IModelBuilder<T> AllowingEdit<T>(this IModelBuilder<T> builder)
            => builder.WithModelDefault(ModelDefaults.AllowEdit, true);

        /// <summary>
        /// Nots the allowing new.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IModelBuilder<T> NotAllowingNew<T>(this IModelBuilder<T> builder)
            => builder.WithModelDefault(ModelDefaults.AllowNew, false);

        /// <summary>
        /// Allowings the new.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IModelBuilder<T> AllowingNew<T>(this IModelBuilder<T> builder)
            => builder.WithModelDefault(ModelDefaults.AllowNew, true);

        /// <summary>
        /// Nots the allowing delete.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IModelBuilder<T> NotAllowingDelete<T>(this IModelBuilder<T> builder)
            => builder.WithModelDefault(ModelDefaults.AllowDelete, false);

        /// <summary>
        /// Allowings the delete.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IModelBuilder<T> AllowingDelete<T>(this IModelBuilder<T> builder)
            => builder.WithModelDefault(ModelDefaults.AllowDelete, true);

        /// <summary>
        /// Allowings the nothing.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IModelBuilder<T> AllowingNothing<T>(this IModelBuilder<T> builder)
            => builder
                .NotAllowingDelete()
                .NotAllowingEdit()
                .NotAllowingNew();

        /// <summary>
        /// Allowings the everything.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IModelBuilder<T> AllowingEverything<T>(this IModelBuilder<T> builder)
            => builder
                .AllowingDelete()
                .AllowingEdit()
                .AllowingNew();

        /// <summary>
        /// Determines whether [is not visible in reports].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IModelBuilder<T> IsNotVisibleInReports<T>(this IModelBuilder<T> builder)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            return builder.WithAttribute(new VisibleInReportsAttribute(false));
        }

        /// <summary>
        /// Determines whether [is visible in reports].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IModelBuilder<T> IsVisibleInReports<T>(this IModelBuilder<T> builder)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            return builder.WithAttribute(new VisibleInReportsAttribute(true));
        }

        /// <summary>
        /// Determines whether [is not visible in dashboards].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IModelBuilder<T> IsNotVisibleInDashboards<T>(this IModelBuilder<T> builder)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            return builder.WithAttribute(new VisibleInDashboardsAttribute(false));
        }

        /// <summary>
        /// Determines whether [is visible in dashboards].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IModelBuilder<T> IsVisibleInDashboards<T>(this IModelBuilder<T> builder)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            return builder.WithAttribute(new VisibleInDashboardsAttribute(true));
        }

        /// <summary>
        /// Withes the default class options.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IModelBuilder<T> WithDefaultClassOptions<T>(this IModelBuilder<T> builder)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            return builder.WithAttribute(new DefaultClassOptionsAttribute());
        }

        /// <summary>
        /// Determines whether [has navigation item] [the specified navigation group name].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="navigationGroupName">Name of the navigation group.</param>
        /// <returns></returns>
        public static IModelBuilder<T> HasNavigationItem<T>(this IModelBuilder<T> builder, string navigationGroupName)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            return builder.WithAttribute(new NavigationItemAttribute(navigationGroupName));
        }

        //TODO: Singleton and Layoutbuilders
        ///// <summary>
        ///// Determines whether the specified class is singleton.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="builder">The builder.</param>
        ///// <returns>IModelBuilder&lt;T&gt;.</returns>
        //public static IModelBuilder<T> IsSingleton<T>(this IModelBuilder<T> builder)
        //    => builder.WithAttribute(new SingletonAttribute());

        ///// <summary>
        ///// Withes the default detail view.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="builder">The builder.</param>
        ///// <param name="layoutBuilderType">Type of the layout builder.</param>
        ///// <returns>IModelBuilder&lt;T&gt;.</returns>
        ///// <autogeneratedoc />
        //public static IModelBuilder<T> WithDefaultDetailView<T>(this IModelBuilder<T> builder, Type layoutBuilderType)
        //    => builder.WithAttribute(new LayoutBuilderAttribute(layoutBuilderType)
        //    {
        //        DetailViewId = builder.DefaultDetailView
        //    });

        ///// <summary>
        ///// Withes the default list view.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="builder">The builder.</param>
        ///// <param name="layoutBuilderType">Type of the layout builder.</param>
        ///// <returns>IModelBuilder&lt;T&gt;.</returns>
        //public static IModelBuilder<T> WithDefaultListView<T>(this IModelBuilder<T> builder, Type layoutBuilderType)
        //    => builder.WithAttribute(new ColumnBuilderAttribute(layoutBuilderType)
        //    {
        //        ListViewId = builder.DefaultListView
        //    });

        ///// <summary>
        ///// Withes the default lookup list view.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="builder">The builder.</param>
        ///// <param name="layoutBuilderType">Type of the layout builder.</param>
        ///// <returns>IModelBuilder&lt;T&gt;.</returns>
        //public static IModelBuilder<T> WithDefaultLookupListView<T>(this IModelBuilder<T> builder, Type layoutBuilderType)
        //    => builder.WithAttribute(new ColumnBuilderAttribute(layoutBuilderType)
        //    {
        //        ListViewId = builder.DefaultLookupListView
        //    });
    }
}