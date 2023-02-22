using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumBasics.Basics
{
    [TestFixture]
    public class WindowTabs
    {
        IWebDriver d = new ChromeDriver();
        string url = "https://demoqa.com/browser-windows";

        [SetUp]
        public void Initialize()
        {
            d.Navigate().GoToUrl(url); // Navigate to URL
            d.Manage().Window.Maximize(); // Maximize window
            d.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        }

        [Test]
        public void ExecuteTest()
        {
            IList<string> expected = new List<string>() { "This is a sample page", "https://demoqa.com/sample",
                         "https://demoqa.com/browser-windows" };

            IList<string> actual = new List<string>();

            // Clickbutton to open new tab
            d.FindElement(By.Id("tabButton")).Click();

            // Get all opened tabs
            IList<string> tabs = d.WindowHandles;

            // Switch to newly opened tab
            //d.SwitchTo().Window(tabs[1]);
            SwitchTab("https://demoqa.com/sample");

            // Add page contents and new url to actual
            actual.Add(d.FindElement(By.Id("sampleHeading")).Text);
            actual.Add(d.Url);

            // Close new tab
            d.Close();

            // Switch back to original tab
            //d.SwitchTo().Window(tabs[0]);
            SwitchTab("https://demoqa.com/browser-windows");

            // Add current url again
            actual.Add(d.Url);

            Assert.That(expected, Is.EqualTo(actual));
        }


        public void SwitchTab(string targetUrl)
        {
            // Get all opened tabs
            IList<string> tabs = d.WindowHandles;

            foreach (var tab in tabs)
            {
                d.SwitchTo().Window(tab);

                if (d.Url.Equals(targetUrl))
                    break;
            }
        }

        [TearDown]
        public void EndTest()
        {
            d.Quit();
        }
    }
}
