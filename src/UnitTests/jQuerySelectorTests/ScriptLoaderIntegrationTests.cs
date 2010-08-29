using System;
using NUnit.Framework;
using WatiN.Core.Constraints.jQuerySelector;

namespace WatiN.Core.UnitTests.jQuerySelectorTests
{
    [TestFixture]
    public class ScriptLoaderIntegrationTests : BrowserTestFixture
    {
        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        public void JQueryInstallScript_loads_jQuery_if_not_loaded(string browserType)
        {
            UseBrowser(browserType);

            ScriptLoader loader = new ScriptLoader();

            GoToResource("burger.htm");

            Assert.AreEqual("undefined", Browser.Eval("typeof window.jQuery"));

            string js = loader.GetJQueryInstallScript();
            Browser.RunScript(js);

            var content = Browser.Eval("window.jQuery('a.plainburger').length");

            Assert.AreEqual("1", content);
            Assert.IsTrue(Browser.Eval("window.jQuery").StartsWith("function"));
        }

        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        public void JQueryInstallScript_does_not_reload_jQuery(string browserType)
        {
            UseBrowser(browserType);

            ScriptLoader loader = new ScriptLoader();

            GoToResource("cheeseburger.htm");

            //Browser.WaitForComplete();
            Browser.Link(Find.ByClass("cheeseburger")).WaitUntilExists();

            Assert.That(Browser.Eval("window.jQuery"), Is.StringStarting("function"));  // make sure jQuery is loaded
            Assert.That(Browser.Eval("window.jQuery"), Is.StringStarting(Browser.Eval("window.$")));  // make sure jQuery is not loaded in compatibility mode
            Browser.RunScript("window.$.isThisTheOriginal$=true;");

            Assert.That(Browser.Eval("window.$.isThisTheOriginal$"), Is.EqualTo("true"));  // we will verify this is not overwritten

            string js = loader.GetJQueryInstallScript();
            Browser.RunScript(js);

            Assert.That(Browser.Eval("window.jQuery.isThisTheOriginal$"), Is.EqualTo("true"));  // we will verify this is not overwritten
            Assert.That(Browser.Eval("window.$.isThisTheOriginal$"), Is.EqualTo("true"));  // we will verify this is not overwritten
        }


        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        public void JQueryInstallScript__loads_jQuery_in_compatability_mode(string browserType)
        {
            UseBrowser(browserType);

            ScriptLoader loader = new ScriptLoader();

            GoToResource("burger.htm");

            // verify jQuery not loaded
            Assert.AreEqual("undefined", Browser.Eval("typeof window.jQuery"));
            Assert.AreEqual("undefined", Browser.Eval("typeof window.$"));

            string js = loader.GetJQueryInstallScript();
            Browser.RunScript(js);

            // verify jQuery is loaded, but $ is still undefined
            Assert.That(Browser.Eval("typeof window.jQuery"), Is.StringStarting("function"));
            Assert.AreEqual("undefined", Browser.Eval("typeof window.$"));
        }

        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        public void GetCssMarkingScript_marks_elements(string browserType)
        {
            UseBrowser(browserType);

            ScriptLoader loader = new ScriptLoader();

            GoToResource("veggieburger.htm");

            Browser.Link(Find.ByClass("veggieburger")).WaitUntilExists();

            Assert.AreEqual("0", Browser.Eval("window.jQuery('.marker').length"));

            var script = loader.GetCssMarkingScript("a.veggieburger", "marker");
            Browser.RunScript(script);

            Assert.AreEqual("1", Browser.Eval("window.jQuery('.marker').length"));
        }

        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        public void GetCssMarkRemovalScript_unmarks_elements(string browserType)
        {
            UseBrowser(browserType);

            ScriptLoader loader = new ScriptLoader();

            GoToResource("veggieburger.htm");

            Browser.Link(Find.ByClass("veggieburger")).WaitUntilExists();

            Assert.AreEqual("1", Browser.Eval("window.jQuery('.veggieburger').length"));

            var script = loader.GetCssMarkRemovalScript("a.veggieburger", "veggieburger");
            Browser.RunScript(script);

            Assert.AreEqual("0", Browser.Eval("window.jQuery('.veggieburger').length"));
        }
    }
}
