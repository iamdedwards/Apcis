using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Apcis.SiteLogic
{
    using Models;
    using System.Linq.Expressions;
    using UReplacementFuncDictionary = Dictionary<string, Func<string, string>>;

    public static class CssHelper
    {
        private static string marginPath = "~/content/css/main_j_dimensions_childwidth.css";
        private static string colorPath = "~/content/css/main_l_colours.css";


        private static string replace(string currentCssExpression, string replacementValue)
        {
            var split = currentCssExpression.Split(':').ToList();
            if (split.Count != 2)
                return "error";
            return string.Format("{0}{1}{2}", split[0], ":", replacementValue);
        }

        private static bool find(string inThis, string findThis)
        {
            return inThis.IgnoreCaseContains(findThis);
        }

        public static void ChangeColours(List<Update<ColourActiviteElementValue>> update)
        {
            IOHelper.ForEachLine(colorPath, line =>
            {
                update.Each(entry =>
                {
                    if (find(line, entry.Before.Colour))
                    {
                        line = replace(entry.Before.Colour, entry.After.Colour);
                        update.Remove(entry);
                    }

                });
                return line;
            });
        }

        public static void ChangeMargin(int changeTo)
        {
            string between = string.Format("{0}{1}{2}", changeTo, "%", "/*between*/");
            string half = string.Format("{0}{1}{2}", 50 - (changeTo), "%", "/*half*/");
            string third = string.Format("{0}{1}{2}", 50 - (2 * changeTo), "%", "/*third*/");
            string quarter = string.Format("{0}{1}{2}", 50 - (3 * changeTo), "%", "/*quarter*/");

            IOHelper.ForEachLine(colorPath, line => {

                if (find(line, "between"))
                    line = replace(line, between);
                if (find(line, "half"))
                    line = replace(line, half);
                if (find(line, "quarter"))
                    line = replace(line, quarter);
                if (find(line, "third"))
                    line = replace(line, third);
                return line;

            });
        }












    }
   
}