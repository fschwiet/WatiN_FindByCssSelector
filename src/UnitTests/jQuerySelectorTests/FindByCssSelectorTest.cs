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
        public void TestFindByCss(string browserType)
        {
            UseBrowser(browserType);

            Browser.GoTo("http://allrecipes.com/");
            
            Browser.Link(Find.ByClass("linkRequiresLogin")).WaitUntilExists(5);

            Browser.FindByCssSelector<Link>(".linkRequiresLogin").WaitUntilExists(5);
        }
    }
}
