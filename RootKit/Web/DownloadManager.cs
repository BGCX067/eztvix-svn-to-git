using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

namespace RootKit.Web
{
    public class DownloadManager
    {
        private byte[] downloadedData;

        public DownloadManager() { }

        public Image DownloadFromUrl(string _URL)
        {
            Image img;
            try
            {
                byte[] imageData = DownloadBytesFromUrl(_URL); //DownloadData function from here
                MemoryStream stream = new MemoryStream(imageData);
                img = Image.FromStream(stream);
                stream.Close();
            }
            catch (Exception e)
            {
                return null;
            }

            return img;
        }

        public byte[] DownloadBytesFromUrl(string _URL)
        {
            downloadedData = new byte[0];
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

        ////Connects to a URL and attempts to download the file
        //private void downloadData(string url)
        //{
        //    progressBar1.Value = 0;

        //    downloadedData = new byte[0];
        //    try
        //    {
        //        //Optional
        //        this.Text = "Connecting...";
        //        Application.DoEvents();

        //        //Get a data stream from the url
        //        WebRequest req = WebRequest.Create(url);
        //        WebResponse response = req.GetResponse();
        //        Stream stream = response.GetResponseStream();

        //        //Download in chuncks
        //        byte[] buffer = new byte[1024];

        //        //Get Total Size
        //        int dataLength = (int)response.ContentLength;

        //        //With the total data we can set up our progress indicators
        //        progressBar1.Maximum = dataLength;
        //        lbProgress.Text = "0/" + dataLength.ToString();

        //        this.Text = "Downloading...";
        //        Application.DoEvents();

        //        //Download to memory
        //        //Note: adjust the streams here to download directly to the hard drive
        //        MemoryStream memStream = new MemoryStream();
        //        while (true)
        //        {
        //            //Try to read the data
        //            int bytesRead = stream.Read(buffer, 0, buffer.Length);

        //            if (bytesRead == 0)
        //            {
        //                //Finished downloading
        //                progressBar1.Value = progressBar1.Maximum;
        //                lbProgress.Text = dataLength.ToString() + "/" + dataLength.ToString();

        //                Application.DoEvents();
        //                break;
        //            }
        //            else
        //            {
        //                //Write the downloaded data
        //                memStream.Write(buffer, 0, bytesRead);

        //                //Update the progress bar
        //                if (progressBar1.Value + bytesRead <= progressBar1.Maximum)
        //                {
        //                    progressBar1.Value += bytesRead;
        //                    lbProgress.Text = progressBar1.Value.ToString() + "/" + dataLength.ToString();

        //                    progressBar1.Refresh();
        //                    Application.DoEvents();
        //                }
        //            }
        //        }

        //        //Convert the downloaded stream to a byte array
        //        downloadedData = memStream.ToArray();

        //        //Clean up
        //        stream.Close();
        //        memStream.Close();
        //    }
        //    catch (Exception)
        //    {
        //        //May not be connected to the internet
        //        //Or the URL might not exist
        //        MessageBox.Show("There was an error accessing the URL.");
        //    }

        //    txtData.Text = downloadedData.Length.ToString();
        //    this.Text = "Download Data through HTTP";
        //}

    }

    public class DownloadManagerThreaded
    {
        private byte[] downloadedData;

        public DownloadManagerThreaded() { }

        public Image DownloadFromUrl(string _URL, String _errorMessage, BackgroundWorker worker, DoWorkEventArgs e)
        {
            Image img;
            try
            {
                byte[] imageData = DownloadBytesFromUrl(_URL, _errorMessage, worker, e); //DownloadData function from here
                MemoryStream stream = new MemoryStream(imageData);
                img = Image.FromStream(stream);
                stream.Close();
            }
            catch (Exception ex)
            {
                return null;
            }

            return img;
        }

        public Image DownloadFromUrl(string _URL, BackgroundWorker worker, DoWorkEventArgs e)
        {
            return DownloadFromUrl(_URL, "There was an error accessing the URL!", worker, e);
        }


