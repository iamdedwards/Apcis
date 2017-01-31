using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Apcis.Extensions;

namespace Apcis.Resources
{

    public enum Gender
    {
        Masculin,
        Feminine,
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class GenderAttribute : System.Attribute
    {
        public virtual Gender Gender { get; }
    }
 
    public class MasculineAttribute : GenderAttribute
    {
        public override Gender Gender { get { return Gender.Masculin; }}
    }

    public class FeminineAttribute : GenderAttribute
    {
        public override Gender Gender { get { return Gender.Feminine; } }
    }

    public class Validate<T>
    {
        public IHtmlString RequiredFor<TProp>(Expression<Func<T, TProp>> propertyFinder)
        {

            var attrs = typeof(T).GetProperty(propertyFinder.GetMemberName()).GetCustomAttributes(false);

            var attr = attrs.SingleOrDefault(att =>  att is GenderAttribute) as GenderAttribute;

            var e = "e".OrEmptyIf(Gender.Masculin == attr.Gender);
            var x = Extensions.NameFromExpression.GetMemberName(propertyFinder);
            string msg = InputValidationTemplates.RequiredField.AsFormat(e, x);
            return new HtmlString(msg);
        }

        //public static string RequiredElement<T>(Type modelType, string fieldName, string rule)
        //{
        //    var msg = RequiredElement(modelType, fieldName);
        //    msg = msg + " " + rule;
        //    return msg;
        //}

    }
}