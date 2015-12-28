using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using EzTvix.Theme;

using Neodynamic.SDK;

using RootKit.Drawings;
using RootKit.Drawings.BasicFilters;
using RootKit.Web;

namespace EzTvix
{
    public partial class ThemeBuilder : Form
    {
        //private String p_currentTheme = "Default";
        private ThemeTemplate p_theme = new ThemeTemplate();
        private ImageTemplate p_currentImage = new ImageTemplate(global::EzTvix.Theme.Default.Alpha);
        private ItemType p_currentItem = ItemType.ItemBack;
        private BackgroundTemplate p_videoTemplate = new BackgroundTemplate();
        private BackgroundTemplateItem p_backgroundCurrentLayer = BackgroundTemplateItem.Cover;
        private Image p_BackgroundImage;
        //private Image p_ForegroundImage;
        //private ItemType p_lastItem;
        private bool p_isThemeChanged = false;
        private bool p_valueSetByCode = false;
        private System.ComponentModel.BackgroundWorker bgwCode;
        public ThemeBuilder()
        {
            InitializeComponent();
        }

        private void ThemeBuilder_Load(object sender, EventArgs e)
        {
            objectTreeView.ExpandAll();
            treeBackgroundVideo.ExpandAll();

            DirectoryInfo[] themeList = p_theme.getThemeList();
            themeComboBox.Items.Add("Default");
            foreach (DirectoryInfo theme in themeList)
            {
                themeComboBox.Items.Add(theme);
            }
            themeComboBox.SelectedIndex = 0;

            bgwCode = new BackgroundWorker();

            bgwCode.WorkerReportsProgress = true;
            bgwCode.WorkerSupportsCancellation = true;

            bgwCode.DoWork += new DoWorkEventHandler(bgwCode_DoWork);
            bgwCode.RunWorkerAsync();
        }
        private void bgwCode_DoWork(object sender, DoWorkEventArgs e)
        {
            DownloadManagerThreaded downMan = new DownloadManagerThreaded();
            BackgroundWorker worker = sender as BackgroundWorker;
            Image img = downMan.DownloadFromUrl("http://www.eztvix.info/Alpha.png", 
                "There is a problem connecting to Internet. \r\nSome functions will not work properly!", 
                worker, e);
            //e.Result = img;
        
        }

        private void objectTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode currentNode = e.Node;

            //tabPanelBackgroundItem.Enabled = p_theme[p_currentItem].layerToSave.contains(LayerToSave.Background);
            
            switch (currentNode.Name.ToLower())
            {
                case "general":
                case "backgroundpicture":
                    p_currentItem = ItemType.Background;
                    //tabPanelBackgroundItem.Enabled = false;
                    tabItem.SelectedIndex = 1;
                    groupBoxBackground.SendToBack();
                    LoadPreview();
                    break;
                case "videobackground":
                    p_currentItem = ItemType.BackgroundVideo;
                    p_theme.BackgroundVideo.DrawText();

                    nudBoxX.Value = decimal.Parse(p_theme.BackgroundVideo[BackgroundTemplateItem.Box].Mask.X.ToString());
                    nudBoxY.Value = decimal.Parse(p_theme.BackgroundVideo[BackgroundTemplateItem.Box].Mask.Y.ToString());
                    nudBoxWidth.Value = decimal.Parse(p_theme.BackgroundVideo[BackgroundTemplateItem.Box].Mask.Width.ToString());
                    nudBoxHeight.Value = decimal.Parse(p_theme.BackgroundVideo[BackgroundTemplateItem.Box].Mask.Height.ToString());
                    nudCoverX.Value = decimal.Parse(p_theme.BackgroundVideo[BackgroundTemplateItem.Cover].Mask.X.ToString());
                    nudCoverY.Value = decimal.Parse(p_theme.BackgroundVideo[BackgroundTemplateItem.Cover].Mask.Y.ToString());
                    nudCoverWidth.Value = decimal.Parse(p_theme.BackgroundVideo[BackgroundTemplateItem.Cover].Mask.Width.ToString());
                    nudCoverHeight.Value = decimal.Parse(p_theme.BackgroundVideo[BackgroundTemplateItem.Cover].Mask.Height.ToString());

                    LoadBackgroundVideoPreview();
                    groupBoxBackground.BringToFront();
                    break;
                //case "videobox":
                //    groupBoxBackground.SendToBack();
                //    this.LoadItem(ItemType.VideoBox);
                //    //p_theme[p_currentItem].save();
                //    LoadPreview();
                //    break;
                default:
                    //tabPanelBackgroundItem.Enabled = p_theme[p_currentItem].layerToSave.contains(LayerToSave.Background);

                    switch (currentNode.Name.ToLower())
                    {
                        case "backgroundpicture":
                        case "itemback" : 
                        case "folder":
                            //tabItem.TabPages["itemTabCover"].Hide();
                            //tabPanelBackgroundItem.Enabled = false;
                            tabItem.SelectedIndex = 1;
                            break;
                        default :
                            //tabPanelBackgroundItem.Enabled = true;
                            tabItem.SelectedIndex = 0;
                            //tabItem.TabPages["itemTabCover"].Show();
                            break;
                    }
                    try
                    {
                        groupBoxBackground.SendToBack();
                        this.LoadItem((ItemType)Enum.Parse(typeof(ItemType), currentNode.Name, true));
                        LoadPreview();
                    }
                    catch (Exception ex)
                    { }
                    break;
            }
        }

