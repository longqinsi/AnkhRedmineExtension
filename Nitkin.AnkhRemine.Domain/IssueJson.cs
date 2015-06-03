using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Nitkin.AnkhRemine.Domain
{
    [DataContract] 
    public class IssueJson
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "start_date")] 
        public string StartDate { get; set;}
        [DataMember(Name = "subject")]
        public string Subject { get; set; }
        [DataMember(Name = "author")] 
        public UserJson Author { get; set; }
        [DataMember(Name = "assigned_to")]
        public UserJson AssignedTo { get; set; }
        [DataMember(Name = "status")]
        public IssueStatusJson Status { get; set; }
        [DataMember(Name = "tracker")]
        public TrackerJson Tracker { get; set; }
        [DataMember(Name = "project")]
        public ProjectJson Project { get; set; }
        [DataMember(Name = "priority")]
        public PriorityJson Priority { get; set; }
    }
}
