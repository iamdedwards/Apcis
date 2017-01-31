using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apcis.Controllers
{
    public class SiteResourcesController : Controller
    {
        // GET: SiteResources
        public PartialViewResult _svgLogo()
        {
            return PartialView("_svgLogo");
        }
    }
}