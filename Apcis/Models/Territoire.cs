using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apcis.Models
{
    
    public class Territoire : IIdentifiable, IDisplayable
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public ICollection<Pole> Poles { get; set; }

        public string Display() { return Nom; }
    }
}