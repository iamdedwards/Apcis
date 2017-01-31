using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apcis.Models
{
    public class Pole : IIdentifiable, IDisplayable
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public ICollection<Prestation> Prestations { get; set; }
        public virtual ICollection<VuePoleTerritoire> Vues { get; set; }

        public string Display() { return Nom; }
    }

    public class VuePoleTerritoire
    {
        public int ID { get; set; }
        public Territoire Territoire { get; set; }
        public ICollection<Prestation> Prestations { get; set; }
    }
}