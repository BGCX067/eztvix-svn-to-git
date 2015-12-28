using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EzTvix
{
    public partial class ImageGallery : Form
    {
        public ImageGallery()
        {
            InitializeComponent();
        }

        private void ImageGallery_Load(object sender, EventArgs e)
        {

            //ImageList imgList = p_theme.getImagesFromResx("Picto");
            //listView1.LargeImageList = imgList;

            //for (int i = 0; i <= imgList.Images.Count - 1; i++)
            //{
            //    //Hereonly Adding icon name into combobox -i want icon image have to add into combobox
            //    listView1.Items.Add("", i);

            //}
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        //    if (listView1.SelectedItems.Count > 0)
        //        p_currentImage.LayerFront = listView1.SelectedItems[0].ImageList.Images[listView1.SelectedItems[0].ImageIndex];
        //    itemPic.Image = p_currentImage;
 
        }
    }
}
