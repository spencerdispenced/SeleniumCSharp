using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumBasics.Advanced
{
    [TestFixture]
    public class HandleAutoCompleteTest
    {
        public static IWebDriver d;
        public static string url;

        [OneTimeSetUp]
        public void Init()
        {
            d = new ChromeDriver();
            url = "https://demoqa.com/auto-complete";
        }

        [SetUp]
        public void Initialize()
        {
            d.Navigate().GoToUrl(url); // Navigate to URL
            d.Manage().Window.Maximize(); // Maximize window
            d.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test]
        public void Test_FindViolet()
        {
            var testColor = "Voilet";

            try
            {
                // // Send 'l' to text box
                WebDriverWait wait = new WebDriverWait(d, TimeSpan.FromSeconds(30));
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("autoCompleteSingleInput"))).SendKeys("l");

                // Wait for auto options to appear
                IWebElement options = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".auto-complete__menu")));

                // Move screen to element
                IJavaScriptExecutor ex1 = (IJavaScriptExecutor)d;
                ex1.ExecuteScript("arguments[0].scrollIntoView({block: 'center', inline: 'nearest'})", options);

                // Click on 'Voilet'
                options.FindElement(By.XPath($"//div[text()='{testColor}']")).Click();
                

                var color = d.FindElement(By.XPath("//*[@id=\"autoCompleteSingleContainer\"]/div/div[1]")).Text;

                Assert.That(color, Is.EqualTo(testColor));
            }
            catch (Exception e)
            {
                Console.WriteLine("FAIL: " + e.Message);
                Assert.Fail();
                d.Quit();
            }
        }


        [OneTimeTearDown]
        public void EndTest()
        {
            d.Quit();
        }
    }
}
