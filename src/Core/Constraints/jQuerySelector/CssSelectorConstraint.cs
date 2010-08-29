using System;
using System.IO;
using WatiN.Core.Interfaces;

namespace WatiN.Core.Constraints.jQuerySelector
{
    public class CssSelectorConstraint : WatiN.Core.Constraints.Constraint
    {
        private readonly DomContainer _domContainer;
        private readonly IScriptLoader _scriptLoader;
        private string _cssSelector;
        private string _markerClass;

        public virtual Constraint ActualConstraint { get; protected set; }

        public CssSelectorConstraint(IScriptLoader scriptLoader, DomContainer domContainer)
        {
            _scriptLoader = scriptLoader;
            _domContainer = domContainer;
        }

        public void Initialize(string cssSelector, string markerClass)
        {
            _cssSelector = cssSelector;
            _markerClass = markerClass;

            ActualConstraint = new MarkerConstraint(markerClass);
        }

        public override void WriteDescriptionTo(TextWriter writer)
        {
            writer.Write(String.Format("CssSelectorConstraint: {0}", _cssSelector));
        }

        protected override void EnterMatch()
        {
            var jqInstallScript = _scriptLoader.GetJQueryInstallScript();
            _domContainer.Eval(jqInstallScript);

            var markingScript = _scriptLoader.GetCssMarkingScript(_cssSelector, _markerClass);
            _domContainer.Eval(markingScript);

            base.EnterMatch();
            //ActualConstraint.EnterMatch();
        }

        /*
        protected override bool MatchesImpl(IAttributeBag attributeBag, ConstraintContext context)
        {
            return ActualConstraint.MatchesImpl(attributeBag, context);
        }
        */
        protected override bool MatchesImpl(IAttributeBag attributeBag, ConstraintContext context)
        {
            return false;
        }

        protected override void ExitMatch()
        {
            base.ExitMatch();
            //ActualConstraint.ExitMatch();

            var unmarkingScript = _scriptLoader.GetCssMarkRemovalScript(_cssSelector, _markerClass);
            _domContainer.Eval(unmarkingScript);

        }
    }
}
