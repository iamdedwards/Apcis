using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apcis.Models
{
    public enum Gravite
    {
        Bas,
        Moyen, 
        Haut
    };

    public class PublicCommentaire
    {
        public int ID { get; set; }
        public int IdentityID { get; set; }
        public DateTime Date { get; set; }
        public Gravite Gravite { get; set; }
        public Public Public { get; set; }
        public string Remarques { get; set; }
    }
}