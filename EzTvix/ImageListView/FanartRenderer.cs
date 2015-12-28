using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using RootKit.API;
using RootKit.Core;
using EzTvix.Provider;

namespace RootKit.Windows.Forms
{
    /// <summary>
    /// Represents the built-in renderers.
    /// </summary>
    public static partial class ImageListViewRenderers
    {
        #region MovieRenderer
        /// <summary>
        /// Displays items with large tiles.
        /// </summary>
        public class FanartRenderer : ImageListView.ImageListViewRenderer
        {
            private Font mCaptionFont;
            private int mTileWidth;
            private int mTextHeight;

            /// <summary>
            /// Gets or sets the width of the tile.
            /// </summary>
            public int TileWidth { get { return mTileWidth; } set { mTileWidth = value; } }

            private Font CaptionFont
            {
                get
                {
                    if (mCaptionFont == null)
                        mCaptionFont = new Font(ImageListView.Font, FontStyle.Bold);
                    return mCaptionFont;
                }
            }

            /// <summary>
            /// Initializes a new instance of the TilesRenderer class.
            /// </summary>
            public FanartRenderer()
                : this(0)
            {
                ;
            }

            /// <summary>
            /// Initializes a new instance of the TilesRenderer class.
            /// </summary>
            /// <param name="tileWidth">Width of tiles in pixels.</param>
            public FanartRenderer(int tileWidth)
            {
                mTileWidth = tileWidth;
            }

            /// <summary>
            /// Releases managed resources.
            /// </summary>
            public override void Dispose()
            {
                if (mCaptionFont != null)
                    mCaptionFont.Dispose();

                base.Dispose();
            }

            /// <summary>
            /// Returns item size for the given view mode.
            /// </summary>
            /// <param name="view">The view mode for which the item measurement should be made.</param>
            public override Size MeasureItem(RootKit.Windows.Forms.View view)
            {
                if (view == RootKit.Windows.Forms.View.Thumbnails)
                {
                    Size itemSize = new Size();
                    mTextHeight = (int)(5.8f * (float)CaptionFont.Height);
                    int textHeight = mImageListView.Font.Height;

                    // Calculate item size
                    Size itemPadding = new Size(4, 4);
                    itemSize.Width = ImageListView.ThumbnailSize.Width + 4 * itemPadding.Width + mTileWidth;
                    itemSize.Height = Math.Max(mTextHeight, ImageListView.ThumbnailSize.Height) + 2 * itemPadding.Height;

                    itemSize.Height += textHeight + System.Math.Max(4, textHeight / 3); // textHeight / 3 = vertical space between thumbnail and text  itemSize.Height += textHeight + System.Math.Max(4, textHeight / 3); // textHeight / 3 = vertical space between thumbnail and text
                    //itemSize.Height += mTextHeight + System.Math.Max(4, mTextHeight / 3); // textHeight / 3 = vertical space between thumbnail and text  itemSize.Height += textHeight + System.Math.Max(4, textHeight / 3); // textHeight / 3 = vertical space between thumbnail and text

                    return itemSize;

                }
                else
                    return base.MeasureItem(view);

                //Size itemPadding = new Size(4, 4);
                //itemSize = mImageListView.ThumbnailSize + itemPadding + itemPadding;
                //itemSize.Height += textHeight + System.Math.Max(4, textHeight / 3); // textHeight / 3 = vertical space between thumbnail and text
                
            }

