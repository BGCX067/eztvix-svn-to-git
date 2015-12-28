using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using EzTvix.Core;
using RootKit.Drawings;
using RootKit.Drawings.BasicFilters;

namespace EzTvix.Theme
{
    public static partial class Extensions
    {
        /// <summary>
        /// Extend the LayerToSave base class
        /// </summary>
        /// <param name="layertosave">Source</param>
        /// <param name="val">value to check</param>
        /// <returns>a boolean indicating the existence of the Val parameter in the LayerToSave</returns>
        public static bool contains(this Layer layertosave, Layer val)
        {
            return ((val & layertosave) == val);
        }
    }

    [Flags]
    public enum Layer
    {
        Background = 0x1,
        Foreground = 0x2,
        Text = 0x4
    }

    class ImageTemplate : ICloneable
    {
        
        #region *** Properties ***
        protected ResizeFilter resizeFilter = new ResizeFilter();
        protected ImageManager imageManager = new ImageManager();

        public Layer layerToSave = Layer.Background | Layer.Foreground | Layer.Text;
        protected String _name = "";
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value.Substring(0, (value.IndexOf(".") <= 0) ? value.Length : value.IndexOf("."));
            this.Extension = value.Substring(value.IndexOf(".") + 1, value.Length - value.IndexOf(".") - 1);
            }
        }
        protected string _Theme;
        protected ImageFormat _extension = ImageFormat.Png;
        public String Extension
        {
            get
            {
                if (_extension.ToString().ToLower() == "jpeg")
                    return "jpg";
                return _extension.ToString();
            }
            set{ 
                switch(value.ToLower())
                {
                    case "jpg":
                    case "jpeg":
                        _extension = ImageFormat.Jpeg;
                        break;
                    case "gif":
                        _extension = ImageFormat.Gif;
                        break;
                    default:
                        _extension = ImageFormat.Png;
                        break;
                }
            }
        }


        private bool saveLayersAsBase64 = false;
        protected Image _baseImage;
        public Image BaseImage
        {
            get { return _baseImage; }
            set
            {
                _baseImage = value;
                //if (isOldMethod) 
                //    _layerFront = value;
                //else
                   this.Foreground.Image = value;
                   this.Foreground.Mask = new Rectangle(0, 0, value.Width, value.Height);
            }
        }

        protected Image _image;
        public Image Image
        {
            get {
                this.build();
                return _image; }
            set { _image = value; }
        }

        protected Rectangle _size = TvixInfo.IconMask;
        /// <summary>
        /// Size of the image (Showing the position and size of the image)
        /// </summary>
        public Rectangle Size
        {
            get { return _size; }
            set { _size = value; }
        }

        #region *** Layers ***
        protected Image _layerAlpha= global::EzTvix.Theme.Default.Empty;
        protected Image LayerAlpha
        {
            get { return _layerAlpha; }
            set
            {
                if (_layerAlpha == null)
                {
                    _layerAlpha = global::EzTvix.Theme.Default.Empty;
                }
            }
        }

        protected ImageLayer _background = new ImageLayer(TvixInfo.IconMask);
        public ImageLayer Background
        {
            get { return _background; }
            set
            {
                if (value != null)
                {
                    _background = value;
                }
                else
                    _background.Image = global::EzTvix.Theme.Default.Empty;
            }
        }

        protected ImageLayer _foreground = new ImageLayer(TvixInfo.IconMask);
        public ImageLayer Foreground
        {
            get { return _foreground; }
            set
            {
                if (value != null)
                {
                    _foreground = value;
                }
                else
                    _foreground.Image = global::EzTvix.Theme.Default.Empty;
            }
        }

        protected ImageLayer _text = new ImageLayer(TvixInfo.IconMask);
        public ImageLayer Text
        {
            get { return _text; }
            set
            {
                if (value != null)
                {
                    _text = value;
                }
                else
                    _text.Image = global::EzTvix.Theme.Default.Empty;
            }
        }
        #endregion
        
        public int Width { get { return _baseImage.Width; } }
        public int Height { get { return _baseImage.Height; } }

        protected bool _isDefault = true;

        public bool isDefault
        {
            get
            {
                return (this._Theme.ToLower() == "default");
            }
            //set { _isDefault = value; }
        }

        #endregion

        #region *** Constructor ***
        public ImageTemplate()
        {
            BaseImage = global::EzTvix.Theme.Default.Empty;
            resizeFilter.Width = 200;
            resizeFilter.Height = 200;
            resizeFilter.KeepAspectRatio = true;
            //_txtFormat.Alignment = StringAlignment.Center;
            this.Text.TextFormat.Alignment = StringAlignment.Center;

        }
        public ImageTemplate(Image _newImage)
            : this()
        {
            BaseImage = _newImage;
        }
        public ImageTemplate(String _imageName, String _themeName)
            : this()
        {
            BaseImage = this.getPic(_imageName, _themeName);
        }
        public ImageTemplate(String _imageName, String _themeName, Layer _layer)
            : this()
        {
            if (_layer == Layer.Background)
            {
                this._background.Image = this.getPic(_imageName, _themeName);
                this._baseImage = this._background.Image;
            }
            else
                BaseImage = this.getPic(_imageName, _themeName);
        }
        public ImageTemplate(String _imageName, String _AlternateImage, String _themeName)
            : this()
        {
            BaseImage = this.getPic(_imageName, _AlternateImage, _themeName);
        }
        public ImageTemplate(ImageTemplate _newImage)
            : this()
        {
            this.Name = _newImage.Name;
            this._Theme = _newImage._Theme;
            this._layerAlpha = _newImage._layerAlpha;
            //this._layerBack = _newImage._layerBack;
            //this._layerFront = _newImage._layerFront;
            //this._layerText = _newImage._layerText;
            //this._mask = _newImage._mask;
            //this._txtFont = _newImage._txtFont;
            //this._txtZone = _newImage._txtZone;
            //this._txtColor = _newImage._txtColor;
            this._baseImage = _newImage._baseImage;
            this._image = _newImage._image;
        }
        #endregion

        #region *** Operators ***
        public static implicit operator Image(ImageTemplate i)
        {

            return i.Image;
        }

        public object Clone()
        {
            ImageTemplate copy = new ImageTemplate();

            //DefaultImage = _newImage;
            copy._layerAlpha = (Image)this._layerAlpha.Clone();
            //copy._layerBack = (Image)this._layerBack.Clone();
            //copy._layerFront = (Image)this._layerFront.Clone();
            //copy._layerText = (Image)this._layerText.Clone();
            //copy._mask = this._mask;
            //copy._txtFont = (Font)this._txtFont.Clone();
            copy._baseImage = (Image)this._baseImage.Clone();
            copy._image = (Image)this._image.Clone();
            return copy;
        }
        #endregion

        #region *** Methods ***
        public void load(Image _base)
        {
            BaseImage = _base;
            
        }

        public void reset()
        {
            _image = _baseImage;
        }

        public void build()
        {
            Graphics _graphicContainer;
            ResizeFilter _resizeFilter = new ResizeFilter();
            Rectangle _square = new Rectangle(0, 0, 200, 200);
            this._image = global::EzTvix.Theme.Default.Empty;

            try
            {
                // Préparation de l'image
                _graphicContainer = System.Drawing.Graphics.FromImage(this._image);

                // chargemet de l'image courante
                resizeFilter.Width = this.Background.Mask.Width;
                resizeFilter.Height = this.Background.Mask.Height;
                resizeFilter.KeepAspectRatio = false;

               _graphicContainer.DrawImageUnscaledAndClipped(resizeFilter.ExecuteFilter(_background.Image), this.Background.Mask);
                _graphicContainer.DrawImageUnscaledAndClipped(this.Foreground, this.Foreground.Mask);
                _graphicContainer.DrawImageUnscaledAndClipped(this.Text, this.Text.Mask);

                _graphicContainer.Dispose();
                _graphicContainer = null;
            }
            catch (Exception e)
            {
            }
        }

        public XmlNode Save()
        {
            String imagePath = TvixInfo.ThemeFolder + @"\" + this._Theme + @"\" + this.Name + "." + this.Extension;
            Graphics _graphicContainer;
            ResizeFilter _resizeFilter = new ResizeFilter();
            Bitmap bmp;

            try
            {
                resizeFilter.Width = this.Size.Width;
                resizeFilter.Height = this.Size.Height;
                resizeFilter.KeepAspectRatio = false;

                bmp = new Bitmap(resizeFilter.ExecuteFilter(global::EzTvix.Theme.Default.Empty));
                // Préparation de l'image
                _graphicContainer = System.Drawing.Graphics.FromImage(bmp);

                // chargemet de l'image courante

                if ((Layer.Background & this.layerToSave) == Layer.Background)
                    _graphicContainer.DrawImageUnscaledAndClipped(resizeFilter.ExecuteFilter(_background.Image), this.Background.Mask);
                if ((Layer.Foreground & this.layerToSave) == Layer.Foreground)
                    _graphicContainer.DrawImageUnscaledAndClipped(this.Foreground, this.Foreground.Mask);
                if ((Layer.Text & this.layerToSave ) == Layer.Text)
                    _graphicContainer.DrawImageUnscaledAndClipped(this.Text, this.Text.Mask);
                
                _graphicContainer.Dispose();
                _graphicContainer = null;
            }
            catch (Exception e)
            {
                bmp = new Bitmap(this.Image);
            }


            XmlDocument doc = new XmlDocument();
            doc.LoadXml(@"<Item name='" + this.Name + "'></Item>");

            if (this.Name != "" && this.Name != null)
            {
                if (File.Exists(imagePath))
                {
                    try
                    {
                        File.Delete(imagePath);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                // using Image.save cause a lock problem (file in use) when all the pictures are opened with image.fromfile
                // image.fromfile replaced by 
                // private Image FromStream(string _imagePath)
                // {
                //    FileStream fs = new FileStream(_imagePath, FileMode.Open);
                //    Image imgPhoto = Image.FromStream(fs);
                //    fs.Close();
                //    fs.Dispose();
                //    return imgPhoto;
                // }

                bmp.Save(imagePath, this._extension);
                bmp.Dispose();
                bmp = null;
            }
            // get root node
            XmlElement root = doc.DocumentElement;

            #region --- Mask ---
            //if (this.Background.Mask != new Rectangle(0, 0, this.Width, this.Height))
            //{
                XmlNode maskNode = doc.CreateNode(XmlNodeType.Element, "Mask", "");
                XmlAttribute maskX = doc.CreateAttribute("x"); maskX.Value = this.Background.Mask.X.ToString();
                XmlAttribute maskY = doc.CreateAttribute("y"); maskY.Value = this.Background.Mask.Y.ToString();
                XmlAttribute maskWidth = doc.CreateAttribute("width"); maskWidth.Value = this.Background.Mask.Width.ToString();
                XmlAttribute maskHeight = doc.CreateAttribute("height"); maskHeight.Value = this.Background.Mask.Height.ToString();
                maskNode.Attributes.Append(maskX);
                maskNode.Attributes.Append(maskY);
                maskNode.Attributes.Append(maskWidth);
                maskNode.Attributes.Append(maskHeight);
                root.AppendChild(maskNode);
            //}
            #endregion
            #region --- Text ---
            XmlNode textFont = doc.CreateNode(XmlNodeType.Element, "Text", "");
            XmlAttribute fontFamily = doc.CreateAttribute("family"); fontFamily.Value = this.Text.TextFont.FontFamily.Name.ToString();
            XmlAttribute fontSize = doc.CreateAttribute("size"); fontSize.Value = this.Text.TextFont.Size.ToString();
            XmlAttribute fontBold = doc.CreateAttribute("bold"); fontBold.Value = (this.Text.TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            XmlAttribute fontItalic = doc.CreateAttribute("italic"); fontItalic.Value = (this.Text.TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            XmlAttribute fontColor = doc.CreateAttribute("color"); fontColor.Value = this.Text.TextColor.Name;
            textFont.Attributes.Append(fontFamily);
            textFont.Attributes.Append(fontSize);
            textFont.Attributes.Append(fontBold);
            textFont.Attributes.Append(fontItalic);
            textFont.Attributes.Append(fontColor);
            root.AppendChild(textFont);

            //if (this.Text.TextZone != new Rectangle(0, 0, this.Width, this.Height))
            //{
                XmlNode textNode = doc.CreateNode(XmlNodeType.Element, "TextZone", "");
                XmlAttribute textX = doc.CreateAttribute("x"); textX.Value = this.Text.TextZone.X.ToString();
                XmlAttribute textY = doc.CreateAttribute("y"); textY.Value = this.Text.TextZone.Y.ToString();
                XmlAttribute textWidth = doc.CreateAttribute("width"); textWidth.Value = this.Text.TextZone.Width.ToString();
                XmlAttribute textHeight = doc.CreateAttribute("height"); textHeight.Value = this.Text.TextZone.Height.ToString();
                textNode.Attributes.Append(textX);
                textNode.Attributes.Append(textY);
                textNode.Attributes.Append(textWidth);
                textNode.Attributes.Append(textHeight);
                root.AppendChild(textNode);
            //}

            #endregion
            #region ---Layers---
            if (saveLayersAsBase64)
            {
                //XmlNode layerNode = doc.CreateNode(XmlNodeType.Element, "Layers", "");
                XmlNode layerNode = doc.CreateNode(XmlNodeType.Element, "Image", "");
                layerNode.InnerText = ImageToBase64(this.BaseImage, this._extension);

                //XmlAttribute imgLayerBack = doc.CreateAttribute("back");
                //imgLayerBack.Value = ImageToBase64(this.Background.Image, this._extension);
                //layerNode.Attributes.Append(imgLayerBack);

                //XmlAttribute imgLayerFront = doc.CreateAttribute("front");
                //imgLayerFront.Value = ImageToBase64(this.Background.Image, this._extension);
                //layerNode.Attributes.Append(imgLayerFront);

                //XmlAttribute imgLayerText = doc.CreateAttribute("text");
                //imgLayerText.Value = ImageToBase64(this.Background.Image, this._extension);
                //layerNode.Attributes.Append(imgLayerText);

                root.AppendChild(layerNode);
            }
            #endregion

            // TODO save shadow

            return root;
        }

        #region *** DrawText ***
        public void DrawText(String text, Boolean dropShadow)
        {
            Image tempImg = global::EzTvix.Theme.Default.Empty;
            this.Background.Image = imageManager.DrawText(tempImg, text, this.Text.TextFormat, this.Text.TextFont, this.Text.Brush, this.Text.TextZone, dropShadow);
            this.Text.DrawText(text, dropShadow);
        }
        public void DrawText(String text, Font textFont, Brush brush, Boolean dropShadow)
        {
            Image tempImg = global::EzTvix.Theme.Default.Empty;
            this.Text.Image = imageManager.DrawText(tempImg, text, this.Text.TextFormat, textFont, brush, this.Text.TextZone, dropShadow);
            this.Text.DrawText(text, textFont, brush, dropShadow);
        }
        //public void DrawText(String _text, Brush _brush)
        //{
        //    Image tempImg = global::EzTvix.Theme.Default.Empty;
        //    this._layerText = imageManager.DrawText(tempImg, _text, _brush);
        //}
        //public void DrawText(String _text, StringFormat _strFormat, Brush _brush)
        //{
        //    Image tempImg = global::EzTvix.Theme.Default.Empty;
        //    this._layerText = imageManager.DrawText(tempImg, _text, _strFormat, _brush);
        //}
        //public void DrawText(String _text, Font _textFont)
        //{
        //    Image tempImg = global::EzTvix.Theme.Default.Empty;
        //    this._layerText = imageManager.DrawText(tempImg, _text, _textFont);
        //}
        //public void DrawText(String _text, StringFormat _strFormat)
        //{
        //    Image tempImg = global::EzTvix.Theme.Default.Empty;
        //    this._layerText = imageManager.DrawText(tempImg, _text, _strFormat);
        //}
        //public void DrawText(String _text, StringFormat _strFormat, Font _textFont, Brush _brush)
        //{
        //    Image tempImg = global::EzTvix.Theme.Default.Empty;
        //    this._layerText = imageManager.DrawText(tempImg, _text, _strFormat, _textFont, _brush);
        //}
        #endregion

        protected Image FromStream(string _imagePath)
        {
            FileStream fs = new FileStream(_imagePath, FileMode.Open);
            Image imgPhoto = Image.FromStream(fs);
            fs.Close();
            fs.Dispose();
            return imgPhoto;
        }

        protected Image getPicFromResx(String _imageName)
        {
            System.Reflection.Assembly a;
            System.IO.Stream file;
            a = System.Reflection.Assembly.GetExecutingAssembly();
            try
            {
                file = a.GetManifestResourceStream("EzTvix.Theme.Default." + _imageName.Replace("\\", ""));
                return Image.FromStream(file);
            }
            catch (ArgumentException e)
            {
                MessageBox.Show("Missing embedded ressource : \r\n - image : " + _imageName, e.Message, MessageBoxButtons.OK);

                return global::EzTvix.Theme.Default.Background;
            }
            catch (Exception e)
            {
                MessageBox.Show(_imageName, e.Message, MessageBoxButtons.OK);
                return global::EzTvix.Theme.Default.Empty;
            }
        }

        protected Image getPic(String _imageName, String _themeName)
        {
            String imagePath = TvixInfo.ThemeFolder + @"\" + _themeName + _imageName;
            this.Name = _imageName.Replace("\\", "");
            this._Theme = _themeName;
            this._extension = (_imageName.ToLower().EndsWith("jpg")) ? ImageFormat.Jpeg : ImageFormat.Png;

            try
            {
                return (isDefault) ? this.getPicFromResx(_imageName) : this.FromStream(imagePath);
            }
            catch (Exception e)
            {
                return this.getPicFromResx(_imageName);
            }
        }

        protected Image getPic(String _imageName, String _AlternateImage, String _themeName)
        {

            String imagePath = TvixInfo.ThemeFolder + @"\" + _themeName + _imageName;
            String alternateImagePath = TvixInfo.ThemeFolder + @"\" + _themeName + _AlternateImage;
            this.Name = _imageName.Replace("\\", "");
            this._Theme = _themeName;
            this._extension = (_imageName.ToLower().EndsWith("jpg")) ? ImageFormat.Jpeg : ImageFormat.Png;

            try
            {
                if (isDefault)
                {
                    return this.getPicFromResx(_imageName);
                }
                else
                {
                    return this.FromStream(imagePath);
                }

            }
            catch (FileNotFoundException e)
            {
                //this.Name = _AlternateImage.Replace("\\", "");
                try
                {
                    return this.FromStream(alternateImagePath);
                }
                catch (Exception ex) {
                    return this.getPicFromResx(_imageName);
                }
            }
            catch (Exception e)
            {
                return this.getPicFromResx(_imageName);
            }
        }

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
        #endregion
    }
}
