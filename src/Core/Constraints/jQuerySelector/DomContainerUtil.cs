namespace WatiN.Core.Constraints.jQuerySelector
{
    public static class DomContainerUtil
    {
        //public static Element ElementByCss(this DomContainer domContainer, string cssSelector)
        //{
        //    CssSelectorConstraint constraint = FindByCss(domContainer, cssSelector);

        //    return domContainer.Element(constraint);
        //}

        public static CssSelectorConstraint FindByCss(DomContainer domContainer, string cssSelector)
        {
            var constraint = new CssSelectorConstraint(new ScriptLoader(), domContainer);
            constraint.Initialize(cssSelector);
            return constraint;
        }
    }
}
