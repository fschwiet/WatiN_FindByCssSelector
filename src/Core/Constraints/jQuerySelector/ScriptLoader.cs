using System.Reflection;
using System.IO;
using WatiN.Core.UtilityClasses;

namespace WatiN.Core.Constraints.jQuerySelector
{
    public interface IScriptLoader
    {
        string GetJQueryInstallScript();
        string GetCssMarkingScript(string cssSelector, string markerClass);
        string GetCssMarkRemovalScript(string cssSelector, string markerClass);
    }

    public class ScriptLoader : IScriptLoader
    {
        private string LoadResourceByName(string name)
        {
            Assembly assembly = Assembly.GetAssembly(typeof(ScriptLoader));

            string resourceName = assembly.GetName().Name + ".Resources." + name;

            StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(resourceName));

            return reader.ReadToEnd();
        }

        public string GetJQueryInstallScript()
        {
            return LoadResourceByName("loadJQuery.js");
        }

        public string GetCssMarkingScript(string cssSelector, string markerClass)
        {
            return
            @"(function(cssSelector, markerClass) 
            { 
                window.jQuery(cssSelector).addClass(markerClass);
            })(" + JavascriptStringEncoder.Encode(cssSelector) + ", " + JavascriptStringEncoder.Encode(markerClass) + ");";
        }
            

        public string GetCssMarkRemovalScript(string cssSelector, string markerClass)
        {
            return
            @"(function(cssSelector, markerClass) 
            { 
                window.jQuery(cssSelector).removeClass(markerClass);
            })(" + JavascriptStringEncoder.Encode(cssSelector) + ", " + JavascriptStringEncoder.Encode(markerClass) + ");";
        }
    }
}
