// <copyright file="XMLInformation.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML
{
    using System.Xml;

    /// <summary>
    /// Nessesary information that is passed through the test set, test case, and test step.
    /// </summary>
    public static class XMLInformation
    {
        /// <summary>
        /// Gets or sets the xml file containing the XML Data.
        /// </summary>
        public static XmlDocument XMLDataFile { get; set; } = null;

        /// <summary>
        /// Gets or sets the xml file containing the test set/case/steps.
        /// </summary>
        public static XmlDocument XMLDocObj { get; set; } = null;

        /// <summary>
        /// Gets or sets the csv logger.
        /// </summary>
        public static CSVLogger CSVLogger { get; set; } = null;

        /// <summary>
        /// Gets or sets a value indicating whether to respectRunAODAFlag or not.
        /// </summary>
        public static bool RespectRunAODAFlag { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to respectRepeatFor or not.
        /// </summary>
        public static bool RespectRepeatFor { get; set; } = false;

        /// <summary>
        /// Gets or sets the log save file location.
        /// </summary>
        public static string LogSaveFileLocation { get; set; } = string.Empty;

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
