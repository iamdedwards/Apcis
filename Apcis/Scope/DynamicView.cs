using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apcis.Scoope
{

    public static class Scope
    {

        private static string ID<T>(string uniqueID)
        {
            if (uniqueID != null)
                return uniqueID;
            var id = typeof(T).ToString();
            return id;
        }

        public static void Remove<A>(string uniqueId = null) where A : class
        {
            if (Contains<A>(uniqueId.OrIfNull(typeof(A).ToString())))
                HttpContext.Current.Session.Remove(uniqueId.OrIfNull("") + typeof(A));
        }

        public static A Recover<A>(string uniqueID = null) where A : class
        {
            return HttpContext.Current.Session[ID<A>(uniqueID)] as A;
        }

        public static A Assign<A>(A anEntity, string uniqueID = null) where A : class
        {
            HttpContext.Current.Session[ID<A>(uniqueID)] = anEntity;
            return anEntity;
        }


        public static bool Contains<A>(string uniqueID = null) where A : class
        {
            return (HttpContext.Current.Session[ID<A>(uniqueID)] == null) ? false : true;
        }

    }


}
