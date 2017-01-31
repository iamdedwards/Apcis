using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace argh
{
    public static class extension
    {
        public static string orIfNull(this string couldBe, string ifNull)
        {
            if (couldBe == null)
                return (ifNull);
            return (couldBe);
        }
    }
    class Program
    {
        private static string template = "<div class=\"holder {0}\">{*}</div><br class=\"clearFloat\"/>";
        static void Main(string[] args)
        {


            string left = "<p></p>";
            string right = "<p></p>";

            var html = String.Format(template, "childrenHalf");
            html.Replace("{*}", left + right);
            Console.WriteLine(html);
            while (true) ;

            
    

         }
    }
}
