using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nitkin.AnkhRedmine.Dal.Interfaces
{
    public interface IBaseDao
    {
        T GetObjectFromJson<T>(string strJson);
        //void SetProxy(bool useProxy, string proxyUrl, string proxyUser, string proxyPwd, int proxyPort);
        //void ClearProxy();
    }
}