using System;
using NUnit.Framework;
using WatiN.Core.Constraints.jQuerySelector;

namespace WatiN.Core.UnitTests.jQuerySelectorTests
{
    [TestFixture]
    public class FindByCssSelectorTest : BrowserTestFixture
    {
        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        public void FindByCssSelector_for_site_that_has_jQuery(string browserType)
        {
            UseBrowser(browserType);

            Browser.GoTo("http://allrecipes.com/");
            
            Browser.FindByCssSelector<Link>(".linkRequiresLogin").WaitUntilExists(5);
        }

        [Test]
        [STAThread]
        [TestCase("IE")]
        [TestCase("FireFox")]
        public void FindByCssSelector_for_site_that_does_not_have_jQuery(string browserType)
        {
            UseBrowser(browserType);

            Browser.GoTo("http://google.com/");

            Browser.FindByCssSelector<TextField>("#main form input[title='Google Search']").WaitUntilExists(5);
        }
    }
}
