using System.Reflection;
using System.IO;
using System.Text;
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

            string resourceName = typeof(ScriptLoader).Namespace + ".Resources." + name;

            StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(resourceName));

            return reader.ReadToEnd();
        }

        public string GetJQueryInstallScript()
        {
            StringBuilder result = new StringBuilder();

            result.Append("with(window) { if (typeof jQuery == 'undefined') {");

            string jQuery = LoadResourceByName("jquery-1.4.2.js");

            result.Append(jQuery);

            result.Append("};};");

            return result.ToString(); 
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
