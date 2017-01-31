using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apcis.Models
{
    public class NiveauEcole
    {
        public int ID { get; set; }
        public string Nom { get; set; }
    }

    public class Ecole
    {
        public int ID { get; set; }
        public NiveauEcole Niveau { get; set; }
        public Dispositif Dispositif { get; set; }
        public string Nom { get; set; }
    }

    public class Dispositif
    {
        public int ID { get; set; }
        public string Nom { get; set; }
    }

    public class CursusActuel
    {
        public int ID { get; set; }
        public string Nom { get; set; }
    }

    public class Etudiant
    {
        public int ID { get; set; }
        public Ecole Ecole { get; set; }
        public CursusActuel EtudesSuivi { get; set; }
        public virtual ICollection<Parent> Parents { get; set; }
    }
}