using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apcis.Models
{
    public class StatusType
    {
        public int ID { get; set; }
        public string Nom { get; set; }
    }

    public class Status
    {
        public int ID { get; set; }
        public StatusType Type { get; set; }
        public TimeSpan? Pendant { get; set; }
        public DateTime Depuis { get; set; }
    }

    public class Contacte
    {
        public int ID { get; set; }
        public DateTime DateHeure { get; set; }
        public string TypeContacte { get; set; }
    }

    public enum Genre
    {
        Male = 1,
        Female = 2,
    };

    public class PublicApplicationUser
    {
        public Public Public {get;set;}
        public ApplicationUser Identity { get; set; }
    }

    public class Public
    {
        public string Nom { get; set;}
        public string Prenom { get; set; }
        public int ID { get; set; }  
        public Genre Genre { get; set; }
        public int Age { get; set; }
        public virtual ICollection<Status> Statuses { get; set; }
        public virtual ICollection<Contacte> Contacte { get; set; }
        public virtual Etudiant EtantEtudiant { get; set; }
        public virtual Parent EtantParent { get; set; }
    }
}