
using System.Web;
using System.Web.Mvc;

namespace Apcis.Html
{
    public static class Layout
    {
        public static string Format(string html, int newLines, int tabs)
        {
            var tab = '\t';
            var newLine = '\n';


            for (int i = tabs; i > 0; i--)
            {
                html = tab + html;
            }

            for (int i = newLines; i > 0; i--)
            {
                html = newLine + html;
            }

            return html;
        }

        public static string Format(string html, int tabs)
        {
            return Format(html, tabs, 1);
        }

        public static string Format(string html)
        {
            return Format(html, 1, 1);
        }

        public static IHtmlString TwoElements(this HtmlHelper helper, IHtmlString left, IHtmlString right)
        {
            return new HtmlString(LayoutWork.ChildrenTemplate("childrenHalf", left.ToString(), right.ToString()));
        }

        public static IHtmlString TwoElements(this HtmlHelper helper, string useClass, IHtmlString left, IHtmlString right)
        {
            return new HtmlString(LayoutWork.ChildrenTemplate("childrenHalf",
              HtmlMethods.addOrUpdateCssClass(left.ToString(), useClass),
              HtmlMethods.addOrUpdateCssClass(right.ToString(), useClass)));
        }

        public static IHtmlString ThreeElements(this HtmlHelper helper, IHtmlString left, IHtmlString middle, IHtmlString right)
        {
            return new HtmlString(LayoutWork.ChildrenTemplate("childrenThird", left.ToString(), middle.ToString(), right.ToString()));
        }

        public static IHtmlString ThreeElements(this HtmlHelper helper, string useClass, IHtmlString left, IHtmlString middle, IHtmlString right)
        {
            return new HtmlString(LayoutWork.ChildrenTemplate("childrenThird", 
                HtmlMethods.addOrUpdateCssClass(left.ToString(), useClass),
                HtmlMethods.addOrUpdateCssClass(middle.ToString(), useClass),
                HtmlMethods.addOrUpdateCssClass(right.ToString(), useClass)));
        }

        public static IHtmlString FourElements(this HtmlHelper helper, IHtmlString left, IHtmlString middleLeft, IHtmlString middleRight,  IHtmlString right)
        {
            return new HtmlString(LayoutWork.ChildrenTemplate("childrenQuarter",
                left.ToString(),
                middleLeft.ToString(),
                middleLeft.ToString(),
                right.ToString()));
        }

        public static IHtmlString FourElements(this HtmlHelper helper, string useClass, IHtmlString left, IHtmlString middleLeft, IHtmlString middleRight, IHtmlString right)
        {
            return new HtmlString(LayoutWork.ChildrenTemplate("childrenQuarter",
               HtmlMethods.addOrUpdateCssClass(left.ToString(), useClass),
               HtmlMethods.addOrUpdateCssClass(middleLeft.ToString(), useClass),
               HtmlMethods.addOrUpdateCssClass(middleLeft.ToString(), useClass),
               HtmlMethods.addOrUpdateCssClass(right.ToString(), useClass)));
        }






        public static IHtmlString ImageTextLeft(this HtmlHelper helper, string imgSrc, string text)
        {
            return new HtmlString(LayoutWork.ImageTextTemplate("left", string.Format("<img {0} />", HtmlMethods.attribute("src", imgSrc)) , string.Format("<span >{0}</span>", text)));
        }

        public static IHtmlString ImageTextLeft(this HtmlHelper helper, string containerClass, string imgSrc, string text)
        {

            return new HtmlString(LayoutWork.ImageTextTemplate("left "+containerClass, string.Format("<img {0} />", HtmlMethods.attribute("src", imgSrc)), string.Format("<span >{0}</span>", text)));

        }



    }
}