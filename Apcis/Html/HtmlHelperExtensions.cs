using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apcis.Html
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString Join(List<IHtmlString> elements)
        {
            return new HtmlString(string.Join(" ", elements.ToList()));
        }

        public static IHtmlString CheckBoxes(this HtmlHelper helper, Dictionary<string, bool> isJoinedDictionary, string dictionaryName)
        {
            List<string> elements = new List<string>();
            var s = nameof(isJoinedDictionary);
            foreach (var keyval in isJoinedDictionary)
            {
                var displayName = keyval.Key.Split(':')[0];
                var htmlToPassValue = string.Format("{0}[{1}]", dictionaryName, keyval.Key);
                var checkbox = string.Format(

                    @"<input {0} {1} {2} {3}>
                      <input {4} {5} {6}>
                    <label {7}>{8}</label>",
                    HtmlMethods.attribute("id", displayName),
                    HtmlMethods.attribute("name", htmlToPassValue),
                    HtmlMethods.attribute("type", "checkbox"),
                    HtmlMethods.attribute("value", "true")
                    .ConcatIf((keyval.Value == true), " " + HtmlMethods.attribute("checked", "checked")),

                    HtmlMethods.attribute("name", htmlToPassValue),
                    HtmlMethods.attribute("type", "hidden"),
                    HtmlMethods.attribute("value", "false"),
                    
                    HtmlMethods.attribute("for", displayName),
                    displayName);
                    elements.Add(checkbox);
              }

            var reduced = elements.Reduce("", (x, y) => { return x += " " + y; });
            return new HtmlString(reduced);

        }

        public static IHtmlString Submit(this HtmlHelper helper, string text)
        {
            var element = "<input type=\"submit\" class=\"button\" value=\"" + text + "\"></input>";
            return new HtmlString(Layout.Format(element));
        }

        public static IHtmlString Submit(this HtmlHelper helper, Dictionary<string, string> htmlAttributes)
        {
            var element = string.Format("<input {0} ></input>",
                HtmlMethods.attribute("type", "submit")
                );
            foreach (var v in htmlAttributes)
            {
                element = HtmlMethods.AddOrUpdateAttribute(element, v.Key, v.Value);
            }
            return new HtmlString(Layout.Format(element));
        }
    }
}