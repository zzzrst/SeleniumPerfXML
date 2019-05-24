﻿// <copyright file="AxeDriver.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.Axe
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using OpenQA.Selenium;
    using Selenium.Axe;

    /// <summary>
    /// This is the driver to deal with Axe.core.
    /// </summary>
    public class AxeDriver
    {
        private const string TALLIEDRESULT = "TalliedResult.csv";
        private const string RULEPAGESUMMARY = "RulePageSummary.csv";

        /// <summary>
        /// Result Type -> { Rule ID -> {Page URL -> HTML, Target, Data, Related Nodes} }.
        /// </summary>
        private Dictionary<string, Dictionary<string, Dictionary<string, List<RuleNodeInformation>>>> results;

        /// <summary>
        /// Page URL -> Provided Page Title, Browser Page Title.
        /// </summary>
        private Dictionary<string, PageInformation> pageInfo;

        /// <summary>
        /// Rule ID -> Description, Rule Tag, Impact, Help, Help URL.
        /// </summary>
        private Dictionary<string, RuleInformation> ruleInfo;

        /// <summary>
        /// [Page URL, Provided Page Title, Browser Page Title, Passes, Violations, Incomplete, Inapplicable].
        /// </summary>
        private List<string> pageSummary;

        /// <summary>
        /// Initializes a new instance of the <see cref="AxeDriver"/> class.
        /// </summary>
        public AxeDriver()
        {
            this.results = new Dictionary<string, Dictionary<string, Dictionary<string, List<RuleNodeInformation>>>>();
            this.pageInfo = new Dictionary<string, PageInformation>();
            this.ruleInfo = new Dictionary<string, RuleInformation>();
            this.pageSummary = new List<string>()
            {
                "Page URL, Provided Page Title, Browser Page Title, Passes, Violations, Incomplete, Inapplicable",
            };
        }

        /// <summary>
        /// This captures the AODA result for this webpage.
        /// </summary>
        /// <param name="driver">Selenium WebDriver used. </param>
        /// <param name="providedPageTitle"> Title of the page. </param>
        public void CaptureResult(IWebDriver driver, string providedPageTitle)
        {
            driver.Manage().Window.FullScreen();
            AxeResult results = driver.Analyze();

            // check if there is any error. If there is, write it out
            Console.WriteLine(results.Error);

            // map page information
            if (!this.pageInfo.ContainsKey(results.Url))
            {
                this.pageInfo[results.Url] = new PageInformation()
                {
                    BrowserPageTitle = driver.Title,
                    ProvidedPageTitle = providedPageTitle,
                };
            }

            // map each axe result
            this.MapAxeResult(results.Inapplicable, AxeResultType.INAPPLICABLE, results.Url);
            this.MapAxeResult(results.Incomplete, AxeResultType.INCOMPLETE, results.Url);
            this.MapAxeResult(results.Passes, AxeResultType.PASS, results.Url);
            this.MapAxeResult(results.Violations, AxeResultType.VIOLATIONS, results.Url);

            // populate mapping of each axe result into a nice list
            this.pageSummary.Add(
                string.Format(
                    $"\"{results.Url}\"," +
                    $"\"{providedPageTitle}\"," +
                    $"\"{driver.Title}\"," +
                    $"\"{results.Passes.Count()}\"," +
                    $"\"{results.Violations.Count()}\"," +
                    $"\"{results.Incomplete.Count()}\"," +
                    $"\"{results.Inapplicable.Count()}\""));
        }

        /// <summary>
        /// Logs the result for this file.
        /// </summary>
        /// <param name="folderLocation">Location to save all the results.</param>
        public void LogResults(string folderLocation)
        {
            List<string> rulePageSummary = new List<string>()
            {
                "Page URl,Provided Page Title,Browser Page Title,Result Type,Description,Rule Tag,Impact,Help,Help URL,Occurance on Page",
            };

            // we are looping through each resultType
            foreach (KeyValuePair<string, Dictionary<string, Dictionary<string, List<RuleNodeInformation>>>> resultType in this.results)
            {
                // we are now looping through each rule
                foreach (KeyValuePair<string, Dictionary<string, List<RuleNodeInformation>>> ruleID in resultType.Value)
                {
                    // we are now looping through each page
                    foreach (KeyValuePair<string, List<RuleNodeInformation>> pageURL in ruleID.Value)
                    {
                        string currentURL = pageURL.Key;
                        string currentProvidedPageTitle = this.pageInfo[pageURL.Key].ProvidedPageTitle;
                        string currentBrowserPageTitle = this.pageInfo[pageURL.Key].BrowserPageTitle;

                        // get each of the nodeInfo out
                        List<JObject> nodeInfoList = new List<JObject>();
                        foreach (RuleNodeInformation node in pageURL.Value)
                        {
                            JObject nodeInfo = new JObject(
                                new JProperty("Page URL", currentURL),
                                new JProperty("Provided Page Title", currentProvidedPageTitle),
                                new JProperty("Browser Page Title", currentBrowserPageTitle),
                                new JProperty("HTML", node.HTML),
                                new JProperty("Target", node.Target));
                            nodeInfoList.Add(nodeInfo);
                        }

                        // add it into rule Summary
                        JObject ruleSummary = new JObject(
                            new JProperty("Rule ID", ruleID.Key),
                            new JProperty("Result Type", resultType.Key),
                            new JProperty("Description", this.ruleInfo[ruleID.Key].Description),
                            new JProperty("Rule Tag", this.ruleInfo[ruleID.Key].RuleTag),
                            new JProperty("Impact", this.ruleInfo[ruleID.Key].Impact),
                            new JProperty("Help", this.ruleInfo[ruleID.Key].Help),
                            new JProperty("Help URL", this.ruleInfo[ruleID.Key].HelpUrl),
                            new JProperty("Nodes", nodeInfoList));

                        // record occurance on page
                        rulePageSummary.Add(
                            string.Format(
                                $"\"{currentURL}\"," +
                                $"\"{currentProvidedPageTitle}\"," +
                                $"\"{currentBrowserPageTitle}\"," +
                                $"\"{resultType.Key}\"," +
                                $"\"{this.ruleInfo[ruleID.Key].Description}\"," +
                                $"\"{string.Join(" ", this.ruleInfo[ruleID.Key].RuleTag)}\"," +
                                $"\"{this.ruleInfo[ruleID.Key].Impact}\"," +
                                $"\"{this.ruleInfo[ruleID.Key].Help}\"," +
                                $"\"{this.ruleInfo[ruleID.Key].HelpUrl}\"," +
                                $"\"{pageURL.Value.Count.ToString()}\""));

                        // write it to file
                        string fileName = $"{string.Join("_", this.ruleInfo[ruleID.Key].RuleTag)}_{ruleID.Key}_{resultType.Key}.json";

                        Directory.CreateDirectory($"{folderLocation}\\Json");

                        using (StreamWriter file = File.CreateText($"{folderLocation}\\Json\\{fileName}"))
                        using (JsonTextWriter writer = new JsonTextWriter(file) { Formatting = Formatting.Indented })
                        {
                            ruleSummary.WriteTo(writer);
                        }
                    }
                }
            }

            // populate RulePageSummary.csv
            using (StreamWriter sw = new StreamWriter(folderLocation + RULEPAGESUMMARY))
            {
                foreach (string rulePage in rulePageSummary)
                {
                    sw.WriteLine(rulePage);
                }
            }

            // write out TalliedResult.csv
            using (StreamWriter sw = new StreamWriter(folderLocation + TALLIEDRESULT))
            {
                foreach (string pageResult in this.pageSummary)
                {
                    sw.WriteLine(pageResult);
                }
            }
        }

        /// <summary>
        /// Populate RuleInfo and Results based on AxeResultItems[] passed in.
        /// </summary>
        /// <param name="axeResults">The different results that were found.</param>
        /// <param name="resultType"> The type of result. </param>
        /// <param name="url">The url used for this page.</param>
        private void MapAxeResult(AxeResultItem[] axeResults, string resultType, string url)
        {
            foreach (AxeResultItem resultItem in axeResults)
            {
                // populate RuleInfo
                if (!this.ruleInfo.ContainsKey(resultItem.Id))
                {
                    this.ruleInfo[resultItem.Id] = new RuleInformation()
                    {
                        Description = resultItem.Description,
                        Help = resultItem.Help,
                        HelpUrl = resultItem.HelpUrl,
                        Impact = resultItem.Impact,
                        RuleTag = resultItem.Tags.ToList(),
                    };
                }

                // populate Results
                foreach (AxeResultNode resultNode in resultItem.Nodes)
                {
                    RuleNodeInformation temp = new RuleNodeInformation()
                    {
                        HTML = resultNode.Html,
                        Target = resultNode.Target,
                    };

                    // add into the results dictionary
                    if (this.results.ContainsKey(resultType))
                    {
                        if (this.results[resultType].ContainsKey(resultItem.Id))
                        {
                            if (this.results[resultType][resultItem.Id].ContainsKey(url))
                            {
                                // add it to the list
                                this.results[resultType][resultItem.Id][url].Add(temp);
                            }
                            else
                            {
                                // adding new rule-node information
                                this.results[resultType][resultItem.Id][url] = new List<RuleNodeInformation>() { temp };
                            }
                        }
                        else
                        {
                            // result Type exists, but ruleID doesn't
                            this.results[resultType][resultItem.Id] = new Dictionary<string, List<RuleNodeInformation>>()
                            {
                                { url, new List<RuleNodeInformation>() { temp } },
                            };
                        }
                    }
                    else
                    {
                        // we have to input new record for result type -> ruleId -> page Url -> {Rule Node Information}
                        this.results[resultType] = new Dictionary<string, Dictionary<string, List<RuleNodeInformation>>>()
                        {
                            {
                                resultItem.Id, new Dictionary<string, List<RuleNodeInformation>>()
                                {
                                    { url, new List<RuleNodeInformation>() { temp } },
                                }
                            },
                        };
                    }
                }
            }
        }
    }
}