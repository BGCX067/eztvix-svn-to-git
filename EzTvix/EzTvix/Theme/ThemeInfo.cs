using System;
using System.Drawing;
using System.Windows.Forms;
using EzTvix.Theme;
using System.IO;
using System.Drawing.Imaging;

namespace EzTvix.Core
{
    public static class ThemeInfo 
    {
        private static String _themePath = Application.StartupPath + "\\Theme\\" + TvixInfo.Theme;
        private static String _playerType = TvixInfo.PlayerType;
        //private static ThemeTemplate _template = new ThemeTemplate(TvixInfo.Theme);

        //public static String Name
        //{ get { return _template.Name; } set { _template.Name = value; } }

        private static Player m_currentPlayer;
        public static Player Player
        {
            get { return m_currentPlayer; }
            set { m_currentPlayer = value; }
        }

        #region *** Videobox Image Name ***
        private static String _videoBoxName = "folder";
        public static String VideoBoxName
        {
            get
            {
                return _videoBoxName;
            }
            set { _videoBoxName = value; }
        }

        private static ImageFormat _videoBoxExtention = ImageFormat.Png;
        public static String VideoBoxExtention
        {
            get
            {
                if (_videoBoxExtention.ToString().ToLower() == "jpeg")
                    return "jpg";
                return _videoBoxExtention.ToString().ToLower();
            }
            set
            {
                switch (value.ToLower())
                {
                    case "jpg":
                    case "jpeg":
                        _videoBoxExtention = ImageFormat.Jpeg;
                        break;
                    case "gif":
                        _videoBoxExtention = ImageFormat.Gif;
                        break;
                    default:
                        _videoBoxExtention = ImageFormat.Png;
                        break;
                }
            }
        }
        public static ImageFormat VideoBoxFormat 
        {
            get { return _videoBoxExtention; }
        }

        public static String VideoBoxFullName
        {
            get
            {
                return _videoBoxName + "." + VideoBoxExtention;
            }
        }
        #endregion

        #region *** Folder Icon Name ***
        private static String _folderIconName = "folder";
        public static String FolderIconName
        {
            get
            {
                return _folderIconName;
            }
            set { _folderIconName = value; }
        }
        private static ImageFormat _folderIconExtention = ImageFormat.Png;
        public static String FolderIconExtention
        {
            get
            {
                if (_folderIconExtention.ToString().ToLower() == "jpeg")
                    return "jpg";
                return _folderIconExtention.ToString().ToLower();
            }
            set
            {
                switch (value.ToLower())
                {
                    case "jpg":
                    case "jpeg":
                        _folderIconExtention = ImageFormat.Jpeg;
                        break;
                    case "gif":
                        _folderIconExtention = ImageFormat.Gif;
                        break;
                    default:
                        _folderIconExtention = ImageFormat.Png;
                        break;
                }
            }
        }
        public static ImageFormat FolderIconFormat
        {
            get { return _folderIconExtention; }
        }
        public static String FolderIconFullName
        {
            get
            {
                return _folderIconName + "." + FolderIconExtention;
            }
        }
        #endregion

        #region *** UP Folder Icon Name ***
        private static String _upFolderIconName = "upfolder";
        public static String UpFolderIconName
        {
            get
            {
                return _upFolderIconName;
            }
            set { _upFolderIconName = value; }
        }
        private static ImageFormat _upFolderIconExtention = ImageFormat.Png;
        public static String UpFolderIconExtention
        {
            get
            {
                if (_upFolderIconExtention.ToString().ToLower() == "jpeg")
                    return "jpg";
                return _upFolderIconExtention.ToString().ToLower();
            }
            set
            {
                switch (value.ToLower())
                {
                    case "jpg":
                    case "jpeg":
                        _upFolderIconExtention = ImageFormat.Jpeg;
                        break;
                    case "gif":
                        _upFolderIconExtention = ImageFormat.Gif;
                        break;
                    default:
                        _upFolderIconExtention = ImageFormat.Png;
                        break;
                }
            }
        }
        public static ImageFormat upFolderIconFormat
        {
            get { return _upFolderIconExtention; }
        }
        public static String upFolderIconFullName
        {
            get
            {
                return _upFolderIconName + "." + UpFolderIconExtention;
            }
        }
        public static bool UpFolderExist = true;

