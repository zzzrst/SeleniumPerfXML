// <copyright file="TimeAndLogAspect.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SeleniumPerfXML.TestActions
{
    using System;
    using MethodBoundaryAspect.Fody.Attributes;

    /// <summary>
    /// This aspect is used to surround a test action with try and catch, while timing each action.
    /// If the test action is successful (no errors performing the action / passed in not to perform action), then runAODA if specified.
    /// </summary>
    [Serializable]
    public class TimeAndLogAspect : OnMethodBoundaryAspect
    {
        private string result = string.Empty;
        private string warning = string.Empty;
        private bool log = false;
        private bool performAction = true;
        private bool runAODA = false;
        private string runAODAName = string.Empty;
        private DateTime start;
        private DateTime end;

        [NonSerialized]
        private SeleniumDriver seleniumDriver;

        /// <inheritdoc/>
        public override void OnEntry(MethodExecutionArgs args)
        {
            this.start = DateTime.UtcNow;
            this.log = bool.Parse(args.Arguments[0].ToString());
            this.result = args.Arguments[1].ToString();
            this.performAction = bool.Parse(args.Arguments[2].ToString());
            this.runAODA = bool.Parse(args.Arguments[3].ToString());
            this.runAODAName = args.Arguments[4].ToString();
            this.seleniumDriver = (SeleniumDriver)args.Arguments[6];
            base.OnEntry(args);
        }

        /// <inheritdoc/>
        public override void OnException(MethodExecutionArgs args)
        {
            this.end = DateTime.UtcNow;
            double totalTime = this.GetTotalElapsedTime();
            args.FlowBehavior = FlowBehavior.Return;
            if (this.log)
            {
                Logger.Info($"\"{this.result}\",\"F\"");
            }
        }

        /// <inheritdoc/>
        public override void OnExit(MethodExecutionArgs args)
        {
            this.end = DateTime.UtcNow;
            double totalTime = this.GetTotalElapsedTime();

            if (this.log)
            {
                if (this.performAction)
                {
                    Logger.Info($"\"{this.result}\",\"{totalTime.ToString()}\"");
                }
                else
                {
                    Logger.Info($"\"{this.result}\",\"N/A\"");
                }
            }

            if (this.runAODA)
            {
                this.seleniumDriver.RunAODA(this.runAODAName);
            }

            base.OnExit(args);
        }

        private double GetTotalElapsedTime()
        {
            return Math.Abs((this.start - this.end).TotalSeconds);
        }
    }
}
