using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using RootKit.Core;


namespace EzTvix.Provider.API
{
    abstract public class MovieProvider : ProviderWorker, iProvider
    {

        #region *** Interface Properties ***
        protected string _apiKey = "";
        public string APIKey
        {
            get
            {
                return _apiKey;
            }
            set
            {
                _apiKey = value;
            }
        }
        protected string _lang = "fr";
        public string Language { get { return _lang.ToUpper(); } set { _lang = value.ToLower(); } }

        #endregion

        #region *** properties ***
        protected XmlDocument movieXml = new XmlDocument();
        protected Movies _movieList;
        public Movies MovieList { get { return _movieList; } }

        protected Movie _movie;
        public Movie Movie { get { return _movie; } }

        protected StartStop myTime = new StartStop();
        public StartStop Timer
        { get { return myTime; } }

        protected int processDivider = 0;
        protected decimal ProgressPercentage = 0;
        #endregion

        #region *** Download ***
        protected Image DownloadFromUrl(string _URL)
        {
            Image img;
            try
            {
                DateTime bef, aft;
                TimeSpan dif;
                bef = DateTime.Now;

                byte[] imageData = DownloadBytesFromUrl(_URL); //DownloadData function from here
                MemoryStream stream = new MemoryStream(imageData);
                img = Image.FromStream(stream);
                stream.Close();
                aft = DateTime.Now;

                dif = aft - bef;
            }
            catch (Exception e)
            {
                return null;
            }

            return img;
        }

        protected Image DownloadFromWebClient(string _url)
        {
            Bitmap bitmap;

            WebClient client = new WebClient();
            client.Proxy = null;
            Stream stream = client.OpenRead(_url);
            bitmap = new Bitmap(stream);
            stream.Flush();
            stream.Close();

            return bitmap;

        }

        protected byte[] DownloadBytesFromUrl(string _URL)
        {
            byte[] downloadedData = new byte[0];
            try
            {
                // Connecting
                //Get a data stream from the url
                WebRequest req = WebRequest.Create(_URL);

                WebResponse response = req.GetResponse();
                Stream stream = response.GetResponseStream();

                //Download in chuncks
                byte[] buffer = new byte[1024];

                ////Get Total Size
                //int dataLength = (int)response.ContentLength;

                ////With the total data we can set up our progress indicators
                //progressBar1.Maximum = dataLength;
                //lbProgress.Text = "0/" + dataLength.ToString();

                //Download to memory
                //Note: adjust the streams here to download directly to the hard drive
                MemoryStream memStream = new MemoryStream();
                while (true)
                {
                    //Try to read the data
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        ////Finished downloading
                        //progressBar1.Value = progressBar1.Maximum;
                        //lbProgress.Text = dataLength.ToString() + "/" + dataLength.ToString();

                        //Application.DoEvents();
                        break;
                    }
                    else
                    {
                        //Write the downloaded data
                        memStream.Write(buffer, 0, bytesRead);

                    }
                }

                //Convert the downloaded stream to a byte array
                downloadedData = memStream.ToArray();

                //Clean up
                stream.Close();
                memStream.Close();
            }
            catch (Exception)
            {
                //May not be connected to the internet
                //Or the URL might not exist
                MessageBox.Show("There was an error accessing the URL.");
            }

            return downloadedData;

        }
        #endregion

        #region *** Methods ***
        abstract public void Search(String search);

        abstract public void GetData(Movie movie);

        abstract public void GetPictureData(Movie movie);

        abstract public Movie LoadMovie(XmlNode movieNode, BackgroundWorker worker, DoWorkProviderEventArgs e);
        abstract public Movie LoadMovie(XmlDocument _movieXml, BackgroundWorker worker, DoWorkProviderEventArgs e);
        #endregion
    }
}
