using Apcis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apcis.SiteLogic
{
    public static partial class DropDowns
    {

        private static Scope _scope;

        private static Scope scope
        {

            get { return (_scope == null) ? new Scope(useSessionState: true) : _scope; }
            set { _scope = value; }
        }

        public static IDictionary<string, object> Scope
        {
            get { return scope.InnerDictionary; }
            set { scope = new SiteLogic.Scope(value); }
        }

        public class DropDown<T> : List<SelectListItem>
        {

            public DropDown(IEnumerable<T> dataSet, Func<T, string> valueFunc, Func<T, string> textFunc, string selected, bool placeholder)
            {
                dataSet.Each(member => Add(new SelectListItem() { Value = valueFunc(member), Text = textFunc(member) }));

                if (placeholder)
                {
                    this.Insert(0, new SelectListItem() { Value = "placeHolder", Text = selected });
                    SelectedValue = "placeHolder";
                }
                else
                {
                    SelectedValue = selected;
                }
            }

            public string SelectedValue = "";

        }

        public static IEnumerable<SelectListItem> SetNumbers(int from, int untill)
        {
            return SetNumbers(from, untill, 1);
        }

        public static IEnumerable<SelectListItem> SetNumbers(int from, int untill, int step)
        {
            return SetNumbers(string.Empty, from, untill, step);
        }

        public static IEnumerable<SelectListItem> SetNumbers(string mask, int from, int untill, int step = 1)
        {
            var elements = new List<SelectListItem>();
            for (int i = from; i <= untill; i += step)
            {
                elements.Add(
                    new SelectListItem()
                    {
                        Text = (mask == string.Empty) ? i.ToString() : string.Format(mask, i),
                        Value = i.ToString()
                    });
            }
            Numbers = elements;
            return elements;
        }

        public static IEnumerable<SelectListItem> Numbers
        {
            get { return scope.Recover<IEnumerable<SelectListItem>>("numbers"); }
            set { scope.Assign(value, "numbers"); }
        }

        public static DropDown<Prestation> Prestations
        {
            get { return scope.Recover<DropDown<Prestation>>(); }
            set { scope.Assign(value); }
        }

        public static DropDown<Pole> Poles
        {
            get { return scope.Recover<DropDown<Pole>>(); }
            set { scope.Assign(value); }
        }

        public static DropDown<Territoire> Territoires
        {
            get { return scope.Recover<DropDown<Territoire>>(); }
            set { scope.Assign(value); }
        }

        public static DropDown<JourEtHeure> JourEtHeure
        {
            get { return scope.Recover<DropDown<JourEtHeure>>(); }
            set { scope.Assign(value); }
        }

      
    }
}