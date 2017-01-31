using Apcis.Models;
using Apcis.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Apcis
{
    public static class ExtensionMethods
    {

        public static string AsFormat(this string self, params object[] objs)
        {
            return string.Format(self, objs);
        }

        public static string Between(this string self, string left, string right)
        {
            var start = self.IndexOf(left);
            if (start < 0 || self.Length <= start + 1)
                return self;
            var strstr = self.Substring(start + 1, (self.Length - 1) - start);
            var end = strstr.IndexOf(right);
            if (end < 0)
                return self;
            var result = strstr.Substring(0, end - 1);
            return result;
        }

   

        public static string ViewName(this HtmlHelper html)
        {
            return html.ViewContext.RouteData.GetRequiredString("action");
        }


        public static void Remove<T>(this ApplicationDbContext db)
        {

            foreach (var t in db.Set(typeof(T)))
            {
                db.Set(typeof(T)).Remove(t);
            }
        }

        public static string ToQueryParams<T>(this IEnumerable<T> list, string listName)
        {
            Func<string, int, string> queryizer = (raw, index) => 
            {
                return string.Format("{0}[{1}]=", listName, index) + raw;
            };
            Func<string, string, int, string> reducer = (curr, next, index) => 
            {
                return 
                queryizer(curr, index) + 
                "&" + 
                queryizer(next, index) + 
                "&".ButIf(index == list.Count(), "");
            };

            var listToString = list.Select(x => x.ToString());
            var query = listToString.Reduce("?", (curr, next, index) =>  reducer(curr, next, index) );
            return query;
        }

        public static class QueryCollectionHelper
        {
            public class EnumerableStringifier
            {
                public string Query { get; private set; }

                public string ToQueryParams<T>(IEnumerable<T> list, string listName)
                {
                    return ToQueryParams(list, listName, (x) => { return x.ToString(); });
                }

                public string ToQueryParams<T>(IEnumerable<T> list, string listName, Func<T, string> customStringifyier)
                {
                   

                    Func<string, int, string> queryizer = (raw, index) =>
                    {
                        return string.Format("{0}[{1}]=", listName, index) + raw;
                    };
                    Func<string, string, int, string> reducer = (curr, next, index) =>
                    {
                        return
                        queryizer(curr, index) +
                        "&" +
                        queryizer(next, index) +
                        "&".ButIf(index == list.Count(), "");
                    };

                    var listToString = list.Select(customStringifyier);
                    var query = listToString.Reduce("?", (curr, next, index) => reducer(curr, next, index));
                    return query;
                } 
            }

            public class EnumerableDestringifier
            {

                private string query { get; set; }

                public EnumerableDestringifier(EnumerableStringifier es)
                {
                    query = es.Query;
                }

                public IEnumerable<int> ToEnumerableInt()
                {
                    List<int> list = new List<int>();
                    var querySplit = query.Split('&');
                    foreach (var q in querySplit)
                    {
                        var value = q.Split('=')[1];
                        int parsed;
                        if (int.TryParse(value, out parsed))
                        {
                            list.Add(parsed);
                        }
                        else
                        {
                            throw new Exception("query parametre was not an integer");
                        }
                    }
                    return list;
                }

                public IEnumerable<string> ToEnumerableString()
                {
                    List<string> list = new List<string>();
                    var querySplit = query.Split('&');
                    foreach (var q in querySplit)
                    {
                        var value = q.Split('=')[1];
                        list.Add(value);
                    }
                    return list;
                }

            }
            
            public static EnumerableStringifier CreateQuery()
            {
                return new EnumerableStringifier();
            }
            public static EnumerableDestringifier Create(EnumerableStringifier es)
            {
                return new EnumerableDestringifier(es);
            }

        }

    

        public static IHtmlString GetHtml(this Controller c, PartialViewResult partialView )
        {
            
            using (var sw = new StringWriter())
            {
                partialView.View = ViewEngines.Engines
                  .FindPartialView(c.ControllerContext, partialView.ViewName).View;

                var vc = new ViewContext(
                  c.ControllerContext, partialView.View, partialView.ViewData, partialView.TempData, sw);
                partialView.View.Render(vc, sw);

                var partialViewString = sw.GetStringBuilder().ToString();

                return new HtmlString(partialViewString);
            }
        }

        

        public static string GetAntiForgeryHeader(this HtmlHelper helper)
        {
            string cookieToken, formToken;
            System.Web.Helpers.AntiForgery.GetTokens(null, out cookieToken, out formToken);
            return cookieToken + ":" + formToken;
        }

        public static string OrIfNull(this string tester, string valueIfNull)
        {
            if (tester == null)
                return (valueIfNull);
            return (tester);
        }

        public static string Replace(this string text, string search, string replace, int limit)
        {
            string replaced = null;
            while (limit > 0)
            {
                int pos = text.IndexOf(search);
                if (pos < 0)
                {
                    if (replaced != null)
                        return replaced;
                    return text;
                }
                replaced = text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
                limit--;
            }
            return replaced;
        }


        public static bool IgnoreCaseContains(this string testThis, string findThis)
        {
            return testThis.ToUpper().Contains(findThis.ToUpper());

        }



        public static int IndexWhere<T>(this ICollection<T> collection, Func<T, bool> whereFunc)
        {
            foreach (var x in collection.Select
            ((value, index) => new { value, index }))
            {
                if (whereFunc(x.value))
                    return (x.index);
            }
            return (-1);
        }

        public static IEnumerable<T> Each<T>(this IEnumerable<T> list, Action<T, int> action)
        {
            list.Select((x, i) => new { value = x, index = i }).ToList().ForEach((x) => { action(x.value, x.index); });
            return list;
        }

        public static IEnumerable<T> Each<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var v in list)
            {
                action(v);
            }
            return list;
        }

        public static T Reduce<T>(this IEnumerable<T> list, T init, Func<T, T, T> reducer)
        {
            foreach (var v in list)
            {
                init = reducer(init, v);
            }
            return init;
        }

        public static T Reduce<T>(this IEnumerable<T> list, T init, Func<T, T, int, T> reducer)
        {
            int i = 0;
            foreach (var v in list)
            {
                init = reducer(init, v, i++);
            }
            return init;
        }


        public static T ButIf<T>(this T t, Expression<Func<T, bool>> ifTrue, T useThisValue)
        {
            if (ifTrue.Compile()(t))
            {
                return (useThisValue);
            }
            return (t);
        }

        public static T ButIf<T>(this T t, bool ifTrue, T useThisValue)
        {
            if (ifTrue)
            {
                return (useThisValue);
            }
            return (t);
        }

        public static void Times(this int i, Action action)
        {
            int repeat = 0;
            while (repeat < i)
            {
                action();
                repeat++;
            }
              
        }

        public static string ConcatIf(this string s, bool condition, string toConcat)
        {
            if (condition)
                return s + toConcat;
            return s;
        }

        public static string Delete(this string s, string deleteThis)
        {
            return s.Replace(deleteThis, "");
        }

        public static string DeleteIf(this string s, bool condition, string removeThis)
        {
            if (condition)
            {
                return s.Replace(removeThis, "");
            }
            return s;
        }

        public static string OrEmptyIf(this string s, bool condition)
        {
            if (condition)
            {
                return "";
            }
            return s;
        }

        public static string InQuotes(this string s)
        {
            return "\"" + s + "\"";
        }


        public static void Times(this int i, Action<int> actionWithIndex)
        {
            int repeat = 0;
            while (repeat < i)
            {
                actionWithIndex(i);
                repeat++;
            }
           

        }


        public static T TimesTo<T>(this int i, T initial, Action<T> actionWithType)
        {
            int repeat = 0;
            
            while (repeat < i)
            {
                actionWithType(initial);
                repeat++;
            }
            return initial;
        }


        public static int ToNearestMultipleOf(this int thisint, int roundTo)
        {
            int up = thisint;
            int down = thisint;

            while (up % roundTo != 0 && down % roundTo != 0)
            {
                up++;
                down--;
            }
            return (up % roundTo == 0) ? up : down;
        }

        

        public static List<T> Replace<T>(this List<T> list, Func<T, bool> where, Func<T, T> replacement)
        {
            List<T> ret = new List<T>();

            foreach (var x in list)
            {
               
                if (where(x))
                {
                    ret.Add(replacement(x));
                }
                else
                    ret.Add(x);
            }
            return (ret);
        }

        public static List<T> Replace<T>(this List<T> list, Func<T, bool> where, T replacement)
        {
            List<T> ret = new List<T>();
            foreach (var x in list)
            {
                
                if (where(x))
                {
                    ret.Add(replacement);
                }
                else
                    ret.Add(x);
            }
            return (ret);
        }

        public static List<T> Init<T>(this List<T> list,  int nNewElements) where T : class, new()
        {
            var i = 0;
            while (i < nNewElements)
            {
                list.Add(new T());
                i++;

            }
            return list;
        }

        public static List<T> Init<T>(this List<T> list, int nNewElements, Func<T> setter) 
            where T :  new()
        { 
            var i = 0;
            while (i < nNewElements)
            {
                list.Add(setter());
                i++;
            }
            return list;
        }

        public static List<T> Init<T>(this List<T> list, int nNewElements, Func<int, T> setter)
           where T : new()
        {

            var i = 0;
            while (i < nNewElements)
            {
                list.Add(setter(i));
                i++;
            }
            return list;
        }

        public static string ExternalViewString(this Controller c, string viewName, string controllerName)
        {
            return string.Format("{0}{1}{2}", "~/Views", controllerName, viewName);
        }


    }
}