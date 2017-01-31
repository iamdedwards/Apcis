using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Collections.Generic;
using Apcis.Models;
using Apcis.SiteLogic;

namespace Apcis.Html
{

    public class CheckBoxList<Entity> : List<CheckBox>
    {

        public CheckBoxList(IEnumerable<Entity> dataSet, Expression<Func<Entity, int>> ID, Expression<Func<Entity, string>> label)
        {
            var idFinder = ID.Compile();
            var labelFinder = label.Compile();
            dataSet.Each(x => Add(new CheckBox(idFinder(x), labelFinder(x))));
        }
    }

    public class CheckBox
    {

        public int Value { get; set; }

        public string Label { get; set; }

        public bool IsChecked { get; set; }

        public CheckBox(int value, string label)
        {
            Value = value;
            Label = label;
        }
    }


    public static class Icon
    {
        public enum ID{
            Telephone = 1,
            Email,
            Map,
            Refresh,
            DownArrow,
            RightArrow,
            RightChevron,
        };

        public class IconValue{

            public string html { get; set; }
            public string name { get; set; }
            public string label { get; set; }
            private string template = "<span class=\"icon {0} \"></span>";

            public IconValue(string givenName)
            {
                html = string.Format(template, givenName);
                name = givenName;
            }
        }

        private static Dictionary<ID, IconValue> values = new Dictionary<ID, IconValue>()
        {
            {ID.Telephone, new IconValue("fa-phone")},
              {ID.Email,  new IconValue("fa-envelope-o") },
                {ID.Map,  new IconValue("fa-map-marker") },
            {ID.Refresh, new IconValue("fa-refresh") },
            {ID.DownArrow, new IconValue("fa-arrow-down")},
            {ID.RightArrow, new IconValue("fa-right-arrow") },
            {ID.RightChevron, new IconValue("fa-chevron-right" ) }

        };

        public static IconValue Get(ID icon, string labelText)
        {
            var x = values[icon];
            x.label = labelText;
            return x;
        }

        public static IconValue Get(ID icon)
        {
            var x = values[icon];
            return x;
        }
    }

    public static class Elements
    {
        private static string quote = "\"";
        private static string cssClass(string cssClass, bool closeQuote = true)
        {
            return "class=" + quote + cssClass + ((closeQuote == true) ? quote : "") + " ";
        }

        private static string attribute(string attribute, string value)
        {
            return attribute + "=" + quote + value + quote + " ";
        }

        public static IHtmlString Button(this HtmlHelper helper, string text)
        {
            var element = string.Format("<a {0}>{1}</a>",
                HtmlMethods.cssClass("button"),
                text);

            return new HtmlString(Layout.Format(element));
        }

        public static IHtmlString Button(this HtmlHelper helper, string text, string onclick)
        {
            var element = string.Format("<a {0} {1}>{2}</a>", 
                HtmlMethods.cssClass("button"),
                HtmlMethods.attribute("onClick", onclick), 
                text);
            
            return new HtmlString(Layout.Format(element));
        }

        public static IHtmlString Button(this HtmlHelper helper, string text, Dictionary<string, string> htmlAttributes)
        {
            var button = string.Format("<a {0}>{1}</a>", HtmlMethods.cssClass("button"), text);
            foreach (var v in htmlAttributes)
            {
               button = HtmlMethods.AddOrUpdateAttribute(button, v.Key, v.Value);
            }
            return new HtmlString(Layout.Format(button));
        }

        public static IHtmlString IconButton(this HtmlHelper helper, Icon.IconValue icon, string link = null)
        {
            string id = icon.name + "Button";

            var html = icon.html;

            html = html.Replace("icon", "icon buttonIcon");

            var element = string.Format("<a {0}{1}>{2}</a>", 
                attribute("id", id), 
                cssClass("button"), html);

            if (link != null)
            {
                element = element.Replace("<a ", string.Format("<a {0}", attribute("href", link)));
            }

            var labelHtml = string.Format("<label {0}{1}>{2}</label>", attribute("for", id), cssClass("label"), icon.label);

            return Wrap(helper, new HtmlString(element), new HtmlString(labelHtml));
        }

       

      

       

        public static IHtmlString Image(this HtmlHelper helper, string href)
        {
            var element = "<img src=\"" + href + "\"></img>";
            return new HtmlString(Layout.Format(element));
        }

        public static IHtmlString Icon(this HtmlHelper helper, string href, string type, string label)
        {
            var template = string.Format("<div><a href=\"{0}\" class=\"icon style2 {1}\"><span class=\"label\">{2}</span></a><div>", href, type, label);
            return new HtmlString(template);

        }


        public static IHtmlString UseClass(this HtmlHelper helper, IHtmlString html, string classInCss, int domElements = 1)
        {
            var text = html.ToString();
            text = text.Replace("class=" + quote, cssClass(classInCss, closeQuote: false), limit: domElements);
            return (new HtmlString(text));
        }


     

        public static IHtmlString Wrap(this HtmlHelper helper, params IHtmlString[] elements)
        {
            return new HtmlString("<div>"+string.Join(" ", elements.ToList())+"</div>");
        }

        public static IHtmlString Wrap(this HtmlHelper helper, string cssClass, params IHtmlString[] elements)
        {
            return new HtmlString("<div " + Elements.cssClass(cssClass) +">"+ string.Join(" ", elements.ToList()) + "</div>");
        }

        public static IHtmlString Input<T, TProperty>(this HtmlHelper<T> helper, Expression<Func<T, TProperty>> expression, string placeHolderText)
        {
            var html = System.Web.Mvc.Html.InputExtensions.TextBoxFor(helper, expression, new { placeholder = placeHolderText, value = placeHolderText });
            html = html.RemoveAttribute("value");
            return html;
        }

       

        private static MvcHtmlString RemoveAttribute(this MvcHtmlString html, string attribute)
        {
            var tmp = "";

            html.ToString().Split(' ').Each(x =>
            {

                if (!x.Contains(attribute))
                    tmp += " " + x;

            });
            var ret = new MvcHtmlString(tmp);
            return ret;

        }

      

    }
}