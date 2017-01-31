using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apcis.Models
{
    public class Parent
    {
        public int ID { get; set; }
        public virtual ICollection<Etudiant> Enfants { get; set;}
    }
}