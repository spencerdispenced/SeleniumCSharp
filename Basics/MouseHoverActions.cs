using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumBasics.Basics
{
    [TestFixture]
    public class MouseHoverActions
    {
        public static IWebDriver d;
        public static string url;

        [OneTimeSetUp]
        public void Init()
        {
            d = new ChromeDriver();
            url = "https://magento.softwaretestingboard.com/";
        }

        [SetUp]
        public void Initialize()
        {
            d.Navigate().GoToUrl(url); // Navigate to URL
            d.Manage().Window.Maximize(); // Maximize window
            d.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test]
        public void Actions_using_Xpath()
        {
            // Move screen to element
            Actions actions = new Actions(d);
            IWebElement first = d.FindElement(By.XPath("//*[@id=\"ui-id-5\"]/span[2]"));
            IWebElement second = d.FindElement(By.XPath("//*[@id=\"ui-id-17\"]/span[2]"));
            IWebElement third = d.FindElement(By.XPath("//*[@id=\"ui-id-20\"]/span"));
            
            actions.MoveToElement(first).MoveToElement(second).MoveToElement(third).Click();
            actions.Perform();

            string expected = d.FindElement(By.XPath("//*[@id=\"page-title-heading\"]/span")).Text;

            Assert.That(expected, Is.EqualTo("Hoodies & Sweatshirts"));
        }

        [Test]
        public void Actions_using_LinkText()
        {
            // Create actions object, move to link with text 'Gear'
            Actions actions = new Actions(d);
            IWebElement gear = d.FindElement(By.LinkText("Gear"));
            actions.MoveToElement(gear);
            actions.Build().Perform();

            // More link appear, click link 'Watches'
            d.FindElement(By.LinkText("Watches")).Click();

            string expected = d.FindElement(By.XPath("//*[@id=\"page-title-heading\"]/span")).Text;

            Assert.That(expected, Is.EqualTo("Watches"));
        }

        [OneTimeTearDown]
        public void EndTest()
        {
            d.Close();
        }
    }
}