        private void treeBackgroundVideo_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode currentNode = e.Node;

            switch (currentNode.Name.ToLower())
            {
                case "videobackgroundnode":
                    this.LoadBackgroundItem((BackgroundTemplateItem.Box));
                    LoadBackgroundVideoPreview();
                    break;
                case "videoboxnode":
                case "videocovernode":
                case "textnode":
                    break;
                default:
                    try
                    {
                        this.LoadBackgroundItem((BackgroundTemplateItem)Enum.Parse(typeof(BackgroundTemplateItem), currentNode.Name, true));
                        LoadBackgroundVideoPreview();
                    }
                    catch (Exception ex)
                    { }
                    break;
            }
        }

        private void themeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DialogResult result = DialogResult.No;
            if (p_isThemeChanged)
            {
                result = MessageBox.Show("Theme has been modified!!!\r\n would you like to save the changes?", "Theme Changed", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            }
            if (result == DialogResult.OK)
            {
                p_theme.Save();
            }

            p_isThemeChanged = false;

            p_theme.Name = themeComboBox.SelectedItem.ToString();
            LoadPreview();
            //LoadItem(p_theme.ItemBack);
            this.LoadItem(ItemType.ItemBack);
            objectTreeView.SelectedNode = objectTreeView.Nodes[0].Nodes[0];
            //themeTreeView.SelectedNode = themeTreeView.Nodes[0].Nodes[0].Nodes[1]; //itemBackground
        }
        
        private void LoadPreview()
        {
            ImageManager _imgMan = new ImageManager();
            
            try
            {
                // définition du resize
                ResizeFilter _resizeFilter = new ResizeFilter();

                _resizeFilter.Width = previewPic.Width;
                _resizeFilter.Height = previewPic.Height;
                _resizeFilter.KeepAspectRatio = false;

                // chargement et resize de l'image
                previewPic.Image = _resizeFilter.ExecuteFilter(p_theme[ItemType.Background].Foreground);
            }
            catch (FileNotFoundException e)
            {
                // empty image par default
                previewPic.Image = p_theme.Background;
            }
        }

        /// <summary>
        /// Load existing theme image
        /// </summary>
        /// <param name="_imgToLoad">Image to load</param>
        private void LoadItem(ItemType itemType)
        {

            ImageManager _imgMan = new ImageManager();
            ResizeFilter _resizeFilter = new ResizeFilter();
            Image _imgFinal = p_theme.ItemBack;

            p_BackgroundImage = p_theme.Empty;
            p_currentItem = itemType;
            switch (p_currentItem)
            {
                case ItemType.VideoBox:
                case ItemType.AudioBox:
                    tabPanelBackgroundItem.Enabled = true;
                    break;
                default:
                    tabPanelBackgroundItem.Enabled = p_theme[p_currentItem].layerToSave.contains(Layer.Background);
                    break;
            }

            pbFront.Image = p_theme[p_currentItem].Foreground;
            pbBack.Image = p_theme[p_currentItem].Foreground;

            try
            {
                itemPic.BackgroundImage = p_theme.ItemBack;
                itemPic.BackgroundImageLayout = ImageLayout.Stretch;
                itemPic.Image = p_theme[p_currentItem].Image;
            }
            catch (FileNotFoundException e)
            {
                // empty image by default
                itemPic.Image = p_theme.ItemBack;
                p_theme[p_currentItem] = p_theme.ItemBack;
            }

            #region *** Init fields ***
            p_valueSetByCode = !p_valueSetByCode;
            lblFontSample.Font = p_theme[p_currentItem].Text.TextFont;
            lblFontSample.ForeColor = p_theme[p_currentItem].Text.TextColor;

            nudTextX.Value = decimal.Parse(p_theme[p_currentItem].Text.TextZone.X.ToString());
            nudTextY.Value = decimal.Parse(p_theme[p_currentItem].Text.TextZone.Y.ToString());
            nudTextWidth.Value = decimal.Parse(p_theme[p_currentItem].Text.TextZone.Width.ToString());
            nudTextHeight.Value = decimal.Parse(p_theme[p_currentItem].Text.TextZone.Height.ToString());

            nudBackX.Value = decimal.Parse(p_theme[p_currentItem].Background.Mask.X.ToString());
            nudBackY.Value = decimal.Parse(p_theme[p_currentItem].Background.Mask.Y.ToString());
            nudBackWidth.Value = decimal.Parse(p_theme[p_currentItem].Background.Mask.Width.ToString());
            nudBackHeight.Value = decimal.Parse(p_theme[p_currentItem].Background.Mask.Height.ToString());
            p_valueSetByCode = !p_valueSetByCode;
            #endregion
        }

        private void btnLoadForeground_Click(object sender, EventArgs e)
        {
            ImageManager imgMan = new ImageManager();
            ImageTemplate _localImage = new ImageTemplate();

            _localImage.Foreground.Image = imgMan.DownloadFromUrl3(textBoxUrlBack.Text);

            p_theme[p_currentItem].Foreground = _localImage.Foreground;

            itemPic.Image = p_theme[p_currentItem].Image;
        }
        
        private void cbUseThemeFolderBack_CheckedChanged(object sender, EventArgs e)
        {
            ImageManager imgMan = new ImageManager();
            ImageTemplate _localImage = new ImageTemplate();

            if (cbUseThemeFolderBack.Checked)
                p_theme[p_currentItem].Background.Image = p_theme.Folder;
            else
                p_theme[p_currentItem].Background.Image = p_theme.Empty;
            itemPic.Image = p_theme[p_currentItem].Image;
            
        }

        private void textBoxUrlBack_DoubleClick(object sender, EventArgs e)
        {
            textBoxUrlBack.Text = "http://www.movieposterdb.com/posters/10_03/2010/1104001/l_1104001_53855dc2.jpg";
        }

        private void btnLoadFileForeground_Click(object sender, EventArgs e)
        {
            Stream myStream;
    
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = openDialog.OpenFile()) != null)
                {

                    p_theme[p_currentItem].Foreground.Image = Image.FromStream(myStream);
                    // Insert code to read the stream here.
                    myStream.Close();
                    myStream.Dispose();
                    if (p_currentItem == ItemType.Background)
                        LoadPreview();
                    //previewPic.Image = p_theme[p_currentItem].Foreground.Image;
                    else
                        itemPic.Image = p_theme[p_currentItem].Image;

                }
            }

            
        }

        private void btnResetCurrentPicture_Click(object sender, EventArgs e)
        {
            p_theme[p_currentItem].Background.Image = global::EzTvix.Theme.Default.Empty;
            p_theme[p_currentItem].Foreground.Image = global::EzTvix.Theme.Default.Empty;
            p_theme[p_currentItem].Text.Image = global::EzTvix.Theme.Default.Empty;
            itemPic.Image = p_theme[p_currentItem];
            pbText.Image = p_theme[p_currentItem].Text;
            pbFront.Image = p_theme[p_currentItem].Foreground;
            pbBack.Image = p_theme[p_currentItem].Background;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            p_theme.Save();


            //// Show the dialog and process the result
            //saveDialog.ShowDialog();
            //// If the file name is not an empty string open it for saving.
            //if (saveDialog.FileName != "")
            //{
            //    // Saves the Image via a FileStream created by the OpenFile method.
            //    System.IO.FileStream fs =
            //       (System.IO.FileStream)saveDialog.OpenFile();
            //    itemPic.Image.Save(fs,System.Drawing.Imaging.ImageFormat.Png);

            //    fs.Close();

            //}

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void itemPic_Click(object sender, EventArgs e)
        {
            pbText.Image = p_theme[p_currentItem].Text;
            pbFront.Image = p_theme[p_currentItem].Foreground;
            pbBack.Image = p_theme[p_currentItem].Background;
        }

        private void textBoxUrlBack_TextChanged(object sender, EventArgs e)
        {
            btnLoadBackground.Enabled = true;

        }

        private void itemPropertyGrid_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {

        }

        private void itemPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
           
        }


        #region *** VideoBackground ***
        private void LoadBackgroundVideoPreview()
        {
            if (!p_valueSetByCode)
            {
                if (p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextData.Trim() != "")
                    p_theme.BackgroundVideo[p_backgroundCurrentLayer].DrawText();

                if (!bw.IsBusy)
                    bw.RunWorkerAsync();
            }
        }
        private Image bw_renderBackgroundVideo()
        {
            ImageManager _imgMan = new ImageManager();
            try
            {

                
                // définition du resize
                ResizeFilter _resizeFilter = new ResizeFilter();

                _resizeFilter.Width = previewPic.Width;
                _resizeFilter.Height = previewPic.Height;
                _resizeFilter.KeepAspectRatio = false;

                return _resizeFilter.ExecuteFilter(p_theme.BackgroundVideo);
            }
            catch (FileNotFoundException e)
            {
                // empty image par default
                return p_theme[ItemType.BackgroundVideo].Foreground;
            }
        }
        //private void renderBackgroundVideo()
        //{
        //    ImageManager _imgMan = new ImageManager();
        //    try
        //    {
        //        // définition du resize
        //        ResizeFilter _resizeFilter = new ResizeFilter();

        //        _resizeFilter.Width = previewPic.Width;
        //        _resizeFilter.Height = previewPic.Height;
        //        _resizeFilter.KeepAspectRatio = false;

        //        previewPic.Image = _resizeFilter.ExecuteFilter(p_theme.BackgroundVideo);
        //    }
        //    catch (FileNotFoundException e)
        //    {
        //        // empty image par default
        //        previewPic.Image = p_theme[ItemType.BackgroundVideo].Foreground;
        //    }
            
        //}

        private void LoadBackgroundItem(BackgroundTemplateItem itemType)
        {
            
            ImageManager _imgMan = new ImageManager();
            ResizeFilter _resizeFilter = new ResizeFilter();
            Image _imgFinal = p_theme.ItemBack;

            //p_BackgroundImage = p_theme.Empty;
            p_backgroundCurrentLayer = itemType;

            #region *** Init fields ***
            p_valueSetByCode = !p_valueSetByCode;
            lblVBackSample.Font = p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextFont;
            lblVBackSample.ForeColor = p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextColor;
            textVBackLabel.Text = p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextData;

            nudVBackTextX.Value = decimal.Parse(p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextZone.X.ToString());
            nudVBackTextY.Value = decimal.Parse(p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextZone.Y.ToString());
            nudVBackTextWidth.Value = decimal.Parse(p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextZone.Width.ToString());
            nudVBackTextHeight.Value = decimal.Parse(p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextZone.Height.ToString());

            switch (p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextFormat.Alignment)
            {
                case StringAlignment.Center: BackgroundAlignCombo.Text = "Center";
                    break;
                case StringAlignment.Far: BackgroundAlignCombo.Text = "Right";
                    break;
                default: BackgroundAlignCombo.Text = "Left";
                    break;
            }
            //nudVBackX.Value = decimal.Parse(p_theme.BackgroundVideo[p_backgroundCurrentLayer].Mask.X.ToString());
            //nudVBackY.Value = decimal.Parse(p_theme.BackgroundVideo[p_backgroundCurrentLayer].Mask.Y.ToString());
            //nudVBackWidth.Value = decimal.Parse(p_theme.BackgroundVideo[p_backgroundCurrentLayer].Width.ToString());
            //nudVBackHeight.Value = decimal.Parse(p_theme.BackgroundVideo[p_backgroundCurrentLayer].Mask.Height.ToString());
            p_valueSetByCode = !p_valueSetByCode;
            #endregion
        }

        private void btnGetBox_Click(object sender, EventArgs e)
        {

            bwVideoBackgroundBox.RunWorkerAsync();
        }

        private void btnLoadBox_Click(object sender, EventArgs e)
        {
            //ImageTemplate _localImage = new ImageTemplate();
            Stream myStream;
            ResizeFilter _res = new ResizeFilter();

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = openDialog.OpenFile()) != null)
                {
                    p_theme.BackgroundVideo.LayerBox.Image = Image.FromStream(myStream);
                    pictureBoxVideoBox.Image = _res.ExecuteFilter(p_theme.BackgroundVideo.LayerBox.Image, pictureBoxVideoBox.Width, pictureBoxVideoBox.Height);
                    myStream.Close();
                }
            }

            LoadBackgroundVideoPreview();
        }

        private void btnLoadCover_Click(object sender, EventArgs e)
        {
            Stream myStream;
            ResizeFilter _res = new ResizeFilter();

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = openDialog.OpenFile()) != null)
                {
                    p_theme.BackgroundVideo.LayerCover.Image = Image.FromStream(myStream);
                    pictureBoxCover.Image = _res.ExecuteFilter(p_theme.BackgroundVideo.LayerCover.Image, pictureBoxCover.Width, pictureBoxCover.Height);
                    myStream.Close();
                    cbDemoCover.Checked = false;
                }
            }
            
            LoadBackgroundVideoPreview();
        }
        private void bwVideoBackgroundBox_loadCover()
        {
            DownloadManagerThreaded downMan = new DownloadManagerThreaded();
            ImageTemplate _localImage = new ImageTemplate();
            ResizeFilter _res = new ResizeFilter();
        }

        private void bwVideoBackgroundBox_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            DownloadManagerThreaded downMan = new DownloadManagerThreaded();
            Image img = downMan.DownloadFromUrl(tbBoxUrl.Text, worker, e);
            
            e.Result = img;
        }

        private void bwVideoBackgroundBox_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.toolStripProgressBar.Value = e.ProgressPercentage;

        }

        private void bwVideoBackgroundBox_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ResizeFilter _res = new ResizeFilter();
            p_theme.BackgroundVideo.LayerBox.Image = (Image)e.Result;
            previewPic.Image = _res.ExecuteFilter(p_theme.BackgroundVideo, previewPic.Width, previewPic.Height); 
        }

        private void nudCoverX_ValueChanged(object sender, EventArgs e)
        {
            Rectangle _mask = new Rectangle(int.Parse(nudCoverX.Value.ToString()),
                p_theme.BackgroundVideo.LayerCover.Mask.Y,
                p_theme.BackgroundVideo.LayerCover.Mask.Width,
                p_theme.BackgroundVideo.LayerCover.Mask.Height);

            p_theme.BackgroundVideo.LayerCover.Mask = _mask;
            if (!p_valueSetByCode)
                LoadBackgroundVideoPreview(); ;
        }
        private void nudCoverY_ValueChanged(object sender, EventArgs e)
        {
            Rectangle _mask = new Rectangle(p_theme.BackgroundVideo.LayerCover.Mask.X,
                int.Parse(nudCoverY.Value.ToString()),
                p_theme.BackgroundVideo.LayerCover.Mask.Width,
                p_theme.BackgroundVideo.LayerCover.Mask.Height);
            p_theme.BackgroundVideo.LayerCover.Mask = _mask;
            if (!p_valueSetByCode)
                LoadBackgroundVideoPreview();
        }
        private void nudCoverWidth_ValueChanged(object sender, EventArgs e)
        {
            Rectangle _mask = new Rectangle(p_theme.BackgroundVideo.LayerCover.Mask.X,
                p_theme.BackgroundVideo.LayerCover.Mask.Y,
                int.Parse(nudCoverWidth.Value.ToString()),
                p_theme.BackgroundVideo.LayerCover.Mask.Height);
            p_theme.BackgroundVideo.LayerCover.Mask = _mask;
            if (!p_valueSetByCode)
                LoadBackgroundVideoPreview();
        }
        private void nudCoverHeight_ValueChanged(object sender, EventArgs e)
        {
            Rectangle _mask = new Rectangle(p_theme.BackgroundVideo.LayerCover.Mask.X,
                p_theme.BackgroundVideo.LayerCover.Mask.Y,
                p_theme.BackgroundVideo.LayerCover.Mask.Width,
                int.Parse(nudCoverHeight.Value.ToString()));
            p_theme.BackgroundVideo.LayerCover.Mask = _mask;
            if (!p_valueSetByCode)
                LoadBackgroundVideoPreview();
        }
        
        private void nudBoxX_ValueChanged(object sender, EventArgs e)
        {
            Rectangle _mask = new Rectangle(int.Parse(nudBoxX.Value.ToString()),
                p_theme.BackgroundVideo.LayerBox.Mask.Y,
                p_theme.BackgroundVideo.LayerBox.Mask.Width,
                p_theme.BackgroundVideo.LayerBox.Mask.Height);

            p_theme.BackgroundVideo.LayerBox.Mask = _mask;
            if (!p_valueSetByCode)
                LoadBackgroundVideoPreview(); ;
        }
        private void nudBoxY_ValueChanged(object sender, EventArgs e)
        {
            Rectangle _mask = new Rectangle(p_theme.BackgroundVideo.LayerBox.Mask.X,
                int.Parse(nudBoxY.Value.ToString()),
                p_theme.BackgroundVideo.LayerBox.Mask.Width,
                p_theme.BackgroundVideo.LayerBox.Mask.Height);
            p_theme.BackgroundVideo.LayerBox.Mask = _mask;
            if (!p_valueSetByCode)
                LoadBackgroundVideoPreview();
        }
        private void nudBoxWidth_ValueChanged(object sender, EventArgs e)
        {
            Rectangle _mask = new Rectangle(p_theme.BackgroundVideo.LayerBox.Mask.X,
                p_theme.BackgroundVideo.LayerBox.Mask.Y,
                int.Parse(nudBoxWidth.Value.ToString()),
                p_theme.BackgroundVideo.LayerBox.Mask.Height);
            p_theme.BackgroundVideo.LayerBox.Mask = _mask;
            if (!p_valueSetByCode)
                LoadBackgroundVideoPreview();
        }
        private void nudBoxHeight_ValueChanged(object sender, EventArgs e)
        {
            Rectangle _mask = new Rectangle(p_theme.BackgroundVideo.LayerBox.Mask.X,
                p_theme.BackgroundVideo.LayerBox.Mask.Y,
                p_theme.BackgroundVideo.LayerBox.Mask.Width,
                int.Parse(nudBoxHeight.Value.ToString()));
            p_theme.BackgroundVideo.LayerBox.Mask = _mask;
            if (!p_valueSetByCode)
                LoadBackgroundVideoPreview();
        }

        private void nudVBackTextX_ValueChanged(object sender, EventArgs e)
        {
            Rectangle _textZone = new Rectangle(int.Parse(nudVBackTextX.Value.ToString()),
                p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextZone.Y,
                p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextZone.Width,
                p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextZone.Height);
            p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextZone = _textZone;
            if (!p_valueSetByCode)
                LoadBackgroundVideoPreview();
        }
        private void nudVBackTextY_ValueChanged(object sender, EventArgs e)
        {
            Rectangle _textZone = new Rectangle(p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextZone.X,
                int.Parse(nudVBackTextY.Value.ToString()),
                p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextZone.Width,
                p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextZone.Height);
            p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextZone = _textZone;
            if (!p_valueSetByCode)
                LoadBackgroundVideoPreview();
        }
        private void nudVBackTextWidth_ValueChanged(object sender, EventArgs e)
        {
            Rectangle _textZone = new Rectangle(p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextZone.X,
               p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextZone.Y,
                int.Parse(nudVBackTextWidth.Value.ToString()),
                p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextZone.Height);
            p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextZone = _textZone;
            if (!p_valueSetByCode)
                LoadBackgroundVideoPreview();
        }
        private void nudVBackTextHeight_ValueChanged(object sender, EventArgs e)
        {
            Rectangle _textZone = new Rectangle(p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextZone.X,
                p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextZone.Y,
                p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextZone.Width,
                int.Parse(nudVBackTextHeight.Value.ToString()));
            p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextZone = _textZone;
            if (!p_valueSetByCode)
                LoadBackgroundVideoPreview();
        }
 
        #endregion

        #region *** Text ***
        public void renderText()
        {
            ImageManager _imgMan = new ImageManager();
            p_theme[p_currentItem].Text.TextZone = new Rectangle(int.Parse(nudTextX.Value.ToString()), int.Parse(nudTextY.Value.ToString()), int.Parse(nudTextWidth.Value.ToString()), int.Parse(nudTextHeight.Value.ToString()));

            try
            {
                p_theme[p_currentItem].DrawText(textBoxLabel.Text, lblFontSample.Font, new SolidBrush(lblFontSample.ForeColor), cbDropShadow.Checked);
                itemPic.Image = p_theme[p_currentItem];
            }
            catch (Exception ex) { }
        }

        private void btnFont_Click(object sender, EventArgs e)
        {

            FontDialog fontDialog = new FontDialog();
            fontDialog.Font = lblFontSample.Font; // new Font("Segoe UI", 20, FontStyle.Regular);
            if (fontDialog.ShowDialog() != DialogResult.Cancel)
            {
                lblFontSample.Font = fontDialog.Font;
            }
            renderText();
        }

        private void btnColorDialog_Click(object sender, EventArgs e)
        {

            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = lblFontSample.ForeColor;
            colorDialog.FullOpen = true;
            if (colorDialog.ShowDialog() != DialogResult.Cancel)
            {
                lblFontSample.ForeColor = colorDialog.Color;
            }
            colorDialog.Dispose();
            colorDialog = null;
            renderText();
        }

        private void textBoxLabel_TextChanged(object sender, EventArgs e)
        {
            renderText();
        }

        private void cbDropShadow_CheckedChanged(object sender, EventArgs e)
        {
            if (!p_valueSetByCode)
                renderText();
        }
        private void nudTextX_ValueChanged(object sender, EventArgs e)
        {
            Rectangle _textZone = new Rectangle(int.Parse(nudTextX.Value.ToString()),
                p_theme[p_currentItem].Text.TextZone.Y,
                p_theme[p_currentItem].Text.TextZone.Width,
                p_theme[p_currentItem].Text.TextZone.Height);
            p_theme[p_currentItem].Text.TextZone = _textZone;
            if (!p_valueSetByCode)
                renderText();
        }
        private void nudTextY_ValueChanged(object sender, EventArgs e)
        {
            Rectangle _textZone = new Rectangle(p_theme[p_currentItem].Text.TextZone.X,
                int.Parse(nudTextY.Value.ToString()),
                p_theme[p_currentItem].Text.TextZone.Width,
                p_theme[p_currentItem].Text.TextZone.Height);
            p_theme[p_currentItem].Text.TextZone = _textZone;
            if (!p_valueSetByCode)
                renderText();
        }
        private void nudTextWidth_ValueChanged(object sender, EventArgs e)
        {
            Rectangle _textZone = new Rectangle(p_theme[p_currentItem].Text.TextZone.X,
                p_theme[p_currentItem].Text.TextZone.Y,
                int.Parse(nudTextWidth.Value.ToString()),
                p_theme[p_currentItem].Text.TextZone.Height);
            p_theme[p_currentItem].Text.TextZone = _textZone;
            if (!p_valueSetByCode)
                renderText();
        }
        private void nudTextHeight_ValueChanged(object sender, EventArgs e)
        {
            Rectangle _textZone = new Rectangle(p_theme[p_currentItem].Text.TextZone.X,
                p_theme[p_currentItem].Text.TextZone.Y,
                p_theme[p_currentItem].Text.TextZone.Width,
                int.Parse(nudTextHeight.Value.ToString()));
            p_theme[p_currentItem].Text.TextZone = _textZone;
            if (!p_valueSetByCode)
                renderText();
        }
        #endregion

        #region *** Background ***

        private void btnLoadBackground_Click(object sender, EventArgs e)
        {
            ImageManager imgMan = new ImageManager();
            ImageTemplate _localImage = new ImageTemplate();

            renderBackground(imgMan.DownloadFromUrl3(textBoxUrlBack.Text));
        }
        private void btnLoadFileBackground_Click(object sender, EventArgs e)
        {
            //ImageTemplate _localImage = new ImageTemplate();
            Stream myStream;

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = openDialog.OpenFile()) != null)
                {
                    //Neodynamic.SDK.ImageDraw imgDraw = new Neodynamic.SDK.ImageDraw();
                    
                    //imgDraw.Canvas.AutoSize = false;
                    //imgDraw.ImageFormat = ImageDrawFormat.Jpeg;
                    ////Set business card size
                    //imgDraw.Canvas.Width = 200;
                    //imgDraw.Canvas.Height = 200;

                    //ImageElement imgElem = ImageElement.FromFile(@"C:\tron.jpg");
                    //imgElem.Width = 200;
                    //imgElem.Height = 200;
                    //imgDraw.Elements.Add(imgElem);

                    //TextElement txtElemAddress = new TextElement();
                    //txtElemAddress.AutoSize = false;
                    //txtElemAddress.Font.Name = "Times New Roman";
                    //txtElemAddress.Font.Italic = false;
                    //txtElemAddress.Font.Bold = true;
                    //txtElemAddress.Font.Size = 11f;
                    //txtElemAddress.Font.Unit = FontUnit.Point;
                    //txtElemAddress.ForeColor = System.Drawing.Color.White;
                    //txtElemAddress.Text = "Tron";
                    //txtElemAddress.TextQuality = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    //txtElemAddress.X = 0;
                    //txtElemAddress.Y = 160;
                    //txtElemAddress.Width = 200;
                    //txtElemAddress.Height = 40;
                    //txtElemAddress.TextAlignment = ContentAlignment.MiddleCenter;
                    ////Add element to output image
                    //imgDraw.Elements.Add(txtElemAddress);


                    //itemPic.Image = imgDraw.GetOutputImage();
                    //p_currentImage.LayerBack = Image.FromStream(myStream);
                    p_BackgroundImage = Image.FromStream(myStream);
                    p_theme[p_currentItem].Background.Image = p_BackgroundImage;
                    renderBackground();

                    //renderBackground(Image.FromStream(myStream));
                    // Insert code to read the stream here.
                    myStream.Close();
                }
            }
            
            
            //itemPic.Image = p_currentImage.Image;

        }
        public void renderBackground()
        {
            ImageManager _imgMan = new ImageManager();

            p_theme[p_currentItem].Background.Mask = new Rectangle(int.Parse(nudBackX.Value.ToString()),
                int.Parse(nudBackY.Value.ToString()),
                int.Parse(nudBackWidth.Value.ToString()),
                int.Parse(nudBackHeight.Value.ToString()));

            //p_currentImage.LayerBack = p_BackgroundImage;
            itemPic.Image = p_theme[p_currentItem].Image;
            itemPropertyGrid.SelectedObject = p_theme[p_currentItem];
            p_isThemeChanged = true;

        }
        public void renderBackground(Image _renderImage)
        {
            ImageManager _imgMan = new ImageManager();

            p_BackgroundImage = _renderImage;
            p_theme[p_currentItem].Background.Image = _renderImage;
            itemPic.Image = p_theme[p_currentItem].Image;
            itemPropertyGrid.SelectedObject = p_theme[p_currentItem];
            p_isThemeChanged = true;

        }
        
        private void nudBackX_ValueChanged(object sender, EventArgs e)
        {
            Rectangle _mask = new Rectangle(int.Parse(nudBackX.Value.ToString()),
                p_theme[p_currentItem].Background.Mask.Y,
                p_theme[p_currentItem].Background.Mask.Width,
                p_theme[p_currentItem].Background.Mask.Height);

            p_theme[p_currentItem].Background.Mask = _mask;
            if (!p_valueSetByCode)
                renderBackground();
        }

        private void nudBackY_ValueChanged(object sender, EventArgs e)
        {
            Rectangle _mask = new Rectangle(p_theme[p_currentItem].Background.Mask.X,
                int.Parse(nudBackY.Value.ToString()),
                p_theme[p_currentItem].Background.Mask.Width,
                p_theme[p_currentItem].Background.Mask.Height);
            p_theme[p_currentItem].Background.Mask = _mask;
            if (!p_valueSetByCode)
                renderBackground();
        }

        private void nudBackWidth_ValueChanged(object sender, EventArgs e)
        {
            Rectangle _mask = new Rectangle(p_theme[p_currentItem].Background.Mask.X,
                p_theme[p_currentItem].Background.Mask.Y,
                int.Parse(nudBackWidth.Value.ToString()),
                p_theme[p_currentItem].Background.Mask.Height);
            p_theme[p_currentItem].Background.Mask = _mask;
            if (!p_valueSetByCode)
                renderBackground();
        }

        private void nudBackHeight_ValueChanged(object sender, EventArgs e)
        {
            Rectangle _mask = new Rectangle(p_theme[p_currentItem].Background.Mask.X,
                p_theme[p_currentItem].Background.Mask.Y,
                p_theme[p_currentItem].Background.Mask.Width,
                int.Parse(nudBackHeight.Value.ToString()));
            p_theme[p_currentItem].Background.Mask = _mask;
            if (!p_valueSetByCode)
                renderBackground();
        }

        #endregion

        #region *** BW pour le chargement général du template video ***
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = bw_renderBackgroundVideo();
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.toolStripProgressBar.Value = e.ProgressPercentage;
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            previewPic.Image = (Image)e.Result;
        }
        #endregion

        #region *** BW URL Video Background ***
        private void bwVideoBackground_DoWork(object sender, DoWorkEventArgs e)
        {
            DownloadManagerThreaded downMan = new DownloadManagerThreaded();
            BackgroundWorker worker = sender as BackgroundWorker;
            Image img = downMan.DownloadFromUrl(tbBackgroundVideoUrl.Text, worker, e);

            e.Result = img;
        }

        private void bwVideoBackground_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.toolStripProgressBar.Value = e.ProgressPercentage;
        }

        private void bwVideoBackground_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ResizeFilter _res = new ResizeFilter();
            p_theme.BackgroundVideo.Foreground.Image = _res.ExecuteFilter((Image)e.Result, p_theme.BackgroundVideo.Foreground.Width, p_theme.BackgroundVideo.Foreground.Height);
            previewPic.Image = _res.ExecuteFilter(p_theme.BackgroundVideo, previewPic.Width, previewPic.Height);
            this.toolStripProgressBar.Value = 0;
        }
        #endregion


        private void btnLoadBackgroundVideo_Click(object sender, EventArgs e)
        {
            //ImageTemplate _localImage = new ImageTemplate();
            Stream myStream;
            ResizeFilter _res = new ResizeFilter();

            if (openDialogJpg.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = openDialogJpg.OpenFile()) != null)
                {
                    p_theme.BackgroundVideo.Foreground.Image = Image.FromStream(myStream);
                    pictureBoxVideoBackground.Image = _res.ExecuteFilter(p_theme.BackgroundVideo.Foreground.Image, pictureBoxVideoBackground.Width, pictureBoxVideoBackground.Height);
                    myStream.Close();
                }
            }
            LoadBackgroundVideoPreview();
        }

        private void btnGetBackground_Click(object sender, EventArgs e)
        {
            
            bwVideoBackground.RunWorkerAsync();
        }

        private void tbBackgroundVideoUrl_DoubleClick(object sender, EventArgs e)
        {
            tbBackgroundVideoUrl.Text = "http://icon-generator.net/imgs/download_lite_win.jpg";
        }

        private void btnVBackFontDialog_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            ImageManager _imgMan = new ImageManager();

            fontDialog.Font = lblVBackSample.Font; // new Font("Segoe UI", 20, FontStyle.Regular);
            if (fontDialog.ShowDialog() != DialogResult.Cancel)
            {
                lblVBackSample.Font = fontDialog.Font;
                p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextFont = lblVBackSample.Font;
            }
               
            LoadBackgroundVideoPreview();
        }

        private void btnVBackColorDialog_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = lblVBackSample.ForeColor;
            colorDialog.FullOpen = true;
            if (colorDialog.ShowDialog() != DialogResult.Cancel)
            {
                lblVBackSample.ForeColor = colorDialog.Color;
                p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextColor = lblVBackSample.ForeColor;
            }
            colorDialog.Dispose();
            colorDialog = null;
            LoadBackgroundVideoPreview();
        }

        private void cbVBackDropShadow_CheckedChanged(object sender, EventArgs e)
        {
            p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextShadow = cbVBackDropShadow.Checked;
            LoadBackgroundVideoPreview();
        }

        private void textVBackLabel_TextChanged(object sender, EventArgs e)
        {
            p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextData = textVBackLabel.Text;
            LoadBackgroundVideoPreview();

        }

        private void cbDemoCover_CheckedChanged(object sender, EventArgs e)
        {
            ResizeFilter _res = new ResizeFilter();
            if (cbDemoCover.Checked)
            {
                p_theme.BackgroundVideo.LayerCover.Image = global::EzTvix.Theme.Default.DemoCover;
                pictureBoxCover.Image = _res.ExecuteFilter(p_theme.BackgroundVideo.LayerCover.Image, pictureBoxCover.Width, pictureBoxCover.Height);

                LoadBackgroundVideoPreview();
            }
        }

        private void btnLoadThemeVideoBox_Click(object sender, EventArgs e)
        {
            //ImageTemplate _localImage = new ImageTemplate();
            ResizeFilter _res = new ResizeFilter();

            p_theme.BackgroundVideo.LayerBox.Image = p_theme.VideoBox.Image;
            pictureBoxVideoBox.Image = _res.ExecuteFilter(p_theme.BackgroundVideo.LayerBox.Image, pictureBoxVideoBox.Width, pictureBoxVideoBox.Height);
            
            
            LoadBackgroundVideoPreview();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.eztvix.info");
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            frmSaveThemeAs newfrm = new frmSaveThemeAs();
            //new DirectoryInfo(TvixInfo.ThemeFolder)).GetDirectories();
            DialogResult dr;
            
            if ((dr = newfrm.ShowDialog()) == DialogResult.OK)
            {
                String NewThemeName = newfrm.NewThemeName;
                //Directory.CreateDirectory(TvixInfo.ThemeFolder + @"\" + NewThemeName);
                try
                {

                    p_theme.CreateThemeConfig(NewThemeName);
                    themeComboBox.Items.Add(NewThemeName);
                }
                catch (Exception ex)
                { }
                //xmlRead = new XmlTextReader(new StringReader(global::EzTvix.Theme.Default.Config));
                //xmlConfig.Load(xmlRead);
                //xmlConfig.Save(TvixInfo.ThemeFolder + @"\" + NewThemeName + @"\theme.xml");
                //xmlRead.Close();
                


            }
            newfrm.Dispose();

        }

        private void comboAlign_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboAlign.Text.ToLower())
            {
                case "center": p_theme[p_currentItem].Text.TextFormat.Alignment = StringAlignment.Center;
                    break;
                case "right": p_theme[p_currentItem].Text.TextFormat.Alignment = StringAlignment.Far;
                    break;
                default : p_theme[p_currentItem].Text.TextFormat.Alignment = StringAlignment.Near;
                    break;
            }
            
            if (!p_valueSetByCode)
                renderText();

        }

        private void BackgroundAlignCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (BackgroundAlignCombo.Text.ToLower())
            {
                case "center": p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextFormat.Alignment = StringAlignment.Center;
                    break;
                case "right": p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextFormat.Alignment = StringAlignment.Far;
                    break;
                default: p_theme.BackgroundVideo[p_backgroundCurrentLayer].TextFormat.Alignment = StringAlignment.Near;
                    break;
            }

            
            LoadBackgroundVideoPreview();

            
        }



    }
}
