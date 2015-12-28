using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using RootKit.Drawings;
using RootKit.Drawings.BasicFilters;
using System.Xml;
using EzTvix.Core;
using System.IO;
using RootKit.API;
using RootKit.Core;
using System.Drawing.Imaging;
using EzTvix.Provider;
namespace EzTvix.Theme
{
    public enum BackgroundTemplateItem
    { 
        BaseImage,
        Plot,
        Title,
        Year,
        Actors,
        Directors,
        Runtime,
        Genre,
        Rating,
        Stars,
        Fanart,
        ImageFormat,
        SoundFormat,
        Résolution,
        Cover,
        Box
    }
    class BackgroundTemplate : ImageTemplate
    {
        #region *** Properties ***

        public new Image Image
        {
            get
            {
                this.build();
                return _image;
            }
            set { _image = value; }
        }

        private Dictionary<BackgroundTemplateItem, ImageLayer> Layers = new Dictionary<BackgroundTemplateItem, ImageLayer>();
 
        public ImageLayer this[BackgroundTemplateItem index]
        {
            get { return this.Layers[index]; }
            set { this.Layers[index] = value; }
        }

        //public String Name = "";
        
        #region *** Layers ***
        //protected ImageLayer _layerPlot = new ImageLayer();
        //public ImageLayer LayerPlot
        //{
        //    get
        //    {
        //        return _layerPlot;
        //    }
        //    set
        //    {
        //        //if (_layerAlpha == null) _layerAlpha = global::EzTvix.Theme.Default.Empty;
        //        if (value != null)
        //        {
        //            resizeFilter.Width = Int32.Parse(_layerPlot.TextZone.Width.ToString());
        //            resizeFilter.Height = Int32.Parse(_layerPlot.TextZone.Height.ToString());
        //            resizeFilter.KeepAspectRatio = false;
        //            _layerPlot.Image = resizeFilter.ExecuteFilter(value);
        //        }
        //        else
        //            _layerPlot.Image = global::EzTvix.Theme.Default.Empty;
        //    }
        //}

        //protected ImageLayer _layerTitle = new ImageLayer();//;
        public ImageLayer LayerTitle 
        {
            get
            {
                return this[BackgroundTemplateItem.Title];
            }
        //    set
        //    {
        //        if (_layerAlpha == null) _layerAlpha = global::EzTvix.Theme.Default.Empty;
        //        if (value != null)
        //        {
        //            resizeFilter.Width = Int32.Parse(_layerRating.TextZone.Width.ToString());
        //            resizeFilter.Height = Int32.Parse(_layerRating.TextZone.Height.ToString());
        //            resizeFilter.KeepAspectRatio = false;
        //            _layerTitle.Image = resizeFilter.ExecuteFilter(value);
        //        }
        //        else
        //            _layerTitle.Image = global::EzTvix.Theme.Default.Empty;

        //        //_layerBack = value;
        //    }
        }

        //protected ImageLayer _layerYear = new ImageLayer();
        //public ImageLayer LayerYear
        //{
        //    get
        //    {
        //        return _layerYear;
        //    }
        //    set
        //    {
        //        if (_layerAlpha == null) _layerAlpha = global::EzTvix.Theme.Default.Empty;
        //        if (value != null)
        //        {
        //            resizeFilter.Width = Int32.Parse(_layerYear.TextZone.Width.ToString()); ;
        //            resizeFilter.Height = Int32.Parse(_layerYear.TextZone.Height.ToString()); ;
        //            resizeFilter.KeepAspectRatio = false;
        //            _layerYear.Image = resizeFilter.ExecuteFilter(value);
        //        }
        //        else
        //            _layerYear.Image = global::EzTvix.Theme.Default.Empty;

        //        //_layerBack = value;
        //    }
        //}

        //protected ImageLayer _layerActors = new ImageLayer();
        //public ImageLayer LayerActors
        //{
        //    get
        //    {
        //        return _layerActors;
        //    }
        //    set
        //    {
        //        if (_layerAlpha == null) _layerAlpha = global::EzTvix.Theme.Default.Empty;
        //        if (value != null)
        //        {
        //            resizeFilter.Width = Int32.Parse(_layerActors.TextZone.Width.ToString()); ;
        //            resizeFilter.Height = Int32.Parse(_layerActors.TextZone.Height.ToString()); ;
        //            resizeFilter.KeepAspectRatio = false;
        //            _layerActors.Image = resizeFilter.ExecuteFilter(value);
        //        }
        //        else
        //            _layerActors.Image = global::EzTvix.Theme.Default.Empty;

        //        //_layerBack = value;
        //    }
        //}

        //protected ImageLayer _layerRuntime = new ImageLayer();
        //public ImageLayer LayerRuntime
        //{
        //    get
        //    {
        //        return _layerRuntime;
        //    }
        //    set
        //    {
        //        if (_layerAlpha == null) _layerAlpha = global::EzTvix.Theme.Default.Empty;
        //        if (value != null)
        //        {
        //            resizeFilter.Width = Int32.Parse(_layerRuntime.TextZone.Width.ToString()); ;
        //            resizeFilter.Height = Int32.Parse(_layerRuntime.TextZone.Height.ToString()); ;
        //            resizeFilter.KeepAspectRatio = false;
        //            _layerRuntime.Image = resizeFilter.ExecuteFilter(value);
        //        }
        //        else
        //            _layerRuntime.Image = global::EzTvix.Theme.Default.Empty;

        //        //_layerBack = value;
        //    }
        //}

        //protected ImageLayer _layerGenre = new ImageLayer();
        //public ImageLayer LayerGenre
        //{
        //    get
        //    {
        //        return _layerGenre;
        //    }
        //    set
        //    {
        //        if (_layerAlpha == null) _layerAlpha = global::EzTvix.Theme.Default.Empty;
        //        if (value != null)
        //        {
        //            resizeFilter.Width = Int32.Parse(_layerGenre.TextZone.Width.ToString()); ;
        //            resizeFilter.Height = Int32.Parse(_layerGenre.TextZone.Height.ToString()); ;
        //            resizeFilter.KeepAspectRatio = false;
        //            _layerGenre.Image = resizeFilter.ExecuteFilter(value);
        //        }
        //        else
        //            _layerGenre.Image = global::EzTvix.Theme.Default.Empty;

        //        //_layerBack = value;
        //    }
        //}

        //protected ImageLayer _layerRating = new ImageLayer();
        //public ImageLayer LayerRating
        //{
        //    get
        //    {
        //        return _layerRating;
        //    }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            resizeFilter.Width = Int32.Parse(_layerRating.TextZone.Width.ToString()); ;
        //            resizeFilter.Height = Int32.Parse(_layerRating.TextZone.Height.ToString()); ;
        //            resizeFilter.KeepAspectRatio = false;
        //            _layerRating.Image = resizeFilter.ExecuteFilter(value);
        //        }
        //        else
        //            _layerRating.Image = global::EzTvix.Theme.Default.Empty;

        //        //_layerBack = value;
        //    }
        //}

        //protected ImageLayer _layerStars = new ImageLayer();
        //public ImageLayer LayerStars
        //{
        //    get
        //    {
        //        return _layerStars;
        //    }
        //    set
        //    {
        //        if (_layerAlpha == null) _layerAlpha = global::EzTvix.Theme.Default.Empty;
        //        if (value != null)
        //        {
        //            resizeFilter.Width = Int32.Parse(_layerStars.TextZone.Width.ToString());
        //            resizeFilter.Height = Int32.Parse(_layerStars.TextZone.Height.ToString());
        //            resizeFilter.KeepAspectRatio = false;
        //            _layerStars.Image = resizeFilter.ExecuteFilter(value);
        //        }
        //        else
        //            _layerStars.Image = global::EzTvix.Theme.Default.Empty;

        //        //_layerBack = value;
        //    }
        //}

        //protected ImageLayer _layerImageFormat = new ImageLayer();
        //public ImageLayer LayerImageFormat
        //{
        //    get
        //    {
        //        return _layerImageFormat;
        //    }
        //    set
        //    {
        //        if (_layerAlpha == null) _layerAlpha = global::EzTvix.Theme.Default.Empty;
        //        if (value != null)
        //        {
        //            resizeFilter.Width = Int32.Parse(_layerImageFormat.TextZone.Width.ToString());
        //            resizeFilter.Height = Int32.Parse(_layerImageFormat.TextZone.Height.ToString());
        //            resizeFilter.KeepAspectRatio = false;
        //            _layerImageFormat.Image = resizeFilter.ExecuteFilter(value);
        //        }
        //        else
        //            _layerImageFormat.Image = global::EzTvix.Theme.Default.Empty;
        //    }
        //}

        //protected ImageLayer _layerSoundFormat = new ImageLayer();
        //public ImageLayer LayerSoundFormat
        //{
        //    get
        //    {
        //        return _layerSoundFormat;
        //    }
        //    set
        //    {
        //        if (_layerAlpha == null) _layerAlpha = global::EzTvix.Theme.Default.Empty;
        //        if (value != null)
        //        {
        //            resizeFilter.Width = Int32.Parse(_layerSoundFormat.TextZone.Width.ToString());
        //            resizeFilter.Height = Int32.Parse(_layerSoundFormat.TextZone.Height.ToString());
        //            resizeFilter.KeepAspectRatio = false;
        //            _layerSoundFormat.Image = resizeFilter.ExecuteFilter(value);
        //        }
        //        else
        //            _layerSoundFormat.Image = global::EzTvix.Theme.Default.Empty;

        //        //_layerBack = value;
        //    }
        //}

        //protected ImageLayer _layerResolution = new ImageLayer();
        //public ImageLayer LayerResolution
        //{
        //    get
        //    {
        //        return _layerResolution;
        //    }
        //    set
        //    {
        //        if (_layerAlpha == null) _layerAlpha = global::EzTvix.Theme.Default.Empty;
        //        if (value != null)
        //        {
        //            resizeFilter.Width = Int32.Parse(_layerResolution.TextZone.Width.ToString());
        //            resizeFilter.Height = Int32.Parse(_layerResolution.TextZone.Height.ToString());
        //            resizeFilter.KeepAspectRatio = false;
        //            _layerResolution.Image = resizeFilter.ExecuteFilter(value);
        //        }
        //        else
        //            _layerResolution.Image = global::EzTvix.Theme.Default.Empty;
        //    }
        //}

        //protected ImageLayer _layerCover = new ImageLayer();
        /// <summary>
        /// Layer containing the Video Cover
        /// </summary>
        public ImageLayer LayerCover
        {
            get
            {
                return this[BackgroundTemplateItem.Cover];
            }
        }

        //protected ImageLayer _layerBox = new ImageLayer();
        /// <summary>
        /// Layer containing the Video box
        /// </summary>
        public ImageLayer LayerBox
        {
            get
            {
                return this[BackgroundTemplateItem.Box];
            }
        }

        #endregion
        #endregion

        #region *** Constructor ***
        public BackgroundTemplate()
            : base()
        {
            //this.ResetLayers();
            this.Background.Mask = new Rectangle(0, 0, 1280, 720);

        }
        public BackgroundTemplate(Image _newImage)
            : base(_newImage)
        {
            this.Background.Mask = new Rectangle(0, 0, 1280, 720);
        }
        public BackgroundTemplate(String _imageName, String _themeName)
            : base(_imageName, _themeName)
        {
            this.Background.Mask = new Rectangle(0, 0, 1280, 720);
        }
        public BackgroundTemplate(String _imageName, String _AlternateImage, String _themeName)
            : base(_imageName, _AlternateImage, _themeName)
        {
            this.Background.Mask = new Rectangle(0, 0, 1280, 720);
        }
        #endregion

        #region *** Operators ***
        public static implicit operator Image(BackgroundTemplate i)
        {
            return i.Image;
        }
        #endregion

        #region *** Method ***
        public void ResetLayers()
        { 
            this.Layers.Clear();
            this.Layers.Add(BackgroundTemplateItem.BaseImage, this.Foreground);

            this.Layers.Add(BackgroundTemplateItem.Fanart, new ImageLayer(global::EzTvix.Theme.Default.Empty, this.Foreground.Mask));
            this.Layers.Add(BackgroundTemplateItem.Cover, new ImageLayer(global::EzTvix.Theme.Default.Empty, this.Foreground.Mask));
            this.Layers.Add(BackgroundTemplateItem.Box, new ImageLayer(global::EzTvix.Theme.Default.Empty, this.Foreground.Mask));

            this.Layers.Add(BackgroundTemplateItem.Title, new ImageLayer(global::EzTvix.Theme.Default.Empty, this.Foreground.Mask, LayerType.Text));

            this.Layers.Add(BackgroundTemplateItem.Plot, new ImageLayer(global::EzTvix.Theme.Default.Empty, this.Foreground.Mask, LayerType.Text));

            this.Layers.Add(BackgroundTemplateItem.Year, new ImageLayer(global::EzTvix.Theme.Default.Empty, this.Foreground.Mask, LayerType.Text));

            this.Layers.Add(BackgroundTemplateItem.Actors, new ImageLayer(global::EzTvix.Theme.Default.Empty, this.Foreground.Mask, LayerType.Text));
            this.Layers.Add(BackgroundTemplateItem.Runtime, new ImageLayer(global::EzTvix.Theme.Default.Empty, this.Foreground.Mask, LayerType.Text));
            this.Layers.Add(BackgroundTemplateItem.Genre, new ImageLayer(global::EzTvix.Theme.Default.Empty, this.Foreground.Mask, LayerType.Text));
            this.Layers.Add(BackgroundTemplateItem.Rating, new ImageLayer(global::EzTvix.Theme.Default.Empty, this.Foreground.Mask, LayerType.Text));
            this.Layers.Add(BackgroundTemplateItem.Stars, new ImageLayer(global::EzTvix.Theme.Default.Empty, this.Foreground.Mask));
            this.Layers.Add(BackgroundTemplateItem.SoundFormat, new ImageLayer(global::EzTvix.Theme.Default.Empty, this.Foreground.Mask));
            this.Layers.Add(BackgroundTemplateItem.ImageFormat, new ImageLayer(global::EzTvix.Theme.Default.Empty, this.Foreground.Mask));
            this.Layers.Add(BackgroundTemplateItem.Directors, new ImageLayer(global::EzTvix.Theme.Default.Empty, this.Foreground.Mask, LayerType.Text));


            
        }

