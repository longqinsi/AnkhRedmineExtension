using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nitkin.AnkhRemine.Domain;

namespace Nitkin.AnkhRedmine.Dal.Interfaces
{
    public interface IIssueDao:IBaseDao
    {
        IssueCollectionJson GetIssuesByProjectId(Uri repositoryUri, string user, string pwd, int projectId, bool assignedToMe);
        IssueCollectionJson GetClosedIssuesByProjectId(Uri repositoryUri, string user, string pwd, int projectId, bool assignedToMe);
        IssueCollectionJson GetAllIssuesByProjectId(Uri repositoryUri, string user, string pwd, int projectId, bool assignedToMe);
    }
}
