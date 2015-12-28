using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Security;
using System.Windows.Forms;

namespace RootKit.Core
{
    public class RegistryInfo
    {
        #region --------- Properties ----------

        private RegistryKey _localMachine = Registry.LocalMachine;
        private RegistryKey _user = Registry.CurrentUser;
        private bool _readOnly = true;

        private RegistryKey _rootKey;
        /// <summary>
        /// RootKey
        /// </summary>
        public RegistryKey RootKey
        {
            get { return _rootKey; }
            set { _rootKey = value; }
        }

        private RegistryKey _activeKey;
        /// <summary>
        /// Active Key
        /// </summary>
        public RegistryKey ActiveKey
        {
            get { return _activeKey; }
            set { _activeKey = value; }
        }

        /// <summary>
        /// Active Key
        /// </summary>
        public String ActiveKeyString
        {
            get { return _activeKey.Name; }
            set { _activeKey = _activeKey.OpenSubKey(value, true); }
        }

        #endregion 

        #region --------- Constructor ***

        public RegistryInfo()
        {
            _rootKey = Registry.LocalMachine;
            _activeKey = Registry.LocalMachine;
        }

        public RegistryInfo(RegistryKey _Key)
        {
            _rootKey = _Key;
            _activeKey = _Key;
        }

        public RegistryInfo(RegistryKey _Key, string _subKey)
        {
            _rootKey = _Key;
            _readOnly = false;
            _activeKey = _Key.OpenSubKey(_subKey, !_readOnly);
            if (_activeKey == null)
                _activeKey = _Key.CreateSubKey(_subKey);
        }

        #endregion

        public object getValue(string _key)
        {
            return _activeKey.GetValue(_key); 
        }
        public string getValueString(string _key)
        {
            return _activeKey.GetValue(_key).ToString();
        }

        public object getValue(RegistryKey _subKey, string _key)
        {
            return _subKey.GetValue(_key);
        }

        public string getValueString(RegistryKey _subKey, string _key)
        {
            return _subKey.GetValue(_key).ToString();
        }


        public void setValue(string _name, object _value)
        {
            try
            {
                //Setting the various values is done using SetValue()
                _activeKey.SetValue(_name, _value);
            }
            catch (SecurityException e)
            {
                MessageBox.Show("You do not have Permission to write in the registry!!!\r\n - " + e.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            { }
        }



    }
}
