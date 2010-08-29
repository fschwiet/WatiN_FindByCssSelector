I branched WatiN to try to add support for Browser.Element(Find.ByCssSelector(cssSelector)).

The original WatiN can be found at http://watin.sourceforge.net/.

All new tests are passing.  Usage is not as clean, here is a sample of whats been added (from FindByCssSelectorTest.cs):


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
