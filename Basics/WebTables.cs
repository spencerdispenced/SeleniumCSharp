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
    public class WebTables
    {
        public static IWebDriver d;
        public static string url;

        [OneTimeSetUp]
        public void Init()
        {
            d = new ChromeDriver();
            url = "https://demoqa.com/webtables";
        }

        [SetUp]
        public void Initialize()
        {
            d.Navigate().GoToUrl(url); // Navigate to URL
            d.Manage().Window.Maximize(); // Maximize window
            d.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test]
        public void TestA()
        {
            IWebElement empTable = d.FindElement(By.ClassName("rt-tbody"));
            IReadOnlyCollection<IWebElement> rows = empTable.FindElements(By.ClassName("rt-tr-group"));

            foreach (var row in rows)
            {
                IReadOnlyCollection<IWebElement> cols = row.FindElements(By.ClassName("rt-td"));

                if (string.IsNullOrEmpty(cols.ElementAt(0).Text) || cols.ElementAt(0).Text.Equals(" "))
                    continue;



                // Assert salary
                var salary = 0;
                Int32.TryParse(cols.ElementAt(4).Text, out salary);
                Assert.That(salary, Is.InRange(2000, 15000));
            }
        }

        [OneTimeTearDown]
        public void EndTest()
        {
            d.Close();
        }
    }
}
