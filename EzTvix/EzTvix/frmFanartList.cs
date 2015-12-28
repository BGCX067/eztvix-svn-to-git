using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RootKit.API;
//using RootKit.API.Providers;
using RootKit.Windows.Forms;
using EzTvix.Provider;
using EzTvix.Provider.API;
using EzTvix.Provider.Data;
using EzTvix.Core;

namespace EzTvix
{
    public partial class frmFanartList : Form
    {
        public string movieID;
        public Movie selectedMovie;
        public string SelectedFanart;
        private MovieProvider provider = new PassionXbmc(TvixInfo.Language);
        //EzTvix.Core.TvixInfo.MovieProvider; 
      
        public frmFanartList()
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
            provider.resetEventHandler();
            provider.RunWorkerCompleted += new ProviderWorker.RunWorkerCompletedEventHandler(provider_FanRunWorkerCompleted);
            provider.ObjectLoaded += new ProviderWorker.ObjectLoadedEventHandler(provider_FanMovieLoaded);
            provider.FanartChanged += new ProviderWorker.FanartChangedEventHandler(provider_FanProcessChanged); ;
            provider.GetPictureData(this.selectedMovie);

            //foreach (KeyValuePair<String, MoviePicture> kvp in selectedMovie.FanArts)
            //{
            //    //                this.imageListViewFanart.Items.Add("Dummy.Key", kvp.Key, kvp.Value.Thumb, selectedMovie);
            //    this.imageListViewFanart.Items.Add("Dummy.Key", kvp.Key, kvp.Value.Thumb, kvp.Value);
            //}

            ImageListViewRenderers.FanartRenderer renderer = new ImageListViewRenderers.FanartRenderer();
            imageListViewFanart.SetRenderer(renderer);

        }

        private void provider_FanRunWorkerCompleted(object sender, RunWorkerCompletedProviderEventArgs e)
        {
            toolStripStatusLabel1.Text = "Time Spent loading thumbs : " + String.Format("{0:D1}s {1:D2}ms", provider.Timer.Seconds, provider.Timer.Milliseconds);
        }

        public void provider_FanMovieLoaded(object sender, ObjectLoadedProviderEventArgs e)
        {
            toolStripStatusLabel1.Text = "Movie loaded : " + ((Movie)e.loadedObject).Title;
            this.selectedMovie = ((Movie)e.loadedObject);
        }

        public void provider_FanProcessChanged(object sender, FanartProgressChangedProviderEventArgs e)
        {
            toolStripStatusLabel1.Text = "Loading fanarts : " + ((Movie)e.loadedObject).Title ;
            KeyValuePair<String, MoviePicture> kvp = ((Movie)e.loadedObject).FanArts.Last();
            
            //reinit de lobjet movie avec le nouveau Fanart chargé
            this.selectedMovie = ((Movie)e.loadedObject);
            this.imageListViewFanart.Items.Add("Dummy.Key", kvp.Key, kvp.Value.Thumb, ((Movie)e.loadedObject).FanArts.Last().Value);
        }


        private void imageListViewCover_SelectionChanged(object sender, EventArgs e)
        {
            if (imageListViewFanart.SelectedItems.Count > 0)
            {
                //selectedMovie.SelectedFanart = imageListViewFanart.SelectedItems[0].Text;
                btnOk.Enabled = true;
            }
            else
            {
                btnOk.Enabled = true;
            }
        }

        private void imageListViewCover_DoubleClick(object sender, EventArgs e)
        {
            if (imageListViewFanart.SelectedItems.Count > 0)
            {
                //selectedMovie.SelectedFanart = imageListViewFanart.SelectedItems[0].Text;
                btnOk.PerformClick();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (imageListViewFanart.SelectedItems.Count > 0)
            {
                selectedMovie.SelectedFanart = imageListViewFanart.SelectedItems[0].Text;
                //btnOk.Enabled = true;
            }
            
        }
    }
}
