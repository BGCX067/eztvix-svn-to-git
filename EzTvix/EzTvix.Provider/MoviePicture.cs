using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using RootKit.Web;
using System.Xml;
using System.Drawing.Imaging;

namespace EzTvix.Provider
{
    public class MoviePicture
    {
        #region *** properties ***
        protected Int32 _id;
        public Int32 ID
        { get { return _id; } set { _id = value; } }

        protected String _type;
        public String Type
        { get { return _type; } set { _type = value; } }

        protected String _size;
        public String Size
        { get { return _size; } set { _size = value; } }

        protected Int32 _width;
        public Int32 Width
        {
            get { return _width; }
            set
            {
                _width = value;
            }
        }

        protected Int32 _height;
        public Int32 Height
        {
            get
            {
                return _height;
            }

            set
            {
                _height = value;
            }
        }

        protected String _urlPreview;
        public String UrlPreview
        { get { return _urlPreview; } set { _urlPreview = value; } }

        protected Image _picturePreview;
        public Image Preview
        { get { return _picturePreview; } set { _picturePreview = value; } }

        protected String _urlThumb;
        public String UrlThumb
        { get { return _urlThumb; } set { _urlThumb = value; } }

        protected Image _pictureThumb;
        public Image Thumb
        { get { return _pictureThumb; } set { _pictureThumb = value; } }

        protected String _urlOriginal;
        public String UrlOriginal
        { get { return _urlOriginal; } set { _urlOriginal = value; } }

        protected Image _pictureOriginal;
        public Image OriginalPic
        { get { return _pictureOriginal; } set { _pictureOriginal = value; } }

        protected Boolean m_thumbLoaded = false;
        public Boolean ThumbLoaded
        { get { return m_thumbLoaded; } set { m_thumbLoaded = value; } }
        #endregion

        #region Methods
        public Image loadPreview()
        {
            DownloadManager downMan = new DownloadManager();
            Image img = downMan.DownloadFromUrl(UrlPreview);
            this._picturePreview = img;
            return this._picturePreview;

        }
        public void loadThumbnail()
        {
            BackgroundWorker thumbCode = new BackgroundWorker();

            thumbCode.WorkerReportsProgress = true;
            thumbCode.WorkerSupportsCancellation = true;

            thumbCode.DoWork += new DoWorkEventHandler(thumbCode_DoWork);
            thumbCode.RunWorkerCompleted += new RunWorkerCompletedEventHandler(thumbCode_RunWorkerCompleted);

            thumbCode.RunWorkerAsync();
        }
        public Image loadOriginal()
        {
            DownloadManager downMan = new DownloadManager();
            Image img = downMan.DownloadFromUrl(UrlOriginal);
            this._pictureOriginal = img;
            return this._pictureOriginal;

        }

        private void thumbCode_DoWork(object sender, DoWorkEventArgs e)
        {
            DownloadManagerThreaded downMan = new DownloadManagerThreaded();
            BackgroundWorker worker = sender as BackgroundWorker;
            Image img = downMan.DownloadFromUrl("http://www.eztvix.info/Alpha.png",
                "", // means that the error message will not beeing displayed.
                worker, e);

            e.Result = img;

        }
        private void thumbCode_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Thumb = (Image)e.Result;
        }


        /// <summary>
        /// get the object as XML
        /// </summary>
        /// <param name="document"> the XML source document to populate</param>
        /// <returns>a XmlNode containing the objet</returns>
        public XmlNode Xml(XmlDocument document)
        {
            //XmlDocument doc = new XmlDocument();

            XmlNode pictureNode = document.CreateNode(XmlNodeType.Element, "Picture", "");
            XmlAttribute idNode = document.CreateAttribute("id"); idNode.Value = this.ID.ToString();
            XmlAttribute typeNode = document.CreateAttribute("type"); typeNode.Value = this.Type;
            pictureNode.Attributes.Append(idNode);
            pictureNode.Attributes.Append(typeNode);


            XmlNode genericNode = document.CreateNode(XmlNodeType.Element, "Thumb", "");
            XmlAttribute urlNode = document.CreateAttribute("url"); urlNode.Value = this.UrlThumb;
            if (this.Thumb != null)
                urlNode.InnerText = RootKit.Core.Converter.ImageToBase64(this.Thumb, ImageFormat.Png);
            genericNode.Attributes.Append(urlNode);
            pictureNode.AppendChild(genericNode);

            genericNode = document.CreateNode(XmlNodeType.Element, "Preview", "");
            urlNode = document.CreateAttribute("url"); urlNode.Value = this.UrlPreview;
            if (this.Preview != null)
                urlNode.InnerText = RootKit.Core.Converter.ImageToBase64(this.Preview, ImageFormat.Png);
            genericNode.Attributes.Append(urlNode);
            pictureNode.AppendChild(genericNode);

            genericNode = document.CreateNode(XmlNodeType.Element, "Original", "");
            urlNode = document.CreateAttribute("url"); urlNode.Value = this.UrlOriginal;
            if (this.Preview != null)
                urlNode.InnerText = RootKit.Core.Converter.ImageToBase64(this.Preview, ImageFormat.Png);
            genericNode.Attributes.Append(urlNode);
            pictureNode.AppendChild(genericNode);

            return pictureNode;
        }
        #endregion
    }
}
