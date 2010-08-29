I branched WatiN to try to add support for Browser.Element(Find.ByCssSelector(cssSelector)).

The original WatiN can be found at http://watin.sourceforge.net/.

All new tests are passing.  Usage is not as clean, here is a sample of what is now supported (from FindByCssSelectorTest.cs):


            Browser.GoTo("http://google.com/");

            Browser.FindByCssSelector<TextField>("#main form input[title='Google Search']").TypeText("watin");
            Browser.FindByCssSelector<Element>("#main input[type='submit'][value='Google Search']").Click();

            Browser.FindByCssSelector<Element>(":contains('WatiN Home')");
