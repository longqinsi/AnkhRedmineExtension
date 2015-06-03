using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using Nitkin.AnkhRedmine.Dal.Interfaces;
using Nitkin.AnkhRemine.Domain;

namespace Nitkin.AnkhRedmine.Dal
{
    public class ProjectDao : BaseDao,IProjectDao
    {
        public IList<ProjectJson> GetProjects(Uri repositoryUri, string user, string pwd)
        {
            return
                GetObjectFromJson<ProjectCollectionJson>(GetJsonFromRedmine(new Uri(repositoryUri.OriginalString + "projects.json?limit=100"),
                                                     user, pwd)).Projects;
            //var projects = from c in GetXmlFromRedmine(new Uri(repositoryUri.OriginalString + "projects.xml?limit=100"), user, pwd).Descendants()
            //               where c.Name.LocalName.ToLower() == "project"
            //               select new Project
            //               {
            //                   Id= int.Parse(c.Descendants("id").Single().Value.ToString().Trim()),
            //                   Name = c.Descendants("name").Single().Value.ToString().Trim()
            //               };
            //return projects.ToList<Project>();
        }

        
    }
}
