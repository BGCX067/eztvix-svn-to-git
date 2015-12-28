using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RootKit.API.Providers;
using System.Xml;
using System.ComponentModel;
using RootKit.Web;
using RootKit.Drawings.BasicFilters;
using System.Drawing;

namespace RootKit.API.Providers
{
    //public class OldAllocine : MovieProvider
    //{
    //    #region *** Interface Properties ***
    //    #endregion

    //    #region *** properties ***

    //    protected QueryType _query = QueryType.Title;
    //    public string Query { get { return _query.ToString(); } set { _query = (QueryType)Enum.Parse(typeof(QueryType), value, true); } }

    //    protected string _lang = "fr";
    //    public string Language { get { return _lang; } set { _lang = value.ToLower(); } }

    //    #endregion
        
    //    #region *** cTor ***
    //    public OldAllocine()
    //    {
    //        // initialisation de la clé API
    //        this._apiKey = "9b8f0779badbad3b46d6718ee95a68ff";

    //        this.WorkerReportsProgress = true;
    //        this.WorkerSupportsCancellation = true;

    //        this.ProgressChanged += new ProgressChangedEventHandler(bwMovies_ProgressChanged);
    //        this.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwMovies_RunWorkerCompleted);
            
    //        //this.ObjectLoaded += new ObjectLoadedEventHandler(bwMovies_ObjectLoaded);
    //    }
    //    #endregion
        
    //    #region *** Methods ***
    //    public override void Search(String _search)
    //    {
    //        //http:// wiki.gromez.fr/dev/api/allocine
    //        //http:// api.allocine.fr/xml/search?q=ironman&partner=1
            
    //        // ne pas mettre de slash a la fin de la searchstring sous peine de ne pas avoir de résultat
    //        String SearchString = "http://api.allocine.fr/xml/search?q={0}&partner=1";
    //        SearchString = String.Format(SearchString, _search);

    //        movieXml.Load(SearchString);
            
    //        this.DoWork += new DoWorkEventHandler(bwMovies_DoWork);
    //        this.RunWorkerAsync();
    //    }

    //    public override void GetMovieData(Movie _movie)
    //    {
    //        //http://passion-xbmc.org/scraper/API/1/Movie.GetInfo/ID/fr/XML/9b8f0779badbad3b46d6718ee95a68ff/53751
    //        Movie movie = new Movie();
    //        String SearchString = "http://passion-xbmc.org/scraper/API/1/Movie.GetInfo/ID/{0}/{1}/{2}/{3}/";
    //        SearchString = String.Format(SearchString, _lang, "XML", _apiKey, _movie.ID.ToString());

    //        movieXml.Load(SearchString);
    //        //XmlNode movieNode = movieXml.SelectSingleNode("//movie");

    //        this.DoWork += new DoWorkEventHandler(bwMovieData_DoWork);
    //        this.RunWorkerAsync(); 
    //        //return this.LoadMovie(movieNode);
    //    }

    //    public override Movie LoadMovie(XmlNode movieNode, BackgroundWorker worker, DoWorkProviderEventArgs e)
    //    {
    //        Movie movie = new Movie();
    //        Dictionary<String, MoviePicture> dicoPics;
    //        String pictureID, pictureType;
    //        //decimal pourcent;
    //        //DownloadManagerThreaded downMan = new DownloadManagerThreaded();
    //        ResizeFilter _resizeFilter = new ResizeFilter();
    //        Image _image;

    //        _resizeFilter.Width = 70;
    //        _resizeFilter.Height = 100;
    //        _resizeFilter.KeepAspectRatio = false;

    //        movie.ID = Convert.ToInt32(movieNode.FirstChild.Attributes["code"].Value);
    //        movie.ID_Allocine = movie.ID;
    //        movie.ID_IMDB = 0;
    //        foreach (XmlNode node in movieNode.FirstChild.ChildNodes)
    //        {
    //            switch (node.Name.ToLower())
    //            {
    //                case "title": movie.Title = node.InnerText;
    //                    break;
    //                case "originaltitle": movie.OriginalTitle = node.InnerText;
    //                    break;
    //                case "release": 
    //                    try { movie.Year = int.Parse(node.FirstChild.InnerText); }
    //                    catch (Exception ex) { };
    //                    break;
    //                case "casting":
    //                    foreach (XmlNode cast in node.ChildNodes)
    //                    {
    //                        //TODO Implement Actor OBJECT (directos would be the same)
    //                        //if (cast.FirstChild.NextSibling.Attributes["code"].Value == "8001") //actor
    //                        switch (cast.FirstChild.NextSibling.Attributes["code"].Value ) 
    //                        {
    //                            case "8001" : //actor
    //                                movie.Actors.Add(
    //                                    new Actor(
    //                                        0, //id
    //                                        cast.FirstChild.InnerText, //name
    //                                        "",
    //                                        0, //thumb ID
    //                                        "" //thumbUrl
    //                                        )
    //                                        );
    //                                break;
    //                            case "8002" : //director
    //                                movie.Directors.Add(cast.FirstChild.InnerText);
    //                                break;
    //                        }
    //                    }
    //                    break;
    //                case "productionyear": 
    //                    try { movie.Year = int.Parse(node.InnerText); }
    //                    catch (Exception ex) { };
    //                    break;
    //                case "poster":
    //                    #region *** Poster ***
    //                    movie.Posters.Clear();
    //                    pictureID =System.Guid.NewGuid().ToString();
    //                    pictureType = "poster";
    //                    try
    //                    {
    //                        movie.Posters.Add(pictureID, new MoviePicture());
    //                    }
    //                    catch (Exception ex) { }
    //                    movie.Posters[pictureID].Type = pictureType;
    //                    movie.Posters[pictureID].Size = "preview";
    //                    movie.Posters[pictureID].UrlPreview = node.Attributes["href"].Value;
    //                    movie.Posters[pictureID].UrlThumb = node.Attributes["href"].Value;
    //                    movie.Posters[pictureID].UrlOriginal = node.Attributes["href"].Value;

