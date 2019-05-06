﻿// <copyright file="SeleniumPerfXMLDriver.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML
{
    using System;
    using CommandLine;

    /// <summary>
    /// Driver class
    /// </summary>
    public class SeleniumPerfXMLDriver
    {
        /// <summary>
        /// Main functionality
        /// </summary>
        /// <param name="args"> The arguments to be passed in. </param>
        /// <returns> 0 if no errors were met. </returns>
        public static int Main(string[] args)
        {
            int resultCode = 0;
            bool errorParsing = false;

            // Paramaters to be used for XML.
            string browser = string.Empty;
            string environment = string.Empty;
            string url = string.Empty;
            bool respectRunAODAFlag = false;
            bool respectRepeatFor = false;
            int timeOutThreshold = 0;
            int warningThreshold = 0;
            string dataFile = string.Empty;
            string csvSaveFileLocation = string.Empty;
            string logSaveFileLocation = string.Empty;
            string screenshotSaveLocation = string.Empty;

            Parser.Default.ParseArguments<SeleniumPerfXMLOptions>(args)
               .WithParsed<SeleniumPerfXMLOptions>(o =>
               {
                   browser = o.Browser ?? string.Empty;
                   environment = o.Environment ?? string.Empty;
                   url = o.URL ?? string.Empty;
                   respectRepeatFor = o.RespectRepeatFor;
                   respectRunAODAFlag = o.RespectRunAodaFlag;
                   timeOutThreshold = o.TimeOutThreshold;
                   warningThreshold = o.WarningThreshold;
                   dataFile = o.DataFile ?? string.Empty;
                   csvSaveFileLocation = o.CSVSaveFileLocation ?? string.Empty;
                   logSaveFileLocation = o.LogSaveLocation ?? string.Empty;
                   screenshotSaveLocation = o.ScreenShotSaveLocation ?? string.Empty;
               })
               .WithNotParsed<SeleniumPerfXMLOptions>(errs =>
               {
                   Console.WriteLine(errs);
                   if (errs != null)
                   {
                       errorParsing = true;
                       resultCode = 1;
                   }
               });

            if (!errorParsing)
            {
                Console.WriteLine("Hello World!");
            }

            return resultCode;
        }
    }
}