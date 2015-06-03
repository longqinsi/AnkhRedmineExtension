using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Linq;
using Nitkin.AnkhRedmine.Dal.Interfaces;
using Nitkin.AnkhRemine.Domain;

namespace Nitkin.AnkhRedmine.Dal
{
    public class BaseDao:IBaseDao
    {
        private UsersLocalSettings usersLocalSettings;
        //protected string ProxyUrl;
        //protected string ProxyUser;
        //protected string ProxyPwd;
        //protected int ProxyPort;
        //protected bool UseProxy;
        public BaseDao()
        {
            IUsersLocalSettingsDao localSettingsDao = new UsersLocalSettingsDao();
            Exception error;
            usersLocalSettings = localSettingsDao.GetUsersLocalSettings(out error);
        }

        private HttpWebRequest MakeRequest(Uri  uri, string username, string pwd)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            req.ProtocolVersion = HttpVersion.Version11;
            req.Method = "GET";
            if (!string.IsNullOrEmpty(username) || !string.IsNullOrEmpty(pwd))
            {
                CredentialCache credCache = new CredentialCache();
                credCache.Add(uri, "Basic", new NetworkCredential(username, pwd));
                req.Credentials = credCache;
            }
            if (usersLocalSettings.UseProxy)
            {
                WebProxy proxy = new WebProxy((new UriBuilder("http", usersLocalSettings.ProxyUrl, usersLocalSettings.ProxyPort)).Uri);
                if (string.IsNullOrEmpty(usersLocalSettings.ProxyUser)||string.IsNullOrEmpty(usersLocalSettings.ProxyPwd))
                {
                    proxy.UseDefaultCredentials=true;
                }
                else
                {
                    proxy.Credentials = new NetworkCredential(GetUserName(), usersLocalSettings.ProxyPwd, GetDomainName());
                }
                req.Proxy = proxy;
            }
            else
            {
                req.Proxy = null;
            }
            return req;
        }
        private string GetDomainName()
        {
            if (CheckIfDomainInUserName())
            {
                return usersLocalSettings.ProxyUser.Substring(0, usersLocalSettings.ProxyPwd.IndexOf(@"\"));
            }
            return string.Empty;
        }
        private string GetUserName()
        {
            if (CheckIfDomainInUserName())
            {
                return usersLocalSettings.ProxyUser.Substring(usersLocalSettings.ProxyUser.IndexOf(@"\") + 1);
            }
            return usersLocalSettings.ProxyUser;
        }
        private bool CheckIfDomainInUserName()
        {
            if (usersLocalSettings.ProxyUser.Contains(@"\") && !usersLocalSettings.ProxyUser.StartsWith(@"\") && !usersLocalSettings.ProxyUser.EndsWith(@"\"))
            {
                return true;
            }
            return false;

        }
        protected XDocument GetXmlFromRedmine(Uri uri, string username, string pwd)
        {
            HttpWebRequest req = MakeRequest(uri, username, pwd);
            StringBuilder outputSB = new StringBuilder();
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                Stream stream = resp.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                // Pipes the stream to a higher level stream reader with the required encoding format. 
                using (StreamReader readStream = new StreamReader(stream, encode))
                {
                    Char[] read = new Char[256];
                    int count = readStream.Read(read, 0, 256);
                    while (count > 0)
                    {
                        outputSB.Append(new String(read, 0, count));
                        count = readStream.Read(read, 0, 256);
                    }
                }
            }
            return XDocument.Parse(outputSB.ToString());
        }
        protected string GetJsonFromRedmine(Uri uri, string username, string pwd)
        {
            //HttpWebRequest req = MakeRequest(uri, username, pwd);
            //StringBuilder outputSB = new StringBuilder();
            //using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            //{
            //    Stream stream = resp.GetResponseStream();
            //    Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            //    // Pipes the stream to a higher level stream reader with the required encoding format. 
            //    using (StreamReader readStream = new StreamReader(stream, encode))
            //    {
            //        Char[] read = new Char[256];
            //        int count = readStream.Read(read, 0, 256);
            //        while (count > 0)
            //        {
            //            outputSB.Append(new String(read, 0, count));
            //            count = readStream.Read(read, 0, 256);
            //        }
            //    }
            //}
            //return outputSB.ToString();
            string response;
            int? responseCode;
            string responseMessage;
            if (HttpWebRequestViaBasicAuthentication.GetResponse(uri.ToString(), username, pwd, out response,
                out responseCode, out responseMessage))
            {
                return response;
            }
            if (responseCode.HasValue)
            {
                throw new WebException(responseMessage, (WebExceptionStatus) responseCode.Value);
            }
            else if (!string.IsNullOrWhiteSpace(responseMessage))
            {
                throw new Exception(responseMessage);
            }
            else
            {
                throw new Exception("从服务器获取数据时发生错误。");
            }
        }

        //public void SetProxy(bool useProxy, string proxyUrl, string proxyUser, string proxyPwd, int proxyPort)
        //{
        //    ProxyUrl = proxyUrl;
        //    ProxyPort = proxyPort;
        //    ProxyPwd = proxyPwd;
        //    ProxyUser = proxyUser;
        //    UseProxy = useProxy;
        //}

        //public void ClearProxy()
        //{
        //    UseProxy = false;
        //}
        public T GetObjectFromJson<T>(string strJson)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(strJson));
            T dataObject =(T)ser.ReadObject(ms);
            return dataObject;
        }
    }
}