    //                    using (_image = DownloadFromWebClient(movie.Posters[pictureID].UrlThumb))
    //                    {
    //                        movie.Posters[pictureID].Preview = _image;
    //                        movie.Posters[pictureID].Thumb = _resizeFilter.ExecuteFilter(_image);
    //                        movie.Posters[pictureID].Width = _image.Width;
    //                        movie.Posters[pictureID].Height = _image.Height;
    //                    }
                        
    //                    if (movie.ThumbCover == null) movie.ThumbCover = movie.Posters[pictureID].Thumb;
    //                    if (movie.Cover == null) movie.Cover = movie.Posters[pictureID].Preview;
    //                    this.ReportProgress(movie);

    //                    #endregion
    //                    break;
    //                case "synopsis": movie.Plot = node.InnerText;
    //                    break;
    //                case "runtime": movie.Runtime = Convert.ToInt32(node.InnerText);
    //                    break;
    //                case "genrelist":
    //                    foreach (XmlNode genre in node.ChildNodes)
    //                    {
    //                        movie.Genres.Add(genre.InnerText);
    //                    }
    //                    break;
    //            }

    //        }

    //        //try
    //        //{
    //        //    foreach (XmlNode rat in movieNode.SelectNodes("ratings/rating"))
    //        //    {
    //        //        movie.Ratings.Add(new Rating(rat.Attributes["type"].Value, rat.Attributes["votes"].Value, rat.InnerText));
    //        //    }
    //        //}
    //        //catch (Exception ex) { }

    //        dicoPics = new Dictionary<String, MoviePicture>();
    //        return movie;
    //    }

    //    private void bwMovieData_DoWork(object sender, DoWorkProviderEventArgs e)
    //    {
    //        BackgroundWorker worker = sender as BackgroundWorker;
    //        _movieList = new Movies();
    //        XmlNode movieNode = movieXml.SelectSingleNode("//movie");
            
    //        int thumbCount = movieXml.SelectNodes("//images/image[@type='Poster' and @size='thumb']").Count;
    //        int movieCount = movieXml.SelectNodes("//movie").Count;

    //        processDivider = (thumbCount < movieCount) ? movieCount : thumbCount;

    //        myTime.Start();

    //        ObjectLoadedProviderEventArgs movieArgs = new ObjectLoadedProviderEventArgs(this.LoadMovie(movieNode, worker, e));
    //        ReportObjectLoaded(movieArgs.loadedObject);
    //    }

 
    //    #endregion 

    //    #region *** BW Methods ***

    //    private void bwMovies_DoWork(object sender, DoWorkProviderEventArgs e)
    //    {
    //        BackgroundWorker worker = sender as BackgroundWorker;
    //        _movieList = new Movies();

    //        //XmlNamespaceManager nsmgr = new XmlNamespaceManager(movieXml.NameTable);
    //        //nsmgr.AddNamespace("atom", "http://www.w3.org/2005/Atom");
    //        //nsmgr.AddNamespace("opensearch", "http://a9.com/-/spec/opensearch/1.1/");

    //        movieXml.InnerXml = movieXml.InnerXml.Replace("atom:", "");
    //        XmlNodeList movieNodes = movieXml.DocumentElement.SelectNodes("//entry/content");
    //        //int thumbCount = movieXml.SelectNodes("//images/image[@type='Poster' and @size='thumb']").Count;
    //        //                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                int movieCount = movieXml.SelectNodes("//movie").Count;

    //        //processDivider = (thumbCount < movieCount) ? movieCount : thumbCount;

    //        myTime.Start();
            
    //        foreach (XmlNode movieNode in movieNodes)
    //        {
    //            if (movieNode.FirstChild.Name.ToLower() == "movie")
    //            {
    //                ObjectLoadedProviderEventArgs moviesArgs = new ObjectLoadedProviderEventArgs(this.LoadMovie(movieNode, worker, e));
    //                _movieList.Add((Movie)moviesArgs.loadedObject);
    //                //OnObjectLoaded(moviesArgs);
    //                ReportObjectLoaded(moviesArgs.loadedObject);
    //            }
    //        }
    //    }

    //    private void bwMovies_ProgressChanged(object sender, ProgressChangedProviderEventArgs e)
    //    {
    //        this.ProgressPercentage = e.ProgressPercentage;
    //    }

    //    private void bwMovies_RunWorkerCompleted(object sender, RunWorkerCompletedProviderEventArgs e)
    //    {
    //        // Timer permettant de calculer le temps du process
    //        myTime.Stop();
    //    }

    //    /// <summary>
    //    /// Should be called in the main process (the one that instanciate the provider)
    //    /// </summary>
    //    /// <param name="sender">The provider object</param>
    //    /// <param name="e"></param>
    //    //private void bwMovies_ObjectLoaded(object sender, ObjectLoadedProviderEventArgs e)
    //    //{
    //    //    //previewPic.Image = (Image)e.Result;
    //    //}
    //    #endregion

    // }

}
