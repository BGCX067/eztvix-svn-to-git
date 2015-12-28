using System;
using System.Collections.Generic;

namespace RootKit.API.Providers
{
    //public enum QueryType
    //{
    //    IMDB, Title, ID
    //}
    public interface OldiProvider 
    {
        #region ----- Properties -----
        String APIKey { get; set; }

        //Int32 ID { get; }
        //Int32 ID_Allocine { get; }
        //Int32 ID_IMDB { get; }

        //String Url { get; }
        //String Title { get; }
        //String OriginalTitle { get; }
        //String Year { get; }
        //String[] Directors { get; }
        //Actor[] Actors { get; }
        //String Plot { get; }
        //String Tagline { get; }
        //String[] Genres { get; }
        //MoviePicture[] Moviepictures { get; }
        //Rating[] Ratings { get; }
        #endregion

        #region ----- Functions -----
        //void Search(string _search);
        //void GetMovieData(Movie _movie);
        
        #endregion
    }
}
