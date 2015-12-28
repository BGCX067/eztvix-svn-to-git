using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EzTvix.Theme;
using EzTvix.Core;
using RootKit.Drawings.BasicFilters;
using System.IO;
using RootKit.Web;

namespace EzTvix
{
    public partial class frmFolderIcon : Form
    {
        private ThemeTemplate p_theme = new ThemeTemplate();
        private Rectangle iconeMask;

        public Image foreImage;
        public Bitmap finalImage = global::EzTvix.Theme.Default.Alpha;
        
       
        public frmFolderIcon()
        {
            InitializeComponent();
            
            // Initialisation du Thème
            p_theme.Name = TvixInfo.Theme;
        }

        /// <summary>
        /// Chargement de la form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmFolderIcon_Load(object sender, EventArgs e)
        {
            //Fond de l'image
            coverPic.BackgroundImage = p_theme[ItemType.ItemBack];
            coverPic.Image = finalImage;
            iconeMask = new Rectangle(0, 0, coverPic.Width, coverPic.Height);

        }

        private void rbNone_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNone.Checked)
                Render();
        }

        private void rbFolder_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFolder.Checked)
                Render();
        }

        private void rbVideo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbVideo.Checked) 
                Render();
        }

        private void rbAudio_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAudio.Checked)
                Render();

        }

        private void Render()
        {
            Graphics _graphicContainer;
            ResizeFilter _resizeFilter = new ResizeFilter();
            Bitmap bmp;
            
            _resizeFilter.KeepAspectRatio = false;
            _resizeFilter.Width = iconeMask.Width;
            _resizeFilter.Height = iconeMask.Height;

            Image image = _resizeFilter.ExecuteFilter(global::EzTvix.Theme.Default.Empty);

            if (foreImage == null)
                foreImage = global::EzTvix.Theme.Default.Empty;

            // Préparation de l'image
            _graphicContainer = System.Drawing.Graphics.FromImage(image);
            bmp = new Bitmap(foreImage);

            try
            {
                if (rbVideo.Checked)
                {
                    if (foreImage != null)
                    {
                        if (foreImage.Width > p_theme[ItemType.VideoBox].Background.Mask.Width || foreImage.Height > p_theme[ItemType.VideoBox].Background.Mask.Height)
                        {
                            bmp = new Bitmap(_resizeFilter.ExecuteFilter(foreImage, 
                                                                         p_theme[ItemType.VideoBox].Background.Mask.Width, 
                                                                         p_theme[ItemType.VideoBox].Background.Mask.Height));
                        }
                    }

                    // Dans le cas des boitiers, le cover doit être mis en premier
                    _graphicContainer.DrawImageUnscaledAndClipped(bmp, p_theme[ItemType.VideoBox].Background.Mask);
                    // chargemet de l'image courante (image de fond)
                    _graphicContainer.DrawImageUnscaledAndClipped(p_theme[ItemType.VideoBox].Foreground, iconeMask);

                }
                if (rbNone.Checked)
                {
                    if (foreImage != null)
                    {
                        if (foreImage.Width > iconeMask.Width || foreImage.Height > iconeMask.Height)
                        {
                            bmp = new Bitmap(_resizeFilter.ExecuteFilter(foreImage, iconeMask.Width, iconeMask.Height));
                        }
                    }

                    _graphicContainer.DrawImageUnscaledAndClipped(bmp, centerImage(coverPic.BackgroundImage, bmp));
                }

                if (rbAudio.Checked)
                {
                    if (foreImage != null)
                    {
                        if (foreImage.Width > p_theme[ItemType.AudioBox].Background.Mask.Width || foreImage.Height > p_theme[ItemType.AudioBox].Background.Mask.Height)
                        {
                            bmp = new Bitmap(_resizeFilter.ExecuteFilter(foreImage,
                                                                          p_theme[ItemType.AudioBox].Background.Mask.Width,
                                                                          p_theme[ItemType.AudioBox].Background.Mask.Height));
                        }
                    }
                    // Dans le cas des boitiers, le cover doit être mis en premier
                    _graphicContainer.DrawImageUnscaledAndClipped(bmp, p_theme[ItemType.AudioBox].Background.Mask);
                    // chargemet de l'image courante (image de fond)
                    _graphicContainer.DrawImageUnscaledAndClipped(p_theme[ItemType.AudioBox].Image, iconeMask);

                }

                if (rbFolder.Checked)
                {
                    if (foreImage != null)
                    {
                        if (foreImage.Width > iconeMask.Width || foreImage.Height > iconeMask.Height)
                        {
                            bmp = new Bitmap(_resizeFilter.ExecuteFilter(foreImage, iconeMask.Width, iconeMask.Height));
                        }
                    }// chargemet de l'image courante (image de fond)
                    _graphicContainer.DrawImageUnscaledAndClipped(p_theme[ItemType.Folder].Image, iconeMask);
                    // Dans le cas des boitiers, le cover doit être mis en premier
                    _graphicContainer.DrawImageUnscaledAndClipped(bmp, centerImage(coverPic.BackgroundImage, bmp));

                }
                ItemType iconType = (rbFolder.Checked) ? ItemType.Folder: ((rbAudio.Checked) ? ItemType.AudioBox : ItemType.VideoBox);

                _graphicContainer = DrawText(_graphicContainer, tbText.Text,
                    p_theme[iconType].Text.TextFormat,
                    p_theme[iconType].Text.TextFont,
                    p_theme[iconType].Text.Brush,
                    p_theme[iconType].Text.TextZone,
                    p_theme[iconType].Text.TextShadow);

                _graphicContainer.Dispose();
                _graphicContainer = null;

                //string fullName = path + ThemeInfo.VideoBackgroundName + "." + ThemeInfo.VideoBackgroundExtention;
                //if (File.Exists(fullName)) File.Delete(fullName);

                //this._image.Save(fullName);
                coverPic.Image = image;
                finalImage = new Bitmap( image );
            }
            catch (Exception e)
            { }
            bmp.Dispose();

        }
        public Rectangle centerImage(Image baseImg , Image coverImg)
        { 
            Int32 x, y, w, h;

            x = (baseImg.Width / 2) - (coverImg.Width / 2);
            y = (baseImg.Height / 2) - (coverImg.Height / 2);
            w = coverImg.Width;
            h = coverImg.Height;

            return new Rectangle (x, y, w, h);
        }

        public Graphics DrawText(Graphics g, String _text, StringFormat _strFormat, Font _textFont, Brush _brush, RectangleF _clip, bool _dropShadow)
        {
            RectangleF shadowClip = new RectangleF(_clip.X + 1, _clip.Y + 1, _clip.Width, _clip.Height);

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            if (_dropShadow)
                g.DrawString(_text, _textFont, Brushes.Black, shadowClip, _strFormat);

            g.DrawString(_text, _textFont, _brush, _clip, _strFormat);

            return g;
        }

        private void btnLoadBackgroundVideo_Click(object sender, EventArgs e)
        {
            Stream myStream;
            ResizeFilter _res = new ResizeFilter();

            if (openDialogJpg.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = openDialogJpg.OpenFile()) != null)
                {
                    foreImage = Image.FromStream(myStream);
                    myStream.Close();
                    Render();
                }

            }
        }

        private void tbText_TextChanged(object sender, EventArgs e)
        {
            Render();
        }

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
            this.toolStripProgressBar.Value = (e.ProgressPercentage > 99) ? 99 : e.ProgressPercentage;
            //toolStripStatusLabel1.Text = e.ProgressPercentage.ToString();
        }

        private void bwVideoBackground_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ResizeFilter _res = new ResizeFilter();

            _res.KeepAspectRatio = true;
            _res.Width = coverPic.Width;
            _res.Height = coverPic.Height;

            if (e.Result != null)
            {
                //p_theme.BackgroundVideo.Foreground.Image = _res.ExecuteFilter((Image)e.Result, p_theme.BackgroundVideo.Foreground.Width, p_theme.BackgroundVideo.Foreground.Height);
                foreImage = _res.ExecuteFilter((Image)e.Result, iconeMask.Width, iconeMask.Height);
                this.toolStripProgressBar.Value = 99;

                Render();
                this.toolStripProgressBar.Value = 0;

            }
        }
        #endregion

        private void btnGetBackground_Click(object sender, EventArgs e)
        {
            bwVideoBackground.RunWorkerAsync();
        }

        private void tbBackgroundVideoUrl_DoubleClick(object sender, EventArgs e)
        {
            tbBackgroundVideoUrl.Text = @"http://www.kinomax.fr/images/Arnaud/S-Z/PosterTronLegacy.jpg";
        }

        private void tbBackgroundVideoUrl_Click(object sender, EventArgs e)
        {
            tbBackgroundVideoUrl.SelectAll();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("IExplore.exe", "http://www.google.com/images?hl=fr&site=&q=" + tbGoogleSearch.Text.Replace(" ", "+"));
        }

        private void tbGoogleSearch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnSearch.PerformClick();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void tbBackgroundVideoUrl_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnGetBackground.PerformClick();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //finalImage = new  Bitmap(coverPic.Image);
            //this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            foreImage = global::EzTvix.Theme.Default.Alpha;
            Render();
        }

    }
}
