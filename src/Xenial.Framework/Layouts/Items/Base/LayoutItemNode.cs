﻿using System;
using System.ComponentModel;

using Xenial.Framework.Utils.Slugger;

namespace Xenial.Framework.Layouts.Items.Base
{
    /// <summary>
    /// 
    /// </summary>
    [XenialCheckLicence]
    public abstract partial record LayoutItemNode
    {
        private static SlugifierConfig CreateDefaultSlugifierConfig() => new()
        {
            CollapseWhiteSpace = true,
            CollapseDashes = true,
            ForceLowerCase = false,
            TrimWhitespace = true,
            StringReplacements =
            {
                ["."] = "-",
                [" "] = "-",
                ["/"] = "-",
                ["\\"] = "-",
            }
        };

        private static Slugifier defaultSlugifier = new(CreateDefaultSlugifierConfig());

        /// <summary>
        /// Gets or sets the default slugifier.
        /// </summary>
        /// <value>The default slugifier.</value>
        /// <exception cref="ArgumentNullException">defaultSlugifier</exception>
        /// <autogeneratedoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal Slugifier Slugifier { get; init; } = DefaultSlugifier ?? new(CreateDefaultSlugifierConfig());

        /// <summary>
        /// Gets or sets the default slugifier.
        /// </summary>
        /// <value>The default slugifier.</value>
        /// <exception cref="ArgumentNullException">defaultSlugifier</exception>
        /// <autogeneratedoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static Slugifier DefaultSlugifier
        {
            get => defaultSlugifier;
            set => defaultSlugifier = value ?? throw new ArgumentNullException(nameof(DefaultSlugifier));
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        /// <autogeneratedoc />
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public string? Id { get; init; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        /// <autogeneratedoc />
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public int? Index { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        /// <autogeneratedoc />
        internal LayoutItemNode? ParentItem { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public LayoutItemNode? Parent
        {
            get => ParentItem;
            set
            {
                if (ParentItem is LayoutItem oldParentItem)
                {
                    oldParentItem.Remove(this);
                }

                ParentItem = value;

                if (ParentItem is LayoutItem newParentItem)
                {
                    newParentItem.Add(this);
                }
            }
        }

        /// <summary>
        /// Gets the is root.
        /// </summary>
        /// <value>The is root.</value>
        /// <autogeneratedoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal bool IsRoot => ParentItem is null;

        /// <summary>
        /// Gets the is leaf.
        /// </summary>
        /// <value>The is leaf.</value>
        /// <autogeneratedoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal virtual bool IsLeaf => true;

        /// <summary>
        /// Gets the level.
        /// </summary>
        /// <value>The level.</value>
        /// <autogeneratedoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal virtual int Level => Parent is null ? 0 : Parent.Level + 1;

        /// <summary>
        /// Gets or sets the size of the relative.
        /// </summary>
        /// <value>The size of the relative.</value>
        /// <autogeneratedoc />
        public double? RelativeSize { get; set; }
    }
}
