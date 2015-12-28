using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Drawing;
using System.ComponentModel;
using RootKit.Web;
using System.Windows.Forms;
using RootKit.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace EzTvix.Provider
{
    public class Movie
    {
        #region *** properties ***

        private XmlDocument m_movieXml;
        public XmlDocument Xml
        {
            get { return m_movieXml; }
            set { m_movieXml = value; }
        }

        private XmlNode m_movieNode;
        public XmlNode Node
        {
            get { return m_movieNode; }
            set { m_movieNode = value; }
        }

        private Int32 _id = 0;
        /// <summary>
        /// Movie ID
        /// </summary>
        public Int32 ID { get { return _id; } set { _id = value; } }

        private Int32 _id_Allocine = 0;
        /// <summary>
        /// Allocinne Movie ID
        /// </summary>
        public Int32 ID_Allocine { get { return _id_Allocine; } set { _id_Allocine = value; } }

        private Int32 _id_IMDB = 0;
        /// <summary>
        /// IMDB Movie ID
        /// </summary>
        public Int32 ID_IMDB { get { return _id_IMDB; } set { _id_IMDB = value; } }

        private String _url = "";
        /// <summary>
        /// Movie Url
        /// </summary>
        public String Url { get { return _url; } set { _url = value; } }

        private String _title = "";
        /// <summary>
        /// Movie Title (FR)
        /// </summary>
        public String Title { get { return _title; } set { _title = value.Replace("&#39;", "'"); } }

        private String _originalTitle = "";
        /// <summary>
        /// Movie title (EN)
        /// </summary>
        public String OriginalTitle { get { return _originalTitle; } set { _originalTitle = value; } }

        private Int32 _year = 0;
        /// <summary>
        /// Year of release
        /// </summary>
        public Int32 Year { get { return _year; } set { _year = value; } }

        private Int32 _runtime = 0;
        /// <summary>
        /// Movie runtime
        /// </summary>
        public Int32 Runtime { get { return _runtime; } set { _runtime = value; } }

        private ArrayList _directors = new ArrayList();
        /// <summary>
        /// Director list
        /// </summary>
        public ArrayList Directors
        {
            get
            {
                return _directors;
            }
            set
            {
                _directors = value;
            }
        }

        private List<Actor> _actors = new List<Actor>();
        //private ArrayList _actors = new ArrayList();
        /// <summary>
        /// Movie casting
        /// </summary>
        public List<Actor> Actors
        {
            get
            {
                return _actors;
            }
            set
            {
                _actors = value;
            }
        }

        private String _plot = "";
        /// <summary>
        /// Movie Plot
        /// </summary>
        public String Plot { get { return _plot; } set { _plot = value; } }

        private String _tagLine = "";
        /// <summary>
        /// Movie plot resume
        /// </summary>
        public String Tagline { get { return _tagLine; } set { _tagLine = value; } }

        private ArrayList _genres = new ArrayList();
        /// <summary>
        /// Movie genres
        /// </summary>
        public ArrayList Genres
        {
            get
            {
                return _genres;
            }
            set
            {
                _genres = value;
            }
        }

        private List<MoviePicture> _moviepictures = new List<MoviePicture>();
        /// <summary>
        /// List of movie pictures (Obsolete)
        /// </summary>
        public List<MoviePicture> Moviepictures
        {
            get
            {
                return _moviepictures;
            }
            set
            {
                _moviepictures = value;
            }
        }

        private Dictionary<String, MoviePicture> _posters = new Dictionary<string, MoviePicture>();
        /// <summary>
        /// Movie Posters
        /// </summary>
        public Dictionary<String, MoviePicture> Posters
        {
            get { return _posters; }
        }
        private Dictionary<String, MoviePicture> _fanArts = new Dictionary<string, MoviePicture>();
        /// <summary>
        /// Movie Fanarts
        /// </summary>
        public Dictionary<String, MoviePicture> FanArts
        {
            get { return _fanArts; }
        }

        private List<Rating> _ratings = new List<Rating>();
        /// <summary>
        /// Movie rating list
        /// </summary>
        public List<Rating> Ratings
        {
            get
            {
                return _ratings;
            }
            set
            {
                _ratings = value;
            }
        }

        private Image _thumbCover;
        /// <summary>
        /// Cover thumb
        /// </summary>
        public Image ThumbCover
        {
            get { return _thumbCover; }
            set { _thumbCover = value; }
        }
        private Image _cover;
        /// <summary>
        ///  Cover Image
        /// </summary>
        public Image Cover
        {
            get { return _cover; }
            set { _cover = value; }
        }
        /// <summary>
        /// Selected Cover ID
        /// </summary>
        public string SelectedCover
        {
            set
            {
                MoviePicture poster = this.Posters[value];

                // Load the selected Cover (poster preview)
                _cover = poster.loadPreview();
            }
        }

        private Image _Fanart;
        /// <summary>
        ///  Cover Image
        /// </summary>
        public Image Fanart
        {
            get { return _Fanart; }
            set { _Fanart = value; }
        }
        /// <summary>
        /// Selected Fanart ID
        /// </summary>
        public string SelectedFanart
        {
            set
            {
                MoviePicture fanart = this.FanArts[value];

                // Load the selected Fanart (Original size)
                _Fanart = fanart.loadOriginal();
            }
        }

        #endregion

        #region *** method ***
        /// <summary>
        /// Get an imagelist object with all poster thumbs
        /// </summary>
        public ImageList posterImageList
        {
            get
            {

                ImageList imgList = new ImageList();
                imgList.ImageSize = new Size(70, 100);
                imgList.ColorDepth = ColorDepth.Depth32Bit;
                string filepath;

                foreach (KeyValuePair<String, MoviePicture> kvp in _posters)
                {
                    if (kvp.Value.Size.ToLower() == "thumb")
                    {
                        filepath = Application.StartupPath + @"\Temp\" + kvp.Key + ".png";
                        try
                        {
                            kvp.Value.Thumb.Save(filepath, ImageFormat.Png);
                            //imgList.Images.Add(kvp.Value.Thumb);

                        }
                        catch (Exception ex)
                        {
                        }
                        try
                        {
                            imgList.Images.Add(Image.FromFile(filepath));

                        }
                        catch (Exception ex) { }
                    }
                }

                return imgList;
            }
        }

        //public ListViewItem[] getPosterListView()
        //{
        //    //List<ListViewItem> itemList = new List<ListViewItem> ();
        //    //string Description;
        //    int i = 0;
        //    ListViewItem itemToAdd;
        //    ListViewItem[] itemList = new ListViewItem[_posters.Count];

        //    foreach (KeyValuePair<String, MoviePicture> kvp in _posters)
        //    {
        //        itemToAdd = new ListViewItem(kvp.Value.ID.ToString());

        //        itemToAdd.ImageIndex = i;

        //        //itemToAdd.SubItems.Add(Description);

        //        itemList[i] = itemToAdd;
        //        i++;
        //    }
        //    i = 0;
        //    return itemList;
        //}
        //public ListViewItem getListViewItem(int imageIndex)
        //{
        //    string Description;
        //    ListViewItem itemToAdd;

        //    Description = this.Title + " - (" + this.OriginalTitle + ") - " + this.Year
        //            + "\r\nRéalisateur : " + this.Directors[0].ToString();

        //    KeyValuePair<String, MoviePicture> tutu = this.Posters.Last();

        //    itemToAdd = new ListViewItem(this.ID.ToString());

        //    itemToAdd.ImageIndex = (imageIndex < 0) ? 0 : imageIndex;

        //    itemToAdd.SubItems.Add(Description);

        //    return itemToAdd;

        //}


        public ListViewItem getListViewPosterItem(int imageIndex)
        {
            ListViewItem itemToAdd;

            itemToAdd = new ListViewItem(this.ID.ToString());

            itemToAdd.ImageIndex = (imageIndex < 0) ? 0 : imageIndex;

            return itemToAdd;
        }


        public void SaveAsXml(String _path)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(@"<Movie id='" + this.ID + "' allocine='" + this.ID_Allocine + "' imdb='" + this.ID_IMDB + "'></Movie>");
            XmlElement root = doc.DocumentElement;

            XmlNode genericNode = doc.CreateNode(XmlNodeType.Element, "Url", "");
            XmlNode subNode;
            genericNode.InnerText = this.Url;
            root.AppendChild(genericNode);

            genericNode = doc.CreateNode(XmlNodeType.Element, "Title", "");
            genericNode.InnerText = this.Title;
            root.AppendChild(genericNode);

            genericNode = doc.CreateNode(XmlNodeType.Element, "OriginalTitle", "");
            genericNode.InnerText = this.OriginalTitle;
            root.AppendChild(genericNode);


            genericNode = doc.CreateNode(XmlNodeType.Element, "Year", "");
            genericNode.InnerText = this.Year.ToString();
            root.AppendChild(genericNode);

            genericNode = doc.CreateNode(XmlNodeType.Element, "Plot", "");
            genericNode.InnerText = this.Plot;
            root.AppendChild(genericNode);

            genericNode = doc.CreateNode(XmlNodeType.Element, "Directors", "");
            foreach (String dir in this.Directors)
            {
                subNode = doc.CreateNode(XmlNodeType.Element, "Director", "");
                subNode.InnerText = dir;
                genericNode.AppendChild(subNode);
            }
            root.AppendChild(genericNode);

            genericNode = doc.CreateNode(XmlNodeType.Element, "Genres", "");
            foreach (String gen in this.Genres)
            {
                subNode = doc.CreateNode(XmlNodeType.Element, "Genre", "");
                subNode.InnerText = gen;
                genericNode.AppendChild(subNode);
            }
            root.AppendChild(genericNode);

            genericNode = doc.CreateNode(XmlNodeType.Element, "Casting", "");
            foreach (Actor act in this.Actors)
                genericNode.AppendChild(act.Xml(doc));
            root.AppendChild(genericNode);

            genericNode = doc.CreateNode(XmlNodeType.Element, "Ratings", "");
            foreach (Rating rat in this.Ratings)
                genericNode.AppendChild(rat.Xml(doc));
            root.AppendChild(genericNode);

            genericNode = doc.CreateNode(XmlNodeType.Element, "Runtime", "");
            genericNode.InnerText = this.Runtime.ToString();
            root.AppendChild(genericNode);

            //Sauvegarde du cover
            genericNode = doc.CreateNode(XmlNodeType.Element, "Cover", "");
            Bitmap bmp = new Bitmap(this.Cover);
            genericNode.InnerText = RootKit.Core.Converter.ImageToBase64(bmp, ImageFormat.Jpeg);
            bmp.Dispose();
            root.AppendChild(genericNode);

            //Sauvegarde du fanart
            genericNode = doc.CreateNode(XmlNodeType.Element, "Fanart", "");
            try
            {
                bmp = new Bitmap(this.Fanart);
                genericNode.InnerText = RootKit.Core.Converter.ImageToBase64(bmp, ImageFormat.Jpeg);
                bmp.Dispose();
            }
            catch (Exception ex)
            { }

            root.AppendChild(genericNode);

            doc.Save(_path + @"/info.xml");

        }

        public void LoadFromXml(String _path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(_path + @"/info.xml");


            //doc.LoadXml(@"<Movie id='" + this.ID + "' allocine='" + this.ID_Allocine + "' imdb='" + this.ID_IMDB + "'></Movie>");
            try
            {
                this.ID = int.Parse(doc.SelectSingleNode("//Movie").Attributes["id"].Value);
            }
            catch (Exception ex) { }
            try
            {
                this.ID_Allocine = int.Parse(doc.SelectSingleNode("//Movie").Attributes["allocine"].Value);
            }
            catch (Exception ex) { }
            try
            {
                this.ID_IMDB = int.Parse(doc.SelectSingleNode("//Movie").Attributes["imdb"].Value);
            }
            catch (Exception ex) { }
            try
            {
                this.Url = doc.SelectSingleNode("//Url").InnerText;
            }
            catch (Exception ex) { }
            try
            {
                this.Title = doc.SelectSingleNode("//Title").InnerText;
            }
            catch (Exception ex) { }
            try
            {
                this.OriginalTitle = doc.SelectSingleNode("//OriginalTitle").InnerText;
            }
            catch (Exception ex) { }
            try
            {
                this.Plot = doc.SelectSingleNode("//Plot").InnerText;
            }
            catch (Exception ex) { }
            try
            {
                this.Year = int.Parse(doc.SelectSingleNode("//Year").InnerText);
            }
            catch (Exception ex) { }


            this.Directors.Clear();
            foreach (XmlNode director in doc.SelectSingleNode("//Directors/Director"))
            {
                try
                {
                    this.Directors.Add(director.InnerText);
                }
                catch (Exception ex) { }
            }

            this.Genres.Clear();
            foreach (XmlNode genre in doc.SelectSingleNode("//Genres"))
            {
                try
                {
                    this.Genres.Add(genre.InnerText);
                }
                catch (Exception ex) { }
            }

            this.Actors.Clear();
            foreach (XmlNode actor in doc.SelectNodes("//Casting/Actor"))
            {
                try
                {
                    this.Actors.Add(new Actor(actor));
                }
                catch (Exception ex) { }
            }

            this.Ratings.Clear();
            foreach (XmlNode rating in doc.SelectNodes("//Ratings/Rating"))
            {
                try
                {
                    this.Ratings.Add(new Rating(rating));
                }
                catch (Exception ex) { }
            }

            try
            {
                this.Runtime = Convert.ToInt32(doc.SelectSingleNode("//Runtime").InnerText);
            }
            catch (Exception ex) { }

            // chargement du cover
            try
            {
                this.Cover = RootKit.Core.Converter.Base64ToImage(doc.SelectSingleNode("//Cover").InnerText);
            }
            catch (Exception ex) { }

            // chargement du fanart
            try
            {
                this.Fanart = RootKit.Core.Converter.Base64ToImage(doc.SelectSingleNode("//Fanart").InnerText);
            }
            catch (Exception ex) { }

        }

        #endregion
    }
}
