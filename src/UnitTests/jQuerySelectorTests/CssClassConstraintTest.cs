using System;
using System.Threading;
using NUnit.Framework;
using WatiN.Core.Constraints.jQuerySelector;

namespace WatiN.Core.UnitTests.jQuerySelectorTests
{
    [TestFixture]
    class CssClassConstraintTest : BrowserTestFixture
    {
        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        public void matches_class_at_beginning_of_string(string browserType)
        {
            UseBrowser(browserType);

            ScriptLoader loader = new ScriptLoader();

            GoToResource("CssClassConstraintTest.htm");

            Browser.Element(Find.ById("first").And(new CssClassConstraint("marker"))).WaitUntilExists(5);
        }

        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        public void matches_class_in_middle_of_string(string browserType)
        {
            UseBrowser(browserType);

            ScriptLoader loader = new ScriptLoader();

            GoToResource("CssClassConstraintTest.htm");

            Browser.Element(Find.ById("second").And(new CssClassConstraint("marker"))).WaitUntilExists(5);
        }

        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        public void matches_class_at_end_of_string(string browserType)
        {
            UseBrowser(browserType);

            ScriptLoader loader = new ScriptLoader();

            GoToResource("CssClassConstraintTest.htm");

            Browser.Element(Find.ById("third").And(new CssClassConstraint("marker"))).WaitUntilExists(5);
        }

        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        public void matches_only_class(string browserType)
        {
            UseBrowser(browserType);

            ScriptLoader loader = new ScriptLoader();

            GoToResource("CssClassConstraintTest.htm");

            Browser.Element(Find.ById("fourth").And(new CssClassConstraint("marker"))).WaitUntilExists(5);
        }
        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        public void doesnt_match_other(string browserType)
        {
            UseBrowser(browserType);

            ScriptLoader loader = new ScriptLoader();

            GoToResource("CssClassConstraintTest.htm");

            Thread.Sleep(TimeSpan.FromSeconds(5).Milliseconds);

            Browser.Element(Find.ById("notmatch_1").And(new CssClassConstraint("marker").Not())).WaitUntilExists(5);
            Browser.Element(Find.ById("notmatch_2").And(new CssClassConstraint("marker").Not())).WaitUntilExists(5);
            Browser.Element(Find.ById("notmatch_3").And(new CssClassConstraint("marker").Not())).WaitUntilExists(5);
        }
    }
}
