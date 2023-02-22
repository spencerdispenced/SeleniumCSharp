using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumBasics.Basics
{
    [TestFixture]
    [Ignore("Done")]

    class OpenPageTest
    {
        IWebDriver d = new ChromeDriver();

        [SetUp]
        public void Initialize()
        {
            // Navigate to URL
            d.Navigate().GoToUrl("http://www.google.com/");

            // Maximize window
            d.Manage().Window.Maximize();
            Thread.Sleep(2000);

        }
        [Test]
        public void ExecuteTest()
        {
            IWebElement searchBox = d.FindElement(By.Name("q"));
            searchBox.SendKeys("reddit");
            Thread.Sleep(2000);

            IWebElement searchButton = d.FindElement(By.Name("btnK"));
            searchButton.Click();
            Thread.Sleep(2000);

        }
        [TearDown]
        public void EndTest()
        {
            d.Close();
        }
    }
}
