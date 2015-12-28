using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EzTvix.Provider.API
{
    public interface iSerieProvider
    {
        #region ----- Properties -----
        String APIKey { get; set; }
        #endregion

        #region ----- Functions -----
        void Search(string _search);
        void GetSerieData(Serie _Serie);

        #endregion
    }
}
