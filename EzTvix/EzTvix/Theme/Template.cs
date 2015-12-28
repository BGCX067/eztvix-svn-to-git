using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Text;
using EzTvix.Core;
using System.Windows.Forms;
using System.Xml;
using System.Reflection;
using System.Drawing.Imaging;
using RootKit.API;
using EzTvix.Provider;

namespace EzTvix.Theme
{
    //public enum FolderViewType
    //{
    //    Icon, List
    //}
    ///// <summary>
    ///// List of item type
    ///// </summary>
    //public enum ItemType
    //{ 
    //    Alpha,
    //    Empty,

    //    Background,
    //    BackgroundVideo,
    //    VideoBox,
    //    VideoCover,
    //    Audio,
    //    AudioBox,
    //    AudioCover,
    //    Folder,
    //    FolderUp,
    //    FolderAudio,
    //    FolderVideo,
    //    FolderPhoto,
    //    ItemBack,

    //    FolderAdventure,
    //    FolderDivx,
    //    FolderDocs,
    //    FolderDvd,
    //    FolderHD,
    //    FolderSeries,
    //    FolderSpectacle,

    //    StarGrey,
    //    StarYellow

    //}

    /// <summary>
    /// Theme Template containing all the items and pictures related to a theme
    /// </summary>
    class Template
    {
        #region *** Properties ***

        private Dictionary<ItemType, ImageTemplate> ItemList = new Dictionary<ItemType, ImageTemplate>();
        //private Dictionary<ItemType, object> ItemList = new Dictionary<ItemType, object>();
        /// <summary>
        /// Indexer
        /// </summary>
        /// <param name="index">Index of the item (ItemType)</param>
        /// <returns>an ImageTemplate objet</returns>
        public ImageTemplate this[ItemType index]
        {
            get { return this.ItemList[index]; }
            set { this.ItemList[index] = value; }
        }
        private XmlDocument xmlConfig = new XmlDocument();

        private String _currentTheme = "Default";
        /// <summary>
        /// Name of the loaded theme
        /// </summary>
        public String Name
        {
            get { return _currentTheme; }
            set
            {
                _currentTheme = value;
                loadTheme();

            }
        }

        /// <summary>
        /// check if default theme is active or not
        /// </summary>
        public bool useDefaultTheme
        { get { return (this.Name.ToLower() == "default"); } }

        #region *** Images ***
        public ImageTemplate Empty { get { return ItemList[ItemType.Empty]; } }
        public ImageTemplate Background { get { return ItemList[ItemType.Background]; } }
        public ImageTemplate Folder { get { return ItemList[ItemType.Folder]; } }
        //public ImageTemplate Audio { get { return ItemList[ItemType.Audio]; } }
        public ImageTemplate AudioBox { get { return ItemList[ItemType.AudioBox]; } }
        //public ImageTemplate AudioCover { get { return ItemList[ItemType.AudioCover]; } }
        public ImageTemplate VideoBox { get { return ItemList[ItemType.VideoBox]; } }
        public ImageTemplate ItemBack { get { return ItemList[ItemType.ItemBack]; } }
        //public ImageTemplate UpFolder { get { return ItemList[ItemType.FolderUp]; } }
        //public ImageTemplate FolderAudio { get { return ItemList[ItemType.FolderAudio]; } }
        //public ImageTemplate FolderPhoto { get { return ItemList[ItemType.FolderPhoto]; } }
        //public ImageTemplate FolderVideo { get { return ItemList[ItemType.FolderVideo]; } }
        private BackgroundTemplate _backgroundVideo = new BackgroundTemplate();
        public BackgroundTemplate BackgroundVideo { get { return _backgroundVideo; } }

        //private ImageTemplate _folderAnime;
        //private ImageTemplate _folderAventure;
        //private ImageTemplate _folderBA;
        //private ImageTemplate _folderComedie;
        //private ImageTemplate _folderDivx;
        //private ImageTemplate _folderDocs;
        //private ImageTemplate _folderDvd;
        //private ImageTemplate _folderKids;
        //private ImageTemplate _folderHD;
        //private ImageTemplate _folderSeries;
        //private ImageTemplate _folderSF;
        //private ImageTemplate _folderSpectacles;
        //private ImageTemplate _folderXXX;
        #endregion
        #endregion

