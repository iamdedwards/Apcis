using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apcis.Models
{
    public class ListeAttente
    {
        public int ID { get; set; }
        public virtual ICollection<Etudiant> Etudiants { get; set; }
        public Prestation Prestation { get; set; }
    }
}