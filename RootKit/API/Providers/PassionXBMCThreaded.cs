
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Xml;
//using RootKit.Core;
//using RootKit.Web;
//using System.Windows.Forms;
//using System.Linq;
//using System.Xml.Linq;


//namespace RootKit.API.Providers
//{
//    public class PassionXBMCThreaded : MovieProvider
//    {
//        #region *** Interface Properties ***
//        #endregion

//        #region *** properties ***

//        protected QueryType _query = QueryType.Title;
//        public string Query { get { return _query.ToString(); } set { _query = (QueryType)Enum.Parse(typeof(QueryType), value, true); } }

//        protected string _lang = "fr";
//        public string Language { get { return _lang; } set { _lang = value.ToLower(); } }

//        #endregion

//        #region *** cTor ***
//        public PassionXBMCThreaded()
//        {
//            // initialisation de la clé API
//            this._apiKey = "9b8f0779badbad3b46d6718ee95a68ff";

//            this.WorkerReportsProgress = true;
//            this.WorkerReportsCoverProgress = true;
//            this.WorkerReportsFanartProgress = true;
//            this.WorkerSupportsCancellation = true;

//            this.ProgressChanged += new ProgressChangedEventHandler(bwMovies_ProgressChanged);
//            this.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwMovies_RunWorkerCompleted);

//            //this.ObjectLoaded += new ObjectLoadedEventHandler(bwMovies_ObjectLoaded);
//        }
//        #endregion

//        #region *** Methods ***
//        public override void Search(String _search)
//        {
//            //http:// passion-xbmc.org/developpement-du-scraper-utilisateurs/api-version-1-0/
//            //http:// passion-xbmc.org/scraper/API/1/Movie.Search/Title/en/XML/9b8f0779badbad3b46d6718ee95a68ff/ironman

//            // ne pas mettre de slash a la fin de la searchstring sous peine de ne pas avoir de résultat
//            String SearchString = "http://passion-xbmc.org/scraper/API/1/Movie.Search/{0}/{1}/{2}/{3}/{4}";
//            SearchString = String.Format(SearchString, _query.ToString(), _lang, "XML", _apiKey, _search);
//            try
//            {
//                movieXml.Load(SearchString);
//            }
//            catch (Exception ex)
//            {
//                string tutu;
//            }
//            this.DoWork += new DoWorkEventHandler(bwMovies_DoWork);
//            this.RunWorkerAsync();
//        }

//        public override void GetMovieData(Movie _movie)
//        {
//            //http://passion-xbmc.org/scraper/API/1/Movie.GetInfo/ID/fr/XML/9b8f0779badbad3b46d6718ee95a68ff/53751
//            Movie movie = new Movie();
//            String SearchString = "http://passion-xbmc.org/scraper/API/1/Movie.GetInfo/ID/{0}/{1}/{2}/{3}/";
//            SearchString = String.Format(SearchString, _lang, "XML", _apiKey, _movie.ID.ToString());

//            movieXml.Load(SearchString);
//            //XmlNode movieNode = movieXml.SelectSingleNode("//movie");

//            this.DoWork += new DoWorkEventHandler(bwMovieData_DoWork);
//            this.RunWorkerAsync();
//            //return this.LoadMovie(movieNode);
//        }

//        public override Movie LoadMovie(XmlNode movieNode, BackgroundWorker worker, DoWorkProviderEventArgs e)
//        {
//            Movie movie = new Movie();
//            Dictionary<String, MoviePicture> dicoPics;
//            String pictureID, pictureType;
//            decimal pourcent;
//            DownloadManagerThreaded downMan = new DownloadManagerThreaded();


//            movie.ID = Convert.ToInt32(movieNode.SelectSingleNode("id").InnerText);
//            movie.ID_Allocine = Convert.ToInt32(movieNode.SelectSingleNode("id_allocine").InnerText);
//            movie.ID_IMDB = Convert.ToInt32(movieNode.SelectSingleNode("id_imdb").InnerText);
//            movie.Url = movieNode.SelectSingleNode("url").InnerText;
//            movie.Title = movieNode.SelectSingleNode("title").InnerText;
//            movie.OriginalTitle = movieNode.SelectSingleNode("originaltitle").InnerText;
//            movie.Plot = movieNode.SelectSingleNode("plot").InnerText;
//            movie.Year = Convert.ToInt32(movieNode.SelectSingleNode("year").InnerText);
//            movie.Runtime = Convert.ToInt32(movieNode.SelectSingleNode("runtime").InnerText);