        #region ***constructor ***
        /// <summary>
        /// Constructor
        /// </summary>
        public Template()
        {
            loadConfig();
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_theme">The theme that will be loaded when the object is created</param>
        public Template(String _theme)
        {
            loadConfig();
            this.Name = _theme;
        }
        #endregion

        #region *** Methods ***
        /// <summary>
        /// Return a valid Theme list from the <code>TvixInfo.ThemeFolder</code> folder.
        /// </summary>
        /// <returns></returns>
        public DirectoryInfo[] getThemeList()
        {
            if (!Directory.Exists(TvixInfo.ThemeFolder))
                Directory.CreateDirectory(TvixInfo.ThemeFolder);

            DirectoryInfo[] themes = (new DirectoryInfo(TvixInfo.ThemeFolder)).GetDirectories();
            List<DirectoryInfo> themesExists = new List<DirectoryInfo>();
            foreach (DirectoryInfo subdirectory in themes)
            {
                if (File.Exists(subdirectory.FullName + @"\Theme.xml"))
                {
                    themesExists.Add(subdirectory);
                }
            }
            return themesExists.ToArray();
        }

        /// <summary>
        /// Load theme.Xml Config file
        /// </summary>
        private void loadConfig()
        {
            XmlTextReader xmlRead;
            String configFilePath = TvixInfo.ThemeFolder + @"\" + _currentTheme + @"\Theme.xml";

            if (useDefaultTheme)
            {
                xmlRead = new XmlTextReader(new StringReader(global::EzTvix.Theme.Default.Theme));
                xmlConfig.Load(xmlRead);
            }
            else
            {
                try
                {
                    xmlConfig.Load(configFilePath);
                }
                catch (FileNotFoundException e)
                {
                    MessageBox.Show("file Config.xml missing or corrupted in path :\r\n" + configFilePath + "\r\nPlease change the current theme!", e.Message, MessageBoxButtons.OK);
                }
            }
        }

        //private Image getDefaultPic(String _image)
        //{
        //    System.Reflection.Assembly a;
        //    System.IO.Stream file;
        //    a = System.Reflection.Assembly.GetExecutingAssembly();
        //    //System.IO.Stream file = a.GetManifestResourceStream("EzTvix.Theme.Default.Tvix.jpg");
        //    try
        //    {
        //        file = a.GetManifestResourceStream("EzTvix.Theme.Default." + _image.Replace("\\", ""));
        //        return Image.FromStream(file);
        //    }
        //    catch (ArgumentException e)
        //    {
        //        MessageBox.Show("Missing embedded ressource : \r\n - image : " + _image, e.Message, MessageBoxButtons.OK);

        //        return global::EzTvix.Theme.Default.Background;

        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(_image, e.Message, MessageBoxButtons.OK);
        //        return global::EzTvix.Theme.Default.Empty;

        //    }

        //}

        //private Image getPic(String _image)
        //{

        //    String imagePath = TvixInfo.ThemeFolder + @"\" + _currentTheme + _image;
        //    try
        //    {
        //        //return (isDefault) ? global::EzTvix.Theme.Default.Background : Image.FromFile(imagePath);
        //        return (useDefaultTheme) ? this.getDefaultPic(_image) : Image.FromFile(imagePath);
        //    }
        //    catch (Exception e)
        //    {
        //        //return global::EzTvix.Theme.Default.Background;
        //        return this.getDefaultPic(_image);
        //    }
        //}

        //private Image getPic(String _image, String _AlternateImage)
        //{

        //    String imagePath = TvixInfo.ThemeFolder + @"\" + _currentTheme + _image;
        //    String alternateImagePath = TvixInfo.ThemeFolder + @"\" + _currentTheme + _AlternateImage;

        //    try
        //    {
        //        if (useDefaultTheme)
        //        {
        //            return this.getDefaultPic(_image);
        //        }
        //        else
        //        {
        //            return Image.FromFile(imagePath);
        //        }

        //    }
        //    catch (FileNotFoundException e)
        //    {
        //        try { return Image.FromFile(alternateImagePath); }
        //        catch (Exception ex) { return this.getDefaultPic(_image); }
        //    }
        //    catch (Exception e)
        //    {
        //        return this.getDefaultPic(_image);
        //    }
        //}

        //public ImageList getImagesFromResx(String _resxName)
        //{
        //    ImageList imageList = new ImageList();
        //    imageList.ImageSize = new Size(200, 200);
        //    imageList.ColorDepth = ColorDepth.Depth32Bit;
        //    Assembly myAssembly = Assembly.GetExecutingAssembly();
        //    String[] names = myAssembly.GetManifestResourceNames();
        //    Stream myStream;
        //    IEnumerable<string> namesWithFourCharacters =
        //        from name in names
        //        where name.Contains("." + _resxName + ".") &&
        //                (name.ToLower().EndsWith(".png") ||
        //                name.ToLower().EndsWith(".jpg") ||
        //                name.ToLower().EndsWith(".gif"))

        //        select name;

        //    foreach (String name in namesWithFourCharacters)
        //    {
        //        //if (name.Contains("." + _resxName + ".") && name.ToLower().EndsWith(".png"))
        //        //{
        //        myStream = this.GetType().Assembly.GetManifestResourceStream(name);
        //        imageList.Images.Add(Image.FromStream(myStream));
        //        //}

        //    }
        //    return imageList;
        //}

        /// <summary>
        /// Load the theme
        /// </summary>
        public void loadTheme()
        {
            FontStyle myFont;
            XmlNode _node;
            this.loadConfig();

            ItemList.Clear();

            //ItemList.Add(ItemType.Background, new ImageTemplate(this.getPic(@"\Background.jpg")));
            ItemList.Add(ItemType.Background, new ImageTemplate(@"\Background.jpg",this._currentTheme));
            ItemList[ItemType.Background].layerToSave = Layer.Foreground;
            ItemList[ItemType.Background].Size = TvixInfo.BackgroundMask;

            this._backgroundVideo = new BackgroundTemplate(@"\BackgroundVideo.jpg", this._currentTheme);
            
            _node = xmlConfig.SelectSingleNode("//Item[@name='" + this._backgroundVideo.Name+"']");
            this._backgroundVideo.load(_node);
                                    
            #region *** VideoBox ***
            ItemList.Add(ItemType.VideoBox, new ImageTemplate(@"\VideoBox.png", this._currentTheme));

            _node = xmlConfig.SelectSingleNode("//Item[@name='" + ItemList[ItemType.VideoBox].Name + "']/Mask");
       //     _node = xmlConfig.SelectSingleNode("//VideoBox/Mask");

            ItemList[ItemType.VideoBox].layerToSave = Layer.Foreground;

            ItemList[ItemType.VideoBox].Background.Mask = new Rectangle(
                Int32.Parse(_node.Attributes["x"].Value),
                Int32.Parse(_node.Attributes["y"].Value),
                Int32.Parse(_node.Attributes["width"].Value),
                Int32.Parse(_node.Attributes["height"].Value));

            _node = xmlConfig.SelectSingleNode("//Item[@name='" + ItemList[ItemType.VideoBox].Name + "']/TextZone");
            ItemList[ItemType.VideoBox].Text.TextZone = new Rectangle(
                            Int32.Parse(_node.Attributes["x"].Value),
                            Int32.Parse(_node.Attributes["y"].Value),
                            Int32.Parse(_node.Attributes["width"].Value),
                            Int32.Parse(_node.Attributes["height"].Value));

            _node = xmlConfig.SelectSingleNode("//Item[@name='" + ItemList[ItemType.VideoBox].Name + "']/Text");

            try
            {
                
                myFont =
                ((Convert.ToBoolean(_node.Attributes["bold"].Value)) ? FontStyle.Bold : FontStyle.Regular)
                |
                ((Convert.ToBoolean(_node.Attributes["italic"].Value)) ? FontStyle.Italic : FontStyle.Regular)
                ;
            }
            catch (Exception e)
            {
                myFont = FontStyle.Regular;
            }

            ItemList[ItemType.VideoBox].Text.TextFont = new Font(
                _node.Attributes["family"].Value,
                float.Parse(_node.Attributes["size"].Value),
                myFont);
            
            ItemList[ItemType.VideoBox].Text.TextColor = Color.FromName(_node.Attributes["color"].Value);

            ItemList[ItemType.VideoBox].Text.TextFormat.Alignment = StringAlignment.Center; 

            #endregion

            #region *** AudioBox ***
            ItemList.Add(ItemType.AudioBox, new ImageTemplate(@"\AudioBox.png", this._currentTheme));
            //ItemList[ItemType.AudioBox].Name = "AudioBox.png";
            ItemList[ItemType.AudioBox].layerToSave = Layer.Foreground;

            _node = xmlConfig.SelectSingleNode("//Item[@name='" + ItemList[ItemType.AudioBox].Name + "']/Mask");
            ItemList[ItemType.AudioBox].Background.Mask = new Rectangle(
                Int32.Parse(_node.Attributes["x"].Value),
                Int32.Parse(_node.Attributes["y"].Value),
                Int32.Parse(_node.Attributes["width"].Value),
                Int32.Parse(_node.Attributes["height"].Value));

            _node = xmlConfig.SelectSingleNode("//Item[@name='" + ItemList[ItemType.AudioBox].Name + "']/TextZone");
            ItemList[ItemType.AudioBox].Text.TextZone = new Rectangle(
                            Int32.Parse(_node.Attributes["x"].Value),
                            Int32.Parse(_node.Attributes["y"].Value),
                            Int32.Parse(_node.Attributes["width"].Value),
                            Int32.Parse(_node.Attributes["height"].Value));

            _node = xmlConfig.SelectSingleNode("//Item[@name='" + ItemList[ItemType.AudioBox].Name + "']/Text");

            try
            {
                myFont =
                ((Convert.ToBoolean(_node.Attributes["bold"].Value)) ? FontStyle.Bold : FontStyle.Regular)
                |
                ((Convert.ToBoolean(_node.Attributes["italic"].Value)) ? FontStyle.Italic : FontStyle.Regular)
                ;
            }
            catch (Exception e)
            {
                myFont = FontStyle.Regular;
            }

            ItemList[ItemType.AudioBox].Text.TextFont = new Font(
                _node.Attributes["family"].Value,
                float.Parse(_node.Attributes["size"].Value),
                myFont);

            ItemList[ItemType.AudioBox].Text.TextColor = Color.FromName(_node.Attributes["color"].Value);

            #endregion

            ItemList.Add(ItemType.ItemBack, new ImageTemplate(@"\ItemBack.png", this._currentTheme));//ItemList[ItemType.ItemBack].Name = "ItemBack.png";
            ItemList[ItemType.ItemBack].layerToSave = Layer.Foreground;

            ItemList.Add(ItemType.Folder, new ImageTemplate(@"\Folder.png", this._currentTheme));//ItemList[ItemType.Folder].Name = "Folder.png";
            ItemList[ItemType.Folder].layerToSave = Layer.Foreground;

            ItemList.Add(ItemType.FolderUp, new ImageTemplate(@"\UpFolder.png", this._currentTheme));//ItemList[ItemType.FolderUp].Name = "UpFolder.png";
            ItemList.Add(ItemType.Audio, new ImageTemplate(@"\Audio.png", this._currentTheme));//ItemList[ItemType.Audio].Name = "Audio.png";

            ItemList.Add(ItemType.AudioCover, new ImageTemplate(@"\AudioCover.png", this._currentTheme));//ItemList[ItemType.AudioCover].Name = "AudioCover.png";
            ItemList.Add(ItemType.VideoCover, new ImageTemplate(@"\VideoCover.png", this._currentTheme));//ItemList[ItemType.VideoCover].Name = "VideoCover.png";
            ItemList.Add(ItemType.FolderAudio, new ImageTemplate(@"\FolderAudio.png", @"\Folder.png", this._currentTheme));//ItemList[ItemType.FolderAudio].Name = "FolderAudio.png";
            ItemList.Add(ItemType.FolderPhoto, new ImageTemplate(@"\FolderPhoto.png", @"\Folder.png", this._currentTheme));//ItemList[ItemType.FolderPhoto].Name = "FolderPhoto.png";
            ItemList.Add(ItemType.FolderVideo, new ImageTemplate(@"\FolderVideo.png", @"\Folder.png", this._currentTheme));//ItemList[ItemType.FolderVideo].Name = "FolderVideo.png";

            ItemList.Add(ItemType.FolderAdventure, new ImageTemplate(@"\FolderAdventure.png", @"\Folder.png", this._currentTheme));
            ItemList.Add(ItemType.FolderDivx, new ImageTemplate(@"\FolderDivx.png", @"\Folder.png", this._currentTheme));
            ItemList.Add(ItemType.FolderDocs, new ImageTemplate(@"\FolderDocs.png", @"\Folder.png", this._currentTheme));
            ItemList.Add(ItemType.FolderDvd, new ImageTemplate(@"\FolderDvd.png", @"\Folder.png", this._currentTheme));
            ItemList.Add(ItemType.FolderHD, new ImageTemplate(@"\FolderHD.png", @"\Folder.png", this._currentTheme));
            ItemList.Add(ItemType.FolderSeries, new ImageTemplate(@"\FolderSeries.png", @"\Folder.png", this._currentTheme));
            ItemList.Add(ItemType.FolderSpectacle, new ImageTemplate(@"\FolderSpectacle.png", @"\Folder.png", this._currentTheme));

            ItemList.Add(ItemType.StarGrey, new ImageTemplate(@"\StarGrey.png", this._currentTheme));
            ItemList.Add(ItemType.StarYellow, new ImageTemplate(@"\StarYellow.png", this._currentTheme));
 
            ItemList.Add(ItemType.Empty, new ImageTemplate(global::EzTvix.Theme.Default.Empty));
            //ItemList.Add(ItemType.Alpha, new ImageTemplate(global::EzTvix.Theme.Default.Alpha));
        }

        /// <summary>
        /// Save the current theme
        /// </summary>
        public void Save()
        {

            String themePath = TvixInfo.ThemeFolder + @"\" + this._currentTheme;
            XmlDocument doc = new XmlDocument();

            Dictionary<ItemType, ImageTemplate>.ValueCollection vCol = ItemList.Values;

            doc.LoadXml(@"<?xml version='1.0' encoding='utf-8'?><Theme name='" + this._currentTheme + "'></Theme>");
            XmlElement root = doc.DocumentElement;
            XmlNode node;

            foreach (ImageTemplate img in vCol)
            {
                if (img.Name != "")
                {
                    node = (XmlNode)(img.Save());
                    root.AppendChild(doc.ImportNode(node, true));
                }
            }

            SetFolderView(themePath, FolderViewType.Icon);
            //if (!File.Exists(themePath + @"\icon.tvix"))
            //    File.Create(themePath + @"\icon.tvix");

            node = (XmlNode)
                this._backgroundVideo.save();
            root.AppendChild(doc.ImportNode(node, true));
            doc.Save(themePath + @"\Theme.xml");

        }

        /// <summary>
        /// Create the basics for the new theme
        /// </summary>
        /// <param name="newTheme">New Theme Name</param>
        public void CreateThemeConfig(String newTheme)
        {
            XmlTextReader xmlRead;
            XmlDocument xmlConfig = new XmlDocument();
            Directory.CreateDirectory(TvixInfo.ThemeFolder + @"\" + newTheme);
            
            xmlRead = new XmlTextReader(new StringReader(global::EzTvix.Theme.Default.Config));
            xmlConfig.Load(xmlRead);
            xmlConfig.Save(TvixInfo.ThemeFolder + @"\" + newTheme + @"\theme.xml");
            xmlRead.Close();
        }


        #endregion
        public void ApplyBackgroundVideo(Movie movie, String path)
        {
            this.BackgroundVideo.Generate(movie, path);
            SaveVideoBox(path);
            this[ItemType.FolderUp].Image.Save(path + @"upfolder.jpg");
        }
        public void Apply(String path)
        {
            string fullName = path + ThemeInfo.VideoBackgroundName + "." + ThemeInfo.VideoBackgroundExtention;
            if (File.Exists(fullName)) File.Delete(fullName);

            this.Background.BaseImage.Save(path + ThemeInfo.VideoBackgroundName + "." + ThemeInfo.VideoBackgroundExtention);
            this[ItemType.FolderUp].Image.Save(path + @"upfolder.jpg");
            
        }
        public void SaveVideoBox(string path)
        {
            string fullName = path + ThemeInfo.VideoBoxName + "." + ThemeInfo.VideoBoxExtention;
            if (File.Exists(fullName)) File.Delete(fullName);
            this[ItemType.VideoBox].Image.Save(fullName);
            
        }
        public void SetFolderView(string path, FolderViewType folderType)
        {
            if (folderType == FolderViewType.Icon)
            {
                if (!File.Exists(path + @"\icon.tvix"))
                {
                    Stream fs = File.Create(path + @"\icon.tvix");
                    fs.Close();
                    fs.Dispose();
                }

            }
            else
            {
                if (File.Exists(path + @"\icon.tvix"))
                    File.Delete(path + @"\icon.tvix");
            }

        }
        public FolderViewType GetFolderView(string path)
        {
            return (File.Exists(path + @"\icon.tvix")) ? FolderViewType.Icon : FolderViewType.List;
        }
    }

}
