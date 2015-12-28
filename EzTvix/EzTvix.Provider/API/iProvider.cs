using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EzTvix.Provider.API
{
    public enum QueryType
    {
        IMDB, Title, ID
    }
    public interface iProvider
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
        void Search(string _search);
        void GetData(Movie _movie);

        #endregion
    }
}