        public void load(XmlNode backgroundNode)
        {
            FontStyle myFont;
            XmlNode _node;
            string textAlign = "";
            string baseNode = "//Item[@name='" + Name + "']";
            this.ResetLayers();
            
            #region *** Title ***
            DrawLayer(BackgroundTemplateItem.Title, backgroundNode);
            ////_node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='title']/Mask");
            //Layers[BackgroundTemplateItem.Title].Mask = new Rectangle(
            //    this.Foreground.Mask.X,
            //    this.Foreground.Mask.Y,
            //    this.Foreground.Mask.Width,
            //    this.Foreground.Mask.Height);

            //_node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Title']/TextZone");
            //Layers[BackgroundTemplateItem.Title].TextZone = new Rectangle(
            //                Int32.Parse(_node.Attributes["x"].Value),
            //                Int32.Parse(_node.Attributes["y"].Value),
            //                Int32.Parse(_node.Attributes["width"].Value),
            //                Int32.Parse(_node.Attributes["height"].Value));
            //_node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Title']/Text");
            //try
            //{
            //    myFont =
            //    ((Convert.ToBoolean(_node.Attributes["bold"].Value)) ? FontStyle.Bold : FontStyle.Regular)
            //    |
            //    ((Convert.ToBoolean(_node.Attributes["italic"].Value)) ? FontStyle.Italic : FontStyle.Regular)
            //    ;
            //}
            //catch (Exception e)
            //{
            //    myFont = FontStyle.Regular;
            //}

            //Layers[BackgroundTemplateItem.Title].TextFont = new Font(
            //    _node.Attributes["family"].Value,
            //    float.Parse(_node.Attributes["size"].Value),
            //    myFont);

            //Layers[BackgroundTemplateItem.Title].TextColor = Color.FromName(_node.Attributes["color"].Value);

            //try
            //{
            //    textAlign = _node.Attributes["align"].Value;
            //}
            //catch (Exception ex)
            //{
            //    textAlign = "";
            //}
            //switch (textAlign.ToLower())
            //{
            //    case "center":
            //        Layers[BackgroundTemplateItem.Title].TextFormat.Alignment = StringAlignment.Center;
            //        break;
            //    case "right":
            //        Layers[BackgroundTemplateItem.Title].TextFormat.Alignment = StringAlignment.Far;
            //        break;
            //    default:
            //        Layers[BackgroundTemplateItem.Title].TextFormat.Alignment = StringAlignment.Near;
            //        break;
            //}
            
            //try
            //{
            //    _node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Title']/TextSample");
            //    Layers[BackgroundTemplateItem.Title].TextData = _node.InnerText;
            //}
            //catch (Exception ex) { }
            #endregion

            #region *** Plot ***
            DrawLayer(BackgroundTemplateItem.Plot, backgroundNode);
            ////_node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Title']/Mask");
            //Layers[BackgroundTemplateItem.Plot].Mask = new Rectangle(
            //    this.Background.Mask.X,
            //    this.Background.Mask.Y,
            //    this.Background.Mask.Width,
            //    this.Background.Mask.Height);

            //_node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Plot']/TextZone");
            //Layers[BackgroundTemplateItem.Plot].TextZone = new Rectangle(
            //                Int32.Parse(_node.Attributes["x"].Value),
            //                Int32.Parse(_node.Attributes["y"].Value),
            //                Int32.Parse(_node.Attributes["width"].Value),
            //                Int32.Parse(_node.Attributes["height"].Value));
            //_node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Plot']/Text");
            //try
            //{
            //    myFont =
            //    ((Convert.ToBoolean(_node.Attributes["bold"].Value)) ? FontStyle.Bold : FontStyle.Regular)
            //    |
            //    ((Convert.ToBoolean(_node.Attributes["italic"].Value)) ? FontStyle.Italic : FontStyle.Regular)
            //    ;
            //}
            //catch (Exception e)
            //{
            //    myFont = FontStyle.Regular;
            //}

            //Layers[BackgroundTemplateItem.Plot].TextFont = new Font(
            //    _node.Attributes["family"].Value,
            //    float.Parse(_node.Attributes["size"].Value),
            //    myFont);

            //Layers[BackgroundTemplateItem.Plot].TextColor = Color.FromName(_node.Attributes["color"].Value);

            //try
            //{
            //    textAlign = _node.Attributes["align"].Value;
            //}
            //catch (Exception ex)
            //{
            //    textAlign = "";
            //}
            //switch (textAlign.ToLower())
            //{
            //    case "center":
            //        Layers[BackgroundTemplateItem.Plot].TextFormat.Alignment = StringAlignment.Center;
            //        break;
            //    case "right":
            //        Layers[BackgroundTemplateItem.Plot].TextFormat.Alignment = StringAlignment.Far;
            //        break;
            //    default:
            //        Layers[BackgroundTemplateItem.Plot].TextFormat.Alignment = StringAlignment.Near;
            //        break;
            //}

            //try
            //{
            //    _node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Plot']/TextSample");
            //    Layers[BackgroundTemplateItem.Plot].TextData = _node.InnerText;
            //}
            //catch (Exception ex) { }
            #endregion

            #region *** Year ***
            DrawLayer(BackgroundTemplateItem.Year, backgroundNode);
            ////_node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Title']/Mask");
            //Layers[BackgroundTemplateItem.Year].Mask = new Rectangle(
            //    this.Background.Mask.X,
            //    this.Background.Mask.Y,
            //    this.Background.Mask.Width,
            //    this.Background.Mask.Height);

            //_node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Year']/TextZone");
            //Layers[BackgroundTemplateItem.Year].TextZone = new Rectangle(
            //                Int32.Parse(_node.Attributes["x"].Value),
            //                Int32.Parse(_node.Attributes["y"].Value),
            //                Int32.Parse(_node.Attributes["width"].Value),
            //                Int32.Parse(_node.Attributes["height"].Value));
            //_node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Year']/Text");
            //try
            //{
            //    myFont =
            //    ((Convert.ToBoolean(_node.Attributes["bold"].Value)) ? FontStyle.Bold : FontStyle.Regular)
            //    |
            //    ((Convert.ToBoolean(_node.Attributes["italic"].Value)) ? FontStyle.Italic : FontStyle.Regular)
            //    ;
            //}
            //catch (Exception e)
            //{
            //    myFont = FontStyle.Regular;
            //}

            //Layers[BackgroundTemplateItem.Year].TextFont = new Font(
            //    _node.Attributes["family"].Value,
            //    float.Parse(_node.Attributes["size"].Value),
            //    myFont);

            //Layers[BackgroundTemplateItem.Year].TextColor = Color.FromName(_node.Attributes["color"].Value);

            //try
            //{
            //    textAlign = _node.Attributes["align"].Value;
            //}
            //catch (Exception ex)
            //{
            //    textAlign = "";
            //}
            //switch (textAlign.ToLower())
            //{
            //    case "center":
            //        Layers[BackgroundTemplateItem.Year].TextFormat.Alignment = StringAlignment.Center;
            //        break;
            //    case "right":
            //        Layers[BackgroundTemplateItem.Year].TextFormat.Alignment = StringAlignment.Far;
            //        break;
            //    default:
            //        Layers[BackgroundTemplateItem.Year].TextFormat.Alignment = StringAlignment.Near;
            //        break;
            //}


            //try
            //{
            //    _node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Year']/TextSample");
            //    Layers[BackgroundTemplateItem.Year].TextData = _node.InnerText;
            //}
            //catch (Exception ex) { }
            #endregion

            #region *** Actors ***
            DrawLayer(BackgroundTemplateItem.Actors, backgroundNode);
            ////_node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Title']/Mask");
            //Layers[BackgroundTemplateItem.Actors].Mask = new Rectangle(
            //    this.Background.Mask.X,
            //    this.Background.Mask.Y,
            //    this.Background.Mask.Width,
            //    this.Background.Mask.Height);

            //_node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Actors']/TextZone");
            //Layers[BackgroundTemplateItem.Actors].TextZone = new Rectangle(
            //                Int32.Parse(_node.Attributes["x"].Value),
            //                Int32.Parse(_node.Attributes["y"].Value),
            //                Int32.Parse(_node.Attributes["width"].Value),
            //                Int32.Parse(_node.Attributes["height"].Value));
            //_node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Actors']/Text");
            //try
            //{
            //    myFont =
            //    ((Convert.ToBoolean(_node.Attributes["bold"].Value)) ? FontStyle.Bold : FontStyle.Regular)
            //    |
            //    ((Convert.ToBoolean(_node.Attributes["italic"].Value)) ? FontStyle.Italic : FontStyle.Regular)
            //    ;
            //}
            //catch (Exception e)
            //{
            //    myFont = FontStyle.Regular;
            //}

            //Layers[BackgroundTemplateItem.Actors].TextFont = new Font(
            //    _node.Attributes["family"].Value,
            //    float.Parse(_node.Attributes["size"].Value),
            //    myFont);

            //Layers[BackgroundTemplateItem.Actors].TextColor = Color.FromName(_node.Attributes["color"].Value);

            //try
            //{
            //    textAlign = _node.Attributes["align"].Value;
            //}
            //catch (Exception ex)
            //{
            //    textAlign = "";
            //}
            //switch (textAlign.ToLower())
            //{
            //    case "center":
            //        Layers[BackgroundTemplateItem.Actors].TextFormat.Alignment = StringAlignment.Center;
            //        break;
            //    case "right":
            //        Layers[BackgroundTemplateItem.Actors].TextFormat.Alignment = StringAlignment.Far;
            //        break;
            //    default:
            //        Layers[BackgroundTemplateItem.Actors].TextFormat.Alignment = StringAlignment.Near;
            //        break;
            //}

            //try
            //{
            //    _node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Actors']/TextSample");
            //    Layers[BackgroundTemplateItem.Actors].TextData = _node.InnerText;
            //}
            //catch (Exception ex) { }
            #endregion

            #region *** Directors ***
            DrawLayer(BackgroundTemplateItem.Directors, backgroundNode);
            ////_node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Directors']/Mask");
            //Layers[BackgroundTemplateItem.Actors].Mask = new Rectangle(
            //    this.Background.Mask.X,
            //    this.Background.Mask.Y,
            //    this.Background.Mask.Width,
            //    this.Background.Mask.Height);

            //_node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Directors']/TextZone");
            //Layers[BackgroundTemplateItem.Directors].TextZone = new Rectangle(
            //                Int32.Parse(_node.Attributes["x"].Value),
            //                Int32.Parse(_node.Attributes["y"].Value),
            //                Int32.Parse(_node.Attributes["width"].Value),
            //                Int32.Parse(_node.Attributes["height"].Value));
            //_node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Directors']/Text");
            //try
            //{
            //    myFont =
            //    ((Convert.ToBoolean(_node.Attributes["bold"].Value)) ? FontStyle.Bold : FontStyle.Regular)
            //    |
            //    ((Convert.ToBoolean(_node.Attributes["italic"].Value)) ? FontStyle.Italic : FontStyle.Regular)
            //    ;
            //}
            //catch (Exception e)
            //{
            //    myFont = FontStyle.Regular;
            //}

            //Layers[BackgroundTemplateItem.Directors].TextFont = new Font(
            //    _node.Attributes["family"].Value,
            //    float.Parse(_node.Attributes["size"].Value),
            //    myFont);

            //Layers[BackgroundTemplateItem.Directors].TextColor = Color.FromName(_node.Attributes["color"].Value);

            //try
            //{
            //    textAlign = _node.Attributes["align"].Value;
            //}
            //catch (Exception ex)
            //{
            //    textAlign = "";
            //}
            //switch (textAlign.ToLower())
            //{
            //    case "center":
            //        Layers[BackgroundTemplateItem.Directors].TextFormat.Alignment = StringAlignment.Center;
            //        break;
            //    case "right":
            //        Layers[BackgroundTemplateItem.Directors].TextFormat.Alignment = StringAlignment.Far;
            //        break;
            //    default:
            //        Layers[BackgroundTemplateItem.Directors].TextFormat.Alignment = StringAlignment.Near;
            //        break;
            //}

            //try
            //{
            //    _node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Directors']/TextSample");
            //    Layers[BackgroundTemplateItem.Directors].TextData = _node.InnerText;
            //}
            //catch (Exception ex) { }
            #endregion
            
            #region *** Runtime ***
            DrawLayer(BackgroundTemplateItem.Runtime, backgroundNode);
            ////_node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Title']/Mask");
            //Layers[BackgroundTemplateItem.Runtime].Mask = new Rectangle(
            //    this.Background.Mask.X,
            //    this.Background.Mask.Y,
            //    this.Background.Mask.Width,
            //    this.Background.Mask.Height);

            //_node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Runtime']/TextZone");
            //Layers[BackgroundTemplateItem.Runtime].TextZone = new Rectangle(
            //                Int32.Parse(_node.Attributes["x"].Value),
            //                Int32.Parse(_node.Attributes["y"].Value),
            //                Int32.Parse(_node.Attributes["width"].Value),
            //                Int32.Parse(_node.Attributes["height"].Value));
            //_node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Runtime']/Text");
            //try
            //{
            //    myFont =
            //    ((Convert.ToBoolean(_node.Attributes["bold"].Value)) ? FontStyle.Bold : FontStyle.Regular)
            //    |
            //    ((Convert.ToBoolean(_node.Attributes["italic"].Value)) ? FontStyle.Italic : FontStyle.Regular)
            //    ;
            //}
            //catch (Exception e)
            //{
            //    myFont = FontStyle.Regular;
            //}

            //Layers[BackgroundTemplateItem.Runtime].TextFont = new Font(
            //    _node.Attributes["family"].Value,
            //    float.Parse(_node.Attributes["size"].Value),
            //    myFont);

            //Layers[BackgroundTemplateItem.Runtime].TextColor = Color.FromName(_node.Attributes["color"].Value);

            //try
            //{
            //    textAlign = _node.Attributes["align"].Value;
            //}
            //catch (Exception ex)
            //{
            //    textAlign = "";
            //}
            //switch (textAlign.ToLower())
            //{
            //    case "center":
            //        Layers[BackgroundTemplateItem.Runtime].TextFormat.Alignment = StringAlignment.Center;
            //        break;
            //    case "right":
            //        Layers[BackgroundTemplateItem.Runtime].TextFormat.Alignment = StringAlignment.Far;
            //        break;
            //    default:
            //        Layers[BackgroundTemplateItem.Runtime].TextFormat.Alignment = StringAlignment.Near;
            //        break;
            //}

            //try
            //{
            //    _node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Runtime']/TextSample");
            //    Layers[BackgroundTemplateItem.Runtime].TextData = _node.InnerText;
            //}
            //catch (Exception ex) { }
            #endregion

            #region *** Genre ***
            DrawLayer(BackgroundTemplateItem.Genre, backgroundNode);
            
            ////_node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Title']/Mask");
            //Layers[BackgroundTemplateItem.Genre].Mask = new Rectangle(
            //    this.Background.Mask.X,
            //    this.Background.Mask.Y,
            //    this.Background.Mask.Width,
            //    this.Background.Mask.Height); 
            
            //_node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Genre']/TextZone");
            //Layers[BackgroundTemplateItem.Genre].TextZone = new Rectangle(
            //                Int32.Parse(_node.Attributes["x"].Value),
            //                Int32.Parse(_node.Attributes["y"].Value),
            //                Int32.Parse(_node.Attributes["width"].Value),
            //                Int32.Parse(_node.Attributes["height"].Value));
            //_node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Genre']/Text");
            //try
            //{
            //    myFont =
            //    ((Convert.ToBoolean(_node.Attributes["bold"].Value)) ? FontStyle.Bold : FontStyle.Regular)
            //    |
            //    ((Convert.ToBoolean(_node.Attributes["italic"].Value)) ? FontStyle.Italic : FontStyle.Regular)
            //    ;
            //}
            //catch (Exception e)
            //{
            //    myFont = FontStyle.Regular;
            //}

            //Layers[BackgroundTemplateItem.Genre].TextFont = new Font(
            //    _node.Attributes["family"].Value,
            //    float.Parse(_node.Attributes["size"].Value),
            //    myFont);

            //Layers[BackgroundTemplateItem.Genre].TextColor = Color.FromName(_node.Attributes["color"].Value);

            //try
            //{
            //    textAlign = _node.Attributes["align"].Value;
            //}
            //catch (Exception ex)
            //{
            //    textAlign = "";
            //}
            //switch (textAlign.ToLower())
            //{
            //    case "center":
            //        Layers[BackgroundTemplateItem.Genre].TextFormat.Alignment = StringAlignment.Center;
            //        break;
            //    case "right":
            //        Layers[BackgroundTemplateItem.Genre].TextFormat.Alignment = StringAlignment.Far;
            //        break;
            //    default:
            //        Layers[BackgroundTemplateItem.Genre].TextFormat.Alignment = StringAlignment.Near;
            //        break;
            //}

            //try
            //{
            //    _node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Genre']/TextSample");
            //    Layers[BackgroundTemplateItem.Genre].TextData = _node.InnerText;
            //}
            //catch (Exception ex) { }
            #endregion

            #region *** Rating ***
            DrawLayer(BackgroundTemplateItem.Rating, backgroundNode);
            ////_node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Title']/Mask");
            //Layers[BackgroundTemplateItem.Rating].Mask = new Rectangle(
            //    this.Background.Mask.X,
            //    this.Background.Mask.Y,
            //    this.Background.Mask.Width,
            //    this.Background.Mask.Height);

            //_node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Rating']/TextZone");
            //Layers[BackgroundTemplateItem.Rating].TextZone = new Rectangle(
            //                Int32.Parse(_node.Attributes["x"].Value),
            //                Int32.Parse(_node.Attributes["y"].Value),
            //                Int32.Parse(_node.Attributes["width"].Value),
            //                Int32.Parse(_node.Attributes["height"].Value));
            //_node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Rating']/Text");
            //try
            //{
            //    myFont =
            //    ((Convert.ToBoolean(_node.Attributes["bold"].Value)) ? FontStyle.Bold : FontStyle.Regular)
            //    |
            //    ((Convert.ToBoolean(_node.Attributes["italic"].Value)) ? FontStyle.Italic : FontStyle.Regular)
            //    ;
            //}
            //catch (Exception e)
            //{
            //    myFont = FontStyle.Regular;
            //}

            //Layers[BackgroundTemplateItem.Rating].TextFont = new Font(
            //    _node.Attributes["family"].Value,
            //    float.Parse(_node.Attributes["size"].Value),
            //    myFont);

            //Layers[BackgroundTemplateItem.Rating].TextColor = Color.FromName(_node.Attributes["color"].Value);

            //try
            //{
            //    textAlign = _node.Attributes["align"].Value;
            //}
            //catch (Exception ex)
            //{
            //    textAlign = "";
            //}
            //switch (textAlign.ToLower())
            //{
            //    case "center":
            //        Layers[BackgroundTemplateItem.Rating].TextFormat.Alignment = StringAlignment.Center;
            //        break;
            //    case "right":
            //        Layers[BackgroundTemplateItem.Rating].TextFormat.Alignment = StringAlignment.Far;
            //        break;
            //    default:
            //        Layers[BackgroundTemplateItem.Rating].TextFormat.Alignment = StringAlignment.Near;
            //        break;
            //}

            //try
            //{
            //    _node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Rating']/TextSample");
            //    Layers[BackgroundTemplateItem.Rating].TextData = _node.InnerText;
            //}
            //catch (Exception ex) { }
            #endregion

            #region *** Stars ***
            _node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Stars']/Mask");
            Layers[BackgroundTemplateItem.Stars].Mask = new Rectangle(
                Int32.Parse(_node.Attributes["x"].Value),
                Int32.Parse(_node.Attributes["y"].Value),
                Int32.Parse(_node.Attributes["width"].Value),
                Int32.Parse(_node.Attributes["height"].Value));
            #endregion

            #region *** SoundFormat ***
            _node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='SoundFormat']/Mask");
            Layers[BackgroundTemplateItem.SoundFormat].Mask = new Rectangle(
                Int32.Parse(_node.Attributes["x"].Value),
                Int32.Parse(_node.Attributes["y"].Value),
                Int32.Parse(_node.Attributes["width"].Value),
                Int32.Parse(_node.Attributes["height"].Value));
            #endregion

