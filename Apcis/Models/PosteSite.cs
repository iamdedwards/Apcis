using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apcis.Models
{
    public class PosteSite
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Titre { get; set; }
        public virtual ICollection<SectionPosteSite> Sections { get; set; }
    }
}