using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml;
 
namespace RootKit.API
{
    //public class OldActor
    //{
    //    #region *** properties ***
    //    protected Int32 _id;
    //    public Int32 ID
    //    { get; set; }

    //    protected String _name;
    //    public String Name
    //    { get; set; }

    //    protected String _character;
    //    public String Character
    //    { get; set; }

    //    protected Int32 _idThumb;
    //    public Int32 IDThumb
    //    { get; set; }

    //    protected String _thumbUrl;
    //    public String ThumbUrl
    //    { get; set; }

    //    protected Image _thumb;
    //    public Image Thumb
    //    { get; set; }
    //    #endregion
    //    #region *** ctor ***
    //    public OldActor(Int32 id, string name, string character, Int32 idThumb, string thumbUrl)
    //    {
    //        this._id = id;
    //        this._name = name;
    //        this._character = character;
    //        this._idThumb = idThumb;
    //        this._thumbUrl = thumbUrl;
    //    }

    //    public OldActor(XmlNode node)
    //    {
    //        this._id = Convert.ToInt32(node.Attributes["id"].Value);
    //        this._name = node.Attributes["name"].Value;
    //        this._character = node.Attributes["character"].Value;
    //        this._idThumb = Convert.ToInt32(node.Attributes["idthumb"].Value);
    //        this._thumbUrl = node.Attributes["thumb"].Value;
    //    }
    //    #endregion
    //    /// <summary>
    //    /// get the object as XML
    //    /// </summary>
    //    /// <param name="document"> the XML source document to populate</param>
    //    /// <returns>a XmlNode containing the objet</returns>
    //    public XmlNode Xml(XmlDocument document)
    //    {
    //        //XmlDocument doc = new XmlDocument();
            
    //        XmlNode genericNode = document.CreateNode(XmlNodeType.Element, "Actor", "");
    //        XmlAttribute idNode = document.CreateAttribute("id"); idNode.Value = this._id.ToString();
    //        XmlAttribute nameNode = document.CreateAttribute("name"); nameNode.Value = this._name;
    //        XmlAttribute characterNode = document.CreateAttribute("character"); characterNode.Value = this._character;
    //        XmlAttribute idThumbNode = document.CreateAttribute("idthumb"); idThumbNode.Value = this._idThumb.ToString();
    //        XmlAttribute thumbNode = document.CreateAttribute("thumb"); thumbNode.Value = this._thumbUrl;
    //        genericNode.Attributes.Append(idNode);
    //        genericNode.Attributes.Append(nameNode);
    //        genericNode.Attributes.Append(characterNode);
    //        genericNode.Attributes.Append(idThumbNode);
    //        genericNode.Attributes.Append(thumbNode);

    //        return genericNode;
    //    }

    //    public override string ToString()
    //    {
    //        return this._name;
    //    }



    //}

}
