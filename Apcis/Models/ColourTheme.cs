using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apcis.Models
{
    public class ColourActiviteElementValue
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
    }

    public class ColourActiviteElement
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<ColourActiviteElementValue> Values { get; set; }
    }

    public class CurrentColourActiviteElement : ColourActiviteElement
    {

    }

}