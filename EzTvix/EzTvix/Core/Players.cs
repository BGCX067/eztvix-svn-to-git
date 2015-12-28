using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Xml;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using System.Drawing;

namespace EzTvix.Core
{
    public class Player
    {

        #region *** properties ***

        private String m_name = "";
        public String Name
        { get { return m_name; } set { m_name = value; } }

        private String m_label = "";
        public String Label
        {
            get
            {
                if (m_label == "")
                    return m_name;
                return m_label;
            }
            set { m_label = value; }
        }

        private String m_folderName = "";
        public String FolderName
        {
            get { return m_folderName; }
            set
            {
                try
                {
                    m_folderName = value;
                }
                catch (Exception e)
                {
                    m_folderName = "";
                }
            }
        }

        private String m_folderExtension = "jpg";
        public String FolderExtension
        {
            get { return m_folderExtension; }
            set
            {
                try { m_folderExtension = value; }
                catch (Exception e)
                { m_folderExtension = ""; }
            }
        }

        private String m_upFolderName = "";
        public String UpFolderName
        {
            get { return m_upFolderName; }
            set
            {
                try { m_upFolderName = value; }
                catch (Exception e)
                { m_upFolderName = ""; }
            }
        }

        private String m_upFolderExtension = "jpg";
        public String UpFolderExtension
        {
            get { return m_upFolderExtension; }
            set
            {
                try { m_upFolderExtension = value; }
                catch (Exception e)
                { m_upFolderExtension = ""; }
            }
        }

        private bool m_upFolderExist = true;
        public bool UpFolderExist
        { get { return m_upFolderExist; } set { m_upFolderExist = value; } }

        private String m_backgroundName = "";
        public String BackgroundName
        {
            get { return m_backgroundName; }
            set
            {
                try { m_backgroundName = value; }
                catch (Exception e)
                { m_backgroundName = ""; }
            }
        }

        private String m_backgroundExtension = "jpg";
        public String BackgroundExtension
        {
            get { return m_backgroundExtension; }
            set
            {
                try { m_backgroundExtension = value; }
                catch (Exception e)
                { m_backgroundExtension = ""; }
            }
        }

        private Size m_icon = new Size(200, 200);
        public Size IconSize
        { get { return m_icon; } set { m_icon = value; } }
         
        private Size m_audioIcon = new Size(200, 200);
        public Size AudioIcon
        { get { return m_audioIcon; } set { m_audioIcon = value; } }

        private Size m_photoIcon = new Size(200, 200);
        public Size PhotoIcon
        { get { return m_photoIcon; } set { m_photoIcon = value; } }

        private Size m_videoIcon = new Size(200, 200);
        public Size VideoIcon 
        { get { return m_videoIcon; } set { m_videoIcon = value; } }


        #endregion

        public override string ToString()
        {
            //return base.ToString();
            return this.Name;
        }
    }

    public class PlayerList
    {
        //public List<Player> Items = new List<Player>();
        public Dictionary<String, Player> Items = new Dictionary<string, Player>();
        #region *** cTor ***
        public PlayerList() { }
        #endregion

        private List<Player> m_list = new List<Player>();
        public List<Player> List
        {
            get
            {
                return new List<Player>(Items.Values);

            }
        }

