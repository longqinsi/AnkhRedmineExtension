using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Nitkin.AnkhRemine.Domain
{
    [DataContract] 
    public class ProjectCollectionJson
    {
        [DataMember(Name = "limit")]
        public int Limit { get; set; }
        [DataMember(Name = "projects")]
        public List<ProjectJson> Projects { get; set; }
    }
}
