using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using Nitkin.AnkhRedmine.Dal.Interfaces;
using Nitkin.AnkhRemine.Domain;

namespace Nitkin.AnkhRedmine.Dal
{
    public class IssueDao : BaseDao, IIssueDao
    {
        #region Implementation of IIssueDao

        

        public IssueCollectionJson GetIssuesByProjectId(Uri repositoryUri, string user, string pwd, int projectId, bool assignedToMe)
        {
            IssueCollectionJson list = GetObjectFromJson<IssueCollectionJson>(GetJsonFromRedmine(new Uri(repositoryUri.OriginalString +
                            string.Format("issues.json?project_id={0}{1}&limit=100", projectId.ToString(CultureInfo.InvariantCulture), assignedToMe ? "&assigned_to_id=me" : "")), user, pwd));

            return list;
            // GetIssuesFromXml(
            //    GetXmlFromRedmine(
            //        new Uri(repositoryUri.OriginalString +
            //                string.Format("issues.xml?project_id={0}{1}&per_page=100", projectId.ToString(CultureInfo.InvariantCulture), assignedToMe ? "&assigned_to_id=me" : "")),
            //        user, pwd));
            //return null;
        }

        public IssueCollectionJson GetClosedIssuesByProjectId(Uri repositoryUri, string user, string pwd, int projectId, bool assignedToMe)
        {
            IssueCollectionJson list = GetObjectFromJson<IssueCollectionJson>(GetJsonFromRedmine(
                 new Uri(repositoryUri.OriginalString +
                           string.Format("issues.json?project_id={0}&status_id=closed{1}&limit=100", projectId.ToString(CultureInfo.InvariantCulture), assignedToMe ? "&assigned_to_id=me" : "")),
                           user, pwd));
            return list;
            //return GetIssuesFromXml(
            //   GetXmlFromRedmine(
            //       new Uri(repositoryUri.OriginalString +
            //               string.Format("issues.xml?project_id={0}&status_id=closed{1}&per_page=100", projectId.ToString(CultureInfo.InvariantCulture), assignedToMe ? "&assigned_to_id=me" : "")),
            //       user, pwd));
        }

        public IssueCollectionJson GetAllIssuesByProjectId(Uri repositoryUri, string user, string pwd, int projectId, bool assignedToMe)
        {
            IssueCollectionJson list = GetObjectFromJson<IssueCollectionJson>(GetJsonFromRedmine(
                  new Uri(repositoryUri.OriginalString +
                          string.Format("issues.json?project_id={0}&status_id=*{1}&limit=100", projectId.ToString(CultureInfo.InvariantCulture), assignedToMe ? "&assigned_to_id=me" : "")),
                           user, pwd));
            return list;
            //return GetIssuesFromXml(
            //  GetXmlFromRedmine(
            //      new Uri(repositoryUri.OriginalString +
            //              string.Format("issues.xml?project_id={0}&status_id=*{1}per_page=100", projectId.ToString(CultureInfo.InvariantCulture), assignedToMe ? "&assigned_to_id=me" : "")),
            //      user, pwd));
        }
        //private IList<Issue> GetIssuesFromXml(XDocument xd)
        //{
        //    var issues = from c in xd.Descendants()
        //                 where c.Name.LocalName.ToLower() == "issue"
        //                 select new Issue()
        //                 {
        //                     Id = int.Parse(c.Descendants("id").Single().Value.ToString().Trim()),
        //                     ProjectName = c.Descendants("project").Single().Attribute("name").Value.ToString().Trim(),
        //                     Subject = c.Descendants("subject").SingleOrDefault().Value.ToString().Trim(),
        //                     AuthorName = c.Descendants("author").SingleOrDefault() != null ? c.Descendants("author").SingleOrDefault().Attribute("name").Value.ToString().Trim() : null,
        //                     AssignedtoName = c.Descendants("assigned_to").SingleOrDefault() != null ? c.Descendants("assigned_to").SingleOrDefault().Attribute("name").Value.ToString().Trim() : null,
        //                     PriorityName = c.Descendants("priority").SingleOrDefault() != null ? c.Descendants("priority").SingleOrDefault().Attribute("name").Value.ToString().Trim() : null,
        //                     StatusName = c.Descendants("status").SingleOrDefault() != null ? c.Descendants("status").SingleOrDefault().Attribute("name").Value.ToString().Trim() : null
        //                 };
        //    var l = issues.ToList();
        //    return issues.ToList();
        //}

        #endregion
    }
}
