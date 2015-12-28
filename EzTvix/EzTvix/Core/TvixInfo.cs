using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Win32;

using RootKit.Core;
using System.Windows.Forms;
using RootKit.API.Providers;
using System.Drawing;
using EzTvix.Provider.API;
using EzTvix.Provider.Data;
using EzTvix.Provider; 

namespace EzTvix.Core
{

    // to convert enum to string and string to enum 
    // //convert enum to string. mode is an Enum of type playerType.
    // //Enum.GetName returns the name the mode is set to
    // Enum.GetName( typeof(playerType), mode)
    // //convert a string to an emum.
    // //buttonState is the string
    // //ButtonState is the enum type
    // (ButtonState)Enum.Parse(typeof(ButtonState);, buttonState, true);
    /// <summary>
    /// 
    /// </summary>
    public enum playerType
    {
        Tvix6500,
        Tvix6600,
        Tvix66xx,
        Tvixs1,
        Tvix6500ABig,
        Tvix6500ASmall,
        Tvix7000ABig,
        Tvix7000ASmall
    }

    public enum movieExtention
    {
        Avi,
        Mkv,
        Iso,
        Mpg,
        Mpeg
    }
    public enum movieProviderType
    {
        XBMCPassion,
        AlloCine
    }


    public static class TvixInfo
    {
        private static RegistryInfo _reg = new RegistryInfo(Registry.LocalMachine, @"Software\EzTvix");
        private static RegistryInfo _API = new RegistryInfo(Registry.LocalMachine, @"Software\EzTvix\API");

        private static String _themeFolder = Application.StartupPath + @"\theme";
        public static String ThemeFolder
        {
            get
            {
                return _themeFolder;
            }
            //set
            //{
            //    _themeFolder = value;
            //    _reg.setValue("ThemeFolder", value);
            //}
        }


        private static String _rootFolder;
        public static String RootFolder
        {
            get
            {
                try
                {
                    _rootFolder = _reg.getValueString("rootFolder");
                }
                catch (Exception e)
                {
                    _rootFolder = @"c:\";
                    _reg.setValue("rootFolder", _rootFolder);
                }
                return _rootFolder;
            }
            set
            {
                _rootFolder = value;
                _reg.setValue("rootFolder", value);
            }
        }

        private static String _lastPathFolder;
        public static String LastPathFolder
        {
            get
            {
                try
                {
                    _lastPathFolder = _reg.getValueString("LastPathFolder");
                }
                catch (Exception e)
                {
                    _lastPathFolder = @"video";
                    _reg.setValue("LastPathFolder", _lastPathFolder);
                }
                return (StoreLastPathFolder) ? _lastPathFolder : "";
            }
            set
            {
                _lastPathFolder = value;
                _reg.setValue("LastPathFolder", value);
            }
        }

        private static bool _storeLastPathFolder;
        public static bool StoreLastPathFolder
        {
            get
            {
                try
                {
                    _storeLastPathFolder = Convert.ToBoolean(_reg.getValue("StoreLastPathFolder"));
                }
                catch (Exception e)
                {
                    _storeLastPathFolder = false;
                    _reg.setValue("StoreLastPathFolder", _storeLastPathFolder);
                }
                return _storeLastPathFolder;
            }
            set
            {
                _storeLastPathFolder = value;
                _reg.setValue("StoreLastPathFolder", value);
            }
        }

        //AutoFilterMovieName
        private static bool _autoFilterMovieName;
        public static bool AutoFilterMovieName
        {
            get
            {
                try
                {
                    _autoFilterMovieName = Convert.ToBoolean(_reg.getValue("AutoFilterMovieName"));
                }
                catch (Exception e)
                {
                    _autoFilterMovieName = true;
                    _reg.setValue("AutoFilterMovieName", _autoFilterMovieName);
                }
                return _autoFilterMovieName;
            }
            set
            {
                _autoFilterMovieName = value;
                _reg.setValue("AutoFilterMovieName", value);
            }
        }

        private static bool _autoRenameMovie; 
        public static bool AutoRenameMovie 
        {
            get
            {
                try
                {
                    _autoRenameMovie = Convert.ToBoolean(_reg.getValue("AutoRenameMovie"));
                }
                catch (Exception e)
                {
                    _autoRenameMovie = true;
                    _reg.setValue("AutoRenameMovie", _autoRenameMovie);
                }
                return _autoRenameMovie;
            }
            set
            {
                _autoRenameMovie = value;
                _reg.setValue("AutoRenameMovie", value);
            }
        }

        private static bool _useFanart;
        public static bool UseFanart
        {
            get
            {
                try
                {
                    _useFanart = Convert.ToBoolean(_reg.getValue("UseFanart"));
                }
                catch (Exception e)
                {
                    _useFanart = true;
                    _reg.setValue("UseFanart", _useFanart);
                }
                return _useFanart;
            }
            set
            {
                _useFanart = value;
                _reg.setValue("UseFanart", value);
            }
        }
        private static String _theme;
        public static String Theme
        {
            get
            {
                try
                {
                    _theme = _reg.getValueString("Theme");
                }
                catch (Exception e)
                {
                    _theme = @"Default";
                    _reg.setValue("Theme", _theme);
                }
                return _theme;
            }
            set
            {
                _theme = value;
                _reg.setValue("Theme", value);
            }
        }

