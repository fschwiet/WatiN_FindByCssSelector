using System;
using System.IO;
using Moq;
using System.Text.RegularExpressions;
using WatiN.Core.Constraints;
using WatiN.Core.Constraints.jQuerySelector;
using WatiN.Core.Interfaces;
using NUnit.Framework;

namespace WatiN.Core.UnitTests.jQuerySelectorTests
{
    // Expose some protected functions to test
    public class TestableCssSelectorConstraint : CssSelectorConstraint
    {
        public TestableCssSelectorConstraint(IScriptLoader scriptLoader, DomContainer domContainer, string markerClass) : base(scriptLoader, domContainer, markerClass)
        {
        }

        public TestableCssSelectorConstraint(IScriptLoader scriptLoader, DomContainer domContainer) 
            : base(scriptLoader, domContainer)
        {
        }

        public void DoEnterMatch()
        {
            EnterMatch();
        }
    }

    [TestFixture]
    class CssSelectorConstraintTest
    {
        [Test]
        public void TestCanBeCreated()
        {
            var sut = new CssSelectorConstraint(null,null);

            Assert.IsInstanceOf(typeof(WatiN.Core.Constraints.Constraint), sut);
        }

        [Test]
        public void is_a_MarkerConstraint_setting_markerClass()
        {
            var expectedMarker = Some.String();

            var sut = new CssSelectorConstraint(null, null, expectedMarker);

            Assert.That(sut.MarkerClass, Is.EqualTo(expectedMarker));
        }

        [Test]
        public void TestEnterMatch_ensuresJQueryIsAvailable()
        {
            string jQueryInstallScript = Some.String();

            Mock<IScriptLoader> scriptLoader = new Mock<IScriptLoader>();
            Mock<WatiN.Core.DomContainer> domContainer = new Mock<DomContainer>();
            
            scriptLoader.Expect(s => s.GetJQueryInstallScript()).Returns(jQueryInstallScript);
            domContainer.Expect(s => s.Eval(jQueryInstallScript)).Returns(Some.String()).Verifiable();

            var sut = new TestableCssSelectorConstraint(scriptLoader.Object, domContainer.Object);

            sut.DoEnterMatch();
        }

        [Test]
        public void TestEnterMatch_usesCssSelectorToAddCssClass()
        {
            string cssSelector = Some.String();
            string markerClass = Some.String();

            string markingScript = Some.String();

            Mock<IScriptLoader> scriptLoader = new Mock<IScriptLoader>();
            Mock<WatiN.Core.DomContainer> domContainer = new Mock<DomContainer>();

            scriptLoader.Expect(s => s.GetCssMarkingScript(cssSelector, markerClass)).Returns(markingScript);
            domContainer.Expect(d => d.Eval(markingScript)).Returns(Some.String()).Verifiable();

            var sut = new TestableCssSelectorConstraint(scriptLoader.Object, domContainer.Object, markerClass);

            sut.Initialize(cssSelector);

            sut.DoEnterMatch();

            domContainer.Verify();
        }


        [Test]
        public void TestExitMatch_removesCssClass()
        {
            string cssSelector = Some.String();
            string markerClass = Some.String();

            string markingScript = Some.String();

            Mock<IScriptLoader> scriptLoader = new Mock<IScriptLoader>();
            Mock<WatiN.Core.DomContainer> domContainer = new Mock<DomContainer>();

            scriptLoader.Expect(s => s.GetCssMarkRemovalScript(cssSelector, markerClass)).Returns(markingScript).Verifiable();

            var sut = new TestableCssSelectorConstraint(scriptLoader.Object, domContainer.Object, markerClass);

            sut.Initialize(cssSelector);

            sut.DoEnterMatch();

            domContainer.Verify();
        }


        [Test]
        public void TestWriteDescriptionTo()
        {
            string cssSelector = Some.String();
            string markerClass = Some.String();

            Mock<System.IO.TextWriter> textWriter = new Mock<TextWriter>();
            textWriter.Expect(tw => tw.Write(It.IsAny<string>())).Callback(
                delegate(string writtenText)
                {
                    Assert.That(writtenText.Contains("CssSelectorConstraint"));
                    Assert.That(writtenText.Contains(cssSelector));
                }
                ).Verifiable();

            var sut = new CssSelectorConstraint(null, null, markerClass);

            sut.Initialize(cssSelector);

            sut.WriteDescriptionTo(textWriter.Object);

            textWriter.Verify();
        }

        [Test]
        public void TestWriteDescriptionTo_uninitialized()
        {
            Mock<System.IO.TextWriter> textWriter = new Mock<TextWriter>();
            textWriter.Expect(tw => tw.Write(It.IsAny<string>())).Callback(
                delegate(string writtenText)
                {
                    Assert.That(writtenText.Contains("CssSelectorConstraint"));
                }
                ).Verifiable();

            var sut = new CssSelectorConstraint(null, null);

            sut.WriteDescriptionTo(textWriter.Object);

            textWriter.Verify();
        }
    }
}
