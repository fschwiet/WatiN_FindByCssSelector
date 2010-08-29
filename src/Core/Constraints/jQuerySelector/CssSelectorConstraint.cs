using System;
using System.IO;
using WatiN.Core.Interfaces;

namespace WatiN.Core.Constraints.jQuerySelector
{
    public class CssSelectorConstraint : MarkerConstraint
    {
        private readonly DomContainer _domContainer;
        private readonly IScriptLoader _scriptLoader;
        private string _cssSelector;

        static int _cssMarkerIndex = 0;

        public CssSelectorConstraint(IScriptLoader scriptLoader, DomContainer domContainer)
            : this(scriptLoader, domContainer, "findByCssMarker" + ++_cssMarkerIndex)
        {
        }

        public CssSelectorConstraint(IScriptLoader scriptLoader, DomContainer domContainer, string markerClass) 
            : base(markerClass)
        {
            _scriptLoader = scriptLoader;
            _domContainer = domContainer;
        }

        public void Initialize(string cssSelector)
        {
            _cssSelector = cssSelector;
        }

        public override void WriteDescriptionTo(TextWriter writer)
        {
            writer.Write(String.Format("CssSelectorConstraint: {0}", _cssSelector));
        }

        protected override void EnterMatch()
        {
            var jqInstallScript = _scriptLoader.GetJQueryInstallScript();
            _domContainer.Eval(jqInstallScript);

            var markingScript = _scriptLoader.GetCssMarkingScript(_cssSelector, base.MarkerClass);
            _domContainer.Eval(markingScript);

            base.EnterMatch();
        }

        protected override void ExitMatch()
        {
            base.ExitMatch();

            var unmarkingScript = _scriptLoader.GetCssMarkRemovalScript(_cssSelector, base.MarkerClass);
            _domContainer.Eval(unmarkingScript);

        }
    }
}
