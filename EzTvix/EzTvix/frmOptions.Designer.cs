namespace EzTvix
{
    partial class frmOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOptions));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupbox1 = new System.Windows.Forms.GroupBox();
            this.cbAutoRenameMovie = new System.Windows.Forms.CheckBox();
            this.cbAutoFilterMovieName = new System.Windows.Forms.CheckBox();
            this.cbStoreLastPathFolder = new System.Windows.Forms.CheckBox();
            this.lbDefaultAPI = new System.Windows.Forms.ComboBox();
            this.groupBoxAPI = new System.Windows.Forms.GroupBox();
            this.rbEnglish = new System.Windows.Forms.RadioButton();
            this.rbFrench = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.lbDefaultPlayer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxTheme = new System.Windows.Forms.GroupBox();
            this.cbUseFanart = new System.Windows.Forms.CheckBox();
            this.themeComboBox = new System.Windows.Forms.ComboBox();
            this.winToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupbox1.SuspendLayout();
            this.groupBoxAPI.SuspendLayout();
            this.groupBoxTheme.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(222, 210);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&Ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(303, 210);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupbox1
            // 
            this.groupbox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupbox1.Controls.Add(this.cbAutoRenameMovie);
            this.groupbox1.Controls.Add(this.cbAutoFilterMovieName);
            this.groupbox1.Controls.Add(this.cbStoreLastPathFolder);
            this.groupbox1.Location = new System.Drawing.Point(12, 12);
            this.groupbox1.Name = "groupbox1";
            this.groupbox1.Size = new System.Drawing.Size(159, 192);
            this.groupbox1.TabIndex = 2;
            this.groupbox1.TabStop = false;
            // 
            // cbAutoRenameMovie
            // 
            this.cbAutoRenameMovie.AutoSize = true;
            this.cbAutoRenameMovie.Location = new System.Drawing.Point(6, 65);
            this.cbAutoRenameMovie.Name = "cbAutoRenameMovie";
            this.cbAutoRenameMovie.Size = new System.Drawing.Size(117, 17);
            this.cbAutoRenameMovie.TabIndex = 2;
            this.cbAutoRenameMovie.Text = "Auto rename movie";
            this.winToolTip.SetToolTip(this.cbAutoRenameMovie, "If checked, the movie title will be changed with the movie found.");
            this.cbAutoRenameMovie.UseVisualStyleBackColor = true;
            // 
            // cbAutoFilterMovieName
            // 
            this.cbAutoFilterMovieName.AutoSize = true;
            this.cbAutoFilterMovieName.Location = new System.Drawing.Point(6, 42);
            this.cbAutoFilterMovieName.Name = "cbAutoFilterMovieName";
            this.cbAutoFilterMovieName.Size = new System.Drawing.Size(130, 17);
            this.cbAutoFilterMovieName.TabIndex = 1;
            this.cbAutoFilterMovieName.Text = "Auto filter movie name";
            this.winToolTip.SetToolTip(this.cbAutoFilterMovieName, "If checked, the movie name will be filtered to remove non consistant values such " +
                    "as \"dvdrip\", \"xvid\", \"screener\" or any ripper name.");
            this.cbAutoFilterMovieName.UseVisualStyleBackColor = true;
            // 
            // cbStoreLastPathFolder
            // 
            this.cbStoreLastPathFolder.AutoSize = true;
            this.cbStoreLastPathFolder.Location = new System.Drawing.Point(6, 19);
            this.cbStoreLastPathFolder.Name = "cbStoreLastPathFolder";
            this.cbStoreLastPathFolder.Size = new System.Drawing.Size(124, 17);
            this.cbStoreLastPathFolder.TabIndex = 0;
            this.cbStoreLastPathFolder.Text = "Start with last folder?";
            this.cbStoreLastPathFolder.UseVisualStyleBackColor = true;
            // 
            // lbDefaultAPI
            // 
            this.lbDefaultAPI.FormattingEnabled = true;
            this.lbDefaultAPI.Items.AddRange(new object[] {
            "XBMCPassion"});
            this.lbDefaultAPI.Location = new System.Drawing.Point(63, 19);
            this.lbDefaultAPI.Name = "lbDefaultAPI";
            this.lbDefaultAPI.Size = new System.Drawing.Size(131, 21);
            this.lbDefaultAPI.TabIndex = 1;
            // 
            // groupBoxAPI
            // 
            this.groupBoxAPI.Controls.Add(this.rbEnglish);
            this.groupBoxAPI.Controls.Add(this.rbFrench);
            this.groupBoxAPI.Controls.Add(this.label2);
            this.groupBoxAPI.Controls.Add(this.lbDefaultPlayer);
            this.groupBoxAPI.Controls.Add(this.label1);
            this.groupBoxAPI.Controls.Add(this.lbDefaultAPI);
            this.groupBoxAPI.Location = new System.Drawing.Point(177, 12);
            this.groupBoxAPI.Name = "groupBoxAPI";
            this.groupBoxAPI.Size = new System.Drawing.Size(200, 114);
            this.groupBoxAPI.TabIndex = 3;
            this.groupBoxAPI.TabStop = false;
            this.groupBoxAPI.Text = "API";
            // 
            // rbEnglish
            // 
            this.rbEnglish.AutoSize = true;
            this.rbEnglish.Location = new System.Drawing.Point(101, 81);
            this.rbEnglish.Name = "rbEnglish";
            this.rbEnglish.Size = new System.Drawing.Size(59, 17);
            this.rbEnglish.TabIndex = 6;
            this.rbEnglish.TabStop = true;
            this.rbEnglish.Text = "English";
            this.rbEnglish.UseVisualStyleBackColor = true;
            // 
            // rbFrench
            // 
            this.rbFrench.AutoSize = true;
            this.rbFrench.Location = new System.Drawing.Point(9, 81);
            this.rbFrench.Name = "rbFrench";
            this.rbFrench.Size = new System.Drawing.Size(58, 17);
            this.rbFrench.TabIndex = 5;
            this.rbFrench.TabStop = true;
            this.rbFrench.Text = "French";
            this.rbFrench.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Player :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbDefaultPlayer
            // 
            this.lbDefaultPlayer.FormattingEnabled = true;
            this.lbDefaultPlayer.Location = new System.Drawing.Point(63, 47);
            this.lbDefaultPlayer.Name = "lbDefaultPlayer";
            this.lbDefaultPlayer.Size = new System.Drawing.Size(131, 21);
            this.lbDefaultPlayer.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Movie :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBoxTheme
            // 
            this.groupBoxTheme.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxTheme.Controls.Add(this.cbUseFanart);
            this.groupBoxTheme.Controls.Add(this.themeComboBox);
            this.groupBoxTheme.Location = new System.Drawing.Point(178, 132);
            this.groupBoxTheme.Name = "groupBoxTheme";
            this.groupBoxTheme.Size = new System.Drawing.Size(200, 72);
            this.groupBoxTheme.TabIndex = 4;
            this.groupBoxTheme.TabStop = false;
            this.groupBoxTheme.Text = "Theme";
            // 
            // cbUseFanart
            // 
            this.cbUseFanart.AutoSize = true;
            this.cbUseFanart.Location = new System.Drawing.Point(8, 48);
            this.cbUseFanart.Name = "cbUseFanart";
            this.cbUseFanart.Size = new System.Drawing.Size(84, 17);
            this.cbUseFanart.TabIndex = 1;
            this.cbUseFanart.Text = "UseFanart ?";
            this.winToolTip.SetToolTip(this.cbUseFanart, "If checked, this will allow the user to choose a fanart.");
            this.cbUseFanart.UseVisualStyleBackColor = true;
            // 
            // themeComboBox
            // 
            this.themeComboBox.FormattingEnabled = true;
            this.themeComboBox.Location = new System.Drawing.Point(7, 20);
            this.themeComboBox.Name = "themeComboBox";
            this.themeComboBox.Size = new System.Drawing.Size(187, 21);
            this.themeComboBox.TabIndex = 0;
            // 
            // frmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 245);
            this.Controls.Add(this.groupBoxTheme);
            this.Controls.Add(this.groupBoxAPI);
            this.Controls.Add(this.groupbox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOptions";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options...";
            this.Load += new System.EventHandler(this.frmOptions_Load);
            this.groupbox1.ResumeLayout(false);
            this.groupbox1.PerformLayout();
            this.groupBoxAPI.ResumeLayout(false);
            this.groupBoxAPI.PerformLayout();
            this.groupBoxTheme.ResumeLayout(false);
            this.groupBoxTheme.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupbox1;
        private System.Windows.Forms.CheckBox cbStoreLastPathFolder;
        private System.Windows.Forms.ComboBox lbDefaultAPI;
        private System.Windows.Forms.GroupBox groupBoxAPI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbAutoFilterMovieName;
        private System.Windows.Forms.GroupBox groupBoxTheme;
        private System.Windows.Forms.ComboBox themeComboBox;
        private System.Windows.Forms.CheckBox cbAutoRenameMovie;
        private System.Windows.Forms.ToolTip winToolTip;
        private System.Windows.Forms.CheckBox cbUseFanart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox lbDefaultPlayer;
        private System.Windows.Forms.RadioButton rbEnglish;
        private System.Windows.Forms.RadioButton rbFrench;
    }
}