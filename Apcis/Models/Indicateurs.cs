using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apcis.Models
{
    public class RegionIndicateur
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public string CodePostale { get; set; }
        public Territoire ApcisPresent { get; set; }
    }

    public class GroupementIndicateur
    {
        public int ID { get; set; }
        public string Nom { get; set; }
    }

    public class Indicateur
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public GroupementIndicateur Groupement { get; set; }
        public virtual ICollection<VueIndicateurRegion> Vues { get; set; }
    }

    public class VueIndicateurRegion
    {
        public int ID { get; set; }
        public RegionIndicateur Region { get; set;}
        public double Value { get; set; }
        public DateTime Annee { get; set; }
        public string Source { get; set; }
    }

    public class HistoriqueIndicateur : Indicateur
    {
        public virtual Indicateur IndicateurActuel { get; set; }
    }
}