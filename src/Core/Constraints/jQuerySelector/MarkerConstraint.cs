using System.Text.RegularExpressions;

namespace WatiN.Core.Constraints.jQuerySelector
{
    public class MarkerConstraint : AttributeConstraint
    {
        public MarkerConstraint(string markerClass)
            //: base("class", new StringContainsAndCaseInsensitiveComparer(markerClass)) 
            : base("class", new Regex(@"(^|\s)" + Regex.Escape(markerClass) + @"(\s|$)"))
        {
        }
    }
}
