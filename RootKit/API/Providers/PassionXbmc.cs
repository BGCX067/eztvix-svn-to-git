using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Collections;
using System.Xml.XPath;
using System.Drawing;
namespace RootKit.API.Providers
{
    /// <summary>
    /// OBSOLETE
    /// </summary>
    //public class PassionXbmc //: ProviderWorker, iProvider
    //{

    //    #region *** Interface Properties ***
    //    protected string _apiKey = "9b8f0779badbad3b46d6718ee95a68ff";
    //    public string APIKey
    //    {
    //        get
    //        {
    //            return _apiKey;
    //        }
    //        set
    //        {
    //            _apiKey = value;
    //        }
    //    }
    //    #endregion



    //    #region *** Methods ***
    //    protected Image DownloadFromUrl(string _URL)
    //    {
    //        Image img;
    //        try
    //        {
    //            DateTime bef, aft;
    //            TimeSpan dif;
    //            bef = DateTime.Now;

    //            byte[] imageData = DownloadBytesFromUrl(_URL); //DownloadData function from here
    //            MemoryStream stream = new MemoryStream(imageData);
    //            img = Image.FromStream(stream);
    //            stream.Close();
    //            aft = DateTime.Now;

    //            dif = aft - bef;
    //        }
    //        catch (Exception e)
    //        {
    //            return null;
    //        }

    //        return img;
    //    }

    //    protected Image DownloadFromWebClient(string _URL)
    //    {
    //        Bitmap bitmap;

    //        WebClient client = new WebClient();
    //        client.Proxy = null;
    //        Stream stream = client.OpenRead(_URL);
    //        bitmap = new Bitmap(stream);
    //        stream.Flush();
    //        stream.Close();

            

    //        return bitmap;

    //    }

    //    protected byte[] DownloadBytesFromUrl(string _URL)
    //    {
    //        byte[] downloadedData = new byte[0];
    //        try
    //        {
    //            // Connecting
    //            //Get a data stream from the url
    //            WebRequest req = WebRequest.Create(_URL);

    //            WebResponse response = req.GetResponse();
    //            Stream stream = response.GetResponseStream();

    //            //Download in chuncks
    //            byte[] buffer = new byte[1024];

    //            ////Get Total Size
    //            //int dataLength = (int)response.ContentLength;

    //            ////With the total data we can set up our progress indicators
    //            //progressBar1.Maximum = dataLength;
    //            //lbProgress.Text = "0/" + dataLength.ToString();

    //            //Download to memory
    //            //Note: adjust the streams here to download directly to the hard drive
    //            MemoryStream memStream = new MemoryStream();
    //            while (true)
    //            {
    //                //Try to read the data
    //                int bytesRead = stream.Read(buffer, 0, buffer.Length);

    //                if (bytesRead == 0)
    //                {
    //                    ////Finished downloading
    //                    //progressBar1.Value = progressBar1.Maximum;
    //                    //lbProgress.Text = dataLength.ToString() + "/" + dataLength.ToString();

    //                    //Application.DoEvents();
    //                    break;
    //                }
    //                else
    //                {
    //                    //Write the downloaded data
    //                    memStream.Write(buffer, 0, bytesRead);

    //                }
    //            }

    //            //Convert the downloaded stream to a byte array
    //            downloadedData = memStream.ToArray();

    //            //Clean up
    //            stream.Close();
    //            memStream.Close();
    //        }
    //        catch (Exception)
    //        {
    //            //May not be connected to the internet
    //            //Or the URL might not exist
    //            MessageBox.Show("There was an error accessing the URL.");
    //        }

    //        return downloadedData;

    //    }

    //    public Movies SearchMovie(String _search)
    //    {
    //        ////http://passion-xbmc.org/developpement-du-scraper-utilisateurs/api-version-1-0/
    //        ////http:// passion-xbmc.org/scraper/API/1/Movie.Search/Title/en/XML/9b8f0779badbad3b46d6718ee95a68ff/ironman
    //        ////http://www.c-sharpcorner.com/UploadFile/mahesh/DownloadUsingHTTP12132005012752AM/DownloadUsingHTTP.aspx