            /// <summary>
            /// Draws the specified item on the given graphics.
            /// </summary>
            /// <param name="g">The System.Drawing.Graphics to draw on.</param>
            /// <param name="item">The ImageListViewItem to draw.</param>
            /// <param name="state">The current view state of item.</param>
            /// <param name="bounds">The bounding rectangle of item in client coordinates.</param>
            public override void DrawItem(Graphics g, ImageListViewItem item, ItemState state, Rectangle bounds)
            {
                if (ImageListView.View == RootKit.Windows.Forms.View.Thumbnails)
                {
                    Size itemPadding = new Size(8, 4);

                    // Paint background
                    using (Brush bItemBack = new SolidBrush(ImageListView.Colors.BackColor))
                    {
                        g.FillRectangle(bItemBack, bounds);
                    }
                    if ((ImageListView.Focused && ((state & ItemState.Selected) != ItemState.None)) ||
                        (!ImageListView.Focused && ((state & ItemState.Selected) != ItemState.None) && ((state & ItemState.Hovered) != ItemState.None)))
                    {
                        using (Brush bSelected = new LinearGradientBrush(bounds, ImageListView.Colors.SelectedColor1, ImageListView.Colors.SelectedColor2, LinearGradientMode.Vertical))
                        {
                            Utility.FillRoundedRectangle(g, bSelected, bounds, 4);
                        }
                    }
                    else if (!ImageListView.Focused && ((state & ItemState.Selected) != ItemState.None))
                    {
                        using (Brush bGray64 = new LinearGradientBrush(bounds, ImageListView.Colors.UnFocusedColor1, ImageListView.Colors.UnFocusedColor2, LinearGradientMode.Vertical))
                        {
                            Utility.FillRoundedRectangle(g, bGray64, bounds, 4);
                        }
                    }
                    if (((state & ItemState.Hovered) != ItemState.None))
                    {
                        using (Brush bHovered = new LinearGradientBrush(bounds, ImageListView.Colors.HoverColor1, ImageListView.Colors.HoverColor2, LinearGradientMode.Vertical))
                        {
                            Utility.FillRoundedRectangle(g, bHovered, bounds, 4);
                        }
                    }

                    // Draw the image
                    Image img = item.ThumbnailImage;
                    if (img != null)
                    {
                        Rectangle pos = Utility.GetSizedImageBounds(img, new Rectangle(bounds.Location + itemPadding, ImageListView.ThumbnailSize), 0.0f, 50.0f);
                        g.DrawImage(img, pos);
                        // Draw image border
                        if (Math.Min(pos.Width, pos.Height) > 32)
                        {
                            using (Pen pOuterBorder = new Pen(ImageListView.Colors.ImageOuterBorderColor))
                            {
                                g.DrawRectangle(pOuterBorder, pos);
                            }
                            if (System.Math.Min(mImageListView.ThumbnailSize.Width, mImageListView.ThumbnailSize.Height) > 32)
                            {
                                using (Pen pInnerBorder = new Pen(ImageListView.Colors.ImageInnerBorderColor))
                                {
                                    g.DrawRectangle(pInnerBorder, Rectangle.Inflate(pos, -1, -1));
                                }
                            }
                        }

                        // Draw item text
                        MoviePicture mPic = ((MoviePicture)item.Tag);
                        string TextToWrite;
                        TextToWrite = mPic.Width + "x" + mPic.Height;
                        SizeF szt = TextRenderer.MeasureText(TextToWrite, mImageListView.Font);
                        RectangleF rt;


                        using (StringFormat sf = new StringFormat())
                        {
                            rt = new RectangleF(bounds.Left + itemPadding.Width, bounds.Top + 2 * itemPadding.Height + mImageListView.ThumbnailSize.Height, mImageListView.ThumbnailSize.Width, szt.Height);
                            sf.Alignment = StringAlignment.Center;
                            sf.FormatFlags = StringFormatFlags.NoWrap;
                            sf.LineAlignment = StringAlignment.Center;
                            sf.Trimming = StringTrimming.EllipsisCharacter;
                            using (Brush bItemFore = new SolidBrush(ImageListView.Colors.ForeColor))
                            {
                                g.DrawString(TextToWrite, mImageListView.Font, bItemFore, rt, sf);
                            }
                        }
                        // Draw item text
                        int lineHeight = CaptionFont.Height;
                        //RectangleF rt;
                        //using (StringFormat sf = new StringFormat())
                        //{
                        //    rt = new RectangleF(bounds.Left + 2 * itemPadding.Width + ImageListView.ThumbnailSize.Width,
                        //        bounds.Top + itemPadding.Height + (Math.Max(ImageListView.ThumbnailSize.Height, mTextHeight) - mTextHeight) / 2,
                        //        mTileWidth, lineHeight);
                        //    sf.Alignment = StringAlignment.Near;
                        //    sf.FormatFlags = StringFormatFlags.NoWrap;
                        //    sf.LineAlignment = StringAlignment.Center;
                        //    sf.Trimming = StringTrimming.EllipsisCharacter;
                        //    MoviePicture mPic = ((MoviePicture)item.Tag);
                        //    string TextToWrite;
                        //    using (Brush bItemFore = new SolidBrush(ImageListView.Colors.ForeColor))
                        //    {

                        //        TextToWrite = mPic.ID.ToString(); // +" (" + mPic.Width + "x" + mPic.Height + ")";

                        //        g.DrawString(TextToWrite, CaptionFont, bItemFore, rt, sf);
                        //    }
                        //    using (Brush bItemDetails = new SolidBrush(ImageListView.Colors.PaneLabelColor))
                        //    {
                        //        rt.Offset(0, 1.5f * lineHeight);

                        //        //TextToWrite = mPic.UrlOriginal;

                        //        //g.DrawString(TextToWrite,
                        //        //    ImageListView.Font, bItemDetails, rt, sf);
                        //        //rt.Offset(0, 1.1f * lineHeight);

                        //        TextToWrite = "Size :" + mPic.Width + "x" + mPic.Height;
                        //        g.DrawString(TextToWrite,
                        //            ImageListView.Font, bItemDetails, rt, sf);
                        //        rt.Offset(0, 1.1f * lineHeight);

                        //    }
                        //}
                    }

                    // Item border
                    using (Pen pWhite128 = new Pen(Color.FromArgb(128, ImageListView.Colors.ControlBackColor)))
                    {
                        Utility.DrawRoundedRectangle(g, pWhite128, bounds.Left + 1, bounds.Top + 1, bounds.Width - 3, bounds.Height - 3, 4);
                    }
                    if (ImageListView.Focused && ((state & ItemState.Selected) != ItemState.None))
                    {
                        using (Pen pHighlight128 = new Pen(ImageListView.Colors.SelectedBorderColor))
                        {
                            Utility.DrawRoundedRectangle(g, pHighlight128, bounds.Left, bounds.Top, bounds.Width - 1, bounds.Height - 1, 4);
                        }
                    }
                    else if (!ImageListView.Focused && ((state & ItemState.Selected) != ItemState.None))
                    {
                        using (Pen pGray128 = new Pen(ImageListView.Colors.UnFocusedBorderColor))
                        {
                            Utility.DrawRoundedRectangle(g, pGray128, bounds.Left, bounds.Top, bounds.Width - 1, bounds.Height - 1, 4);
                        }
                    }
                    else if ((state & ItemState.Selected) == ItemState.None)
                    {
                        using (Pen pGray64 = new Pen(ImageListView.Colors.BorderColor))
                        {
                            Utility.DrawRoundedRectangle(g, pGray64, bounds.Left, bounds.Top, bounds.Width - 1, bounds.Height - 1, 4);
                        }
                    }

                    if (ImageListView.Focused && ((state & ItemState.Hovered) != ItemState.None))
                    {
                        using (Pen pHighlight64 = new Pen(ImageListView.Colors.HoverBorderColor))
                        {
                            Utility.DrawRoundedRectangle(g, pHighlight64, bounds.Left, bounds.Top, bounds.Width - 1, bounds.Height - 1, 4);
                        }
                    }

                    // Focus rectangle
                    if (ImageListView.Focused && ((state & ItemState.Focused) != ItemState.None))
                    {
                        ControlPaint.DrawFocusRectangle(g, bounds);
                    }
                }
                else
                    base.DrawItem(g, item, state, bounds);
            }
        }
        #endregion

    }
}