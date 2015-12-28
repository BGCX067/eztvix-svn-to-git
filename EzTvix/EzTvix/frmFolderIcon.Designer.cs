namespace EzTvix
{
    partial class frmFolderIcon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFolderIcon));
            this.coverPic = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbText = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbAudio = new System.Windows.Forms.RadioButton();
            this.rbVideo = new System.Windows.Forms.RadioButton();
            this.rbFolder = new System.Windows.Forms.RadioButton();
            this.rbNone = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tbGoogleSearch = new System.Windows.Forms.TextBox();
            this.btnLoadBackgroundVideo = new System.Windows.Forms.Button();
            this.tbBackgroundVideoUrl = new System.Windows.Forms.TextBox();
            this.btnGetBackground = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.openDialogJpg = new System.Windows.Forms.OpenFileDialog();
            this.bwVideoBackground = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.coverPic)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // coverPic
            // 
            this.coverPic.Location = new System.Drawing.Point(6, 19);
            this.coverPic.Name = "coverPic";
            this.coverPic.Size = new System.Drawing.Size(200, 200);
            this.coverPic.TabIndex = 1;
            this.coverPic.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.coverPic);
            this.groupBox2.Location = new System.Drawing.Point(217, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(213, 227);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Preview";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbText);
            this.groupBox3.Location = new System.Drawing.Point(13, 249);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(336, 57);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Icon Text";
            // 
            // tbText
            // 
            this.tbText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbText.Location = new System.Drawing.Point(6, 23);
            this.tbText.Name = "tbText";
            this.tbText.Size = new System.Drawing.Size(323, 20);
            this.tbText.TabIndex = 0;
            this.tbText.TextChanged += new System.EventHandler(this.tbText_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rbAudio);
            this.groupBox1.Controls.Add(this.rbVideo);
            this.groupBox1.Controls.Add(this.rbFolder);
            this.groupBox1.Controls.Add(this.rbNone);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(198, 73);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Background";
            // 
            // rbAudio
            // 
            this.rbAudio.AutoSize = true;
            this.rbAudio.Location = new System.Drawing.Point(110, 44);
            this.rbAudio.Name = "rbAudio";
            this.rbAudio.Size = new System.Drawing.Size(75, 17);
            this.rbAudio.TabIndex = 3;
            this.rbAudio.Text = "&Audio icon";
            this.rbAudio.UseVisualStyleBackColor = true;
            this.rbAudio.CheckedChanged += new System.EventHandler(this.rbAudio_CheckedChanged);
            // 
            // rbVideo
            // 
            this.rbVideo.AutoSize = true;
            this.rbVideo.Location = new System.Drawing.Point(110, 20);
            this.rbVideo.Name = "rbVideo";
            this.rbVideo.Size = new System.Drawing.Size(75, 17);
            this.rbVideo.TabIndex = 2;
            this.rbVideo.Text = "&Video icon";
            this.rbVideo.UseVisualStyleBackColor = true;
            this.rbVideo.CheckedChanged += new System.EventHandler(this.rbVideo_CheckedChanged);
            // 
            // rbFolder
            // 
            this.rbFolder.AutoSize = true;
            this.rbFolder.Location = new System.Drawing.Point(7, 44);
            this.rbFolder.Name = "rbFolder";
            this.rbFolder.Size = new System.Drawing.Size(77, 17);
            this.rbFolder.TabIndex = 1;
            this.rbFolder.Text = "&Folder icon";
            this.rbFolder.UseVisualStyleBackColor = true;
            this.rbFolder.CheckedChanged += new System.EventHandler(this.rbFolder_CheckedChanged);
            // 
            // rbNone
            // 
            this.rbNone.AutoSize = true;
            this.rbNone.Checked = true;
            this.rbNone.Location = new System.Drawing.Point(7, 20);
            this.rbNone.Name = "rbNone";
            this.rbNone.Size = new System.Drawing.Size(51, 17);
            this.rbNone.TabIndex = 0;
            this.rbNone.TabStop = true;
            this.rbNone.Text = "&None";
            this.rbNone.UseVisualStyleBackColor = true;
            this.rbNone.CheckedChanged += new System.EventHandler(this.rbNone_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.btnReset);
            this.groupBox4.Controls.Add(this.btnSearch);
            this.groupBox4.Controls.Add(this.tbGoogleSearch);
            this.groupBox4.Controls.Add(this.btnLoadBackgroundVideo);
            this.groupBox4.Controls.Add(this.tbBackgroundVideoUrl);
            this.groupBox4.Controls.Add(this.btnGetBackground);
            this.groupBox4.Location = new System.Drawing.Point(13, 93);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(194, 146);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Foreground";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSearch.Location = new System.Drawing.Point(7, 117);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(181, 23);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "Search Google";
            this.buttonToolTip.SetToolTip(this.btnSearch, "Search google to find a picture, and copy/paste the link");
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tbGoogleSearch
            // 
            this.tbGoogleSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbGoogleSearch.Location = new System.Drawing.Point(7, 91);
            this.tbGoogleSearch.Name = "tbGoogleSearch";
            this.tbGoogleSearch.Size = new System.Drawing.Size(181, 20);
            this.tbGoogleSearch.TabIndex = 12;
            this.tbGoogleSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbGoogleSearch_KeyUp);
            // 
            // btnLoadBackgroundVideo
            // 
            this.btnLoadBackgroundVideo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadBackgroundVideo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLoadBackgroundVideo.Location = new System.Drawing.Point(6, 46);
            this.btnLoadBackgroundVideo.Name = "btnLoadBackgroundVideo";
            this.btnLoadBackgroundVideo.Size = new System.Drawing.Size(126, 23);
            this.btnLoadBackgroundVideo.TabIndex = 11;
            this.btnLoadBackgroundVideo.Text = "Load Image";
            this.buttonToolTip.SetToolTip(this.btnLoadBackgroundVideo, "Load an image from your Hard Drive");
            this.btnLoadBackgroundVideo.UseVisualStyleBackColor = true;
            this.btnLoadBackgroundVideo.Click += new System.EventHandler(this.btnLoadBackgroundVideo_Click);
            // 
            // tbBackgroundVideoUrl
            // 
            this.tbBackgroundVideoUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBackgroundVideoUrl.Location = new System.Drawing.Point(6, 19);
            this.tbBackgroundVideoUrl.Name = "tbBackgroundVideoUrl";
            this.tbBackgroundVideoUrl.Size = new System.Drawing.Size(126, 20);
            this.tbBackgroundVideoUrl.TabIndex = 9;
            this.tbBackgroundVideoUrl.Text = "http://";
            this.tbBackgroundVideoUrl.Click += new System.EventHandler(this.tbBackgroundVideoUrl_Click);
            this.tbBackgroundVideoUrl.DoubleClick += new System.EventHandler(this.tbBackgroundVideoUrl_DoubleClick);
            this.tbBackgroundVideoUrl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbBackgroundVideoUrl_KeyUp);
            // 
            // btnGetBackground
            // 
            this.btnGetBackground.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetBackground.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGetBackground.Location = new System.Drawing.Point(138, 17);
            this.btnGetBackground.Name = "btnGetBackground";
            this.btnGetBackground.Size = new System.Drawing.Size(50, 23);
            this.btnGetBackground.TabIndex = 10;
            this.btnGetBackground.Text = "Get";
            this.buttonToolTip.SetToolTip(this.btnGetBackground, "Load the file from a valid url.");
            this.btnGetBackground.UseVisualStyleBackColor = true;
            this.btnGetBackground.Click += new System.EventHandler(this.btnGetBackground_Click);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(355, 254);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "&Ok";
            this.btnOk.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(355, 283);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // openDialogJpg
            // 
            this.openDialogJpg.DefaultExt = "Jpg";
            this.openDialogJpg.Filter = "Jpeg Image|*.jpg|Png Image|*.png|Gif Image|*.gif|Bitmap Image|*.bmp";
            this.openDialogJpg.RestoreDirectory = true;
            // 
            // bwVideoBackground
            // 
            this.bwVideoBackground.WorkerReportsProgress = true;
            this.bwVideoBackground.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwVideoBackground_DoWork);
            this.bwVideoBackground.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwVideoBackground_ProgressChanged);
            this.bwVideoBackground.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwVideoBackground_RunWorkerCompleted);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 314);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(442, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(25, 17);
            this.toolStripStatusLabel1.Text = "      ";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(138, 45);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(50, 23);
            this.btnReset.TabIndex = 14;
            this.btnReset.Text = "&Reset";
            this.buttonToolTip.SetToolTip(this.btnReset, "Reset the forground image.");
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // frmFolderIcon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(442, 336);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFolderIcon";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Folder Icon";
            this.Load += new System.EventHandler(this.frmFolderIcon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.coverPic)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox coverPic;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbAudio;
        private System.Windows.Forms.RadioButton rbVideo;
        private System.Windows.Forms.RadioButton rbFolder;
        private System.Windows.Forms.RadioButton rbNone;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tbGoogleSearch;
        private System.Windows.Forms.Button btnLoadBackgroundVideo;
        private System.Windows.Forms.TextBox tbBackgroundVideoUrl;
        private System.Windows.Forms.Button btnGetBackground;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tbText;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.OpenFileDialog openDialogJpg;
        private System.ComponentModel.BackgroundWorker bwVideoBackground;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolTip buttonToolTip;
        private System.Windows.Forms.Button btnReset;
    }
}