    //        ////List<Movie> movieList = new List<Movie>();
    //        //Movies movieList = new Movies();
    //        //ArrayList al = new ArrayList();

    //        //DateTime bef, aft;
    //        //TimeSpan dif;

    //        //String SearchString = "http://passion-xbmc.org/scraper/API/1/Movie.Search/{0}/{1}/{2}/{3}/{4}/";
    //        //SearchString = String.Format(SearchString, _query.ToString(), _lang, "XML", _apiKey, _search);

    //        //XmlDocument doc = new XmlDocument();
    //        ////bef = DateTime.Now;
    //        //doc.Load(SearchString);
    //        ////aft = DateTime.Now;
    //        ////dif = aft - bef;
    //        //XmlNodeList movieNodes = doc.SelectNodes("//results/movie");

    //        //Movie movie;
    //        //Dictionary<String, MoviePicture> dicoPics;
    //        //String pictureID;
    //        //XmlNodeList _nodes;


    //        //foreach (XmlNode movieNode in movieNodes)
    //        //{
    //        //    movieList.Add(this.LoadMovie(movieNode));
    //        //}
    //        //return movieList;
    //        throw new NotImplementedException();
    //    }

    //    public virtual Movie GetMovieInfo(Movie _movie)
    //    {
    //        ////http://passion-xbmc.org/scraper/API/1/Movie.GetInfo/ID/fr/XML/9b8f0779badbad3b46d6718ee95a68ff/53751
    //        //Movie movie = new Movie();
    //        //String SearchString = "http://passion-xbmc.org/scraper/API/1/Movie.GetInfo/ID/{0}/{1}/{2}/{3}/";
    //        //SearchString = String.Format(SearchString, _lang, "XML", _apiKey, _movie.ID.ToString());

    //        //XmlDocument doc = new XmlDocument();

    //        //doc.Load(SearchString);
    //        //XmlNode movieNode = doc.SelectSingleNode("//movie");

    //        //return this.LoadMovie(movieNode);
    //        throw new NotImplementedException();
    //    }

    //    protected virtual Movie LoadMovie(XmlNode movieNode)
    //    {
    //        throw new NotImplementedException();
    //        //Movie movie = new Movie();
    //        //XmlNodeList _nodes;
    //        //Dictionary<String, MoviePicture> dicoPics;
    //        //String pictureID, pictureType;

    //        //movie.ID = Convert.ToInt32(movieNode.SelectSingleNode("id").InnerText);
    //        //movie.ID_Allocine = Convert.ToInt32(movieNode.SelectSingleNode("id_allocine").InnerText);
    //        //movie.ID_IMDB = Convert.ToInt32(movieNode.SelectSingleNode("id_imdb").InnerText);
    //        //movie.Url = movieNode.SelectSingleNode("url").InnerText;
    //        //movie.Title = movieNode.SelectSingleNode("title").InnerText;
    //        //movie.OriginalTitle = movieNode.SelectSingleNode("originaltitle").InnerText;
    //        //movie.Plot = movieNode.SelectSingleNode("plot").InnerText;
    //        //movie.Year = Convert.ToInt32(movieNode.SelectSingleNode("year").InnerText);

    //        ////_nodes = movieNode.SelectNodes("director");
    //        //try
    //        //{
    //        //    foreach (XmlNode dir in movieNode.SelectNodes("directors/director"))
    //        //    {
    //        //        movie.Directors.Add(dir.InnerText);
    //        //    }
    //        //}
    //        //catch (Exception ex) { }
    //        //try
    //        //{
    //        //    foreach (XmlNode cast in movieNode.SelectNodes("casting/person "))
    //        //    {
    //        //        //TODO Implement Actor OBJECT (directos would be the same)
    //        //        movie.Actors.Add(
    //        //            new Actor(
    //        //                Int32.Parse(cast.Attributes["id"].Value),
    //        //                cast.Attributes["name"].Value,
    //        //                cast.Attributes["character"].Value,
    //        //                Int32.Parse(cast.Attributes["idthumb"].Value),
    //        //                cast.Attributes["thumb"].Value
    //        //                )
    //        //                );
    //        //    }
    //        //}
    //        //catch (Exception ex) { }
    //        //try
    //        //{
    //        //    foreach (XmlNode rat in movieNode.SelectNodes("ratings/rating"))
    //        //    {
    //        //        movie.Ratings.Add(new Rating(rat.Attributes["type"].Value, rat.Attributes["votes"].Value, rat.InnerText));
    //        //    }
    //        //}
    //        //catch (Exception ex) { }
    //        //try
    //        //{
    //        //    foreach (XmlNode genre in movieNode.SelectNodes("genres/genre"))
    //        //    {
    //        //        //movie.Ratings.Add(new Rating(rat.Attributes["type"].Value, rat.Attributes["votes"].Value, rat.InnerText));
    //        //        movie.Genres.Add(genre.InnerText);
    //        //    }
    //        //}
    //        //catch (Exception ex) { }


