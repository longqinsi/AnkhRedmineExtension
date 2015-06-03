using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Nitkin.AnkhRemine.Domain
{
    [DataContract] 
    public class IssueCollectionJson
    {
        [DataMember(Name = "limit")] 
        public int Limit { get; set; }
        [DataMember(Name = "issues")] 
        public List<IssueJson> Issues { get; set; }
    }
}
