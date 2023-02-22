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
    [Ignore("Done")]

    public class Login
    {
        // Login to SwagLabs website
        IWebDriver d = new ChromeDriver();
        string url = "https://www.saucedemo.com/";

        [SetUp]
        public void Initialize()
        {
            // Navigate to URL
            d.Navigate().GoToUrl(url);

            // Maximize window
            d.Manage().Window.Maximize();
            Thread.Sleep(2000);

        }
        [Test]
        public void ExecuteTest()
        {
            d.FindElement(By.Id("user-name")).SendKeys("standard_user");

            d.FindElement(By.Id("password")).SendKeys("secret_sauce");

            d.FindElement(By.Id("login-button")).Click();

            string text = d.FindElement(By.XPath("//*[@id=\"header_container\"]/div[2]/span")).Text;

            Assert.AreEqual("PRODUCTS", text);
            Thread.Sleep(2000);

        }
        [TearDown]
        public void EndTest()
        {
            d.Close();
        }
    }
}
