using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RootKit.API;
using RootKit.API.Providers;
using EzTvix.Core;
using RootKit.Windows.Forms;
using EzTvix.Provider;
using EzTvix.Provider.API;
using EzTvix.Provider.Data;

namespace EzTvix
{
    public partial class frmMovieList : Form
    {
        // This delegate enables asynchronous calls for setting
        // the ListViewItem property on a ListView control.
        //delegate void SetlvItemCallback(PassionXBMCThreaded provider,  Movie movie);

        public Movies newMovieList;
        public string movieID;
        public string SearchString;
        public Movie selectedMovie;
        private MovieProvider provider =  EzTvix.Core.TvixInfo.MovieProvider;
        //private MovieProvider provider = new Allocine();
            
        public frmMovieList()
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
            //provider.DoWork += new DoWorkEventHandler(bwMovies_DoWork);
            provider.Language = TvixInfo.Language;
            provider.RunWorkerCompleted += new  ProviderWorker.RunWorkerCompletedEventHandler(provider_RunWorkerCompleted);
            provider.ObjectLoaded += new ProviderWorker.ObjectLoadedEventHandler(provider_MovieLoaded);

 //           provider.APIKey = TvixInfo.ApiKeyXbmcPassion;
            provider.Search(SearchString);

            ImageListViewRenderers.MovieRenderer renderer = new ImageListViewRenderers.MovieRenderer();
            imageListViewMovie.SetRenderer(renderer);
            

        }
        private void provider_RunWorkerCompleted(object sender, RunWorkerCompletedProviderEventArgs e)
        {
            toolStripStatusLabel1.Text = "Time Spent loading thumbs : " + String.Format("{0:D1}s {1:D2}ms", provider.Timer.Seconds, provider.Timer.Milliseconds);
            //toolStripStatusLabel1.Text = "Time Spent : " + provider.Timer.TimeDiff.ToString();
            this.newMovieList = provider.MovieList;
        }
        public void provider_MovieLoaded(object sender, ObjectLoadedProviderEventArgs e)
        {
            toolStripStatusLabel1.Text = "oblect loaded : " + ((Movie)e.loadedObject).Title;
            

            if (((Movie)e.loadedObject).Posters.Count > 0)
            {
                KeyValuePair<String, MoviePicture> kvp = ((Movie)e.loadedObject).Posters.Last();

                this.imageListViewMovie.Items.Add("tutu.Key", kvp.Key, kvp.Value.Thumb, e.loadedObject);
            }
            else
                this.imageListViewMovie.Items.Add("tutu.Key", ((Movie)e.loadedObject).Title, null, e.loadedObject);
            
        }

        private void movieListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (imageListViewMovie.SelectedItems.Count > 0)
            {
                btnOk.Enabled = true;
                selectedMovie = (Movie)imageListViewMovie.SelectedItems[0].Tag;
            }
        }

        private void movieListView_DoubleClick(object sender, EventArgs e)
        {
            if (imageListViewMovie.SelectedItems.Count > 0)
            {
                selectedMovie = (Movie)imageListViewMovie.SelectedItems[0].Tag;
                btnOk.PerformClick();
            }
        }
    }
}
