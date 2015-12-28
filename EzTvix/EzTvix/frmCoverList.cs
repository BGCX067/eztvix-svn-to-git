using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EzTvix.Provider;
using EzTvix.Provider.API;
using EzTvix.Provider.Data;
using EzTvix.Core;


namespace EzTvix
{
    public partial class frmCoverList : Form
    {
        public string movieID;
        public Movie selectedMovie;
        public string selectedCover;
        private MovieProvider provider = TvixInfo.MovieProvider; 
      
        public frmCoverList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// http://objectlistview.sourceforge.net
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMovieList_Load(object sender, EventArgs e)
        {
            provider.Language = TvixInfo.Language;
            provider.RunWorkerCompleted += new ProviderWorker.RunWorkerCompletedEventHandler(provider_RunWorkerCompleted);
            //provider.ObjectLoaded += new ProviderWorker.ObjectLoadedEventHandler(provider_MovieLoaded);
            
            provider.CoverChanged += new ProviderWorker.CoverChangedEventHandler(provider_CoverProcessChanged);
            //provider.FanartChanged += new ProviderWorker.FanartChangedEventHandler(provider_FanProcessChanged); ;
            provider.GetData(this.selectedMovie);
        }
        private void provider_RunWorkerCompleted(object sender, RunWorkerCompletedProviderEventArgs e)
        {
            toolStripStatusLabel1.Text = "Time Spent loading thumbs : " + String.Format("{0:D1}s {1:D2}ms", provider.Timer.Seconds, provider.Timer.Milliseconds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void provider_MovieLoaded(object sender, ObjectLoadedProviderEventArgs e)
        {
            toolStripStatusLabel1.Text = "Movie loaded : " + ((Movie)e.loadedObject).Title ;
            this.selectedMovie = ((Movie)e.loadedObject);
        }

        public void provider_CoverProcessChanged(object sender, CoverProgressChangedProviderEventArgs e)
        {
            toolStripStatusLabel1.Text = "Loading covers : " + ((Movie)e.loadedObject).Title;

            KeyValuePair<String, MoviePicture> kvp = ((Movie)e.loadedObject).Posters.Last();

            //reinit de lobjet movie avec le nouveau cover chargé
            this.selectedMovie = ((Movie)e.loadedObject);
            this.imageListViewCover.Items.Add("Dummy.Key", kvp.Key, kvp.Value.Thumb, e.loadedObject);

        }
        public void provider_FanProcessChanged(object sender, FanartProgressChangedProviderEventArgs e)
        {
            toolStripStatusLabel1.Text = "Covers loaded : " + ((Movie)e.loadedObject).Title + " - Pre-loading fanarts...";
        }

        private void imageListViewCover_SelectionChanged(object sender, EventArgs e)
        {
            if (imageListViewCover.SelectedItems.Count > 0)
            {
                selectedCover = imageListViewCover.SelectedItems[0].Text;
                btnOk.Enabled = true;
            }
            else
            {
                btnOk.Enabled = true;
            }
        }

        private void imageListViewCover_DoubleClick(object sender, EventArgs e)
        {
            if (imageListViewCover.SelectedItems.Count > 0)
            {
                selectedCover = imageListViewCover.SelectedItems[0].Text;
                btnOk.PerformClick();
            }
        }
    }
}
