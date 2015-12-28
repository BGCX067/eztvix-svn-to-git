using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace EzTvix.Provider
{
    public class Serie
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

        #endregion
    }
}