        //private static String _themeMode;
        //public static String ThemeMode
        //{
        //    get
        //    {
        //        try
        //        {
        //            _themeMode = _reg.getValueString("ThemeMode");
        //        }
        //        catch (NullReferenceException e)
        //        {
        //            _themeMode = @"Default";
        //            _reg.setValue("ThemeMode", _themeMode);
        //        }
        //        return _themeMode;
        //    }
        //    set
        //    {
        //        _themeMode = value;
        //        _reg.setValue("ThemeMode", value);
        //    }
        //}

        private static String _playerType;
        public static String PlayerType
        {
            get
            {
                try
                {
                    // enum conversion
                    //_playerType = (playerType)Enum.Parse(typeof(playerType), _reg.getValueString("PlayerType"), true);
                    _playerType =  _reg.getValueString("PlayerType");
                }
                catch (Exception e)
                {
                    _playerType = "Tvix6500"; // playerType.Tvix6500ABig;
                    _reg.setValue("PlayerType", _playerType);
                }
                return _playerType;
            }
            set
            {
                _playerType = value;
                _reg.setValue("PlayerType", value);
            }
        }

        private static movieProviderType _provider;
        public static String Provider
        {
            get
            { 
                try
                { 
                    //_provider = _reg.getValueString("MovieProvider");
                    _provider = (movieProviderType)Enum.Parse(typeof(movieProviderType), _reg.getValueString("MovieProvider"), true);
                }
                catch (Exception e)
                {
                    //_provider = @"PassionXBMC";
                    _provider = movieProviderType.XBMCPassion;
                    _reg.setValue("MovieProvider", _provider.ToString());
                }
                return _provider.ToString();
            }
            set
            {
                _provider = (movieProviderType)Enum.Parse(typeof(movieProviderType), value, true);
                _reg.setValue("MovieProvider", value);
            }
        }
        public static MovieProvider MovieProvider
        {
            get
            {
                switch (_provider)
                {
                    case movieProviderType.XBMCPassion:
                        return new PassionXbmc(_Language);
                        break;
                    case movieProviderType.AlloCine:
                        return new Allocine();
                        break;
                    default:
                        return new PassionXbmc(_Language);
                        break;
                }
            }
        }
        
        private static Rectangle _iconMask = new Rectangle(0, 0, 200, 200);
        public static Rectangle IconMask
        {
            get {return _iconMask; }
            set { _iconMask = value; }
        }

        private static Rectangle _backgroundMask = new Rectangle(0, 0, 1280, 720);
        public static Rectangle BackgroundMask
        {
            get { return _backgroundMask; }
            set { _backgroundMask = value; }
        }

        //private static RegistryInfo _API = new RegistryInfo(Registry.LocalMachine, @"Software\EzTvix\API");
        private static string _apiKeyXbmcPassion = "9b8f0779badbad3b46d6718ee95a68ff";
        public static string ApiKeyXbmcPassion
        {
            get
            {
                if (_apiKeyXbmcPassion.Length <= 0)
                {
                    try
                    {
                        _apiKeyXbmcPassion = _API.getValueString("XBMCPassion");
                    }
                    catch (Exception e)
                    {
                        _apiKeyXbmcPassion = "";
                        _API.setValue("XBMCPassion", _apiKeyXbmcPassion);
                    }
                }
                return _apiKeyXbmcPassion;
            }
            //set
            //{
            //    _apiKeyXbmcPassion = value;
            //    _API.setValue("XBMCPassion", value);
            //}
        
        }


        //private static RegistryInfo _API = new RegistryInfo(Registry.LocalMachine, @"Software\EzTvix\API");
        private static bool _ShowTooltips = true;
        public static bool ShowTooltips
        {
            get
            {
                try
                {
                    _ShowTooltips = Convert.ToBoolean(_reg.getValueString("ShowTooltips"));
                }
                catch (Exception ex)
                {
                    _ShowTooltips = true;
                    _reg.setValue("ShowTooltips", _ShowTooltips);
                }
                return _ShowTooltips;
            }
            set
            {
                _ShowTooltips = value;
                _reg.setValue("ShowTooltips", value);
            }

        }


        //private static RegistryInfo _API = new RegistryInfo(Registry.LocalMachine, @"Software\EzTvix\API");
        private static string _Language = "EN";
        public static string Language
        {
            get
            {
                try
                {
                    _Language = _reg.getValueString("Language");
                }
                catch (Exception ex)
                {
                    _Language = "EN";
                    _reg.setValue("Language", _Language);
                }
                return _Language;
            }
            set
            {
                _Language = value;
                _reg.setValue("Language", value);
            }

        }

    }
}