        public byte[] DownloadBytesFromUrl(string _URL, String _errorMessage, BackgroundWorker worker, DoWorkEventArgs e)
        {
            downloadedData = new byte[0];
            try
            {
                // Connecting
                //Get a data stream from the url
                WebRequest req = WebRequest.Create(_URL);

                WebResponse response = req.GetResponse();
                Stream stream = response.GetResponseStream();

                //Download in chuncks
                byte[] buffer = new byte[1024];

                //Get Total Size
                int dataLength = (int)response.ContentLength;
                int pourcent = 0;
                int progressIncrement = dataLength / 100;
                int totalBytesRead = 0;
                ////With the total data we can set up our progress indicators
                //progressBar1.Maximum = dataLength;
                worker.ReportProgress(pourcent);
                //lbProgress.Text = "0/" + dataLength.ToString();

                //Download to memory
                //Note: adjust the streams here to download directly to the hard drive
                MemoryStream memStream = new MemoryStream();
                while (true)
                {
                    //Try to read the data
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    totalBytesRead += bytesRead;
                    if (bytesRead == 0)
                    {
                        ////Finished downloading
                        worker.ReportProgress(100);
                        //progressBar1.Value = progressBar1.Maximum;
                        //lbProgress.Text = dataLength.ToString() + "/" + dataLength.ToString();

                        //Application.DoEvents();
                        break;
                    }
                    else
                    {
                        //Write the downloaded data
                        worker.ReportProgress(totalBytesRead / progressIncrement);
                        memStream.Write(buffer, 0, bytesRead);

                    }
                }
                worker.ReportProgress(100);
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
                if (_errorMessage.Length> 0)
                    MessageBox.Show(_errorMessage);
            }

            return downloadedData;

        }

        ////Connects to a URL and attempts to download the file
        //private void downloadData(string url)
        //{
        //    progressBar1.Value = 0;

        //    downloadedData = new byte[0];
        //    try
        //    {
        //        //Optional
        //        this.Text = "Connecting...";
        //        Application.DoEvents();

        //        //Get a data stream from the url
        //        WebRequest req = WebRequest.Create(url);
        //        WebResponse response = req.GetResponse();
        //        Stream stream = response.GetResponseStream();

        //        //Download in chuncks
        //        byte[] buffer = new byte[1024];

        //        //Get Total Size
        //        int dataLength = (int)response.ContentLength;

        //        //With the total data we can set up our progress indicators
        //        progressBar1.Maximum = dataLength;
        //        lbProgress.Text = "0/" + dataLength.ToString();

        //        this.Text = "Downloading...";
        //        Application.DoEvents();

        //        //Download to memory
        //        //Note: adjust the streams here to download directly to the hard drive
        //        MemoryStream memStream = new MemoryStream();
        //        while (true)
        //        {
        //            //Try to read the data
        //            int bytesRead = stream.Read(buffer, 0, buffer.Length);

        //            if (bytesRead == 0)
        //            {
        //                //Finished downloading
        //                progressBar1.Value = progressBar1.Maximum;
        //                lbProgress.Text = dataLength.ToString() + "/" + dataLength.ToString();

        //                Application.DoEvents();
        //                break;
        //            }
        //            else
        //            {
        //                //Write the downloaded data
        //                memStream.Write(buffer, 0, bytesRead);

        //                //Update the progress bar
        //                if (progressBar1.Value + bytesRead <= progressBar1.Maximum)
        //                {
        //                    progressBar1.Value += bytesRead;
        //                    lbProgress.Text = progressBar1.Value.ToString() + "/" + dataLength.ToString();

        //                    progressBar1.Refresh();
        //                    Application.DoEvents();
        //                }
        //            }
        //        }

        //        //Convert the downloaded stream to a byte array
        //        downloadedData = memStream.ToArray();

        //        //Clean up
        //        stream.Close();
        //        memStream.Close();
        //    }
        //    catch (Exception)
        //    {
        //        //May not be connected to the internet
        //        //Or the URL might not exist
        //        MessageBox.Show("There was an error accessing the URL.");
        //    }

        //    txtData.Text = downloadedData.Length.ToString();
        //    this.Text = "Download Data through HTTP";
        //}

    }


}