        public void LoadXml()
        {
            XmlDocument doc = new XmlDocument();
            XmlNodeList nodeList;
            Player player;

            try
            {
                doc.Load(Application.StartupPath + @"/Players.xml");
                nodeList = doc.SelectNodes("//Player");

                foreach (XmlNode reader in nodeList)
                {
                    player = new Player();
                    player.Name = reader.Attributes["name"].Value;
                    player.FolderName = reader.SelectSingleNode("Folder").Attributes["filename"].Value;
                    player.FolderExtension = reader.SelectSingleNode("Folder").Attributes["extension"].Value;
                    try
                    {
                        player.Label = reader.Attributes["label"].Value;
                    }
                    catch (Exception ex)
                    {
                        player.Label = "";
                    }
                    try
                    {
                        player.UpFolderName = reader.SelectSingleNode("Upfolder").Attributes["filename"].Value;
                        player.UpFolderExtension = reader.SelectSingleNode("Upfolder").Attributes["extension"].Value;
                        player.UpFolderExist = true;
                    }
                    catch (Exception ex)
                    {
                        player.UpFolderExist = false;
                    }

                    try
                    {
                        player.IconSize = new Size(
                                            int.Parse(reader.SelectSingleNode("IconSize").Attributes["height"].Value),
                                            int.Parse(reader.SelectSingleNode("IconSize").Attributes["width"].Value));
                    }
                    catch (Exception ex)
                    {
                    }
                    try
                    {
                        player.AudioIcon = new Size(
                                            int.Parse(reader.SelectSingleNode("AudioIcon").Attributes["height"].Value),
                                            int.Parse(reader.SelectSingleNode("AudioIcon").Attributes["width"].Value));
                    }
                    catch (Exception ex)
                    {
                        player.AudioIcon = player.IconSize;
                    }
                    try
                    {
                        player.PhotoIcon = new Size(
                                            int.Parse(reader.SelectSingleNode("PhotoIcon").Attributes["height"].Value),
                                            int.Parse(reader.SelectSingleNode("PhotoIcon").Attributes["width"].Value));
                    }
                    catch (Exception ex)
                    {
                        player.PhotoIcon = player.IconSize;
                    }
                    try
                    {
                        player.VideoIcon = new Size(
                                            int.Parse(reader.SelectSingleNode("VideoIcon").Attributes["height"].Value),
                                            int.Parse(reader.SelectSingleNode("VideoIcon").Attributes["width"].Value));
                    }
                    catch (Exception ex)
                    {
                        player.VideoIcon = player.IconSize;
                    }

                    player.BackgroundName = reader.SelectSingleNode("Background").Attributes["filename"].Value;
                    player.BackgroundExtension = reader.SelectSingleNode("Background").Attributes["extension"].Value;
                    Items.Add(player.Name, player);
                }

            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show("File Players.xml missing or corrupted in path :\r\n" + Application.StartupPath + "\r\nDefault settings will be applied!", e.Message, MessageBoxButtons.OK);
                Items.Clear();
                player = new Player();
                player.Name = "Tvix6500";
                player.FolderName = "Folder";
                player.FolderExtension = "Png";
                player.UpFolderName = "UpFolder";
                player.UpFolderExtension = "Png";
                player.BackgroundName = "Tvix";
                player.BackgroundExtension = "Jpg";
                Items.Add(player.Name, player);
            }
    

        }


        public void LoadThemeData()
        {
            this.LoadXml();
            ThemeInfo.VideoBackgroundExtention = this.Items[TvixInfo.PlayerType].BackgroundExtension;
            ThemeInfo.VideoBackgroundName = this.Items[TvixInfo.PlayerType].BackgroundName;
            ThemeInfo.UpFolderIconExtention = this.Items[TvixInfo.PlayerType].UpFolderExtension;
            ThemeInfo.UpFolderIconName = this.Items[TvixInfo.PlayerType].UpFolderName;
            ThemeInfo.FolderIconExtention = this.Items[TvixInfo.PlayerType].FolderExtension;
            ThemeInfo.FolderIconName = this.Items[TvixInfo.PlayerType].FolderName;
            ThemeInfo.UpFolderExist = this.Items[TvixInfo.PlayerType].UpFolderExist;
            ThemeInfo.VideoBoxExtention = this.Items[TvixInfo.PlayerType].FolderExtension;
            ThemeInfo.VideoBoxName = this.Items[TvixInfo.PlayerType].FolderName;
            ThemeInfo.Player = this.Items[TvixInfo.PlayerType];
        }


    }
    
}
