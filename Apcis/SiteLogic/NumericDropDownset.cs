using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apcis.SiteLogic
{
    public class PercentageDropDownSetElement : List<SelectListItem>
    {
        public int Min { get; set; }
        public int Max { get; set; }

        private int _step = 1;
        public int Step { get { return _step; } set { _step = value; } }

        public string Mask { get; set; }

        public PercentageDropDownSetElement AdjustMax(int max)
        {
            return DropDowns.SetNumbers(Mask, Min, max, Step) as PercentageDropDownSetElement;
        }
    }

    public class PercentageDropDownSet : List<PercentageDropDownSetElement>
    {
        public void AdjustMax(PercentageDropDownSetElement caller)
        {
            this.Each((x) =>
            {
                if (x != caller)
                {
                    var newMax = 100 / this.Count - caller.Max;
                    x = x.AdjustMax(newMax);
                }
            });
        }
    }
}