            #region *** ImageFormat ***
            _node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='ImageFormat']/Mask");
            Layers[BackgroundTemplateItem.ImageFormat].Mask = new Rectangle(
                Int32.Parse(_node.Attributes["x"].Value),
                Int32.Parse(_node.Attributes["y"].Value),
                Int32.Parse(_node.Attributes["width"].Value),
                Int32.Parse(_node.Attributes["height"].Value));
            #endregion

            #region *** VideoCover ***
            _node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Cover']/Mask");
            this.Layers[BackgroundTemplateItem.Cover].Mask = new Rectangle(
                Int32.Parse(_node.Attributes["x"].Value),
                Int32.Parse(_node.Attributes["y"].Value),
                Int32.Parse(_node.Attributes["width"].Value),
                Int32.Parse(_node.Attributes["height"].Value));
            #endregion

            #region *** VideoBox ***
            _node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Box']/Mask");
            this.Layers[BackgroundTemplateItem.Box].Mask = new Rectangle(
                Int32.Parse(_node.Attributes["x"].Value),
                Int32.Parse(_node.Attributes["y"].Value),
                Int32.Parse(_node.Attributes["width"].Value),
                Int32.Parse(_node.Attributes["height"].Value));
            this.Layers[BackgroundTemplateItem.Box].Name = "Box";

            string imagePath = TvixInfo.ThemeFolder + @"\" + this._Theme + @"\" + this.Layers[BackgroundTemplateItem.Box].Name + ".png";
            try
            {
                this.Layers[BackgroundTemplateItem.Box].Image = this.FromStream(imagePath);
            }
            catch (Exception ex) { }

            #endregion

            #region *** Fanart ***
            _node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Fanart']/Mask");
            this.Layers[BackgroundTemplateItem.Fanart].Mask = new Rectangle(
                Int32.Parse(_node.Attributes["x"].Value),
                Int32.Parse(_node.Attributes["y"].Value),
                Int32.Parse(_node.Attributes["width"].Value),
                Int32.Parse(_node.Attributes["height"].Value));
            this.Layers[BackgroundTemplateItem.Fanart].Name = "Fanart";
            #endregion