//            //_nodes = movieNode.SelectNodes("director");
//            try
//            {
//                foreach (XmlNode dir in movieNode.SelectNodes("directors/director"))
//                {
//                    movie.Directors.Add(dir.InnerText);
//                }
//            }
//            catch (Exception ex) { }
//            try
//            {
//                foreach (XmlNode cast in movieNode.SelectNodes("casting/person "))
//                {
//                    //TODO Implement Actor OBJECT (directos would be the same)
//                    movie.Actors.Add(
//                        new Actor(
//                            Int32.Parse(cast.Attributes["id"].Value),
//                            cast.Attributes["name"].Value,
//                            cast.Attributes["character"].Value,
//                            Int32.Parse(cast.Attributes["idthumb"].Value),
//                            cast.Attributes["thumb"].Value
//                            )
//                            );
//                }
//            }
//            catch (Exception ex) { }
//            try
//            {
//                foreach (XmlNode rat in movieNode.SelectNodes("ratings/rating"))
//                {
//                    movie.Ratings.Add(new Rating(rat.Attributes["type"].Value, rat.Attributes["votes"].Value, rat.InnerText));
//                }
//            }
//            catch (Exception ex) { }
//            try
//            {
//                foreach (XmlNode genre in movieNode.SelectNodes("genres/genre"))
//                {
//                    //movie.Ratings.Add(new Rating(rat.Attributes["type"].Value, rat.Attributes["votes"].Value, rat.InnerText));
//                    movie.Genres.Add(genre.InnerText);
//                }
//            }
//            catch (Exception ex) { }

//            //int thumbCount = movieNode.SelectNodes("images/image[@type='Poster' and @size='thumb']").Count;
//            movie.Posters.Clear();
//            movie.FanArts.Clear();
//            foreach (XmlNode img in movieNode.SelectNodes("images/image"))
//            {
//                pictureID = img.Attributes["id"].Value;
//                pictureType = img.Attributes["type"].Value;
//                if (pictureType.ToLower() == "poster")
//                {
//                    #region --- poster dico ---
//                    try
//                    {
//                        movie.Posters.Add(pictureID, new MoviePicture());
//                    }
//                    catch (Exception ex) { }
//                    movie.Posters[pictureID].Type = pictureType;
//                    movie.Posters[pictureID].Size = img.Attributes["size"].Value; ;
//                    movie.Posters[pictureID].ID = int.Parse(pictureID);

//                    switch (img.Attributes["size"].Value.ToLower())
//                    {
//                        case "original":
//                            movie.Posters[pictureID].Width = Convert.ToInt32(img.Attributes["width"].Value);
//                            movie.Posters[pictureID].Height = Convert.ToInt32(img.Attributes["height"].Value);
//                            movie.Posters[pictureID].UrlOriginal = img.Attributes["url"].Value.Replace(".peg", ".jpg");
//                            break;
//                        case "preview":
//                            movie.Posters[pictureID].UrlPreview = img.Attributes["url"].Value.Replace(".peg", ".jpg");
//                            break;
//                        case "thumb":
//                            movie.Posters[pictureID].UrlThumb = img.Attributes["url"].Value.Replace(".peg", ".jpg");
//                            movie.Posters[pictureID].Thumb = DownloadFromWebClient(movie.Posters[pictureID].UrlThumb);
//                            //movie.Posters[pictureID].Thumb = downMan.DownloadFromUrl(movie.Posters[pictureID].UrlThumb,worker, e);

//                            //worker.ReportProgress(pourcent);
//                            if (movie.ThumbCover == null) movie.ThumbCover = movie.Posters[pictureID].Thumb;
//                            this.ReportProgress(movie);
//                            this.ReportCoverProgress(movie);

//                            break;
//                        default:
//                            break;
//                    }
//                    #endregion
//                }
//                else
//                {
//                    #region --- Fanart dico---
//                    try
//                    {
//                        movie.FanArts.Add(pictureID, new MoviePicture());

