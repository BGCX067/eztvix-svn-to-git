using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using EzTvix.Core;
using EzTvix.Provider;
using EzTvix.Theme;
using RootKit.API.Providers;
using RootKit.Core;
using RootKit.Drawings;
using RootKit.Drawings.BasicFilters;
using RootKit.Web;
using RootKit.Windows.Forms;
using EzTvix.Provider.API;

namespace EzTvix
{

    public partial class MainForm : Form
    {

        private ThemeTemplate p_theme = new ThemeTemplate();
        private String p_folderIconeName = "folder.png";
        private String p_folderIconeBackupName = "folder.bck";
        private String p_folderBackgroundName = "tvix.jpg";
        private String p_folderXmlFileName = "folder.xml";
        //private System.ComponentModel.BackgroundWorker bgwCode;
        private List<Movie> movieList;
        private Movies newMovieList = new Movies();
        private string folderName;
        private bool fileOpened = false;
        private string pCurrentPath = "";
        private string pFileName = "";
        private Movie currentMovie = new Movie();


        private String CurrentFolderPath
        {
            get
            {
                return ((TreeNodePath)((folderBrowser).SelectedNode)).Path + "\\";
            }
        }


        public MainForm()
        {
            InitializeComponent();
            p_theme.Name = TvixInfo.Theme;
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show the FolderBrowserDialog.
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                folderName = folderBrowserDialog1.SelectedPath;
                if (!fileOpened)
                {
                    // No file is opened, bring up openFileDialog in selected path.
                    //openFileDialog1.InitialDirectory = folderName;
                    //openFileDialog1.FileName = null;
                    //openMenuItem.PerformClick();
                }
            }


        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                FileStream stream = new System.IO.FileStream("list", FileMode.OpenOrCreate);
                try
                {
                    BinaryFormatter binary = new BinaryFormatter();
                    this.folderBrowser.SelectedDirectories = binary.Deserialize(stream) as System.Collections.Specialized.StringCollection;
                }
                catch { }
                stream.Close();
                //
                this.folderBrowser.DriveTypes = DriveTypes.LocalDisk | DriveTypes.NetworkDrive;

                this.folderBrowser.DataSource = new TreeViewFolderBrowserDataProvider();
                this.folderBrowser.RootFolder = System.Environment.SpecialFolder.MyComputer;
                //
                this.folderBrowser.Populate(System.Environment.SpecialFolder.MyComputer);
                
                //this.folderBrowser.ShowFolder("m:\\video\\hd", true);
                string _path = TvixInfo.RootFolder + TvixInfo.LastPathFolder;
                this.folderBrowser.ShowFolder(_path, true);
                this.btnSearch.NoteText = "using [" + TvixInfo.Provider + "] provider ";
                //
                //ShowSelectedDirs();
            }
            //
            Theme_Changed();

            //base.OnLoad(e);
            System.ComponentModel.BackgroundWorker bgwCode;
            bgwCode = new BackgroundWorker();

            bgwCode.WorkerReportsProgress = true;
            bgwCode.WorkerSupportsCancellation = true;

