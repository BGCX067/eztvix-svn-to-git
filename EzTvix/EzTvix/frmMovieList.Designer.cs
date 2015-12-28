namespace EzTvix
{
    partial class frmMovieList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMovieList));
            RootKit.Windows.Forms.ImageListViewColor imageListViewColor1 = new RootKit.Windows.Forms.ImageListViewColor();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.imageListViewMovie = new RootKit.Windows.Forms.ImageListView();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icon_128.png");
            this.imageList1.Images.SetKeyName(1, "Alpha_128.png");
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(445, 262);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(526, 262);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 288);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(613, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // imageListViewMovie
            // 
            this.imageListViewMovie.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            imageListViewColor1.BackColor = System.Drawing.SystemColors.Window;
            imageListViewColor1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            imageListViewColor1.CellForeColor = System.Drawing.SystemColors.ControlText;
            imageListViewColor1.ColumnHeaderBackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            imageListViewColor1.ColumnHeaderBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            imageListViewColor1.ColumnHeaderForeColor = System.Drawing.SystemColors.WindowText;
            imageListViewColor1.ColumnHeaderHoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            imageListViewColor1.ColumnHeaderHoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            imageListViewColor1.ColumnSelectColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            imageListViewColor1.ColumnSeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            imageListViewColor1.ControlBackColor = System.Drawing.SystemColors.Window;
            imageListViewColor1.ForeColor = System.Drawing.SystemColors.ControlText;
            imageListViewColor1.HoverBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            imageListViewColor1.HoverColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            imageListViewColor1.HoverColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            imageListViewColor1.ImageInnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            imageListViewColor1.ImageOuterBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            imageListViewColor1.InsertionCaretColor = System.Drawing.SystemColors.Highlight;
            imageListViewColor1.PaneBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            imageListViewColor1.PaneLabelColor = System.Drawing.SystemColors.GrayText;
            imageListViewColor1.PaneSeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            imageListViewColor1.SelectedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            imageListViewColor1.SelectedColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            imageListViewColor1.SelectedColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            imageListViewColor1.SelectionRectangleBorderColor = System.Drawing.SystemColors.Highlight;
            imageListViewColor1.SelectionRectangleColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            imageListViewColor1.SelectionRectangleColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))), ((int)(((byte)(205)))));
            imageListViewColor1.UnFocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            imageListViewColor1.UnFocusedColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            imageListViewColor1.UnFocusedColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.imageListViewMovie.Colors = imageListViewColor1;
            this.imageListViewMovie.DefaultImage = ((System.Drawing.Image)(resources.GetObject("imageListViewMovie.DefaultImage")));
            this.imageListViewMovie.ErrorImage = ((System.Drawing.Image)(resources.GetObject("imageListViewMovie.ErrorImage")));
            this.imageListViewMovie.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imageListViewMovie.Location = new System.Drawing.Point(12, 12);
            this.imageListViewMovie.Name = "imageListViewMovie";
            this.imageListViewMovie.Size = new System.Drawing.Size(589, 244);
            this.imageListViewMovie.TabIndex = 4;
            this.imageListViewMovie.Text = "";
            this.imageListViewMovie.ThumbnailSize = new System.Drawing.Size(96, 100);
            this.imageListViewMovie.SelectionChanged += new System.EventHandler(this.movieListView_SelectedIndexChanged);
            this.imageListViewMovie.DoubleClick += new System.EventHandler(this.movieListView_DoubleClick);
            // 
            // frmMovieList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 310);
            this.Controls.Add(this.imageListViewMovie);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(623, 340);
            this.Name = "frmMovieList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Movies... ";
            this.Load += new System.EventHandler(this.frmMovieList_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private RootKit.Windows.Forms.ImageListView imageListViewMovie;
    }
}