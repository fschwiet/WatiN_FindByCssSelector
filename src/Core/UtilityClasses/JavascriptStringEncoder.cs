using System.Text;

namespace WatiN.Core.UtilityClasses
{
    /// <summary>
    /// This code originated from Rick Strahl's blog, http://www.west-wind.com/WebLog/posts/114530.aspx
    /// I did add changes for Bryans comment on 7/8/2010  (escape encoding ',')
    /// </summary>

    class JavascriptStringEncoder
    {
        /// <summary>
        /// Encodes a string to be represented as a string literal. The format
        /// is essentially a JSON string.
        /// 
        /// The string returned includes outer quotes 
        /// Example Output: "Hello \"Rick\"!\r\nRock on"
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Encode(string s)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\"");
            JsonStringEncodeWithinDoubleParenthesis(sb, s);
            sb.Append("\"");

            return sb.ToString();
        }

        public static void JsonStringEncodeWithinDoubleParenthesis(StringBuilder sb, string s)
        {
            foreach (char c in s)
            {
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\"");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    case '<':
                        sb.Append("\\x3c");
                        break;
                    default:
                        int i = (int)c;
                        if (i < 32 || i > 127)
                        {
                            sb.AppendFormat("\\u{0:X04}", i);
                        }
                        else
                        {
                            sb.Append(c);
                        }
                        break;
                }
            }
        }
    }
}
