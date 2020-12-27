﻿using System;

using DevExpress.Persistent.Base;
using DevExpress.Utils;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable IDE1006 // Naming Styles

namespace Xenial.Framework.Layouts.Items.Base
{
    /// <summary>
    /// 
    /// </summary>
    [XenialCheckLicence]
    public partial record LayoutViewItem : LayoutViewItemNode
    {
        public string ViewItemId { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutPropertyEditorItem"/> class.
        /// </summary>
        /// <param name="viewItemId">The property editor.</param>
        /// <autogeneratedoc />
        public LayoutViewItem(string viewItemId)
            => (ViewItemId, Id) = (viewItemId, Slugifier.GenerateSlug(viewItemId));

        /// <summary>
        /// Gets or sets the caption horizontal alignment.
        /// </summary>
        /// <value>The caption horizontal alignment.</value>
        public HorzAlignment? CaptionHorizontalAlignment { get; set; }

        /// <summary>
        /// Gets or sets the caption location.
        /// </summary>
        /// <value>The caption location.</value>
        public Locations? CaptionLocation { get; set; }

        /// <summary>
        /// Gets or sets the caption vertical alignment.
        /// </summary>
        /// <value>The caption vertical alignment.</value>
        public VertAlignment? CaptionVerticalAlignment { get; set; }

        /// <summary>
        /// Gets or sets the caption word wrap.
        /// </summary>
        /// <value>The caption word wrap.</value>
        public WordWrap? CaptionWordWrap { get; set; }

        /// <summary>
        /// Gets or sets the caption.
        /// </summary>
        /// <value>The caption.</value>
        public string? Caption { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show caption].
        /// </summary>
        /// <value><c>null</c> if [show caption] contains no value, <c>true</c> if [show caption]; otherwise, <c>false</c>.</value>
        /// <autogeneratedoc />
        public bool? ShowCaption { get; set; }

        /// <summary>
        /// Gets or sets the tool tip.
        /// </summary>
        /// <value>The tool tip.</value>
        /// <autogeneratedoc />
        public string? ToolTip { get; set; }

        /// <summary>
        /// Gets or sets the type of the tool tip icon.
        /// </summary>
        /// <value>The type of the tool tip icon.</value>
        /// <autogeneratedoc />
        public ToolTipIconType? ToolTipIconType { get; set; }

        /// <summary>
        /// Gets or sets the tool tip title.
        /// </summary>
        /// <value>The tool tip title.</value>
        /// <autogeneratedoc />
        public string? ToolTipTitle { get; set; }
    }
}
