using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using RootKit.Drawings.BasicFilters;
using System.Xml;

namespace RootKit.Drawings
{

    public enum LayerType
    { 
        Image,
        Text
    }
    public class ImageLayer
    {

        #region *** Properties ***
        protected ResizeFilter resizeFilter = new ResizeFilter();
        protected ImageManager imageManager = new ImageManager();
        private Font p_defaultFont;
        private Brush p_defaultBrush;
        private StringFormat p_defaultStrFormat;


        //protected Image _textImage = global::RootKit.Drawings.Images.Alpha;
        protected Image _baseImage;
        /// <summary>
        /// Image of the Layer
        /// </summary>
        public Image Image
        {
            get
            {
                if (this._baseImage == null)
                    this._baseImage = global::RootKit.Drawings.Images.Alpha;
                return _baseImage;
            }
            set
            {
                _baseImage = value;
            }
        }

        public Image ImageMasked
        {
            get
            {
                resizeFilter.Width = this.Mask.Width;
                resizeFilter.Height = this.Mask.Height;
                resizeFilter.KeepAspectRatio = false;
                return resizeFilter.ExecuteFilter(this.Image);
            }
        }

        protected Rectangle _mask = new Rectangle(0, 0, 200, 200);
        /// <summary>
        /// Mask of the image (Showing the position and size of the image)
        /// </summary>
        public Rectangle Mask
        {
            get { return _mask; }
            set { _mask = value; }
        }

        protected Font _textFont = new Font("Segoe UI", 20, FontStyle.Regular);
        /// <summary>
        /// Text font
        /// </summary>
        [CategoryAttribute("Text"), DescriptionAttribute("Text Font")]
        public Font TextFont
        {
            get { return _textFont; }
            set { _textFont = value; }
        }

        protected Color _textColor = Color.White;
        /// <summary>
        /// Text Color (use System.Drawing.Color enum to change the color)
        /// </summary>
        [CategoryAttribute("Text"), DescriptionAttribute("Text Color")]
        public Color TextColor { get { return _textColor; } set { _textColor = value; } }
        /// <summary>
        /// Get a System.Drawing.SolidBrush from the Text Color
        /// </summary>
        public Brush Brush { get { return new SolidBrush(_textColor); } }

        protected StringFormat _textFormat = new StringFormat();
        /// <summary>
        /// Text Format containing properties like textalign (e.g.: use TextFormat.Alignment = StringAlignment.Center)
        /// </summary>
        public StringFormat TextFormat { get { return _textFormat; } set { _textFormat = value; } }

        protected RectangleF _textZone = new RectangleF(0, 0, 0, 0);
        /// <summary>
        /// Zone where the text will be displayed
        /// </summary>
        [CategoryAttribute("Text"), DescriptionAttribute("Text Zone")]
        public Rectangle TextZone
        {
            get
            {
                return new Rectangle(
                    Int32.Parse(_textZone.X.ToString()),
                    int.Parse(_textZone.Y.ToString()),
                    int.Parse(_textZone.Width.ToString()),
                    int.Parse(_textZone.Height.ToString()));
                //return _txtZone; 
            }
            set
            {
                this._textZone = new RectangleF(
                    float.Parse(value.X.ToString()),
                    float.Parse(value.Y.ToString()),
                    float.Parse(value.Width.ToString()),
                    float.Parse(value.Height.ToString()));
            }
        }

        public string Name = "";
        public int Width { get { return _baseImage.Width; } }
        public int Height { get { return _baseImage.Height; } }

        private String _textData = "";
        public String TextData { get { return _textData; } set { _textData = value; } }

        private bool _textDropShadow = false;
        public bool TextShadow { get { return _textDropShadow; } set { _textDropShadow = value; } }
        public LayerType layerType = LayerType.Image;
        #endregion
        
        #region *** Operators ***
        public static implicit operator Image(ImageLayer i)
        {
            return i.Image;
        }
        #endregion

        #region *** constructor ***
        public ImageLayer()
        {
            //this.Image = global::RootKit.Drawings.Images.Alpha; // not needed already donc at field init (_baseImage)
            this.resizeFilter.Width = 200;
            this.resizeFilter.Height = 200;
            this.resizeFilter.KeepAspectRatio = false;
            this.TextFormat.Alignment = StringAlignment.Center;
        }
        //public ImageLayer(Int32 width, Int32 height)
        //    : this()
        //{
        //    this.resizeFilter.Width = width;
        //    this.resizeFilter.Height = height;
        //}
        public ImageLayer(Rectangle _maskInfo)
            : this()
        {
            this.resizeFilter.Width = _maskInfo.Width;
            this.resizeFilter.Height = _maskInfo.Height;
            this.Mask = _maskInfo;
        }
        //public ImageLayer(Image img, Int32 width, Int32 height)
        //    : this(width, height)
        //{
        //    if (img.Width != width || img.Height != height)
        //        this.Image = this.resizeFilter.ExecuteFilter(img);
        //    else
        //        this.Image = img;
        //}
        public ImageLayer(Image img, Rectangle _maskInfo)
            : this(_maskInfo)
        {
            if (img.Width != _maskInfo.Width || img.Height != _maskInfo.Height)
                this.Image = this.resizeFilter.ExecuteFilter(img);
            else
                this.Image = img;
        }