        #endregion

        #region *** VideoBackGround name ***
        private static String _videoBackgroundName = "tvix";
        public static String VideoBackgroundName
        {
            get
            {
                return _videoBackgroundName;
            }
            set { _videoBackgroundName = value; }
        }
        private static ImageFormat _videoBackgroundExtention = ImageFormat.Jpeg;
        public static String VideoBackgroundExtention
        {
            get
            {
                if (_videoBackgroundExtention.ToString().ToLower() == "jpeg")
                    return "jpg";
                return _videoBackgroundExtention.ToString().ToLower();
            }
            set
            {
                switch (value.ToLower())
                {
                    case "jpg":
                    case "jpeg":
                        _videoBackgroundExtention = ImageFormat.Jpeg;
                        break;
                    case "gif":
                        _videoBackgroundExtention = ImageFormat.Gif;
                        break;
                    default:
                        _videoBackgroundExtention = ImageFormat.Png;
                        break;
                }
            }
        }
        public static ImageFormat VideoBackgroundFormat
        {
            get { return _videoBackgroundExtention; }
        }
        public static String VideoBackgroundFullName
        {
            get
            {
                return _videoBackgroundName + "." + VideoBackgroundExtention;
            }
        }
        #endregion

        private static bool isBigFolder
        {
            get
            {
                return (_playerType == "Tvix6500ABig" || _playerType == "Tvix7000ABig");
            }
        }

        private static Image _empty;
        public static Image Empty
        {
            get
            {

                if(TvixInfo.Theme == "Default")
                {
                    _empty = (isBigFolder) ? global::EzTvix.Theme.Default.Empty : global::EzTvix.Theme.Default.Empty ;
                }
                else
                {
                    _empty = FromStream(_themePath + "\\Empty.png");
                }

                
                return _empty;
            }
        }

        private static Image _folderBack;
        public static Image FolderBack
        {
            get
            {
                if (TvixInfo.Theme == "Default")
                {
                    _folderBack = (isBigFolder) ? global::EzTvix.Theme.Default.ItemBack : global::EzTvix.Theme.Default.ItemBack;
                }
                else
                {
                    _folderBack = FromStream(_themePath + "\\ItemBack.png");
                }
                return _folderBack;
            }
        }

        private static Image _videoBox;
        public static Image VideoBox
        {
            get
            {
                if (TvixInfo.Theme == "Default")
                {
                    _videoBox = (isBigFolder) ? global::EzTvix.Theme.Default.VideoBox : global::EzTvix.Theme.Default.VideoBox;
                }
                else
                {
                    _videoBox = FromStream(_themePath + "\\VideoBox.png");
                }
                return _videoBox;
            }
        }

        private static Image _audioBox;
        public static Image AudioBox
        {
            get
            {
                if (TvixInfo.Theme == "Default")
                {
                    _audioBox = (isBigFolder) ? global::EzTvix.Theme.Default.AudioBox : global::EzTvix.Theme.Default.AudioBox;
                }
                else
                {
                    _audioBox = FromStream(_themePath + "\\AudioBox.png");
                }
                return _audioBox;
            }
        }

        private static Image _emptyBackground;
        public static Image EmptyBackground
        {
            get
            {
                if (TvixInfo.Theme == "Default")
                {
                    _emptyBackground = global::EzTvix.Theme.Default.BackgroundEmpty;
                }
                else
                {
                    _emptyBackground = FromStream(_themePath + "\\EmptyBackground.png");
                }
                return _emptyBackground;
            }
        }

        private static Image FromStream(string _imagePath)
        {
            FileStream fs = new FileStream(_imagePath, FileMode.Open);
            Image imgPhoto = Image.FromStream(fs);
            fs.Close();
            fs.Dispose();
            return imgPhoto;
        }

    }
}
