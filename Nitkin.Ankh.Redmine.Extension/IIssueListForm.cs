using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nitkin.AnkhRemine.Domain;

namespace Nitkin.Ankh.Redmine.Extension
{
    interface IIssueListForm
    {
        IList<IssueFacade> GetFixesIssues();
        IList<IssueFacade> GetRefsIssues();
    }
}