        public ImageLayer(Image img, Rectangle _maskInfo, LayerType _type)
            : this(_maskInfo)
        {
            this.layerType = _type;
            if (img.Width != _maskInfo.Width || img.Height != _maskInfo.Height)
                this.Image = this.resizeFilter.ExecuteFilter(img);
            else
                this.Image = img;
        }
        #endregion

        #region *** Method ***

        public void DrawText()
        {
            this.DrawText(_textData);
        }
        public void DrawText(String text)
        {
            this.DrawText(text, _textDropShadow);
        }
        public void DrawText(String text, Boolean dropShadow)
        {
            Image tempImg = global::RootKit.Drawings.Images.Alpha;
            tempImg = resizeFilter.ExecuteFilter(tempImg);
            this._baseImage = imageManager.DrawText(tempImg, text, TextFormat, TextFont, Brush, TextZone, dropShadow);
            //            this.Text.Image = imageManager.DrawText(tempImg, text, _txtFormat, _txtFont, Brush, _txtZone, dropShadow);
        }
        public void DrawText(String text, Font textFont, Brush brush, Boolean dropShadow)
        {
            Image tempImg = global::RootKit.Drawings.Images.Alpha;
            tempImg = resizeFilter.ExecuteFilter(tempImg);
            this._baseImage = imageManager.DrawText(tempImg, text, TextFormat, textFont, brush, TextZone, dropShadow);
        }


        #region *** DrawText ***
        //..
        //public Graphics DrawText(Graphics g, String _text)
        //{
        //    return DrawText(g, _text, p_defaultStrFormat);
        //}
        //public Graphics DrawText(Graphics g, String _text, Brush _brush)
        //{
        //    return DrawText(g, _text, p_defaultStrFormat, _brush);
        //}
        //public Graphics DrawText(Graphics g, String _text, StringFormat _strFormat, Brush _brush)
        //{
        //    return DrawText(g, _text, _strFormat, p_defaultFont, _brush);
        //}
        //public Graphics DrawText(Graphics g, String _text, Font _textFont)
        //{
        //    return DrawText(g, _text, p_defaultStrFormat, _textFont, p_defaultBrush);
        //}
        //public Graphics DrawText(Graphics g, String _text, Font _textFont, Brush _brush)
        //{
        //    return DrawText(g, _text, p_defaultStrFormat, _textFont, _brush);
        //}
        //public Graphics DrawText(Graphics g, String _text, StringFormat _strFormat)
        //{
        //    return DrawText(g, _text, _strFormat, p_defaultFont, p_defaultBrush);
        //}
        //public Graphics DrawText(Graphics g, String _text, StringFormat _strFormat, Font _textFont, Brush _brush)
        //{
        //    return DrawText(g, _text, _strFormat, _textFont, _brush, new RectangleF(0, 0, _bitmap.Width, _bitmap.Height), true);
        //}
        public Graphics DrawText(Graphics g)
        {
            return this.DrawText(g, _textData, TextFormat, TextFont, Brush, TextZone, _textDropShadow);
        }
        public Graphics DrawText(Graphics g, String text)
        {
            return this.DrawText(g, text, TextFormat, TextFont, Brush, TextZone, _textDropShadow);
        }
        public Graphics DrawText(Graphics g, String _text, StringFormat _strFormat, Font _textFont, Brush _brush, RectangleF _clip, bool _dropShadow)
        {
            RectangleF shadowClip = new RectangleF(_clip.X + 1, _clip.Y + 1, _clip.Width, _clip.Height);
            //RectangleF shadowClip = new RectangleF(_clip.X + 2, _clip.Y + 2, _clip.Width, _clip.Height);

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            //_strFormat.Alignment = StringAlignment.Near;
            if (_dropShadow)
                g.DrawString(_text, _textFont, Brushes.Black, shadowClip, _strFormat);

            //_strFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            //g.DrawString(_text, _textFont, _brush, _clip, _strFormat);
            g.DrawString(_text, _textFont, _brush, _clip, _strFormat);

            return g;
        }

        #endregion



        public Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
        public string ImageToBase64(Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);

