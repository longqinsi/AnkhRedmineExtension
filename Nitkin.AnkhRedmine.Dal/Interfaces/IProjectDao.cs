using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nitkin.AnkhRemine.Domain;

namespace Nitkin.AnkhRedmine.Dal.Interfaces
{
    public interface IProjectDao:IBaseDao
    {
        IList<ProjectJson> GetProjects(Uri repositoryUri, string user,string pwd);
    }
}
