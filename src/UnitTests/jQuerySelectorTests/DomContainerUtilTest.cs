using System;
using NUnit.Framework;
using WatiN.Core.Constraints.jQuerySelector;

namespace WatiN.Core.UnitTests.jQuerySelectorTests
{
    [TestFixture]
    public class DomContainerUtilTest : BrowserTestFixture
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

            Browser.Link(DomContainerUtil.FindByCss(Browser.DomContainer, ".linkRequiresLogin")).WaitUntilExists(5);
            //.And(DomContainerUtil.FindByCss(Browser, "body"))
            //Browser.Element(Find.ById("main")
            //    .And(DomContainerUtil.FindByCss(Browser, "#main"))
            //    ).WaitUntilExists();
        }
    }
}
