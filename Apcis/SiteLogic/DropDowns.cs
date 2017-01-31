using Apcis.Html;
using Apcis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Apcis.SiteLogic
{

    public static partial class DropDowns
    {

        private static DropDown<T> preSelected<T>(IEnumerable<T> data, Func<T, string> valueFunc, Func<T, string> textFunc, int selected) where T : class
        {
            return new DropDown<T>(
               dataSet: Apcis.DAL.DAO.GetDbSet<T>(),
               valueFunc: valueFunc,
               textFunc: textFunc,
               selected: selected.ToString(),
               placeholder: false);
        }

      

        private static DropDown<T> withPlaceHolder<T>(IEnumerable<T> data, Func<T, string> valueFunc, Func<T, string> textFunc, string placeholder) where T : class
        {
            return new DropDown<T>(
               dataSet: Apcis.DAL.DAO.GetDbSet<T>(),
               valueFunc: valueFunc,
               textFunc: textFunc,
               selected: placeholder.ToString(),
               placeholder: true);
        }

        public static DropDown<TNameable> Default<TNameable>(int selected) where TNameable : class, IIdentifiable, IDisplayable
        {
            return preSelected(Apcis.DAL.DAO.GetDbSet<TNameable>(), x => x.ID.ToString(), x => x.Display(), selected);
        }

        public static DropDown<TNameable> Default<TNameable>(string placeHolder) where TNameable : class, IIdentifiable, IDisplayable
        {
            return withPlaceHolder(Apcis.DAL.DAO.GetDbSet<TNameable>(), x => x.ID.ToString(), x => x.Display(), placeHolder);
        }

        public static  DropDown<TNameable> Filter<TNameable>(Expression<Func<TNameable, bool>> where, int selected) where TNameable : class, IIdentifiable, IDisplayable
        {
            return preSelected(Apcis.DAL.DAO.GetDbSet<TNameable>().Where(where), x => x.ID.ToString(), x => x.Display(), selected);
        }

        public static DropDown<TNameable> Filter<TNameable>(Expression<Func<TNameable, bool>> where, string placeholder) where TNameable : class, IIdentifiable, IDisplayable
        {
            return withPlaceHolder(Apcis.DAL.DAO.GetDbSet<TNameable>().Where(where), x => x.ID.ToString(), x => x.Display(), placeholder);
        }

        public static DropDown<T> Create<T>(IEnumerable<T> data, string placeholder) where T : class, IIdentifiable, IDisplayable
        {
            return withPlaceHolder(data, x => x.ID.ToString(), x => x.Display(), placeholder);
        }

        public static DropDown<T> Create<T>(IEnumerable<T> data, int selected) where T : class, IIdentifiable, IDisplayable
        {
            return preSelected(data, x => x.ID.ToString(), x => x.Display(), selected);
        }

    }
}