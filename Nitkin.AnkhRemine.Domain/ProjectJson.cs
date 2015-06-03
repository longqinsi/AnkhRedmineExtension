using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Nitkin.AnkhRemine.Domain
{
    [DataContract]
    public class ProjectJson
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
