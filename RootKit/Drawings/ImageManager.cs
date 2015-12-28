using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace RootKit.Drawings
{
    public class ImageManager
    {
        private Font p_defaultFont;
        private Brush p_defaultBrush;
        private StringFormat p_defaultStrFormat;
        private Image p_DownloadedImage;
       
        public ImageManager()
        {
            
            p_defaultFont = new Font("Segoe UI", 20);
            p_defaultBrush = Brushes.White;
            p_defaultStrFormat = new StringFormat();
            p_defaultStrFormat.Alignment = StringAlignment.Center;
            p_defaultStrFormat.LineAlignment = StringAlignment.Far;
        }

        #region *** DrawText ***
        public Image DrawText(Image _bitmap, String _text)
            {
                return DrawText(_bitmap, _text, p_defaultStrFormat);
            }
        public Image DrawText(Image _bitmap, String _text, Brush _brush)
            {
                return DrawText(_bitmap, _text, p_defaultStrFormat, _brush);
            }
        public Image DrawText(Image _bitmap, String _text, StringFormat _strFormat, Brush _brush)
            {
                return DrawText(_bitmap, _text, _strFormat, p_defaultFont, _brush);
            }
        public Image DrawText(Image _bitmap, String _text, Font _textFont)
            {
                return DrawText(_bitmap, _text, p_defaultStrFormat, _textFont, p_defaultBrush);
            }
        public Image DrawText(Image _bitmap, String _text, Font _textFont, Brush _brush)
            {
                return DrawText(_bitmap, _text, p_defaultStrFormat, _textFont, _brush);
            }
        public Image DrawText(Image _bitmap, String _text, StringFormat _strFormat)
        {
            return DrawText(_bitmap, _text, _strFormat, p_defaultFont, p_defaultBrush);
        }
        public Image DrawText(Image _bitmap, String _text, StringFormat _strFormat, Font _textFont, Brush _brush)
        {
            return DrawText(_bitmap, _text, _strFormat, _textFont, _brush, new RectangleF(0, 0, _bitmap.Width, _bitmap.Height), true);
        }
        public Image DrawText(Image _bitmap, String _text, StringFormat _strFormat, Font _textFont, Brush _brush, RectangleF _clip, bool _dropShadow)
        {
            Graphics g = Graphics.FromImage(_bitmap);
            RectangleF shadowClip = new RectangleF(_clip.X + 1, _clip.Y + 1, _clip.Width, _clip.Height);
            if (_textFont.Bold)
                shadowClip = new RectangleF(_clip.X + 2, _clip.Y + 2, _clip.Width, _clip.Height);

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            //_strFormat.Alignment = StringAlignment.Near;
            if (_dropShadow) 
                g.DrawString(_text, _textFont, Brushes.Black, shadowClip, _strFormat);

            //_strFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            //g.DrawString(_text, _textFont, _brush, _clip, _strFormat);
            g.DrawString(_text, _textFont, _brush, _clip, _strFormat);
            
            return _bitmap;
        }

        #endregion

        #region *** Download Image ***
            /// <summary>  
        /// Function to download Image from website  
        /// </summary>  
        /// <param name="_URL">URL address to download image</param>  
        /// <returns>Image</returns>  
            public Image DownloadFromUrl(string _URL)
        {
            System.Drawing.Image _tmpImage = null;

            try
            {
                // Open a connection  
                System.Net.HttpWebRequest _HttpWebRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(_URL);

                _HttpWebRequest.AllowWriteStreamBuffering = true;

                // You can also specify additional header values like the user agent or the referer: (Optional)  
                _HttpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
                _HttpWebRequest.Referer = "http://www.movieposterdb.com";

                // set timeout for 20 seconds (Optional)  
                _HttpWebRequest.Timeout = 20000;

                // Request response:  
                System.Net.WebResponse _WebResponse = _HttpWebRequest.GetResponse();

                // Open data stream:  
                System.IO.Stream _WebStream = _WebResponse.GetResponseStream();


                // convert webstream to image  
                _tmpImage = System.Drawing.Image.FromStream(_WebStream);

                // Cleanup  
                _WebResponse.Close();
                _WebResponse.Close();
            }
            catch (Exception _Exception)
            {
                // Error  
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
                return null;
            }

            return _tmpImage;

        }

            public Image DownloadFromUrl2(string _URL)
        {
            WebRequest requestPic = WebRequest.Create(_URL);

            WebResponse responsePic = requestPic.GetResponse();

            return Image.FromStream(responsePic.GetResponseStream());

        }
        public Image DownloadFromUrl3(string _URL)
        {
            RootKit.Web.DownloadManager _downloader = new Web.DownloadManager();
            p_DownloadedImage = _downloader.DownloadFromUrl(_URL);
            return p_DownloadedImage;
        }
        #endregion

        private Image cropImage(Image img, Rectangle cropArea)
        {
            Bitmap bmpImage = new Bitmap(img);
            Bitmap bmpCrop = bmpImage.Clone(cropArea,
                                            bmpImage.PixelFormat);
            return (Image)(bmpCrop);
        }

        public Image resizeImage(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }

        private void saveJpeg(string path, Bitmap img, long quality)
        {
            // Encoder parameter for image quality
            EncoderParameter qualityParam =
                new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

            // Jpeg image codec
            ImageCodecInfo jpegCodec = getEncoderInfo("image/jpeg");
            
            if (jpegCodec == null)
                return;

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(path, jpegCodec, encoderParams);
        }

        private ImageCodecInfo getEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }

        private void save(String _fullPath, Bitmap img, ImageFormat _imgFormat)
        {
            img.Save(_fullPath, _imgFormat);
        }

        public ImageList getImagesFromResx(String _resxName)
        {
            ImageList imageList = new ImageList();

            Assembly myAssembly = Assembly.GetExecutingAssembly();
            String[] names = myAssembly.GetManifestResourceNames();
            Stream myStream;
            IEnumerable<string> namesWithFourCharacters =
                from name in names
                where name.Contains("." + _resxName + ".") &&
                        (name.ToLower().EndsWith(".png") ||
                        name.ToLower().EndsWith(".jpg") ||
                        name.ToLower().EndsWith(".gif"))

                select name;

            foreach (String name in namesWithFourCharacters)
            {
                //if (name.Contains("." + _resxName + ".") && name.ToLower().EndsWith(".png"))
                //{
                myStream = this.GetType().Assembly.GetManifestResourceStream(name);
                imageList.Images.Add(Image.FromStream(myStream));
                //}

            }
            return imageList;
        }
    }
}