    //        //foreach (XmlNode img in movieNode.SelectNodes("images/image"))
    //        //{
    //        //    pictureID = img.Attributes["id"].Value;
    //        //    pictureType = img.Attributes["type"].Value;
    //        //    if (pictureType.ToLower() == "poster")
    //        //    { // poster dico
    //        //        try
    //        //        {
    //        //            movie.Posters.Add(pictureID, new MoviePicture());
    //        //        }
    //        //        catch (Exception ex) { }
    //        //        movie.Posters[pictureID].Type = pictureType;
    //        //        movie.Posters[pictureID].Size = img.Attributes["size"].Value; ;
    //        //        movie.Posters[pictureID].ID = int.Parse(pictureID);

    //        //        switch (img.Attributes["size"].Value.ToLower())
    //        //        {
    //        //            case "original":
    //        //                movie.Posters[pictureID].Width = Convert.ToInt32(img.Attributes["width"].Value);
    //        //                movie.Posters[pictureID].Height = Convert.ToInt32(img.Attributes["height"].Value);
    //        //                movie.Posters[pictureID].UrlOriginal = img.Attributes["url"].Value;
    //        //                break;
    //        //            case "preview":
    //        //                movie.Posters[pictureID].UrlPreview = img.Attributes["url"].Value;
    //        //                break;
    //        //            case "thumb":
    //        //                movie.Posters[pictureID].UrlThumb = img.Attributes["url"].Value;
    //        //                movie.Posters[pictureID].Thumb = DownloadFromWebClient(movie.Posters[pictureID].UrlThumb);
    //        //                if (movie.ThumbCover == null) movie.ThumbCover = movie.Posters[pictureID].Thumb;

    //        //                break;
    //        //            default:
    //        //                break;
    //        //        }
    //        //    }
    //        //    else
    //        //    {// Fanart dico
    //        //        try
    //        //        {
    //        //            movie.FanArts.Add(pictureID, new MoviePicture());

    //        //        }
    //        //        catch (Exception ex) { }
    //        //        movie.FanArts[pictureID].Type = pictureType;
    //        //        movie.FanArts[pictureID].Size = img.Attributes["size"].Value; ;
    //        //        movie.FanArts[pictureID].ID = int.Parse(pictureID);

    //        //        switch (img.Attributes["size"].Value.ToLower())
    //        //        {
    //        //            case "original":
    //        //                movie.FanArts[pictureID].Width = Convert.ToInt32(img.Attributes["width"].Value);
    //        //                movie.FanArts[pictureID].Height = Convert.ToInt32(img.Attributes["height"].Value);
    //        //                movie.FanArts[pictureID].UrlOriginal = img.Attributes["url"].Value;
    //        //                break;
    //        //            case "preview":
    //        //                movie.FanArts[pictureID].UrlPreview = img.Attributes["url"].Value;
    //        //                break;
    //        //            case "thumb":
    //        //                movie.FanArts[pictureID].UrlThumb = img.Attributes["url"].Value;
    //        //                //if (movie.FanArts[pictureID].Type.ToLower() == "poster")
    //        //                //    movie.FanArts[pictureID].Thumb = DownloadFromUrl(movie.FanArts[pictureID].Urlthumb);
    //        //                break;
    //        //            default:
    //        //                break;
    //        //        }
    //        //    }

    //        //}

    //        //dicoPics = new Dictionary<String, MoviePicture>();

    //        //return movie;
    //    }

    //    #endregion 
    //}

}

