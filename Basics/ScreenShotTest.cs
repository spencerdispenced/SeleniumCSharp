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
    public class ScreenShotTest
    {
        IWebDriver d = new ChromeDriver();
        string url = "https://magento.softwaretestingboard.com/";
        string screenShotPath = "Screen1.PNG";


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
            // Move screen to element
            Actions actions = new Actions(d);
            IWebElement first = d.FindElement(By.XPath("//*[@id=\"ui-id-5\"]/span[2]"));
            IWebElement second = d.FindElement(By.XPath("//*[@id=\"ui-id-17\"]/span[2]"));
            IWebElement third = d.FindElement(By.XPath("//*[@id=\"ui-id-20\"]/span"));

            actions.MoveToElement(first).MoveToElement(second).MoveToElement(third).Click();
            actions.Perform();

            // Take ScreenShot
            Screenshot scrFile = ((ITakesScreenshot)d).GetScreenshot();
            scrFile.SaveAsFile(screenShotPath);

            string expected = d.FindElement(By.XPath("//*[@id=\"page-title-heading\"]/span")).Text;

            Assert.That(expected, Is.EqualTo("Hoodies & Sweatshirts"));
        }

        [TearDown]
        public void EndTest()
        {
            d.Close();
        }
    }
}
