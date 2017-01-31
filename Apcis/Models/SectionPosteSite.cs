using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apcis.Models
{
    public class SectionPosteSite
    {
        public int ID { get; set; }
        public string Format { get; set; }
        public virtual ICollection<string> ElementsTextes { get; set; }
        public virtual ICollection<string> UrlsImages { get; set; }
    }
}