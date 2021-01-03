﻿using System.Collections.Generic;

using Xenial.Framework.Layouts.Items.Base;

#pragma warning disable CA1710 //Rename Type to end in Collection -> By Design
#pragma warning disable CA2227 //Collection fields should not have a setter: By Design

namespace Xenial.Framework.Layouts.Items.PubTernal
{
    /// <summary>
    /// Interface IEnumerableLayoutItemNode
    /// Implements the <see cref="ICollection{T}" />
    /// Implements the <see cref="IEnumerable{T}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="ICollection{T}" />
    /// <seealso cref="IEnumerable{T}" />
    /// <autogeneratedoc />
    public interface IEnumerableLayoutItemNode<T> : ICollection<T>, IEnumerable<T>
        where T : LayoutItemNode
    {
        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        /// <value>The children.</value>
        /// <autogeneratedoc />
        LayoutItemCollection<T> Children { get; set; }
    }
}
