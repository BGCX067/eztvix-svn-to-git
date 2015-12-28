using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EzTvix.Core;
using EzTvix.Theme;
using System.IO;

namespace EzTvix
{
    public partial class frmOptions : Form
    {
        private ThemeTemplate p_theme = new ThemeTemplate();
        
        public frmOptions()
        {
            InitializeComponent();
        }

        private void frmOptions_Load(object sender, EventArgs e)
        {
            DirectoryInfo[] themeList = p_theme.getThemeList();
            themeComboBox.Items.Add("Default");
            foreach (DirectoryInfo theme in themeList)
            {
                themeComboBox.Items.Add(theme.Name);
            }
            themeComboBox.SelectedItem = TvixInfo.Theme;

            PlayerList myList = new PlayerList();
            myList.LoadXml();
            lbDefaultPlayer.ValueMember = "Name";
            lbDefaultPlayer.DisplayMember = "Label";
            lbDefaultPlayer.DataSource = myList.List;
            lbDefaultPlayer.SelectedValue = TvixInfo.PlayerType;

            cbStoreLastPathFolder.Checked = TvixInfo.StoreLastPathFolder;
            cbAutoFilterMovieName.Checked = TvixInfo.AutoFilterMovieName;
            cbAutoRenameMovie.Checked = TvixInfo.AutoRenameMovie;
            cbUseFanart.Checked = TvixInfo.UseFanart;
            rbEnglish.Checked = (TvixInfo.Language.ToUpper() == "EN");
            rbFrench.Checked = (TvixInfo.Language.ToUpper() == "FR");

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            TvixInfo.StoreLastPathFolder = cbStoreLastPathFolder.Checked;
            TvixInfo.AutoFilterMovieName = cbAutoFilterMovieName.Checked;
            TvixInfo.AutoRenameMovie = cbAutoRenameMovie.Checked;
            TvixInfo.UseFanart = cbUseFanart.Checked; 
            TvixInfo.Theme = themeComboBox.Text;
            TvixInfo.PlayerType = lbDefaultPlayer.SelectedValue.ToString();
            TvixInfo.Language = (rbFrench.Checked) ? "FR" : "EN";

            PlayerList myPlayer = new PlayerList();
            myPlayer.LoadThemeData();
        }
    }
}
