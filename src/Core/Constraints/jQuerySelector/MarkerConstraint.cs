using System.Text.RegularExpressions;

namespace WatiN.Core.Constraints.jQuerySelector
{
    public class MarkerConstraint : AttributeConstraint
    {
        public string MarkerClass { get; private set; }

        public MarkerConstraint(string markerClass)
            //: base("class", new StringContainsAndCaseInsensitiveComparer(markerClass)) 
            : base("class", new Regex(@"(^|\s)" + Regex.Escape(markerClass) + @"(\s|$)"))
        {
            MarkerClass = markerClass;
        }
    }
}
