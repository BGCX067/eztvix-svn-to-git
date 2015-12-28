using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Shell;
using System.IO;
using EzTvix.Core;

namespace EzTvix
{
    public partial class frmSaveThemeAs : Form
    {
        public string NewThemeName = "";
        private bool _CancelClose = false;

        public frmSaveThemeAs()
        {
            InitializeComponent();
        }

        private void frmSaveThemeAs_Load(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(TvixInfo.ThemeFolder + @"\" + textBoxName.Text))
            {
                NewThemeName = textBoxName.Text;
            }
            else
            {
                MessageBox.Show("Theme already exists!");
                _CancelClose = true;
            }
        }

        private void frmSaveThemeAs_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = _CancelClose;
            _CancelClose = false;
        }

    }
}