                return base64String;
            }
        }

        public XmlNode Save()
        {
            XmlNode ItemNode;
            XmlAttribute itemName;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(@"<Layer name='" + this.Name  + "'></Layer>");

            // get root node
            XmlElement root = doc.DocumentElement;
            //ItemNode = doc.CreateNode(XmlNodeType.Element, "Layer", "");
            //itemName = doc.CreateAttribute("name"); itemName.Value = "Title";
            //ItemNode.Attributes.Append(itemName);
            switch (layerType)
            {
                case LayerType.Image:
                    #region --- Mask ---
                    // mask is saved only if different from base size
                    //if (this.Mask != new Rectangle(0, 0, this.Width, this.Height))
                    //{
                        XmlNode maskNode = doc.CreateNode(XmlNodeType.Element, "Mask", "");
                        XmlAttribute maskX = doc.CreateAttribute("x"); maskX.Value = this.Mask.X.ToString();
                        XmlAttribute maskY = doc.CreateAttribute("y"); maskY.Value = this.Mask.Y.ToString();
                        XmlAttribute maskWidth = doc.CreateAttribute("width"); maskWidth.Value = this.Mask.Width.ToString();
                        XmlAttribute maskHeight = doc.CreateAttribute("height"); maskHeight.Value = this.Mask.Height.ToString();
                        maskNode.Attributes.Append(maskX);
                        maskNode.Attributes.Append(maskY);
                        maskNode.Attributes.Append(maskWidth);
                        maskNode.Attributes.Append(maskHeight);
                        root.AppendChild(maskNode);
                    //}
                    #endregion
                    #region ---Layers---
                    //XmlNode layerNode = doc.CreateNode(XmlNodeType.Element, "Layers", "");

                    //XmlAttribute imgLayerBack = doc.CreateAttribute("back");
                    //imgLayerBack.Value = ImageToBase64(this..Image, this._extension);
                    //layerNode.Attributes.Append(imgLayerBack);

                    //XmlAttribute imgLayerFront = doc.CreateAttribute("front");
                    //imgLayerFront.Value = ImageToBase64(this.Image, this._extension);
                    //layerNode.Attributes.Append(imgLayerFront);

                    //XmlAttribute imgLayerText = doc.CreateAttribute("text");
                    //imgLayerText.Value = ImageToBase64(this.Image, this._extension);
                    //layerNode.Attributes.Append(imgLayerText);

                    //root.AppendChild(layerNode);
                    #endregion
                    break;

                case LayerType.Text:
                    #region --- Text ---
                    //if (this.TextZone != new Rectangle(0, 0, this.Width, this.Height))
                    //{
                        XmlNode textFont = doc.CreateNode(XmlNodeType.Element, "Text", "");
                        XmlAttribute fontFamily = doc.CreateAttribute("family"); fontFamily.Value = this.TextFont.FontFamily.Name.ToString();
                        XmlAttribute fontSize = doc.CreateAttribute("size"); fontSize.Value = this.TextFont.Size.ToString();
                        XmlAttribute fontBold = doc.CreateAttribute("bold"); fontBold.Value = (this.TextFont.Style == (FontStyle.Bold | FontStyle.Italic) || this.TextFont.Style == FontStyle.Bold ).ToString();
                        XmlAttribute fontItalic = doc.CreateAttribute("italic"); fontItalic.Value = (this.TextFont.Style == (FontStyle.Bold | FontStyle.Italic) || this.TextFont.Style == FontStyle.Italic).ToString();
                        XmlAttribute fontColor = doc.CreateAttribute("color"); fontColor.Value = this.TextColor.Name;
                        XmlAttribute fontShadow = doc.CreateAttribute("shadow"); fontShadow.Value = this.TextShadow.ToString();
                        textFont.Attributes.Append(fontFamily);
                        textFont.Attributes.Append(fontSize);
                        textFont.Attributes.Append(fontBold);
                        textFont.Attributes.Append(fontItalic);
                        textFont.Attributes.Append(fontColor);
                        if (this.TextShadow)
                            textFont.Attributes.Append(fontShadow);
                        root.AppendChild(textFont);



                        XmlNode textNode = doc.CreateNode(XmlNodeType.Element, "TextZone", "");
                        XmlAttribute textX = doc.CreateAttribute("x"); textX.Value = this.TextZone.X.ToString();
                        XmlAttribute textY = doc.CreateAttribute("y"); textY.Value = this.TextZone.Y.ToString();
                        XmlAttribute textWidth = doc.CreateAttribute("width"); textWidth.Value = this.TextZone.Width.ToString();
                        XmlAttribute textHeight = doc.CreateAttribute("height"); textHeight.Value = this.TextZone.Height.ToString();
                        textNode.Attributes.Append(textX);
                        textNode.Attributes.Append(textY);
                        textNode.Attributes.Append(textWidth);
                        textNode.Attributes.Append(textHeight);
                        root.AppendChild(textNode);

                        XmlNode textSample = doc.CreateNode(XmlNodeType.Element, "TextSample", "");
                        textSample.InnerText = this.TextData;
                        root.AppendChild(textSample);

                //}

                    #endregion
                    break;
            }



            return root;
        }

        #endregion
    }
}
