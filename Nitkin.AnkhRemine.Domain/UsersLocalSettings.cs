using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nitkin.AnkhRemine.Domain
{
    public class UsersLocalSettings
    {
        private string userName="";
        private string userPassword="";
        private bool useProxy;
        private string proxyUrl="";
        private string proxyUser="";
        private string proxyPwd="";
        private int proxyPort;
        //private int defaultProjectId;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string UserPassword
        {
            get { return userPassword; }
            set { userPassword = value; }
        }

        public string ProxyUrl
        {
            get { return proxyUrl; }
            set { proxyUrl = value; }
        }

        public string ProxyPwd
        {
            get { return proxyPwd; }
            set { proxyPwd = value; }
        }

        //public int DefaultProjectId
        //{
        //    get { return defaultProjectId; }
        //    set { defaultProjectId = value; }
        //}

        public bool UseProxy
        {
            get { return useProxy; }
            set { useProxy = value; }
        }

        public int ProxyPort
        {
            get { return proxyPort; }
            set { proxyPort = value; }
        }

        public string ProxyUser
        {
            get { return proxyUser; }
            set { proxyUser = value; }
        }
    }
}
