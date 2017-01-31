using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Apcis
{
    public static class BundleHelper
    {
        public static void Css(bool update)
        {
            if (!update)
                return;
         
            List<string> css = new List<string>();
            var fileNames = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/content/css/main-split"));
            fileNames.Each(fn => css.AddRange(File.ReadAllLines(fn)));
            File.WriteAllLines(HttpContext.Current.Server.MapPath("~/content/css/main.css"), css);
        }
    }
}