            //
        }

        public void DrawLayer(BackgroundTemplateItem layerItem, XmlNode backgroundNode)
        {
            FontStyle myFont;
            XmlNode _node;
            string textAlign = "";
            string baseNode = "//Item[@name='" + Name + "']";

            //_node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='title']/Mask");
            Layers[layerItem].Mask = new Rectangle(
                this.Foreground.Mask.X,
                this.Foreground.Mask.Y,
                this.Foreground.Mask.Width,
                this.Foreground.Mask.Height);

//            _node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='Title']/TextZone");
            _node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='" + layerItem + "']/TextZone");
            Layers[layerItem].TextZone = new Rectangle(
                            Int32.Parse(_node.Attributes["x"].Value),
                            Int32.Parse(_node.Attributes["y"].Value),
                            Int32.Parse(_node.Attributes["width"].Value),
                            Int32.Parse(_node.Attributes["height"].Value));
            _node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='" + layerItem + "']/Text");
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

            Layers[layerItem].TextFont = new Font(
                _node.Attributes["family"].Value,
                float.Parse(_node.Attributes["size"].Value),
                myFont);

            Layers[layerItem].TextColor = Color.FromName(_node.Attributes["color"].Value);

            try
            {
                textAlign = _node.Attributes["align"].Value;
            }
            catch (Exception ex)
            {
                textAlign = "";
            }
            switch (textAlign.ToLower())
            {
                case "center":
                    Layers[layerItem].TextFormat.Alignment = StringAlignment.Center;
                    break;
                case "right":
                    Layers[layerItem].TextFormat.Alignment = StringAlignment.Far;
                    break;
                default:
                    Layers[layerItem].TextFormat.Alignment = StringAlignment.Near;
                    break;
            }

            try
            {
                _node = backgroundNode.SelectSingleNode(baseNode + "/Layer[@name='" + layerItem + "']/TextSample");
                Layers[layerItem].TextData = _node.InnerText;
            }
            catch (Exception ex) { }
        }

        public void DrawText()
        {
            foreach (KeyValuePair<BackgroundTemplateItem, ImageLayer> kvp in this.Layers)
            {
                if (kvp.Value.TextData.Trim() != "")
                    kvp.Value.DrawText();
            }
        }

        public XmlNode save()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(@"<Item name='" + this.Name + "'></Item>");
            XmlElement root = doc.DocumentElement;
            XmlNode node;
            foreach (KeyValuePair<BackgroundTemplateItem, ImageLayer> kvp in this.Layers)
            {
                kvp.Value.Name = kvp.Key.ToString();
                node = kvp.Value.Save();
                root.AppendChild(doc.ImportNode(node, true));
                
            }
            
            String imagePath = TvixInfo.ThemeFolder + @"\" + this._Theme + @"\" + this.Name + "." + this.Extension;
            Bitmap bmp = new Bitmap(this.Foreground.Image);
 
            if (this.Name != "" && this.Name != null)
            {
                if (File.Exists(imagePath))
                {
                    try
                    {
                        File.Delete(imagePath);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                #region // using Image.save cause a lock problem (file in use) when all the pictures are opened with image.fromfile
                 // error description
                // image.fromfile replaced by 
                // private Image FromStream(string _imagePath)
                // {
                //    FileStream fs = new FileStream(_imagePath, FileMode.Open);
                //    Image imgPhoto = Image.FromStream(fs);
                //    fs.Close();
                //    fs.Dispose();
                //    return imgPhoto;
                // }
                #endregion

                bmp.Save(imagePath, this._extension);
                bmp.Dispose();
                bmp = null;
            }

            imagePath = TvixInfo.ThemeFolder + @"\" + this._Theme + @"\" + this.Layers[BackgroundTemplateItem.Box].Name + ".png";
            bmp = new Bitmap(this.Layers[BackgroundTemplateItem.Box].Image);

            if (File.Exists(imagePath))
            {
                try
                {
                    File.Delete(imagePath);
                }
                catch (Exception ex)
                {
                }
            }

            bmp.Save(imagePath, ImageFormat.Png);
            bmp.Dispose();
            bmp = null;
          
            return root;
        
        }
        public XmlNode save2()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(@"<Item name='" + this.Name + "'></Item>");

            XmlElement root = doc.DocumentElement;
            XmlNode ItemNode;
            XmlAttribute itemName;
            XmlNode maskNode;
            XmlAttribute maskX, maskY, maskWidth, maskHeight;
            XmlNode textFont;
            XmlAttribute fontFamily, fontSize, fontBold, fontItalic, fontColor;
            XmlNode textNode;
            XmlAttribute textX, textY, textWidth, textHeight;

            #region *** Title ***
            ItemNode = doc.CreateNode(XmlNodeType.Element, "Layer", "");
            itemName = doc.CreateAttribute("name"); itemName.Value = "Title";
            ItemNode.Attributes.Append(itemName);

            #region --- Mask ---
            if (Layers[BackgroundTemplateItem.Title].Mask != new Rectangle(0, 0, this.Width, this.Height))
            {
                maskNode = doc.CreateNode(XmlNodeType.Element, "Mask", "");
                maskX = doc.CreateAttribute("x"); maskX.Value = Layers[BackgroundTemplateItem.Title].Mask.X.ToString();
                maskY = doc.CreateAttribute("y"); maskY.Value = Layers[BackgroundTemplateItem.Title].Mask.Y.ToString();
                maskWidth = doc.CreateAttribute("width"); maskWidth.Value = Layers[BackgroundTemplateItem.Title].Mask.Width.ToString();
                maskHeight = doc.CreateAttribute("height"); maskHeight.Value = Layers[BackgroundTemplateItem.Title].Mask.Height.ToString();
                maskNode.Attributes.Append(maskX);
                maskNode.Attributes.Append(maskY);
                maskNode.Attributes.Append(maskWidth);
                maskNode.Attributes.Append(maskHeight);
                ItemNode.AppendChild(maskNode);
            }
            #endregion
            #region --- Text ---
            textFont = doc.CreateNode(XmlNodeType.Element, "Text", "");
            fontFamily = doc.CreateAttribute("family"); fontFamily.Value = Layers[BackgroundTemplateItem.Title].TextFont.FontFamily.Name.ToString();
            fontSize = doc.CreateAttribute("size"); fontSize.Value = Layers[BackgroundTemplateItem.Title].TextFont.Size.ToString();
            fontBold = doc.CreateAttribute("bold"); fontBold.Value = (Layers[BackgroundTemplateItem.Title].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontItalic = doc.CreateAttribute("italic"); fontItalic.Value = (Layers[BackgroundTemplateItem.Title].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontColor = doc.CreateAttribute("color"); fontColor.Value = Layers[BackgroundTemplateItem.Title].TextColor.Name;
            textFont.Attributes.Append(fontFamily);
            textFont.Attributes.Append(fontSize);
            textFont.Attributes.Append(fontBold);
            textFont.Attributes.Append(fontItalic);
            textFont.Attributes.Append(fontColor);
            ItemNode.AppendChild(textFont);

            if (Layers[BackgroundTemplateItem.Title].TextZone != new Rectangle(0, 0, this.Width, this.Height))
            {
                textNode = doc.CreateNode(XmlNodeType.Element, "TextZone", "");
                textX = doc.CreateAttribute("x"); textX.Value = Layers[BackgroundTemplateItem.Title].TextZone.X.ToString();
                textY = doc.CreateAttribute("y"); textY.Value = Layers[BackgroundTemplateItem.Title].TextZone.Y.ToString();
                textWidth = doc.CreateAttribute("width"); textWidth.Value = Layers[BackgroundTemplateItem.Title].TextZone.Width.ToString();
                textHeight = doc.CreateAttribute("height"); textHeight.Value = Layers[BackgroundTemplateItem.Title].TextZone.Height.ToString();
                textNode.Attributes.Append(textX);
                textNode.Attributes.Append(textY);
                textNode.Attributes.Append(textWidth);
                textNode.Attributes.Append(textHeight);
                ItemNode.AppendChild(textNode);
            }
            root.AppendChild(ItemNode);
            #endregion
            #region ---Layers---
            //XmlNode layerNode = doc.CreateNode(XmlNodeType.Element, "Layers", "");

            //XmlAttribute imgLayerBack = doc.CreateAttribute("back");
            //imgLayerBack.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerBack);

            //XmlAttribute imgLayerFront = doc.CreateAttribute("front");
            //imgLayerFront.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerFront);

            //XmlAttribute imgLayerText = doc.CreateAttribute("text");
            //imgLayerText.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerText);

            //ItemNode.AppendChild(layerNode);
            #endregion
            #endregion

            #region *** Plot ***
            ItemNode = doc.CreateNode(XmlNodeType.Element, "Layer", "");
            itemName = doc.CreateAttribute("name"); itemName.Value = "Plot";
            ItemNode.Attributes.Append(itemName);

            #region --- Mask ---
            if (Layers[BackgroundTemplateItem.Plot].Mask != new Rectangle(0, 0, this.Width, this.Height))
            {
                maskNode = doc.CreateNode(XmlNodeType.Element, "Mask", "");
                maskX = doc.CreateAttribute("x"); maskX.Value = Layers[BackgroundTemplateItem.Plot].Mask.X.ToString();
                maskY = doc.CreateAttribute("y"); maskY.Value = Layers[BackgroundTemplateItem.Plot].Mask.Y.ToString();
                maskWidth = doc.CreateAttribute("width"); maskWidth.Value = Layers[BackgroundTemplateItem.Plot].Mask.Width.ToString();
                maskHeight = doc.CreateAttribute("height"); maskHeight.Value = Layers[BackgroundTemplateItem.Plot].Mask.Height.ToString();
                maskNode.Attributes.Append(maskX);
                maskNode.Attributes.Append(maskY);
                maskNode.Attributes.Append(maskWidth);
                maskNode.Attributes.Append(maskHeight);
                ItemNode.AppendChild(maskNode);
            }
            #endregion
            #region --- Text ---
            textFont = doc.CreateNode(XmlNodeType.Element, "Text", "");
            fontFamily = doc.CreateAttribute("family"); fontFamily.Value = Layers[BackgroundTemplateItem.Plot].TextFont.FontFamily.Name.ToString();
            fontSize = doc.CreateAttribute("size"); fontSize.Value = Layers[BackgroundTemplateItem.Plot].TextFont.Size.ToString();
            fontBold = doc.CreateAttribute("bold"); fontBold.Value = (Layers[BackgroundTemplateItem.Plot].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontItalic = doc.CreateAttribute("italic"); fontItalic.Value = (Layers[BackgroundTemplateItem.Plot].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontColor = doc.CreateAttribute("color"); fontColor.Value = Layers[BackgroundTemplateItem.Plot].TextColor.Name;
            textFont.Attributes.Append(fontFamily);
            textFont.Attributes.Append(fontSize);
            textFont.Attributes.Append(fontBold);
            textFont.Attributes.Append(fontItalic);
            textFont.Attributes.Append(fontColor);
            ItemNode.AppendChild(textFont);

            if (Layers[BackgroundTemplateItem.Plot].TextZone != new Rectangle(0, 0, this.Width, this.Height))
            {
                textNode = doc.CreateNode(XmlNodeType.Element, "TextZone", "");
                textX = doc.CreateAttribute("x"); textX.Value = Layers[BackgroundTemplateItem.Plot].TextZone.X.ToString();
                textY = doc.CreateAttribute("y"); textY.Value = Layers[BackgroundTemplateItem.Plot].TextZone.Y.ToString();
                textWidth = doc.CreateAttribute("width"); textWidth.Value = Layers[BackgroundTemplateItem.Plot].TextZone.Width.ToString();
                textHeight = doc.CreateAttribute("height"); textHeight.Value = Layers[BackgroundTemplateItem.Plot].TextZone.Height.ToString();
                textNode.Attributes.Append(textX);
                textNode.Attributes.Append(textY);
                textNode.Attributes.Append(textWidth);
                textNode.Attributes.Append(textHeight);
                ItemNode.AppendChild(textNode);
            }
            root.AppendChild(ItemNode);
            #endregion
            #region ---Layers---
            //XmlNode layerNode = doc.CreateNode(XmlNodeType.Element, "Layers", "");

            //XmlAttribute imgLayerBack = doc.CreateAttribute("back");
            //imgLayerBack.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerBack);

            //XmlAttribute imgLayerFront = doc.CreateAttribute("front");
            //imgLayerFront.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerFront);

            //XmlAttribute imgLayerText = doc.CreateAttribute("text");
            //imgLayerText.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerText);

            //ItemNode.AppendChild(layerNode);
            #endregion
            #endregion

            #region *** Year ***
            ItemNode = doc.CreateNode(XmlNodeType.Element, "Layer", "");
            itemName = doc.CreateAttribute("name"); itemName.Value = "Year";
            ItemNode.Attributes.Append(itemName);

            #region --- Mask ---
            if (Layers[BackgroundTemplateItem.Year].Mask != new Rectangle(0, 0, this.Width, this.Height))
            {
                maskNode = doc.CreateNode(XmlNodeType.Element, "Mask", "");
                maskX = doc.CreateAttribute("x"); maskX.Value = Layers[BackgroundTemplateItem.Year].Mask.X.ToString();
                maskY = doc.CreateAttribute("y"); maskY.Value = Layers[BackgroundTemplateItem.Year].Mask.Y.ToString();
                maskWidth = doc.CreateAttribute("width"); maskWidth.Value = Layers[BackgroundTemplateItem.Year].Mask.Width.ToString();
                maskHeight = doc.CreateAttribute("height"); maskHeight.Value = Layers[BackgroundTemplateItem.Year].Mask.Height.ToString();
                maskNode.Attributes.Append(maskX);
                maskNode.Attributes.Append(maskY);
                maskNode.Attributes.Append(maskWidth);
                maskNode.Attributes.Append(maskHeight);
                ItemNode.AppendChild(maskNode);
            }
            #endregion
            #region --- Text ---
            textFont = doc.CreateNode(XmlNodeType.Element, "Text", "");
            fontFamily = doc.CreateAttribute("family"); fontFamily.Value = Layers[BackgroundTemplateItem.Year].TextFont.FontFamily.Name.ToString();
            fontSize = doc.CreateAttribute("size"); fontSize.Value = Layers[BackgroundTemplateItem.Year].TextFont.Size.ToString();
            fontBold = doc.CreateAttribute("bold"); fontBold.Value = (Layers[BackgroundTemplateItem.Year].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontItalic = doc.CreateAttribute("italic"); fontItalic.Value = (Layers[BackgroundTemplateItem.Year].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontColor = doc.CreateAttribute("color"); fontColor.Value = Layers[BackgroundTemplateItem.Year].TextColor.Name;
            textFont.Attributes.Append(fontFamily);
            textFont.Attributes.Append(fontSize);
            textFont.Attributes.Append(fontBold);
            textFont.Attributes.Append(fontItalic);
            textFont.Attributes.Append(fontColor);
            ItemNode.AppendChild(textFont);

            if (Layers[BackgroundTemplateItem.Year].TextZone != new Rectangle(0, 0, this.Width, this.Height))
            {
                textNode = doc.CreateNode(XmlNodeType.Element, "TextZone", "");
                textX = doc.CreateAttribute("x"); textX.Value = Layers[BackgroundTemplateItem.Year].TextZone.X.ToString();
                textY = doc.CreateAttribute("y"); textY.Value = Layers[BackgroundTemplateItem.Year].TextZone.Y.ToString();
                textWidth = doc.CreateAttribute("width"); textWidth.Value = Layers[BackgroundTemplateItem.Year].TextZone.Width.ToString();
                textHeight = doc.CreateAttribute("height"); textHeight.Value = Layers[BackgroundTemplateItem.Year].TextZone.Height.ToString();
                textNode.Attributes.Append(textX);
                textNode.Attributes.Append(textY);
                textNode.Attributes.Append(textWidth);
                textNode.Attributes.Append(textHeight);
                ItemNode.AppendChild(textNode);
            }
            root.AppendChild(ItemNode);
            #endregion
            #region ---Layers---
            //XmlNode layerNode = doc.CreateNode(XmlNodeType.Element, "Layers", "");

            //XmlAttribute imgLayerBack = doc.CreateAttribute("back");
            //imgLayerBack.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerBack);

            //XmlAttribute imgLayerFront = doc.CreateAttribute("front");
            //imgLayerFront.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerFront);

            //XmlAttribute imgLayerText = doc.CreateAttribute("text");
            //imgLayerText.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerText);

            //ItemNode.AppendChild(layerNode);
            #endregion
            #endregion

            #region *** Actors ***
            ItemNode = doc.CreateNode(XmlNodeType.Element, "Layer", "");
            itemName = doc.CreateAttribute("name"); itemName.Value = "Actors";
            ItemNode.Attributes.Append(itemName);

            #region --- Mask ---
            if (Layers[BackgroundTemplateItem.Actors].Mask != new Rectangle(0, 0, this.Width, this.Height))
            {
                maskNode = doc.CreateNode(XmlNodeType.Element, "Mask", "");
                maskX = doc.CreateAttribute("x"); maskX.Value = Layers[BackgroundTemplateItem.Actors].Mask.X.ToString();
                maskY = doc.CreateAttribute("y"); maskY.Value = Layers[BackgroundTemplateItem.Actors].Mask.Y.ToString();
                maskWidth = doc.CreateAttribute("width"); maskWidth.Value = Layers[BackgroundTemplateItem.Actors].Mask.Width.ToString();
                maskHeight = doc.CreateAttribute("height"); maskHeight.Value = Layers[BackgroundTemplateItem.Actors].Mask.Height.ToString();
                maskNode.Attributes.Append(maskX);
                maskNode.Attributes.Append(maskY);
                maskNode.Attributes.Append(maskWidth);
                maskNode.Attributes.Append(maskHeight);
                ItemNode.AppendChild(maskNode);
            }
            #endregion
            #region --- Text ---
            textFont = doc.CreateNode(XmlNodeType.Element, "Text", "");
            fontFamily = doc.CreateAttribute("family"); fontFamily.Value = Layers[BackgroundTemplateItem.Actors].TextFont.FontFamily.Name.ToString();
            fontSize = doc.CreateAttribute("size"); fontSize.Value = Layers[BackgroundTemplateItem.Actors].TextFont.Size.ToString();
            fontBold = doc.CreateAttribute("bold"); fontBold.Value = (Layers[BackgroundTemplateItem.Actors].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontItalic = doc.CreateAttribute("italic"); fontItalic.Value = (Layers[BackgroundTemplateItem.Actors].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontColor = doc.CreateAttribute("color"); fontColor.Value = Layers[BackgroundTemplateItem.Actors].TextColor.Name;
            textFont.Attributes.Append(fontFamily);
            textFont.Attributes.Append(fontSize);
            textFont.Attributes.Append(fontBold);
            textFont.Attributes.Append(fontItalic);
            textFont.Attributes.Append(fontColor);
            ItemNode.AppendChild(textFont);

            if (Layers[BackgroundTemplateItem.Actors].TextZone != new Rectangle(0, 0, this.Width, this.Height))
            {
                textNode = doc.CreateNode(XmlNodeType.Element, "TextZone", "");
                textX = doc.CreateAttribute("x"); textX.Value = Layers[BackgroundTemplateItem.Actors].TextZone.X.ToString();
                textY = doc.CreateAttribute("y"); textY.Value = Layers[BackgroundTemplateItem.Actors].TextZone.Y.ToString();
                textWidth = doc.CreateAttribute("width"); textWidth.Value = Layers[BackgroundTemplateItem.Actors].TextZone.Width.ToString();
                textHeight = doc.CreateAttribute("height"); textHeight.Value = Layers[BackgroundTemplateItem.Actors].TextZone.Height.ToString();
                textNode.Attributes.Append(textX);
                textNode.Attributes.Append(textY);
                textNode.Attributes.Append(textWidth);
                textNode.Attributes.Append(textHeight);
                ItemNode.AppendChild(textNode);
            }
            root.AppendChild(ItemNode);
            #endregion
            #region ---Layers---
            //XmlNode layerNode = doc.CreateNode(XmlNodeType.Element, "Layers", "");

            //XmlAttribute imgLayerBack = doc.CreateAttribute("back");
            //imgLayerBack.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerBack);

            //XmlAttribute imgLayerFront = doc.CreateAttribute("front");
            //imgLayerFront.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerFront);

            //XmlAttribute imgLayerText = doc.CreateAttribute("text");
            //imgLayerText.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerText);

            //ItemNode.AppendChild(layerNode);
            #endregion
            #endregion

            #region *** Directors ***
            ItemNode = doc.CreateNode(XmlNodeType.Element, "Layer", "");
            itemName = doc.CreateAttribute("name"); itemName.Value = "Directors";
            ItemNode.Attributes.Append(itemName);

            #region --- Mask ---
            if (Layers[BackgroundTemplateItem.Directors].Mask != new Rectangle(0, 0, this.Width, this.Height))
            {
                maskNode = doc.CreateNode(XmlNodeType.Element, "Mask", "");
                maskX = doc.CreateAttribute("x"); maskX.Value = Layers[BackgroundTemplateItem.Directors].Mask.X.ToString();
                maskY = doc.CreateAttribute("y"); maskY.Value = Layers[BackgroundTemplateItem.Directors].Mask.Y.ToString();
                maskWidth = doc.CreateAttribute("width"); maskWidth.Value = Layers[BackgroundTemplateItem.Directors].Mask.Width.ToString();
                maskHeight = doc.CreateAttribute("height"); maskHeight.Value = Layers[BackgroundTemplateItem.Directors].Mask.Height.ToString();
                maskNode.Attributes.Append(maskX);
                maskNode.Attributes.Append(maskY);
                maskNode.Attributes.Append(maskWidth);
                maskNode.Attributes.Append(maskHeight);
                ItemNode.AppendChild(maskNode);
            }
            #endregion
            #region --- Text ---
            textFont = doc.CreateNode(XmlNodeType.Element, "Text", "");
            fontFamily = doc.CreateAttribute("family"); fontFamily.Value = Layers[BackgroundTemplateItem.Directors].TextFont.FontFamily.Name.ToString();
            fontSize = doc.CreateAttribute("size"); fontSize.Value = Layers[BackgroundTemplateItem.Directors].TextFont.Size.ToString();
            fontBold = doc.CreateAttribute("bold"); fontBold.Value = (Layers[BackgroundTemplateItem.Directors].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontItalic = doc.CreateAttribute("italic"); fontItalic.Value = (Layers[BackgroundTemplateItem.Directors].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontColor = doc.CreateAttribute("color"); fontColor.Value = Layers[BackgroundTemplateItem.Directors].TextColor.Name;
            textFont.Attributes.Append(fontFamily);
            textFont.Attributes.Append(fontSize);
            textFont.Attributes.Append(fontBold);
            textFont.Attributes.Append(fontItalic);
            textFont.Attributes.Append(fontColor);
            ItemNode.AppendChild(textFont);

            if (Layers[BackgroundTemplateItem.Directors].TextZone != new Rectangle(0, 0, this.Width, this.Height))
            {
                textNode = doc.CreateNode(XmlNodeType.Element, "TextZone", "");
                textX = doc.CreateAttribute("x"); textX.Value = Layers[BackgroundTemplateItem.Directors].TextZone.X.ToString();
                textY = doc.CreateAttribute("y"); textY.Value = Layers[BackgroundTemplateItem.Directors].TextZone.Y.ToString();
                textWidth = doc.CreateAttribute("width"); textWidth.Value = Layers[BackgroundTemplateItem.Directors].TextZone.Width.ToString();
                textHeight = doc.CreateAttribute("height"); textHeight.Value = Layers[BackgroundTemplateItem.Directors].TextZone.Height.ToString();
                textNode.Attributes.Append(textX);
                textNode.Attributes.Append(textY);
                textNode.Attributes.Append(textWidth);
                textNode.Attributes.Append(textHeight);
                ItemNode.AppendChild(textNode);
            }
            root.AppendChild(ItemNode);
            #endregion
            #region ---Layers---
            //XmlNode layerNode = doc.CreateNode(XmlNodeType.Element, "Layers", "");

            //XmlAttribute imgLayerBack = doc.CreateAttribute("back");
            //imgLayerBack.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerBack);

            //XmlAttribute imgLayerFront = doc.CreateAttribute("front");
            //imgLayerFront.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerFront);

            //XmlAttribute imgLayerText = doc.CreateAttribute("text");
            //imgLayerText.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerText);

            //ItemNode.AppendChild(layerNode);
            #endregion
            #endregion

            #region *** Runtime ***
            ItemNode = doc.CreateNode(XmlNodeType.Element, "Layer", "");
            itemName = doc.CreateAttribute("name"); itemName.Value = "Runtime";
            ItemNode.Attributes.Append(itemName);

            #region --- Mask ---
            if (Layers[BackgroundTemplateItem.Runtime].Mask != new Rectangle(0, 0, this.Width, this.Height))
            {
                maskNode = doc.CreateNode(XmlNodeType.Element, "Mask", "");
                maskX = doc.CreateAttribute("x"); maskX.Value = Layers[BackgroundTemplateItem.Runtime].Mask.X.ToString();
                maskY = doc.CreateAttribute("y"); maskY.Value = Layers[BackgroundTemplateItem.Runtime].Mask.Y.ToString();
                maskWidth = doc.CreateAttribute("width"); maskWidth.Value = Layers[BackgroundTemplateItem.Runtime].Mask.Width.ToString();
                maskHeight = doc.CreateAttribute("height"); maskHeight.Value = Layers[BackgroundTemplateItem.Runtime].Mask.Height.ToString();
                maskNode.Attributes.Append(maskX);
                maskNode.Attributes.Append(maskY);
                maskNode.Attributes.Append(maskWidth);
                maskNode.Attributes.Append(maskHeight);
                ItemNode.AppendChild(maskNode);
            }
            #endregion
            #region --- Text ---
            textFont = doc.CreateNode(XmlNodeType.Element, "Text", "");
            fontFamily = doc.CreateAttribute("family"); fontFamily.Value = Layers[BackgroundTemplateItem.Runtime].TextFont.FontFamily.Name.ToString();
            fontSize = doc.CreateAttribute("size"); fontSize.Value = Layers[BackgroundTemplateItem.Runtime].TextFont.Size.ToString();
            fontBold = doc.CreateAttribute("bold"); fontBold.Value = (Layers[BackgroundTemplateItem.Runtime].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontItalic = doc.CreateAttribute("italic"); fontItalic.Value = (Layers[BackgroundTemplateItem.Runtime].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontColor = doc.CreateAttribute("color"); fontColor.Value = Layers[BackgroundTemplateItem.Runtime].TextColor.Name;
            textFont.Attributes.Append(fontFamily);
            textFont.Attributes.Append(fontSize);
            textFont.Attributes.Append(fontBold);
            textFont.Attributes.Append(fontItalic);
            textFont.Attributes.Append(fontColor);
            ItemNode.AppendChild(textFont);

            if (Layers[BackgroundTemplateItem.Runtime].TextZone != new Rectangle(0, 0, this.Width, this.Height))
            {
                textNode = doc.CreateNode(XmlNodeType.Element, "TextZone", "");
                textX = doc.CreateAttribute("x"); textX.Value = Layers[BackgroundTemplateItem.Runtime].TextZone.X.ToString();
                textY = doc.CreateAttribute("y"); textY.Value = Layers[BackgroundTemplateItem.Runtime].TextZone.Y.ToString();
                textWidth = doc.CreateAttribute("width"); textWidth.Value = Layers[BackgroundTemplateItem.Runtime].TextZone.Width.ToString();
                textHeight = doc.CreateAttribute("height"); textHeight.Value = Layers[BackgroundTemplateItem.Runtime].TextZone.Height.ToString();
                textNode.Attributes.Append(textX);
                textNode.Attributes.Append(textY);
                textNode.Attributes.Append(textWidth);
                textNode.Attributes.Append(textHeight);
                ItemNode.AppendChild(textNode);
            }
            root.AppendChild(ItemNode);
            #endregion
            #region ---Layers---
            //XmlNode layerNode = doc.CreateNode(XmlNodeType.Element, "Layers", "");

            //XmlAttribute imgLayerBack = doc.CreateAttribute("back");
            //imgLayerBack.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerBack);

            //XmlAttribute imgLayerFront = doc.CreateAttribute("front");
            //imgLayerFront.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerFront);

            //XmlAttribute imgLayerText = doc.CreateAttribute("text");
            //imgLayerText.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerText);

            //ItemNode.AppendChild(layerNode);
            #endregion
            #endregion

            #region *** Genre ***
            ItemNode = doc.CreateNode(XmlNodeType.Element, "Layer", "");
            itemName = doc.CreateAttribute("name"); itemName.Value = "Genre";
            ItemNode.Attributes.Append(itemName);

            #region --- Mask ---
            if (Layers[BackgroundTemplateItem.Genre].Mask != new Rectangle(0, 0, this.Width, this.Height))
            {
                maskNode = doc.CreateNode(XmlNodeType.Element, "Mask", "");
                maskX = doc.CreateAttribute("x"); maskX.Value = Layers[BackgroundTemplateItem.Genre].Mask.X.ToString();
                maskY = doc.CreateAttribute("y"); maskY.Value = Layers[BackgroundTemplateItem.Genre].Mask.Y.ToString();
                maskWidth = doc.CreateAttribute("width"); maskWidth.Value = Layers[BackgroundTemplateItem.Genre].Mask.Width.ToString();
                maskHeight = doc.CreateAttribute("height"); maskHeight.Value = Layers[BackgroundTemplateItem.Genre].Mask.Height.ToString();
                maskNode.Attributes.Append(maskX);
                maskNode.Attributes.Append(maskY);
                maskNode.Attributes.Append(maskWidth);
                maskNode.Attributes.Append(maskHeight);
                ItemNode.AppendChild(maskNode);
            }
            #endregion
            #region --- Text ---
            textFont = doc.CreateNode(XmlNodeType.Element, "Text", "");
            fontFamily = doc.CreateAttribute("family"); fontFamily.Value = Layers[BackgroundTemplateItem.Genre].TextFont.FontFamily.Name.ToString();
            fontSize = doc.CreateAttribute("size"); fontSize.Value = Layers[BackgroundTemplateItem.Genre].TextFont.Size.ToString();
            fontBold = doc.CreateAttribute("bold"); fontBold.Value = (Layers[BackgroundTemplateItem.Genre].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontItalic = doc.CreateAttribute("italic"); fontItalic.Value = (Layers[BackgroundTemplateItem.Genre].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontColor = doc.CreateAttribute("color"); fontColor.Value = Layers[BackgroundTemplateItem.Genre].TextColor.Name;
            textFont.Attributes.Append(fontFamily);
            textFont.Attributes.Append(fontSize);
            textFont.Attributes.Append(fontBold);
            textFont.Attributes.Append(fontItalic);
            textFont.Attributes.Append(fontColor);
            ItemNode.AppendChild(textFont);

            if (Layers[BackgroundTemplateItem.Genre].TextZone != new Rectangle(0, 0, this.Width, this.Height))
            {
                textNode = doc.CreateNode(XmlNodeType.Element, "TextZone", "");
                textX = doc.CreateAttribute("x"); textX.Value = Layers[BackgroundTemplateItem.Genre].TextZone.X.ToString();
                textY = doc.CreateAttribute("y"); textY.Value = Layers[BackgroundTemplateItem.Genre].TextZone.Y.ToString();
                textWidth = doc.CreateAttribute("width"); textWidth.Value = Layers[BackgroundTemplateItem.Genre].TextZone.Width.ToString();
                textHeight = doc.CreateAttribute("height"); textHeight.Value = Layers[BackgroundTemplateItem.Genre].TextZone.Height.ToString();
                textNode.Attributes.Append(textX);
                textNode.Attributes.Append(textY);
                textNode.Attributes.Append(textWidth);
                textNode.Attributes.Append(textHeight);
                ItemNode.AppendChild(textNode);
            }
            root.AppendChild(ItemNode);
            #endregion
            #region ---Layers---
            //XmlNode layerNode = doc.CreateNode(XmlNodeType.Element, "Layers", "");

            //XmlAttribute imgLayerBack = doc.CreateAttribute("back");
            //imgLayerBack.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerBack);

            //XmlAttribute imgLayerFront = doc.CreateAttribute("front");
            //imgLayerFront.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerFront);

            //XmlAttribute imgLayerText = doc.CreateAttribute("text");
            //imgLayerText.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerText);

            //ItemNode.AppendChild(layerNode);
            #endregion
            #endregion

            #region *** Rating ***
            ItemNode = doc.CreateNode(XmlNodeType.Element, "Layer", "");
            itemName = doc.CreateAttribute("name"); itemName.Value = "Rating";
            ItemNode.Attributes.Append(itemName);

            #region --- Mask ---
            if (Layers[BackgroundTemplateItem.Rating].Mask != new Rectangle(0, 0, this.Width, this.Height))
            {
                maskNode = doc.CreateNode(XmlNodeType.Element, "Mask", "");
                maskX = doc.CreateAttribute("x"); maskX.Value = Layers[BackgroundTemplateItem.Rating].Mask.X.ToString();
                maskY = doc.CreateAttribute("y"); maskY.Value = Layers[BackgroundTemplateItem.Rating].Mask.Y.ToString();
                maskWidth = doc.CreateAttribute("width"); maskWidth.Value = Layers[BackgroundTemplateItem.Rating].Mask.Width.ToString();
                maskHeight = doc.CreateAttribute("height"); maskHeight.Value = Layers[BackgroundTemplateItem.Rating].Mask.Height.ToString();
                maskNode.Attributes.Append(maskX);
                maskNode.Attributes.Append(maskY);
                maskNode.Attributes.Append(maskWidth);
                maskNode.Attributes.Append(maskHeight);
                ItemNode.AppendChild(maskNode);
            }
            #endregion
            #region --- Text ---
            textFont = doc.CreateNode(XmlNodeType.Element, "Text", "");
            fontFamily = doc.CreateAttribute("family"); fontFamily.Value = Layers[BackgroundTemplateItem.Rating].TextFont.FontFamily.Name.ToString();
            fontSize = doc.CreateAttribute("size"); fontSize.Value = Layers[BackgroundTemplateItem.Rating].TextFont.Size.ToString();
            fontBold = doc.CreateAttribute("bold"); fontBold.Value = (Layers[BackgroundTemplateItem.Rating].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontItalic = doc.CreateAttribute("italic"); fontItalic.Value = (Layers[BackgroundTemplateItem.Rating].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontColor = doc.CreateAttribute("color"); fontColor.Value = Layers[BackgroundTemplateItem.Rating].TextColor.Name;
            textFont.Attributes.Append(fontFamily);
            textFont.Attributes.Append(fontSize);
            textFont.Attributes.Append(fontBold);
            textFont.Attributes.Append(fontItalic);
            textFont.Attributes.Append(fontColor);
            ItemNode.AppendChild(textFont);

            if (Layers[BackgroundTemplateItem.Rating].TextZone != new Rectangle(0, 0, this.Width, this.Height))
            {
                textNode = doc.CreateNode(XmlNodeType.Element, "TextZone", "");
                textX = doc.CreateAttribute("x"); textX.Value = Layers[BackgroundTemplateItem.Rating].TextZone.X.ToString();
                textY = doc.CreateAttribute("y"); textY.Value = Layers[BackgroundTemplateItem.Rating].TextZone.Y.ToString();
                textWidth = doc.CreateAttribute("width"); textWidth.Value = Layers[BackgroundTemplateItem.Rating].TextZone.Width.ToString();
                textHeight = doc.CreateAttribute("height"); textHeight.Value = Layers[BackgroundTemplateItem.Rating].TextZone.Height.ToString();
                textNode.Attributes.Append(textX);
                textNode.Attributes.Append(textY);
                textNode.Attributes.Append(textWidth);
                textNode.Attributes.Append(textHeight);
                ItemNode.AppendChild(textNode);
            }
            root.AppendChild(ItemNode);
            #endregion
            #region ---Layers---
            //XmlNode layerNode = doc.CreateNode(XmlNodeType.Element, "Layers", "");

            //XmlAttribute imgLayerBack = doc.CreateAttribute("back");
            //imgLayerBack.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerBack);

            //XmlAttribute imgLayerFront = doc.CreateAttribute("front");
            //imgLayerFront.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerFront);

            //XmlAttribute imgLayerText = doc.CreateAttribute("text");
            //imgLayerText.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerText);

            //ItemNode.AppendChild(layerNode);
            #endregion
            #endregion

            #region *** Stars ***
            ItemNode = doc.CreateNode(XmlNodeType.Element, "Layer", "");
            itemName = doc.CreateAttribute("name"); itemName.Value = "Stars";
            ItemNode.Attributes.Append(itemName);

            #region --- Mask ---
            if (Layers[BackgroundTemplateItem.Stars].Mask != new Rectangle(0, 0, this.Width, this.Height))
            {
                maskNode = doc.CreateNode(XmlNodeType.Element, "Mask", "");
                maskX = doc.CreateAttribute("x"); maskX.Value = Layers[BackgroundTemplateItem.Stars].Mask.X.ToString();
                maskY = doc.CreateAttribute("y"); maskY.Value = Layers[BackgroundTemplateItem.Stars].Mask.Y.ToString();
                maskWidth = doc.CreateAttribute("width"); maskWidth.Value = Layers[BackgroundTemplateItem.Stars].Mask.Width.ToString();
                maskHeight = doc.CreateAttribute("height"); maskHeight.Value = Layers[BackgroundTemplateItem.Year].Mask.Height.ToString();
                maskNode.Attributes.Append(maskX);
                maskNode.Attributes.Append(maskY);
                maskNode.Attributes.Append(maskWidth);
                maskNode.Attributes.Append(maskHeight);
                ItemNode.AppendChild(maskNode);
            }
            #endregion
            #region --- Text ---
            textFont = doc.CreateNode(XmlNodeType.Element, "Text", "");
            fontFamily = doc.CreateAttribute("family"); fontFamily.Value = Layers[BackgroundTemplateItem.Stars].TextFont.FontFamily.Name.ToString();
            fontSize = doc.CreateAttribute("size"); fontSize.Value = Layers[BackgroundTemplateItem.Stars].TextFont.Size.ToString();
            fontBold = doc.CreateAttribute("bold"); fontBold.Value = (Layers[BackgroundTemplateItem.Stars].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontItalic = doc.CreateAttribute("italic"); fontItalic.Value = (Layers[BackgroundTemplateItem.Stars].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontColor = doc.CreateAttribute("color"); fontColor.Value = Layers[BackgroundTemplateItem.Stars].TextColor.Name;
            textFont.Attributes.Append(fontFamily);
            textFont.Attributes.Append(fontSize);
            textFont.Attributes.Append(fontBold);
            textFont.Attributes.Append(fontItalic);
            textFont.Attributes.Append(fontColor);
            ItemNode.AppendChild(textFont);

            if (Layers[BackgroundTemplateItem.Stars].TextZone != new Rectangle(0, 0, this.Width, this.Height))
            {
                textNode = doc.CreateNode(XmlNodeType.Element, "TextZone", "");
                textX = doc.CreateAttribute("x"); textX.Value = Layers[BackgroundTemplateItem.Stars].TextZone.X.ToString();
                textY = doc.CreateAttribute("y"); textY.Value = Layers[BackgroundTemplateItem.Stars].TextZone.Y.ToString();
                textWidth = doc.CreateAttribute("width"); textWidth.Value = Layers[BackgroundTemplateItem.Stars].TextZone.Width.ToString();
                textHeight = doc.CreateAttribute("height"); textHeight.Value = Layers[BackgroundTemplateItem.Stars].TextZone.Height.ToString();
                textNode.Attributes.Append(textX);
                textNode.Attributes.Append(textY);
                textNode.Attributes.Append(textWidth);
                textNode.Attributes.Append(textHeight);
                ItemNode.AppendChild(textNode);
            }
            root.AppendChild(ItemNode);
            #endregion
            #region ---Layers---
            //XmlNode layerNode = doc.CreateNode(XmlNodeType.Element, "Layers", "");

            //XmlAttribute imgLayerBack = doc.CreateAttribute("back");
            //imgLayerBack.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerBack);

            //XmlAttribute imgLayerFront = doc.CreateAttribute("front");
            //imgLayerFront.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerFront);

            //XmlAttribute imgLayerText = doc.CreateAttribute("text");
            //imgLayerText.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerText);

            //ItemNode.AppendChild(layerNode);
            #endregion
            #endregion

            #region *** SoundFormat ***
            ItemNode = doc.CreateNode(XmlNodeType.Element, "Layer", "");
            itemName = doc.CreateAttribute("name"); itemName.Value = "SoundFormat";
            ItemNode.Attributes.Append(itemName);

            #region --- Mask ---
            if (Layers[BackgroundTemplateItem.SoundFormat].Mask != new Rectangle(0, 0, this.Width, this.Height))
            {
                maskNode = doc.CreateNode(XmlNodeType.Element, "Mask", "");
                maskX = doc.CreateAttribute("x"); maskX.Value = Layers[BackgroundTemplateItem.SoundFormat].Mask.X.ToString();
                maskY = doc.CreateAttribute("y"); maskY.Value = Layers[BackgroundTemplateItem.SoundFormat].Mask.Y.ToString();
                maskWidth = doc.CreateAttribute("width"); maskWidth.Value = Layers[BackgroundTemplateItem.SoundFormat].Mask.Width.ToString();
                maskHeight = doc.CreateAttribute("height"); maskHeight.Value = Layers[BackgroundTemplateItem.SoundFormat].Mask.Height.ToString();
                maskNode.Attributes.Append(maskX);
                maskNode.Attributes.Append(maskY);
                maskNode.Attributes.Append(maskWidth);
                maskNode.Attributes.Append(maskHeight);
                ItemNode.AppendChild(maskNode);
            }
            #endregion
            #region --- Text ---
            textFont = doc.CreateNode(XmlNodeType.Element, "Text", "");
            fontFamily = doc.CreateAttribute("family"); fontFamily.Value = Layers[BackgroundTemplateItem.SoundFormat].TextFont.FontFamily.Name.ToString();
            fontSize = doc.CreateAttribute("size"); fontSize.Value = Layers[BackgroundTemplateItem.SoundFormat].TextFont.Size.ToString();
            fontBold = doc.CreateAttribute("bold"); fontBold.Value = (Layers[BackgroundTemplateItem.SoundFormat].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontItalic = doc.CreateAttribute("italic"); fontItalic.Value = (Layers[BackgroundTemplateItem.SoundFormat].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontColor = doc.CreateAttribute("color"); fontColor.Value = Layers[BackgroundTemplateItem.SoundFormat].TextColor.Name;
            textFont.Attributes.Append(fontFamily);
            textFont.Attributes.Append(fontSize);
            textFont.Attributes.Append(fontBold);
            textFont.Attributes.Append(fontItalic);
            textFont.Attributes.Append(fontColor);
            ItemNode.AppendChild(textFont);

            if (Layers[BackgroundTemplateItem.SoundFormat].TextZone != new Rectangle(0, 0, this.Width, this.Height))
            {
                textNode = doc.CreateNode(XmlNodeType.Element, "TextZone", "");
                textX = doc.CreateAttribute("x"); textX.Value = Layers[BackgroundTemplateItem.SoundFormat].TextZone.X.ToString();
                textY = doc.CreateAttribute("y"); textY.Value = Layers[BackgroundTemplateItem.SoundFormat].TextZone.Y.ToString();
                textWidth = doc.CreateAttribute("width"); textWidth.Value = Layers[BackgroundTemplateItem.SoundFormat].TextZone.Width.ToString();
                textHeight = doc.CreateAttribute("height"); textHeight.Value = Layers[BackgroundTemplateItem.SoundFormat].TextZone.Height.ToString();
                textNode.Attributes.Append(textX);
                textNode.Attributes.Append(textY);
                textNode.Attributes.Append(textWidth);
                textNode.Attributes.Append(textHeight);
                ItemNode.AppendChild(textNode);
            }
            root.AppendChild(ItemNode);
            #endregion
            #region ---Layers---
            //XmlNode layerNode = doc.CreateNode(XmlNodeType.Element, "Layers", "");

            //XmlAttribute imgLayerBack = doc.CreateAttribute("back");
            //imgLayerBack.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerBack);

            //XmlAttribute imgLayerFront = doc.CreateAttribute("front");
            //imgLayerFront.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerFront);

            //XmlAttribute imgLayerText = doc.CreateAttribute("text");
            //imgLayerText.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerText);

            //ItemNode.AppendChild(layerNode);
            #endregion
            #endregion

            #region *** ImageFormat ***
            ItemNode = doc.CreateNode(XmlNodeType.Element, "Layer", "");
            itemName = doc.CreateAttribute("name"); itemName.Value = "ImageFormat";
            ItemNode.Attributes.Append(itemName);

            #region --- Mask ---
            if (Layers[BackgroundTemplateItem.ImageFormat].Mask != new Rectangle(0, 0, this.Width, this.Height))
            {
                maskNode = doc.CreateNode(XmlNodeType.Element, "Mask", "");
                maskX = doc.CreateAttribute("x"); maskX.Value = Layers[BackgroundTemplateItem.ImageFormat].Mask.X.ToString();
                maskY = doc.CreateAttribute("y"); maskY.Value = Layers[BackgroundTemplateItem.ImageFormat].Mask.Y.ToString();
                maskWidth = doc.CreateAttribute("width"); maskWidth.Value = Layers[BackgroundTemplateItem.ImageFormat].Mask.Width.ToString();
                maskHeight = doc.CreateAttribute("height"); maskHeight.Value = Layers[BackgroundTemplateItem.ImageFormat].Mask.Height.ToString();
                maskNode.Attributes.Append(maskX);
                maskNode.Attributes.Append(maskY);
                maskNode.Attributes.Append(maskWidth);
                maskNode.Attributes.Append(maskHeight);
                ItemNode.AppendChild(maskNode);
            }
            #endregion
            #region --- Text ---
            textFont = doc.CreateNode(XmlNodeType.Element, "Text", "");
            fontFamily = doc.CreateAttribute("family"); fontFamily.Value = Layers[BackgroundTemplateItem.ImageFormat].TextFont.FontFamily.Name.ToString();
            fontSize = doc.CreateAttribute("size"); fontSize.Value = Layers[BackgroundTemplateItem.ImageFormat].TextFont.Size.ToString();
            fontBold = doc.CreateAttribute("bold"); fontBold.Value = (Layers[BackgroundTemplateItem.ImageFormat].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontItalic = doc.CreateAttribute("italic"); fontItalic.Value = (Layers[BackgroundTemplateItem.ImageFormat].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontColor = doc.CreateAttribute("color"); fontColor.Value = Layers[BackgroundTemplateItem.ImageFormat].TextColor.Name;
            textFont.Attributes.Append(fontFamily);
            textFont.Attributes.Append(fontSize);
            textFont.Attributes.Append(fontBold);
            textFont.Attributes.Append(fontItalic);
            textFont.Attributes.Append(fontColor);
            ItemNode.AppendChild(textFont);

            if (Layers[BackgroundTemplateItem.ImageFormat].TextZone != new Rectangle(0, 0, this.Width, this.Height))
            {
                textNode = doc.CreateNode(XmlNodeType.Element, "TextZone", "");
                textX = doc.CreateAttribute("x"); textX.Value = Layers[BackgroundTemplateItem.ImageFormat].TextZone.X.ToString();
                textY = doc.CreateAttribute("y"); textY.Value = Layers[BackgroundTemplateItem.ImageFormat].TextZone.Y.ToString();
                textWidth = doc.CreateAttribute("width"); textWidth.Value = Layers[BackgroundTemplateItem.ImageFormat].TextZone.Width.ToString();
                textHeight = doc.CreateAttribute("height"); textHeight.Value = Layers[BackgroundTemplateItem.ImageFormat].TextZone.Height.ToString();
                textNode.Attributes.Append(textX);
                textNode.Attributes.Append(textY);
                textNode.Attributes.Append(textWidth);
                textNode.Attributes.Append(textHeight);
                ItemNode.AppendChild(textNode);
            }
            root.AppendChild(ItemNode);
            #endregion
            #region ---Layers---
            //XmlNode layerNode = doc.CreateNode(XmlNodeType.Element, "Layers", "");

            //XmlAttribute imgLayerBack = doc.CreateAttribute("back");
            //imgLayerBack.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerBack);

            //XmlAttribute imgLayerFront = doc.CreateAttribute("front");
            //imgLayerFront.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerFront);

            //XmlAttribute imgLayerText = doc.CreateAttribute("text");
            //imgLayerText.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerText);

            //ItemNode.AppendChild(layerNode);
            #endregion
            #endregion

            #region *** Cover ***
            ItemNode = doc.CreateNode(XmlNodeType.Element, "Layer", "");
            itemName = doc.CreateAttribute("name"); itemName.Value = "Cover";
            ItemNode.Attributes.Append(itemName);

            #region --- Mask ---
            if (Layers[BackgroundTemplateItem.Cover].Mask != new Rectangle(0, 0, this.Width, this.Height))
            {
                maskNode = doc.CreateNode(XmlNodeType.Element, "Mask", "");
                maskX = doc.CreateAttribute("x"); maskX.Value = Layers[BackgroundTemplateItem.Cover].Mask.X.ToString();
                maskY = doc.CreateAttribute("y"); maskY.Value = Layers[BackgroundTemplateItem.Cover].Mask.Y.ToString();
                maskWidth = doc.CreateAttribute("width"); maskWidth.Value = Layers[BackgroundTemplateItem.Cover].Mask.Width.ToString();
                maskHeight = doc.CreateAttribute("height"); maskHeight.Value = Layers[BackgroundTemplateItem.Cover].Mask.Height.ToString();
                maskNode.Attributes.Append(maskX);
                maskNode.Attributes.Append(maskY);
                maskNode.Attributes.Append(maskWidth);
                maskNode.Attributes.Append(maskHeight);
                ItemNode.AppendChild(maskNode);
            }
            #endregion
            #region --- Text ---
            textFont = doc.CreateNode(XmlNodeType.Element, "Text", "");
            fontFamily = doc.CreateAttribute("family"); fontFamily.Value = Layers[BackgroundTemplateItem.Cover].TextFont.FontFamily.Name.ToString();
            fontSize = doc.CreateAttribute("size"); fontSize.Value = Layers[BackgroundTemplateItem.Cover].TextFont.Size.ToString();
            fontBold = doc.CreateAttribute("bold"); fontBold.Value = (Layers[BackgroundTemplateItem.Cover].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontItalic = doc.CreateAttribute("italic"); fontItalic.Value = (Layers[BackgroundTemplateItem.Cover].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontColor = doc.CreateAttribute("color"); fontColor.Value = Layers[BackgroundTemplateItem.Cover].TextColor.Name;
            textFont.Attributes.Append(fontFamily);
            textFont.Attributes.Append(fontSize);
            textFont.Attributes.Append(fontBold);
            textFont.Attributes.Append(fontItalic);
            textFont.Attributes.Append(fontColor);
            ItemNode.AppendChild(textFont);

            if (Layers[BackgroundTemplateItem.Cover].TextZone != new Rectangle(0, 0, this.Width, this.Height))
            {
                textNode = doc.CreateNode(XmlNodeType.Element, "TextZone", "");
                textX = doc.CreateAttribute("x"); textX.Value = Layers[BackgroundTemplateItem.Cover].TextZone.X.ToString();
                textY = doc.CreateAttribute("y"); textY.Value = Layers[BackgroundTemplateItem.Cover].TextZone.Y.ToString();
                textWidth = doc.CreateAttribute("width"); textWidth.Value = Layers[BackgroundTemplateItem.Cover].TextZone.Width.ToString();
                textHeight = doc.CreateAttribute("height"); textHeight.Value = Layers[BackgroundTemplateItem.Cover].TextZone.Height.ToString();
                textNode.Attributes.Append(textX);
                textNode.Attributes.Append(textY);
                textNode.Attributes.Append(textWidth);
                textNode.Attributes.Append(textHeight);
                ItemNode.AppendChild(textNode);
            }
            root.AppendChild(ItemNode);
            #endregion
            #region ---Layers---
            //XmlNode layerNode = doc.CreateNode(XmlNodeType.Element, "Layers", "");

            //XmlAttribute imgLayerBack = doc.CreateAttribute("back");
            //imgLayerBack.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerBack);

            //XmlAttribute imgLayerFront = doc.CreateAttribute("front");
            //imgLayerFront.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerFront);

            //XmlAttribute imgLayerText = doc.CreateAttribute("text");
            //imgLayerText.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerText);

            //ItemNode.AppendChild(layerNode);
            #endregion
            #endregion

            #region *** Box ***
            ItemNode = doc.CreateNode(XmlNodeType.Element, "Layer", "");
            itemName = doc.CreateAttribute("name"); itemName.Value = "Box";
            ItemNode.Attributes.Append(itemName);

            #region --- Mask ---
            if (Layers[BackgroundTemplateItem.Box].Mask != new Rectangle(0, 0, this.Width, this.Height))
            {
                maskNode = doc.CreateNode(XmlNodeType.Element, "Mask", "");
                maskX = doc.CreateAttribute("x"); maskX.Value = Layers[BackgroundTemplateItem.Box].Mask.X.ToString();
                maskY = doc.CreateAttribute("y"); maskY.Value = Layers[BackgroundTemplateItem.Box].Mask.Y.ToString();
                maskWidth = doc.CreateAttribute("width"); maskWidth.Value = Layers[BackgroundTemplateItem.Box].Mask.Width.ToString();
                maskHeight = doc.CreateAttribute("height"); maskHeight.Value = Layers[BackgroundTemplateItem.Box].Mask.Height.ToString();
                maskNode.Attributes.Append(maskX);
                maskNode.Attributes.Append(maskY);
                maskNode.Attributes.Append(maskWidth);
                maskNode.Attributes.Append(maskHeight);
                ItemNode.AppendChild(maskNode);
            }
            #endregion
            #region --- Text ---
            textFont = doc.CreateNode(XmlNodeType.Element, "Text", "");
            fontFamily = doc.CreateAttribute("family"); fontFamily.Value = Layers[BackgroundTemplateItem.Box].TextFont.FontFamily.Name.ToString();
            fontSize = doc.CreateAttribute("size"); fontSize.Value = Layers[BackgroundTemplateItem.Box].TextFont.Size.ToString();
            fontBold = doc.CreateAttribute("bold"); fontBold.Value = (Layers[BackgroundTemplateItem.Box].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontItalic = doc.CreateAttribute("italic"); fontItalic.Value = (Layers[BackgroundTemplateItem.Box].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontColor = doc.CreateAttribute("color"); fontColor.Value = Layers[BackgroundTemplateItem.Box].TextColor.Name;
            textFont.Attributes.Append(fontFamily);
            textFont.Attributes.Append(fontSize);
            textFont.Attributes.Append(fontBold);
            textFont.Attributes.Append(fontItalic);
            textFont.Attributes.Append(fontColor);
            ItemNode.AppendChild(textFont);

            if (Layers[BackgroundTemplateItem.Box].TextZone != new Rectangle(0, 0, this.Width, this.Height))
            {
                textNode = doc.CreateNode(XmlNodeType.Element, "TextZone", "");
                textX = doc.CreateAttribute("x"); textX.Value = Layers[BackgroundTemplateItem.Box].TextZone.X.ToString();
                textY = doc.CreateAttribute("y"); textY.Value = Layers[BackgroundTemplateItem.Box].TextZone.Y.ToString();
                textWidth = doc.CreateAttribute("width"); textWidth.Value = Layers[BackgroundTemplateItem.Box].TextZone.Width.ToString();
                textHeight = doc.CreateAttribute("height"); textHeight.Value = Layers[BackgroundTemplateItem.Box].TextZone.Height.ToString();
                textNode.Attributes.Append(textX);
                textNode.Attributes.Append(textY);
                textNode.Attributes.Append(textWidth);
                textNode.Attributes.Append(textHeight);
                ItemNode.AppendChild(textNode);
            }
            root.AppendChild(ItemNode);
            #endregion
            #region ---Layers---
            //XmlNode layerNode = doc.CreateNode(XmlNodeType.Element, "Layers", "");

            //XmlAttribute imgLayerBack = doc.CreateAttribute("back");
            //imgLayerBack.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerBack);

            //XmlAttribute imgLayerFront = doc.CreateAttribute("front");
            //imgLayerFront.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerFront);

            //XmlAttribute imgLayerText = doc.CreateAttribute("text");
            //imgLayerText.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerText);

            //ItemNode.AppendChild(layerNode);
            #endregion
            #endregion


            #region *** Fanart ***
            ItemNode = doc.CreateNode(XmlNodeType.Element, "Layer", "");
            itemName = doc.CreateAttribute("name"); itemName.Value = "Fanart";
            ItemNode.Attributes.Append(itemName);

            #region --- Mask ---
            if (Layers[BackgroundTemplateItem.Fanart].Mask != new Rectangle(0, 0, this.Width, this.Height))
            {
                maskNode = doc.CreateNode(XmlNodeType.Element, "Mask", "");
                maskX = doc.CreateAttribute("x"); maskX.Value = Layers[BackgroundTemplateItem.Fanart].Mask.X.ToString();
                maskY = doc.CreateAttribute("y"); maskY.Value = Layers[BackgroundTemplateItem.Fanart].Mask.Y.ToString();
                maskWidth = doc.CreateAttribute("width"); maskWidth.Value = Layers[BackgroundTemplateItem.Fanart].Mask.Width.ToString();
                maskHeight = doc.CreateAttribute("height"); maskHeight.Value = Layers[BackgroundTemplateItem.Fanart].Mask.Height.ToString();
                maskNode.Attributes.Append(maskX);
                maskNode.Attributes.Append(maskY);
                maskNode.Attributes.Append(maskWidth);
                maskNode.Attributes.Append(maskHeight);
                ItemNode.AppendChild(maskNode);
            }
            #endregion
            #region --- Text ---
            textFont = doc.CreateNode(XmlNodeType.Element, "Text", "");
            fontFamily = doc.CreateAttribute("family"); fontFamily.Value = Layers[BackgroundTemplateItem.Fanart].TextFont.FontFamily.Name.ToString();
            fontSize = doc.CreateAttribute("size"); fontSize.Value = Layers[BackgroundTemplateItem.Fanart].TextFont.Size.ToString();
            fontBold = doc.CreateAttribute("bold"); fontBold.Value = (Layers[BackgroundTemplateItem.Fanart].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontItalic = doc.CreateAttribute("italic"); fontItalic.Value = (Layers[BackgroundTemplateItem.Fanart].TextFont.Style == (FontStyle.Bold | FontStyle.Italic)).ToString();
            fontColor = doc.CreateAttribute("color"); fontColor.Value = Layers[BackgroundTemplateItem.Fanart].TextColor.Name;
            textFont.Attributes.Append(fontFamily);
            textFont.Attributes.Append(fontSize);
            textFont.Attributes.Append(fontBold);
            textFont.Attributes.Append(fontItalic);
            textFont.Attributes.Append(fontColor);
            ItemNode.AppendChild(textFont);

            if (Layers[BackgroundTemplateItem.Fanart].TextZone != new Rectangle(0, 0, this.Width, this.Height))
            {
                textNode = doc.CreateNode(XmlNodeType.Element, "TextZone", "");
                textX = doc.CreateAttribute("x"); textX.Value = Layers[BackgroundTemplateItem.Fanart].TextZone.X.ToString();
                textY = doc.CreateAttribute("y"); textY.Value = Layers[BackgroundTemplateItem.Fanart].TextZone.Y.ToString();
                textWidth = doc.CreateAttribute("width"); textWidth.Value = Layers[BackgroundTemplateItem.Fanart].TextZone.Width.ToString();
                textHeight = doc.CreateAttribute("height"); textHeight.Value = Layers[BackgroundTemplateItem.Fanart].TextZone.Height.ToString();
                textNode.Attributes.Append(textX);
                textNode.Attributes.Append(textY);
                textNode.Attributes.Append(textWidth);
                textNode.Attributes.Append(textHeight);
                ItemNode.AppendChild(textNode);
            }
            root.AppendChild(ItemNode);
            #endregion
            #region ---Layers---
            //XmlNode layerNode = doc.CreateNode(XmlNodeType.Element, "Layers", "");

            //XmlAttribute imgLayerBack = doc.CreateAttribute("back");
            //imgLayerBack.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerBack);

            //XmlAttribute imgLayerFront = doc.CreateAttribute("front");
            //imgLayerFront.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerFront);

            //XmlAttribute imgLayerText = doc.CreateAttribute("text");
            //imgLayerText.Value = ImageToBase64(this.Background.Image, this._extension);
            //layerNode.Attributes.Append(imgLayerText);

            //ItemNode.AppendChild(layerNode);
            #endregion
            #endregion
            return root;
        }

        public void GenerateFast(Movie movie, String path)
        {
            Graphics _graphicContainer;
            ResizeFilter _resizeFilter = new ResizeFilter();
            resizeFilter.KeepAspectRatio = false;
            resizeFilter.Width = this.Foreground.Mask.Width;
            resizeFilter.Height = this.Foreground.Mask.Height;
            
            this._image = resizeFilter.ExecuteFilter(global::EzTvix.Theme.Default.Empty);

            try
            {
                // Préparation de l'image
                _graphicContainer = System.Drawing.Graphics.FromImage(this._image);


                //// chargemet de l'image courante (image de fond)
                _graphicContainer.DrawImageUnscaledAndClipped(this.Foreground, this.Foreground.Mask);

                this.Layers[BackgroundTemplateItem.Title].TextData = movie.Title;
                //_graphicContainer = this.LayerTitle.DrawText(_graphicContainer);
                
                this.Layers[BackgroundTemplateItem.Plot].TextData = movie.Plot;
                //_graphicContainer = this.Layers[BackgroundTemplateItem.Plot].DrawText(_graphicContainer);

                this.Layers[BackgroundTemplateItem.Actors].TextData = (movie.Actors.Join(", ")).Reduce(85, "...");
                //_graphicContainer = this.Layers[BackgroundTemplateItem.Actors].DrawText(_graphicContainer);

                this.Layers[BackgroundTemplateItem.Directors].TextData = movie.Directors.ToString(", ");
                //_graphicContainer = this.Layers[BackgroundTemplateItem.Directors].DrawText(_graphicContainer);

                this.Layers[BackgroundTemplateItem.Year].TextData = movie.Year.ToString();
                //_graphicContainer = this.Layers[BackgroundTemplateItem.Year].DrawText(_graphicContainer);

                this.Layers[BackgroundTemplateItem.Rating].TextData = movie.Ratings[0].Score.ToString();
                //_graphicContainer = this.Layers[BackgroundTemplateItem.Rating].DrawText(_graphicContainer);

                this.Layers[BackgroundTemplateItem.Genre].TextData = movie.Genres.ToString(", ");
                //_graphicContainer = this.Layers[BackgroundTemplateItem.Genre].DrawText(_graphicContainer);
                
                this.Layers[BackgroundTemplateItem.Runtime].TextData = movie.Runtime.ToRuntime();

                this.Layers[BackgroundTemplateItem.Cover].Image = movie.Cover;

                // if fanart is used in theme, then use it, else use empty fanart
                if (TvixInfo.UseFanart)
                    this.Layers[BackgroundTemplateItem.Fanart].Image = movie.Fanart;
                else
                    this.Layers[BackgroundTemplateItem.Fanart].Image = global::EzTvix.Theme.Default.Alpha;



                foreach (KeyValuePair<BackgroundTemplateItem, ImageLayer> kvp in this.Layers)
                {
                    if (kvp.Value.layerType == LayerType.Text)
                        _graphicContainer = kvp.Value.DrawText(_graphicContainer);
                    else
                        _graphicContainer.DrawImageUnscaledAndClipped(kvp.Value.ImageMasked, kvp.Value.Mask);
                }
                //_graphicContainer.DrawImageUnscaledAndClipped(this.Layers[BackgroundTemplateItem.Cover].ImageMasked, this.Layers[BackgroundTemplateItem.Cover].Mask);
                //_graphicContainer.DrawImageUnscaledAndClipped(this.Layers[BackgroundTemplateItem.Box].ImageMasked, this.Layers[BackgroundTemplateItem.Box].Mask);
                //_graphicContainer.DrawImage(

                _graphicContainer.Dispose();
                _graphicContainer = null;

                string fullName = path + ThemeInfo.VideoBackgroundName + "." + ThemeInfo.VideoBackgroundExtention;
                if (File.Exists(fullName)) File.Delete(fullName);

                Bitmap back = new Bitmap(this._image);
                back.Save(fullName, ThemeInfo.VideoBackgroundFormat);

                back.Dispose();
            }
            catch (Exception e)
            { }

        }
        public void Generate(Movie movie, String path)
        {
            this.Layers[BackgroundTemplateItem.Title].DrawText(movie.Title);
            this.Layers[BackgroundTemplateItem.Plot].DrawText(movie.Plot);
            this.Layers[BackgroundTemplateItem.Actors].DrawText(movie.Actors.Join(", "));
            this.Layers[BackgroundTemplateItem.Directors].DrawText(movie.Directors.ToString(", "));

            //this.Layers[BackgroundTemplateItem.Runtime].DrawText(movie.Runtime.ToString());
            this.Layers[BackgroundTemplateItem.Year].DrawText(movie.Year.ToString());

            this.Layers[BackgroundTemplateItem.Rating].DrawText(movie.Ratings[0].Score.ToString());
            this.Layers[BackgroundTemplateItem.Genre].DrawText(movie.Genres.ToString(", "));
            this.Layers[BackgroundTemplateItem.Cover].Image = movie.Cover;

            //this.LayerStars.DrawText(movie.
            //this.LayerImageFormat
            //this.LayerSoundFormat
            //this.LayerResolution

            this.build();
            string fullName = path + ThemeInfo.VideoBackgroundName + "." + ThemeInfo.VideoBackgroundExtention;
            if (File.Exists(fullName)) File.Delete(fullName);
            
            this.Image.Save(path + ThemeInfo.VideoBackgroundName + "." + ThemeInfo.VideoBackgroundExtention);
            //return this.Image;
        }
        public new void build()
        {
            Graphics _graphicContainer;
            ResizeFilter _resizeFilter = new ResizeFilter();
            resizeFilter.KeepAspectRatio = false;
            resizeFilter.Width = this.Foreground.Mask.Width;
            resizeFilter.Height = this.Foreground.Mask.Height;


            this._image = resizeFilter.ExecuteFilter(global::EzTvix.Theme.Default.Empty);

            try
            {
                // Préparation de l'image
                _graphicContainer = System.Drawing.Graphics.FromImage(this._image);


                //// chargemet de l'image courante

                _graphicContainer.DrawImageUnscaledAndClipped(this.Foreground, this.Foreground.Mask);

                _graphicContainer.DrawImageUnscaledAndClipped(this.Layers[BackgroundTemplateItem.Cover].ImageMasked, this.Layers[BackgroundTemplateItem.Cover].Mask);
                _graphicContainer.DrawImageUnscaledAndClipped(this.Layers[BackgroundTemplateItem.Box].ImageMasked, this.Layers[BackgroundTemplateItem.Box].Mask);

                _graphicContainer.DrawImageUnscaledAndClipped(this.Layers[BackgroundTemplateItem.Plot], this.Foreground.Mask);

               _graphicContainer.DrawImageUnscaledAndClipped(this.Layers[BackgroundTemplateItem.Title], this.Layers[BackgroundTemplateItem.Title].Mask);
//                 _graphicContainer.DrawImageUnscaledAndClipped(this.Layers[BackgroundTemplateItem.Title], this.Foreground.Mask);
                _graphicContainer.DrawImageUnscaledAndClipped(this.Layers[BackgroundTemplateItem.Actors], this.Foreground.Mask);
                _graphicContainer.DrawImageUnscaledAndClipped(this.Layers[BackgroundTemplateItem.Directors], this.Foreground.Mask);
                _graphicContainer.DrawImageUnscaledAndClipped(this.Layers[BackgroundTemplateItem.Runtime], this.Foreground.Mask);
                _graphicContainer.DrawImageUnscaledAndClipped(this.Layers[BackgroundTemplateItem.Year], this.Foreground.Mask);

                _graphicContainer.DrawImageUnscaledAndClipped(this.Layers[BackgroundTemplateItem.Rating], this.Foreground.Mask);
                _graphicContainer.DrawImageUnscaledAndClipped(this.Layers[BackgroundTemplateItem.Genre], this.Foreground.Mask);
                //_graphicContainer.DrawImageUnscaledAndClipped(this.LayerStars, this.Foreground.Mask);
                //_graphicContainer.DrawImageUnscaledAndClipped(this.LayerImageFormat, this.Foreground.Mask);
                //_graphicContainer.DrawImageUnscaledAndClipped(this.LayerSoundFormat, this.Foreground.Mask);
                //_graphicContainer.DrawImageUnscaledAndClipped(this.LayerResolution, this.Foreground.Mask);


                //_graphicContainer.DrawImage(

                _graphicContainer.Dispose();
                _graphicContainer = null;



            }
            catch (Exception e)
            {
            }

        }
        #endregion

    }
}
