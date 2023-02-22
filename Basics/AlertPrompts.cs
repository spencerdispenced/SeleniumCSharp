using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
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
    public class AlertPrompts
    {
        public static IWebDriver d;
        public static string url;

        [OneTimeSetUp]
        public void Init()
        {
            d = new ChromeDriver();
            url = "https://demoqa.com/alerts";  // This site goes down sometimes
        }

        [SetUp]
        public void Initialize()
        {
            d.Navigate().GoToUrl(url); // Navigate to URL
            d.Manage().Window.Maximize(); // Maximize window
            d.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test]
        public void RegularAlert()
        {
            // Click button to create alert
            d.FindElement(By.Id("alertButton")).Click();

            IAlert alert = d.SwitchTo().Alert();

            string alertMessage = alert.Text;

            alert.Accept();

            Assert.That(alertMessage, Is.EqualTo("You clicked a button"));
        }

        [Test]
        public void TimerAlert()
        {
            // Create Explicit wait object
            WebDriverWait wait = new WebDriverWait(d, TimeSpan.FromSeconds(10));
            
            // Click button to create alert
            d.FindElement(By.Id("timerAlertButton")).Click();

            // Wait for alert to appear
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());

            // Grab Text
            string alertMessage = d.SwitchTo().Alert().Text;

            // Accept alert
            d.SwitchTo().Alert().Accept();

            // Assert message in Alert
            Assert.That(alertMessage, Is.EqualTo("This alert appeared after 5 seconds"));

        }

        [Test]
        public void AcceptAlertMessage()
        {
            
            // Click button to create alert
            d.FindElement(By.Id("confirmButton")).Click();

            // Accept Alert
            d.SwitchTo().Alert().Accept();

            string confirmMessage = d.FindElement(By.Id("confirmResult")).Text;
           
            // Assert message in Alert
            Assert.That(confirmMessage, Is.EqualTo("You selected Ok"));

        }

        [Test]
        public void CancelAlertMessage()
        {
            // Click button to create alert
            d.FindElement(By.Id("confirmButton")).Click();

            // Dismiss Alert
            d.SwitchTo().Alert().Dismiss();

            string confirmMessage = d.FindElement(By.Id("confirmResult")).Text;

            // Assert message in Alert
            Assert.That(confirmMessage, Is.EqualTo("You selected Cancel"));
        }

        [Test]
        public void PromptAlert()
        {
            // Click button to create alert
            d.FindElement(By.Id("promtButton")).Click();

            string sentMessage = "Spencer";

            d.SwitchTo().Alert().SendKeys(sentMessage);
            d.SwitchTo().Alert().Accept();

            string promptMessage = d.FindElement(By.Id("promptResult")).Text;

            // Assert message in Alert
            Assert.That(promptMessage, Is.EqualTo("You entered " + sentMessage));
        }

        [OneTimeTearDown]
        public void EndTest()
        {
            d.Close();
        }
    }
}
