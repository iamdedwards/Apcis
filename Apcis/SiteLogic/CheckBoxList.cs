using Apcis.DAL;
using Apcis.Html;
using Apcis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace Apcis.SiteLogic
{

    public class IsJoinedDictionary
    {
       
        public static Dictionary<string, bool> Default<T>() where T : class, IDisplayable, IIdentifiable
        {
            var set = DAO.GetDbSet<T>().AsEnumerable();
            var dict = set.ToDictionary(key => string.Format("{0}:{1}", key.Display(), key.ID), value => false); 
            return dict;
        }

        private static Dictionary<string, bool> toDictionary<T>(IEnumerable<T> set, bool isJoined ) where T : class, IDisplayable, IIdentifiable
        {
            var dict = set.ToDictionary(key => string.Format("{0}:{1}", key.Display(), key.ID),
                    value => isJoined);
            return dict;
        }

        public static Dictionary<string, bool> PreSelectFromDbSet<T>(IEnumerable<T> selected) where T : class, IDisplayable, IIdentifiable
        {

            var set = DAO.GetDbSet<T>().AsEnumerable();
            var dictionary = toDictionary(set.Where(x => !selected.Contains(x)), isJoined: false);

            toDictionary(selected, isJoined: true).Each(x => { dictionary.Add(x.Key, x.Value); });

            return dictionary;
        }

        public static Dictionary<string, bool> PreSelectFromData<T>(IEnumerable<T> data) where T : class, IDisplayable, IIdentifiable
        {
            return toDictionary(data, isJoined: true);
        }

        public static IEnumerable<T> ToDbSet<T>(Dictionary<string, bool> dataset) where T : class, IDisplayable
        {
            List<T> list = new List<T>();
            var set = dataset.Where(j => j.Value == true).Select(d => int.Parse(d.Key.Split(':')[1]));
            set.Each(x => list.Add(DAO.Find<T>(x)));
            return list;
        }

        public static IEnumerable<int> ToIDSet<T>(Dictionary<string, bool> dataset) where T : class, IDisplayable, IIdentifiable
        {
            return ToDbSet<T>(dataset).Select(x => x.ID);
        }

      
        public static IHtmlString ToCheckBoxes(Dictionary<string, bool>isJoinedDictionary, string dictionaryName)
        {
            List<string> elements = new List<string>();

            foreach (var keyval in isJoinedDictionary)
            {
                var displayName = keyval.Key.Split(':')[0];
                var name = string.Format("{0}[{1}]", dictionaryName, keyval.Key);

                var checkBox = checkBoxSetter(dictionaryName, name, keyval.Value, false);

                var hidden = hiddenInputSetter(name);

                var label = labelSetter(displayName);

                elements.Add(checkBox + hidden + label);
            }

            var reduced = elements.Reduce("", (x, y) => { return x += " " + y; });
            return new HtmlString(reduced);

        }


        private static string checkBoxSetter(string displayName, string inputName, bool isJoined, bool isDisabled)
        {

            var checkBox = string.Format("<input id={0} name={1} type={2} value={3} [checked] [disabled]>",

                displayName.InQuotes(),
                 inputName.InQuotes(),
                 "checkbox".InQuotes(),
                 "true".InQuotes()
                   );

            checkBox = checkBox.Replace("[checked]", ((isJoined) ? "checked=" + "checked".InQuotes() : ""));

            var disabled = "onclick=" + "return false;".InQuotes() + " class=" + "disabled".InQuotes();

            checkBox = checkBox.Replace("[disabled]", ((isDisabled) ? disabled : ""));

            return checkBox;
          
        }


        private static string hiddenInputSetter(string name)
        {
            var hidden = string.Format("\n<input name={0} type={1} value={2}>\n",
                    name.InQuotes(), "hidden".InQuotes(), "false".InQuotes());
            return hidden;
        }

        private static string labelSetter(string displayName)
        {
            var label = string.Format("<label for={0}>{1}</label>\n",
                    displayName.InQuotes(), displayName);
            return label;
        }
       

        public static IHtmlString ToCheckBoxes(Dictionary<string, bool> isJoinedDictionary, string dictionaryName, IEnumerable<int> checkable)
        {
            List<string> elements = new List<string>();

            foreach (var keyval in isJoinedDictionary)
            {
                var displayName = keyval.Key.Split(':')[0];
                var inputName = string.Format("{0}[{1}]", dictionaryName, keyval.Key);

                var checkBox = checkBoxSetter(displayName, inputName, keyval.Value, !checkable.Contains(int.Parse(keyval.Key.Split(':')[1])));

                var hidden = hiddenInputSetter(inputName);

                var label = labelSetter(displayName);

                elements.Add(checkBox + hidden + label);
            }

            var reduced = elements.Reduce("", (x, y) => { return x += " " + y; });
            return new HtmlString(reduced);

        }

    }
  
}