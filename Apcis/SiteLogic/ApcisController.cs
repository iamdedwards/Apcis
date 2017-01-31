using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apcis.SiteLogic
{
    public class ApcisController : Controller
    {
        public ApcisController()
        {
            DropDowns.Scope = ViewData;
        }
    }
}