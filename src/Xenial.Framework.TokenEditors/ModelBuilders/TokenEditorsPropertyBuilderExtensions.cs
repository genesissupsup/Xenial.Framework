﻿using System;
using System.Collections.Generic;
using System.Text;

using DevExpress.Persistent.Base;

namespace Xenial.Framework.ModelBuilders
{
    /// <summary>
    /// Class TokenEditorsPropertyBuilderExtensions.
    /// </summary>
    /// <autogeneratedoc />
    public static class TokenEditorsPropertyBuilderExtensions
    {
        /// <summary>
        /// Use the Token String Property Editor <see cref="TokenStringEditorAttribute"/>
        /// </summary>
        /// <typeparam name="TClassType">The type of the type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IPropertyBuilder<string?, TClassType> UseTokenStringPropertyEditor<TClassType>(this IPropertyBuilder<string?, TClassType> builder)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            return builder.WithAttribute(new TokenStringEditorAttribute());
        }

        /// <summary>
        /// Use the Token Objects Property Editor <see cref="TokenObjectsEditorAttribute"/>
        /// </summary>
        /// <typeparam name="TClassType">The type of the type.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IPropertyBuilder<System.Collections.IList?, TClassType> UseTokenObjectsPropertyEditor<TClassType>(this IPropertyBuilder<System.Collections.IList?, TClassType> builder)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            return builder.WithAttribute(new TokenObjectsEditorAttribute());
        }
    }
}
