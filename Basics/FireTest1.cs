using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumBasics.Basics
{
    [TestFixture]
    [Ignore("Ignore Firefox")]

    class FireTest1
    {
        IWebDriver d = new FirefoxDriver();
        [SetUp]
        public void Setup()
        {
            // Navigate to URL
            d.Navigate().GoToUrl("http://www.google.com/");

            // Maximize window
            d.Manage().Window.Maximize();
            Thread.Sleep(2000);
        }


        [Test]
        public void TestMethod()
        {
            IWebElement searchBox = d.FindElement(By.Name("q"));
            searchBox.SendKeys("reddit");
            Thread.Sleep(2000);

            IWebElement searchButton = d.FindElement(By.Name("btnK"));
            searchButton.Click();
            Thread.Sleep(2000);
        }

        [TearDown]
        public void Teardown()
        {
            d.Close();

        }
    }
}
