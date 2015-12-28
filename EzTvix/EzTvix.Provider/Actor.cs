using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml;


namespace EzTvix.Provider
{
    public class Actor
    {
        #region *** properties ***
        protected Int32 m_id;
        public Int32 ID
        { get{ return m_id;} set { m_id = value; } }

        protected String m_name;
        public String Name
        { get { return m_name; } set { m_name = value; } }

        protected String m_character;
        public String Character
        { get { return m_character; } set { m_character = value; } }

        protected Int32 m_idThumb;
        public Int32 IDThumb
        { get { return m_idThumb; } set { m_idThumb = value; } }

        protected String m_thumbUrl;
        public String ThumbUrl
        { get { return m_thumbUrl; } set { m_thumbUrl = value; } }

        protected Image m_thumb;
        public Image Thumb
        { get { return m_thumb; } set { m_thumb = value; } }

        /// <summary>
        /// This matches the First, Second, Third, and Don't Care options on the site, which determine if the actor is shown on the series page or not. First (SortOrder=0), Second (SortOrder=1), and Third (SortOrder=2) generally mean the actor plays a primary role in the series. Don't Care (SortOrder=3) generally means the actor plays a lesser role. In some series there are no primary actors, so all actors will have a SortOrder of 3. The actors are also listed in the report in SortOrder, followed by those with images, and then finally by Name. So using the order they show up in the file is a valid method. 
        /// </summary>
        private int m_sortOrder;
        public int SortOrder
        { get { return m_sortOrder; } set { m_sortOrder = value; } }
        #endregion

        #region *** ctor ***
        public Actor(Int32 id, string name, string character, Int32 idThumb, string thumbUrl)
        {
            this.m_id = id;
            this.m_name = name;
            this.m_character = character;
            this.m_idThumb = idThumb;
            this.m_thumbUrl = thumbUrl;
        }

        public Actor(XmlNode node)
        {
            this.m_id = Convert.ToInt32(node.Attributes["id"].Value);
            this.m_name = node.Attributes["name"].Value;
            this.m_character = node.Attributes["character"].Value;
            this.m_idThumb = Convert.ToInt32(node.Attributes["idthumb"].Value);
            this.m_thumbUrl = node.Attributes["thumb"].Value;
        }
        #endregion

        /// <summary>
        /// get the object as XML
        /// </summary>
        /// <param name="document"> the XML source document to populate</param>
        /// <returns>a XmlNode containing the objet</returns>
        public XmlNode Xml(XmlDocument document)
        {
            //XmlDocument doc = new XmlDocument();

            XmlNode genericNode = document.CreateNode(XmlNodeType.Element, "Actor", "");
            XmlAttribute idNode = document.CreateAttribute("id"); idNode.Value = this.m_id.ToString();
            XmlAttribute nameNode = document.CreateAttribute("name"); nameNode.Value = this.m_name;
            XmlAttribute characterNode = document.CreateAttribute("character"); characterNode.Value = this.m_character;
            XmlAttribute idThumbNode = document.CreateAttribute("idthumb"); idThumbNode.Value = this.m_idThumb.ToString();
            XmlAttribute thumbNode = document.CreateAttribute("thumb"); thumbNode.Value = this.m_thumbUrl;
            genericNode.Attributes.Append(idNode);
            genericNode.Attributes.Append(nameNode);
            genericNode.Attributes.Append(characterNode);
            genericNode.Attributes.Append(idThumbNode);
            genericNode.Attributes.Append(thumbNode);

            return genericNode;
        }

        public override string ToString()
        {
            return this.m_name;
        }
    }
}
