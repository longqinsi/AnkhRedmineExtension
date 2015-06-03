using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Nitkin.AnkhRedmine.Dal.Interfaces;
using Nitkin.AnkhRemine.Domain;

namespace Nitkin.AnkhRedmine.Dal
{
    public class UsersLocalSettingsDao:IUsersLocalSettingsDao
    {
        #region Implementation of IUsersLocalSettingsDao

        public UsersLocalSettings GetUsersLocalSettings(out Exception error)
        {
            error = null;
            UsersLocalSettings settings = new UsersLocalSettings();
            if (!File.Exists(settingsFilePath))
            {
                SaveUsersLocalSettings(settings);
                return settings;
            }
            XDocument xd = XDocument.Load(settingsFilePath);
            var v=(from c in xd.Descendants()
                    where c.Name.LocalName.ToLower() == "settings"
                    select new UsersLocalSettings
                               {
                                   UserName = c.Descendants("UserName").Single().Value.Trim()
                                   ,
                                   UserPassword = c.Descendants("UserPwd").Single().Value.Trim()
                                   ,
                                   ProxyUrl = c.Descendants("ProxyUrl").Single().Value.Trim()
                                   ,
                                   ProxyPwd = c.Descendants("ProxyPwd").Single().Value.Trim()
                                   ,
                                   UseProxy =
                                       Convert.ToBoolean(c.Descendants("UseProxy").Single().Value.Trim(),
                                                         CultureInfo.InvariantCulture)
                                   ,
                                   //DefaultProjectId =
                                   //    Convert.ToInt32(c.Descendants("DefaultProject").Single().Value.Trim(),
                                   //                    CultureInfo.InvariantCulture),
                                   ProxyPort = Convert.ToInt32(c.Descendants("ProxyPort").Single().Value.Trim(),
                                       CultureInfo.InvariantCulture)
                                       ,
                                   ProxyUser = c.Descendants("ProxyUser").Single().Value.Trim()

                               }).Single();
            try
            {
                v.UserPassword = Secure.DecryptString(v.UserPassword);
            }
            catch (Exception ex)
            {
                error = ex;
            }
            return v;
        }

        public void SaveUsersLocalSettings(UsersLocalSettings settings)
        {
            if (!Directory.Exists(settingsFolder))
            {
                Directory.CreateDirectory(settingsFolder);
            }
            XmlDocument xd=new XmlDocument();
            xd.LoadXml(
                String.Format("<settings><UserName>{0}</UserName><UserPwd>{1}</UserPwd><ProxyUrl>{2}</ProxyUrl><ProxyPwd>{3}</ProxyPwd><UseProxy>{4}</UseProxy><ProxyPort>{5}</ProxyPort><ProxyUser>{6}</ProxyUser></settings>"
                ,settings.UserName.Trim()
                , Secure.EncryptString(settings.UserPassword.Trim())
                ,settings.ProxyUrl.Trim()
                ,settings.ProxyPwd.Trim()
                ,settings.UseProxy.ToString(CultureInfo.InvariantCulture)
                //, settings.DefaultProjectId.ToString(CultureInfo.InvariantCulture)
                , settings.ProxyPort.ToString(CultureInfo.InvariantCulture)
                ,settings.ProxyUser.Trim()
                )
                );
            xd.Save(settingsFilePath);
        }
        private string settingsFolder
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"AnkhRedmineExtension");
            }
        }
        private string settingsFilePath
        {
            get
            {
                return Path.Combine(settingsFolder, "settings.xml");
            }
        }
        #endregion
    }
}
