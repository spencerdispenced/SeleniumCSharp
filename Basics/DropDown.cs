using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumBasics.Basics
{
    [TestFixture]
    public class DropDown
    {
        public static IWebDriver d;
        public static string url;

        [OneTimeSetUp]
        public void Init()
        {
            d = new ChromeDriver();
            url = "https://ultimateqa.com/simple-html-elements-for-automation/";
        }

        [SetUp]
        public void Initialize()
        {
            d.Navigate().GoToUrl(url); // Navigate to URL
            d.Manage().Window.Maximize(); // Maximize window
            d.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test]
        public void SingleItemInDropDown()
        {
            // Find DD element, used for Actions class
            var dropDownElement = d.FindElement(By.XPath("//*[@id=\"post-909\"]/div/div[1]/div/div[3]/div/div[1]/div[9]/div/div/div/select"));

            // Turn IWebElement into SelectElement, used for DD operations
            var dd = new SelectElement(dropDownElement);

            // Move screen to element
            Actions actions = new Actions(d);
            actions.MoveToElement(dropDownElement);
            actions.Perform();

            // Select 'Audi
            dd.SelectByText("Audi");

            // Get current text
            var ddText = dd.SelectedOption.Text;

            Assert.That(ddText, Is.EqualTo("Audi"));

        }

        [Test]
        public void AllItemsmInDropDown()
        {
            // Find DD element, used for Actions class
            var dropDownElement = d.FindElement(By.XPath("//*[@id=\"post-909\"]/div/div[1]/div/div[3]/div/div[1]/div[9]/div/div/div/select"));

            // Turn IWebElement into SelectElement, used for DD operations
            var dd = new SelectElement(dropDownElement);

            // Move screen to element
            Actions actions = new Actions(d);
            actions.MoveToElement(dropDownElement);
            actions.Perform();

            // Get all options in DD
            IList<IWebElement> all_options = dd.Options;

            // List of values desired in DD
            HashSet<string> test_options = new HashSet<string>() { "Audi", "Volvo", "Saab", "Opel"};

            var flag = true;

            // If too many or too little options
            if (all_options.Count != test_options.Count)
                flag = false;

            // Options can only be desired
            foreach (var item in all_options)
            {
                if (!test_options.Contains(item.Text))
                {
                    flag = false;
                }
            }

            Assert.That(flag, Is.True);

        }

        [OneTimeTearDown]
        public void EndTest()
        {
            d.Close();
        }
    }
}