            bgwCode.DoWork += new DoWorkEventHandler(bgwCode_DoWork);
            bgwCode.RunWorkerAsync();
            PlayerList myPlayer = new PlayerList();
            myPlayer.LoadThemeData();
            //myPlayer.LoadXml();
            //ThemeInfo.VideoBackgroundExtention = myPlayer.Items[TvixInfo.PlayerType].BackgroundExtension;
            //ThemeInfo.VideoBackgroundName = myPlayer.Items[TvixInfo.PlayerType].BackgroundName;
            //ThemeInfo.UpFolderIconExtention = myPlayer.Items[TvixInfo.PlayerType].UpFolderExtension;
            //ThemeInfo.UpFolderIconName = myPlayer.Items[TvixInfo.PlayerType].UpFolderName;
            //ThemeInfo.FolderIconExtention = myPlayer.Items[TvixInfo.PlayerType].FolderExtension;
            //ThemeInfo.FolderIconName = myPlayer.Items[TvixInfo.PlayerType].FolderName;
            //ThemeInfo.UpFolderExist = myPlayer.Items[TvixInfo.PlayerType].UpFolderExist;
            //ThemeInfo.VideoBoxExtention = myPlayer.Items[TvixInfo.PlayerType].FolderExtension;
            //ThemeInfo.VideoBoxName = myPlayer.Items[TvixInfo.PlayerType].FolderName;
 
        }

        private void Theme_Changed()
        {
            coverPic.BackgroundImage = p_theme[ItemType.ItemBack];
            toolStripThemeLabel.Text = p_theme.Name;
        }
        private void bgwCode_DoWork(object sender, DoWorkEventArgs e)
        {
            DownloadManagerThreaded downMan = new DownloadManagerThreaded();
            BackgroundWorker worker = sender as BackgroundWorker;
            Image img = downMan.DownloadFromUrl("http://www.eztvix.info/Alpha.png",
                "There is a problem connecting to Internet. \r\nSome functions will not work properly!",
                worker, e);

            e.Result = img;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ImageManager imgMan = new ImageManager();
            Image _localImage;

            Font txtFont = new Font("Segoe UI", 10, FontStyle.Bold | FontStyle.Italic);

            _localImage = imgMan.DownloadFromUrl3("http://www.movieposterdb.com/posters/10_03/2010/1104001/l_1104001_53855dc2.jpg");
            _localImage = imgMan.resizeImage(_localImage, new Size(200, 200));
            coverPic.Image = (Image)imgMan.DrawText(new Bitmap(_localImage), "Tron", txtFont);

        }

        private void folderBrowser_AfterSelect(object sender, TreeViewEventArgs e)
        {
            PopulateFileListbox();
        }

        private void PopulateFileListbox()
        {
            //folderBrowser
            Stopwatch watch = new Stopwatch();
            String DirectoryPath = ((TreeNodePath)(folderBrowser.SelectedNode)).Path;

            // Get the (custom) directory specified by the user
            DirectoryInfo dirCustom = new DirectoryInfo(DirectoryPath);

            // We'll store the file list here
            FileInfo[] filCustom;

            filCustom = dirCustom.GetFilesWithPattern("*.mkv,*.avi,*.mov,*.iso");
            listBoxFile.Items.Clear();

            // Loop through the file list
            foreach (FileInfo filFile in filCustom)
            {
                // Write to the big textbox
                listBoxFile.Items.Add(filFile);
            }

            loadFolderPic(DirectoryPath + "\\");
            loadBackgroundPic(DirectoryPath + "\\");
            tbMovieName.Text = "";

        }
        private void listBoxFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            FileSelected();
        }


        private void FileSelected()
        {
            if (listBoxFile.SelectedIndex < 0)
                return;
            //pCurrentPath = CurrentFolderPath.Replace("\\\\", "\\");
            pFileName = listBoxFile.SelectedItem.ToString();
            //String _fullPath = CurrentFolderPath.Replace("\\\\", "\\") + pFileName;
            
            try
            {
                tbMovieName.Text = this.FilterMovieName(pFileName);
            }
            catch (Exception ex)
            { }
        }
        private string FilterMovieName(string filename)
        {
            string tempFileName = filename;
            //Stopwatch timer = new Stopwatch();
            //timer.Start();
            if (TvixInfo.AutoFilterMovieName)
            {
                tempFileName = tempFileName.Replace(" ", ".").FilterMovieName();
                tempFileName = tempFileName.RemoveFileExtension("iso,mkv,avi,mov").Replace(".", " ");
                tempFileName = tempFileName.ToTitleCase(true);
            }
            //timer.Stop();
            return tempFileName;
        }
        /// <summary>
        /// Chargment de l'image de la pochette
        /// </summary>
        /// <param name="_folderPath">Path venant du FolderTreeview</param>
        private void loadFolderPic(String _folderPath)
        {
            Graphics _graphicContainer;
            Image _imgCover, _imgFinal;

            try
            {
                //chargement du cover (si non existant, exception "FileNotFoundException"
                _imgCover = FromStream(_folderPath.Replace("\\\\", "\\") + ThemeInfo.VideoBoxFullName);

                _imgFinal = global::EzTvix.Theme.Default.Empty;
                _graphicContainer = Graphics.FromImage(_imgFinal);
                _graphicContainer.DrawImageUnscaledAndClipped(_imgCover, new Rectangle(0, 0, 200, 200));

                coverPic.Image = _imgFinal;
                _graphicContainer.Dispose();
            }
            catch (FileNotFoundException e)
            {
                // empty image par default
                coverPic.Image = ThemeInfo.FolderBack;
            }
            catch (Exception ex)
            {
            }
        }

        private Image FromStream(string _imagePath)
        {
            FileStream fs = new FileStream(_imagePath, FileMode.Open);
            Image imgPhoto = Image.FromStream(fs);
            fs.Close();
            fs.Dispose();
            return imgPhoto;
        }

        /// <summary>
        /// Chargment de l'image de fond
        /// </summary>
        /// <param name="_folderPath">Path venant du FolderTreeview</param>
        private void loadBackgroundPic(String _folderPath)
        {
            ImageManager _imgMan = new ImageManager();
            try
            {
                // définition du resize
                ResizeFilter _resizeFilter = new ResizeFilter();

                _resizeFilter.Width = BackgroundBigPic.Width;
                _resizeFilter.Height = BackgroundBigPic.Height;
                _resizeFilter.KeepAspectRatio = false;

                //using (Image newPic = Image.FromFile(savedFileName))
                //{
                //    newPic.Save(tempFileName, encoders[1], codecParams);
                //}
                // chargement et resize de l'image
//                BackgroundBigPic.Image = _resizeFilter.ExecuteFilter(FromStream(_folderPath.Replace("\\\\", "\\") + p_folderBackgroundName));
                BackgroundBigPic.Image = _resizeFilter.ExecuteFilter(FromStream(_folderPath.Replace("\\\\", "\\") + ThemeInfo.VideoBackgroundFullName));

                //_resizeFilter.Width = 200;
                //_resizeFilter.Height = 133;

                //// chargement et resize de l'image
                //BackgroundPic.Image = _resizeFilter.ExecuteFilter(FromStream(_folderPath.Replace("\\\\", "\\") + p_folderBackgroundName));
            }
            catch (FileNotFoundException e)
            {
                // empty image par default
                BackgroundBigPic.Image = global::EzTvix.Theme.Default.Alpha;
            }
            catch (Exception ex)
            {
                // Nothing to do
            }
        }

        private void BackgroundBigPic_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                currentMovie.LoadFromXml(CurrentFolderPath);

                frmCoverList myCoverList = new frmCoverList();
                myCoverList.selectedMovie = currentMovie;
                if (myCoverList.ShowDialog() == DialogResult.OK)
                {
                    myCoverList.selectedMovie.SelectedCover = myCoverList.selectedCover;
                    if (TvixInfo.UseFanart)
                    {
                        frmFanartList myFanartList = new frmFanartList();
                        myFanartList.selectedMovie = myCoverList.selectedMovie;
                        if (myFanartList.ShowDialog() == DialogResult.OK)
                        {

                        }
                    }
                    UpdateMovieBackground(myCoverList.selectedMovie);
                }
            }
            catch (FileNotFoundException ex)
            {
                //nothing to do... file "info.xml" not found...
            }

            //ImageManager _imgMan = new ImageManager();
            //String _folderPath = ((TreeNodePath)((folderBrowser).SelectedNode)).Path + "\\";

            //// définition du resize
            //ResizeFilter _resizeFilter = new ResizeFilter();

            //if (BackgroundBigPic.Width == 720)
            //{
            //    BackgroundBigPic.Width = 360;
            //    BackgroundBigPic.Height = 202;

            //}
            //else
            //{
            //    BackgroundBigPic.Width = 720;
            //    BackgroundBigPic.Height = 405;
            //}
            //_resizeFilter.Width = BackgroundBigPic.Width;
            //_resizeFilter.Height = BackgroundBigPic.Height;
            //_resizeFilter.KeepAspectRatio = false;

            //// chargement et resize de l'image
            //BackgroundBigPic.Image = _resizeFilter.ExecuteFilter(FromStream(_folderPath.Replace("\\\\", "\\") + p_folderBackgroundName));
        }

        private void getMovies(string search)
        {
            frmMovieList myMovieList = new frmMovieList();
            myMovieList.SearchString = search;
            if (myMovieList.ShowDialog() == DialogResult.OK)
            {
                if (myMovieList.selectedMovie.Posters.Count > 0)
                {
                    frmCoverList myCoverList = new frmCoverList();
                    myCoverList.selectedMovie = myMovieList.selectedMovie;
                    if (myCoverList.ShowDialog() == DialogResult.OK)
                    {
                        myCoverList.selectedMovie.SelectedCover = myCoverList.selectedCover;
                        if (TvixInfo.UseFanart)
                        {
                            frmFanartList myFanartList = new frmFanartList();
                            myFanartList.selectedMovie = myCoverList.selectedMovie;
                            
                            if (myFanartList.ShowDialog() == DialogResult.OK)
                            {

                            }
                        }
                        UpdateMovieBackground(myCoverList.selectedMovie);
                    }
                    myCoverList.Dispose();
                }
                else
                {
                    MovieProvider provider = TvixInfo.MovieProvider;

                    provider.Language = TvixInfo.Language;
                    
                    provider.GetData(myMovieList.selectedMovie);
                    myMovieList.selectedMovie.Cover = p_theme[ItemType.VideoCover].Background.Image;

                    p_theme[ItemType.VideoBox].Background.Image = myMovieList.selectedMovie.Cover;
                    p_theme.ApplyBackgroundVideo(myMovieList.selectedMovie, CurrentFolderPath);
                    if (TvixInfo.AutoRenameMovie)
                        RenameMovie(myMovieList.selectedMovie.Title);
                    coverPic.Image = p_theme[ItemType.VideoBox];
                    //BackgroundBigPic.Image
                    loadFolderPic(CurrentFolderPath);
                    loadBackgroundPic(CurrentFolderPath);
                    provider.Dispose();
                }
            }
            myMovieList.Dispose();

        }

        private void UpdateMovieBackground(Movie movie, bool saveXml = true)
        {
            //movie.SelectedCover = myCoverList.selectedCover;
            p_theme[ItemType.VideoBox].Background.Image = movie.Cover;
            //p_theme[ItemType.BackgroundVideo].
            p_theme.ApplyBackgroundVideo(movie, CurrentFolderPath, saveXml);
            if (TvixInfo.AutoRenameMovie)
                RenameMovie(movie.Title);
            coverPic.Image = p_theme[ItemType.VideoBox];
           
            loadFolderPic(CurrentFolderPath);
            loadBackgroundPic(CurrentFolderPath);
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (tbMovieName.Text.Trim().Length > 0)
                getMovies(tbMovieName.Text);
            else
                MessageBox.Show("No Movie Name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void tbMovieName_DoubleClick(object sender, EventArgs e)
        {
            tbMovieName.Text = this.FilterMovieName(tbMovieName.Text);
        }

        #region *** Menu Events ***
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOptions options = new frmOptions();
            if (options.ShowDialog() == DialogResult.OK)
            {
                p_theme.Name = TvixInfo.Theme;
                //coverPic.BackgroundImage = p_theme[ItemType.ItemBack].Image;
                Theme_Changed();
            }
        }
        private void themeBuilderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThemeBuilder themeBuilder = new ThemeBuilder();
            if (themeBuilder.ShowDialog() == DialogResult.OK)
            {
            }
        }

        #endregion

        #region *** Groupbox Movie ***
        private void btnRenameMovie_Click(object sender, EventArgs e)
        {
            RenameMovie(tbMovieName.Text.Trim());
        }

        private void RenameMovie(String movieName)
        {
            FileInfo _fileInfo = (FileInfo)listBoxFile.SelectedItem;
            movieName = Regex.Replace(movieName, "[/|:|?|*|\"|<|>]", " - ");
            movieName = movieName.Replace("\\", " - ");
            if (_fileInfo == null || tbMovieName.Text.Trim().Length <= 0) return;

            string src = _fileInfo.FullName;
            string temp = _fileInfo.DirectoryName + @"\" + Guid.NewGuid().ToString() + movieName + _fileInfo.Extension;
            string dest = _fileInfo.DirectoryName + @"\" + movieName + _fileInfo.Extension;
            File.Move(src, temp);
            File.Move(temp, dest);
            //File.Move(src, dest);

            _fileInfo = new FileInfo(dest);

            PopulateFileListbox();
            listBoxFile.SelectedIndex = listBoxFile.FindString(_fileInfo.Name);
        }

        private void tbMovieName_TextChanged(object sender, EventArgs e)
        {
            btnRenameMovie.Enabled = !(((FileInfo)listBoxFile.SelectedItem) == null || tbMovieName.Text.Trim().Length <= 0);
        }

        private void btnMoveToFolder_Click(object sender, EventArgs e)
        {
            FileInfo _fileInfo = (FileInfo)listBoxFile.SelectedItem;

            if (_fileInfo == null || tbMovieName.Text.Trim().Length <= 0)
                return;

            DirectoryInfo newDirInfo = _fileInfo.Directory.CreateSubdirectory(tbMovieName.Text.Trim());

            string src = _fileInfo.FullName;
            string dest = newDirInfo.FullName + @"\" + tbMovieName.Text.Trim() + _fileInfo.Extension;
            File.Move(src, dest);

            _fileInfo = new FileInfo(dest);

            // selection du dernier folder
            this.folderBrowser.Populate(newDirInfo.FullName);
            PopulateFileListbox();
        }


        #endregion

        private void iconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.iconsToolStripMenuItem.Checked)
            {
                this.listToolStripMenuItem.Checked = false;
                this.iconsToolStripMenuItem.Checked = true;
                p_theme.SetFolderView(CurrentFolderPath, FolderViewType.Icon);
            }
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.listToolStripMenuItem.Checked)
            {
                this.listToolStripMenuItem.Checked = true;
                this.iconsToolStripMenuItem.Checked = false;
                p_theme.SetFolderView(CurrentFolderPath, FolderViewType.List);
            }
        }

        private void contextMenuFolder_Opening(object sender, CancelEventArgs e)
        {
            if (p_theme.GetFolderView(CurrentFolderPath) == FolderViewType.Icon)
            {
                this.listToolStripMenuItem.Checked = false;
                this.iconsToolStripMenuItem.Checked = true;
            }
            else
            {
                this.listToolStripMenuItem.Checked = true;
                this.iconsToolStripMenuItem.Checked = false;
            }

        }

        private void applyThemeBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            p_theme.Apply(CurrentFolderPath);
            loadBackgroundPic(CurrentFolderPath);
            try
            {
                currentMovie.LoadFromXml(CurrentFolderPath);
                UpdateMovieBackground(currentMovie, false);
            }
            catch (FileNotFoundException ex)
            {
                //nothing to do... file "info.xml" not found...
            }
            
            
        }

        private void coverPic_DoubleClick(object sender, EventArgs e)
        {
            frmFolderIcon myFolderIcon = new frmFolderIcon();

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(CurrentFolderPath + @"/info.xml");
                myFolderIcon.foreImage = RootKit.Core.Converter.Base64ToImage(doc.SelectSingleNode("//Cover").InnerText);
            }
            catch (Exception ex) {
                myFolderIcon.foreImage = new Bitmap(coverPic.Image);
            }
            myFolderIcon.finalImage = new Bitmap(coverPic.Image);
            if (myFolderIcon.ShowDialog() == DialogResult.OK)
            {
                coverPic.Image = myFolderIcon.finalImage;
                p_theme.SaveFolderIcon(coverPic.Image, CurrentFolderPath);
            }

        }

        private void tbMovieName_KeyUp(object sender, KeyEventArgs e)
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

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAbout frmAbout = new frmAbout();

            frmAbout.ShowDialog();
        }



    }
}
