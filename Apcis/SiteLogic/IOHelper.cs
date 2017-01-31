using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Apcis.SiteLogic
{
    public static class IOHelper
    {
        public static void ForEachLine(string path, Func<string, string> modify)
        {
            var newLines = File.ReadAllLines(path).ToList().Each((x) => modify(x));
            File.WriteAllLines(path, newLines);
        }
    }
}