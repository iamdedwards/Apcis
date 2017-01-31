using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apcis;
using Apcis.Controllers;
using System.Web.Mvc;

namespace ApcisTestConsole.Debuggers
{
    static class PrestationsControllerDebug
    {
        public static void Create()
        {
            var controller = new PrestationsController();
            var result = controller.Create();
            Console.WriteLine(result.ToString());
        }
    }
}
