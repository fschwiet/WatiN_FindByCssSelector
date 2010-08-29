using NUnit.Framework;
using WatiN.Core.Constraints.jQuerySelector;

namespace WatiN.Core.UnitTests.jQuerySelectorTests
{
    [TestFixture]
    class ScriptLoaderTest
    {
        [Test]
        public void TestCanLoadJqueryInstallScript()
        {
            var sut = new ScriptLoader();

            string result = sut.GetJQueryInstallScript();

            Assert.IsTrue(result.Length > 100);
        }


        [Test]
        public void TestGetCssMarkingScript()
        {
            string cssSelector = Some.String();
            string markerClass = Some.String();

            var sut = new ScriptLoader();

            var result = sut.GetCssMarkingScript(cssSelector, markerClass);

            Assert.IsTrue(result.Length > 10);
        }


        [Test]
        public void TestGetCssMarkRemovalScript()
        {
            string cssSelector = Some.String();
            string markerClass = Some.String();

            var sut = new ScriptLoader();

            var result = sut.GetCssMarkRemovalScript(cssSelector, markerClass);

            Assert.IsTrue(result.Length > 10);
        }
    }
}