//                    }
//                    catch (Exception ex) { }
//                    movie.FanArts[pictureID].Type = pictureType;
//                    movie.FanArts[pictureID].Size = img.Attributes["size"].Value; ;
//                    movie.FanArts[pictureID].ID = int.Parse(pictureID);

//                    switch (img.Attributes["size"].Value.ToLower())
//                    {
//                        case "original":
//                            movie.FanArts[pictureID].Width = Convert.ToInt32(img.Attributes["width"].Value);
//                            movie.FanArts[pictureID].Height = Convert.ToInt32(img.Attributes["height"].Value);
//                            movie.FanArts[pictureID].UrlOriginal = img.Attributes["url"].Value;
//                            break;
//                        case "preview":
//                            movie.FanArts[pictureID].UrlPreview = img.Attributes["url"].Value;
//                            break;
//                        case "thumb":
//                            movie.FanArts[pictureID].UrlThumb = img.Attributes["url"].Value;
//                            movie.FanArts[pictureID].Thumb = DownloadFromWebClient(movie.FanArts[pictureID].UrlThumb);

//                            this.ReportFanartProgress(movie);
//                            //if (movie.FanArts[pictureID].Type.ToLower() == "poster")
//                            //    movie.FanArts[pictureID].Thumb = DownloadFromUrl(movie.FanArts[pictureID].Urlthumb);
//                            break;
//                        default:
//                            break;
//                    }
//                    #endregion
//                }

//            }

//            dicoPics = new Dictionary<String, MoviePicture>();
//            return movie;
//        }

//        private void bwMovieData_DoWork(object sender, DoWorkProviderEventArgs e)
//        {
//            BackgroundWorker worker = sender as BackgroundWorker;
//            _movieList = new Movies();
//            XmlNode movieNode = movieXml.SelectSingleNode("//movie");

//            int thumbCount = movieXml.SelectNodes("//images/image[@type='Poster' and @size='thumb']").Count;
//            int movieCount = movieXml.SelectNodes("//movie").Count;

//            processDivider = (thumbCount < movieCount) ? movieCount : thumbCount;

//            myTime.Start();

//            ObjectLoadedProviderEventArgs movieArgs = new ObjectLoadedProviderEventArgs(this.LoadMovie(movieNode, worker, e));
//            ReportObjectLoaded(movieArgs.loadedObject);
//        }


//        #endregion

//        #region *** BW Methods ***

//        private void bwMovies_DoWork(object sender, DoWorkProviderEventArgs e)
//        {
//            BackgroundWorker worker = sender as BackgroundWorker;
//            _movieList = new Movies();
//            XmlNodeList movieNodes = movieXml.SelectNodes("//results/movie");
//            int thumbCount = movieXml.SelectNodes("//images/image[@type='Poster' and @size='thumb']").Count;
//            int movieCount = movieXml.SelectNodes("//movie").Count;

//            processDivider = (thumbCount < movieCount) ? movieCount : thumbCount;

//            myTime.Start();

//            foreach (XmlNode movieNode in movieNodes)
//            {
//                ObjectLoadedProviderEventArgs moviesArgs = new ObjectLoadedProviderEventArgs(this.LoadMovie(movieNode, worker, e));
//                _movieList.Add((Movie)moviesArgs.loadedObject);
//                //OnObjectLoaded(moviesArgs);
//                ReportObjectLoaded(moviesArgs.loadedObject);
//            }
//        }

//        private void bwMovies_ProgressChanged(object sender, ProgressChangedProviderEventArgs e)
//        {
//            this.ProgressPercentage = e.ProgressPercentage;
//        }

//        private void bwMovies_RunWorkerCompleted(object sender, RunWorkerCompletedProviderEventArgs e)
//        {
//            // Timer permettant de calculer le temps du process
//            myTime.Stop();
//        }

//        /// <summary>
//        /// Should be called in the main process (the one that instanciate the provider)
//        /// </summary>
//        /// <param name="sender">The provider object</param>
//        /// <param name="e"></param>
//        //private void bwMovies_ObjectLoaded(object sender, ObjectLoadedProviderEventArgs e)
//        //{
//        //    //previewPic.Image = (Image)e.Result;
//        //}
//        #endregion

//    }

//}


