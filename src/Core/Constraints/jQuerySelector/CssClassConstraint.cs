using System.IO;
using System.Text.RegularExpressions;
using WatiN.Core.Comparers;
using WatiN.Core.Interfaces;

namespace WatiN.Core.Constraints.jQuerySelector
{
    public class CssClassConstraint : Constraint // AttributeConstraint
    {
        private RegexComparer _comparer;
        public string ExpectedCssClass { get; private set; }

        public CssClassConstraint(string markerClass)
        {
            _comparer = new RegexComparer(new Regex(@"(^|\s)" + Regex.Escape(markerClass) + @"(\s|$)"));
            ExpectedCssClass = markerClass;
        }

        /// <inheritdoc />//
        public override void WriteDescriptionTo(TextWriter writer)
        {
            writer.Write("CssClassConstraint '{0}'", ExpectedCssClass);
        }

        protected override bool MatchesImpl(IAttributeBag attributeBag, ConstraintContext context)
        {
            string classValue = attributeBag.GetAttributeValue("class");

            if (classValue == null)
                classValue = attributeBag.GetAttributeValue("ClassName");

            if (classValue == null)
                return false;

            return _comparer.Compare(classValue);
        }
    }
}
