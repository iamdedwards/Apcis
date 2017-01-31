using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Apcis.SiteLogic;
using Apcis.Models;
using Apcis.ViewModels;

namespace Apcis.Controllers
{
    using ViewModel = UpdateSet<ColourActiviteElementValue>;

    //public class CssController : Controller
    //{
    //    private ApplicationDbContext db = new ApplicationDbContext();

    //    private ViewModel populateViewModel()
    //    {
    //        var current = db.SiteColorActiviteElements.Single(x => x is CurrentColourActiviteElement).Values;
    //        return new UpdateSet<ColourActiviteElementValue>(current);
    //    }

    //    [HttpGet]
    //    public ActionResult ChangeColors()
    //    {
    //        return View("Content", populateViewModel());
    //    }

    //    [HttpPost]
    //    public ActionResult ChangeColors(ViewModel vm)
    //    {
    //        CssHelper.ChangeColours(vm);
    //        db.SiteColorActiviteElements.Single(x => x is CurrentColourActiviteElement).Values = vm.After;
    //        return View("Content", vm);
    //    }

    //    [HttpPost]
    //    private ActionResult ChangeMargin(int i)
    //    {
    //        CssHelper.ChangeMargin(i);
    //        return View("Content", populateViewModel());
    //    } 
    //}
}