using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apcis.Html
{

    public static class HtmlMethods
    {
        public static  string quote = "\"";
        public static string cssClass(string cssClass, bool closeQuote = true)
        {
            return " class=" + quote + cssClass + ((closeQuote == true) ? quote : "") + " ";
        }

        public static string attribute(string attribute, string value)
        {
            return attribute + "=" + quote + value + quote + " ";
        }

        public static string addOrUpdateCssClass(string html, string cssClassString)
        {
            var split = html.Split('>').ToList();
            html = (split[0].Contains("class")) ?
                html.Replace("class=\"", cssClass(cssClassString, closeQuote:false)) : 
                html.Replace(">", cssClass(cssClassString) + ">", limit: 1);
            return html;
        }

        public static string AddOrUpdateAttribute(string html, string attrKey, string attrVal)
        {         
            html = html.Replace(">", attribute(attrKey, attrVal) + ">", limit: 1);
            return html;
        }

        public static string RemoveAttribute(string html, string attribute)
        {
            var tmp = "";
            bool goingThrough = false;

            html.Split(' ').Each(x =>
            {
               
                if (x.Contains(attribute))
                {
                    goingThrough = true;
                }
                if (goingThrough == true && x.Contains(quote))
                {
                    goingThrough = false;
                }
                if (!x.Contains(attribute) && !goingThrough)
                    tmp += " " + x;
            });
            return tmp;
        }
    }

    public class LayoutWork
    {

       
        
      

        public static string ChildrenTemplate(string cssClass, params string[] values)
        {
            var template = "<div class=\"holder {0}\">\n!values!\n</div>\n<br class=\"clearFloat\"/>";

            var html = String.Format(template, cssClass);

            var split = values[values.Count() - 1].Split('>').ToList();

            values[values.Count() - 1] = (split[0].Contains("class")) ?
                values.Last().Replace("class=\"", "class=\"lastChild ") :
                values.Last().Replace(">", " class=\"lastChild\">", limit: 1);

            var valuesConcat = values.Reduce("", (x, y) => { y = Layout.Format(y, tabs: 1); return x += " " + y; });

            html = html.Replace("!values!", valuesConcat);

            return html;
        }

        public static string ImageTextTemplate(string direction, string left, string right)
        {
            var template = "<div {0} >\n {1}{2} </div>\n <br {3}/>";

            var html = String.Format(template,
                
                HtmlMethods.cssClass("imageText " + direction), 
                Layout.Format(left, 1, 1),
                Layout.Format(right, 1, 1),
                HtmlMethods.cssClass("clearFloat")
                
                );

            return html;
        }
    }

}