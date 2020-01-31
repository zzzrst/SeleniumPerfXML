namespace SeleniumPerfXML
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// Nessesary information that is passed through the test set, test case, and test step.
    /// </summary>
    public static class XMLInformation
    {
        /// <summary>
        /// Gets the xml file containing the test set/case/steps.
        /// </summary>
        public static XmlDocument XMLDataFile { get; }

        /// <summary>
        /// 
        /// </summary>
        public static XmlDocument XMLDocObj { get; }

        /// <summary>
        /// Gets a value indicating whether to respectRunAODAFlag or not.
        /// </summary>
        public static bool RespectRunAODAFlag { get; } = false;

        /// <summary>
        /// Gets a value indicating whether to respectRepeatFor or not.
        /// </summary>
        public static bool RespectRepeatFor { get; } = false;

        /// <summary>
        /// Replaces a string if it is a token and shown.
        /// </summary>
        /// <param name="possibleToken">A string that may be a token.</param>
        /// <returns>The provided string or value of the token.</returns>
        public static string ReplaceIfToken(string possibleToken)
        {
            if (possibleToken.Contains("${{") && possibleToken.Contains("}}") && XMLDataFile != null)
            {
                XmlNode tokens = XMLDataFile.GetElementsByTagName("Tokens")[0];
                string tokenKey = possibleToken.Substring(possibleToken.IndexOf("${{") + 3);
                tokenKey = tokenKey.Substring(0, tokenKey.IndexOf("}}"));

                // Find the appropriate token
                foreach (XmlNode token in tokens.ChildNodes)
                {
                    if (token.Attributes["key"] != null && token.Attributes["key"].InnerText == tokenKey && token.Attributes["value"] != null)
                    {
                        return possibleToken.Replace("${{" + $"{tokenKey}" + "}}", token.Attributes["value"].InnerText);
                    }
                }
            }

            return possibleToken;
        }
    }
}
