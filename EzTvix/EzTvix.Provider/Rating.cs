using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace EzTvix.Provider
{
    public class Rating
    {
        #region *** properties ***
        protected String _type;
        public String Type
        { get { return _type; } }

        protected Int32 _votes;
        public Int32 Votes
        { get { return _votes; } }

        protected Decimal _score;
        public Decimal Score
        { get { return _score; } }

        #endregion

        #region *** constructor ***
        public Rating(String type, String votes, String score)
        {
            _type = type;
            _votes = Convert.ToInt32(votes);
            _score = Convert.ToDecimal(score);
        }
        public Rating(String type, Int32 votes, decimal score)
        {
            _type = type;
            _votes = votes;
            _score = score;
        }

        public Rating(XmlNode node)
        {
            _type = node.Attributes["type"].Value;
            _votes = Convert.ToInt32(node.Attributes["votes"].Value);
            _score = Convert.ToDecimal(node.InnerText);
        }
        #endregion

        #region *** Methods ***
        /// <summary>
        /// get the object as XML
        /// </summary>
        /// <param name="document"> the XML source document to populate</param>
        /// <returns>a XmlNode containing the objet</returns>
        public XmlNode Xml(XmlDocument document)
        {
            XmlNode genericNode = document.CreateNode(XmlNodeType.Element, "Rating", "");
            XmlAttribute typeNode = document.CreateAttribute("type"); typeNode.Value = this.Type;
            XmlAttribute votesNode = document.CreateAttribute("votes"); votesNode.Value = this.Votes.ToString();
            genericNode.Attributes.Append(typeNode);
            genericNode.Attributes.Append(votesNode);
            genericNode.InnerText = this.Score.ToString();

            return genericNode;
        }

        #endregion
    }
}
