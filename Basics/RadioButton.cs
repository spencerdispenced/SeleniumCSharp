using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
    public class RadioButton
    {
        // Login to SwagLabs website
        IWebDriver d = new ChromeDriver();
        string url = "https://ultimateqa.com/simple-html-elements-for-automation/";

        [SetUp]
        public void Initialize()
        {
            d.Navigate().GoToUrl(url);
            d.Manage().Window.Maximize();
            d.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        }
        [Test]
        public void ExecuteTest()
        {

            var button_other = d.FindElements(By.Name("gender")).ElementAt(2);
            Thread.Sleep(2000);

            button_other.Click();
            Thread.Sleep(2000);
            Assert.That(button_other.Selected, Is.True);

        }
        [TearDown]
        public void EndTest()
        {
            d.Close();
        }
    }
}
