﻿using System;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Principal;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using TechTalk.SpecFlow;
using SecurityEssentials.Acceptance.Tests.Web.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.Resources;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.Events;

namespace SecurityEssentials.Acceptance.Tests.Web.Steps
{
	[Binding]
	public class Hooks
	{

		[BeforeFeature]
		public static void BeforeFeature()
		{

            IWebDriver webDriver = new FirefoxDriver();
			webDriver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 5));
			FeatureContext.Current.SetWebDriver(webDriver);

			var baseUri = new Uri(ConfigurationManager.AppSettings["WebServerUrl"]);
			FeatureContext.Current.SetBaseUri(baseUri);

		}

		[AfterFeature]
		public static void AfterFeature()
		{
			if (FeatureContext.Current.HasWebDriver()) FeatureContext.Current.GetWebDriver().Quit();
		}

		[AfterScenario]
		public static void AfterScenario() {
			if (ScenarioContext.Current.TestError != null && Convert.ToBoolean(ConfigurationManager.AppSettings["TakeScreenShotOnFailure"]) == true) {
				string fileName = string.Format("{0}TestFailure-{1}.png", ConfigurationManager.AppSettings["TestScreenCaptureDirectory"].ToString(), DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss"));
				FeatureContext.Current.GetWebDriver().TakeScreenshot().SaveAsFile(fileName, ImageFormat.Png);
			}
		}


	}
}