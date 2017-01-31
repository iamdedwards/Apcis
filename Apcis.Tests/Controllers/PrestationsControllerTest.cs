using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apcis;
using Apcis.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace Apcis.Tests.Controllers
{
    [TestClass]
    public class PrestationsControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Disposer
            var controller = new PrestationsController();

            // Agir
            ViewResult result = controller.Create() as ViewResult;

            // Affirmer
           
            result.ToString();
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
