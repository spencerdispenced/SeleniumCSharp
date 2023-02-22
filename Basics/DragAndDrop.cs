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
    public class DragAndDrop
    {
        IWebDriver d = new ChromeDriver();
        string url = "https://demoqa.com/droppable";

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
            // Find source and target locations
            var source = d.FindElement(By.Id("draggable"));
            var target = d.FindElement(By.Id("droppable"));

            // Perform drag and drop actions
            new Actions(d).DragAndDrop(source, target).Perform();

            // Assert item dropped
            var text = target.Text;
            Assert.That(text, Is.EqualTo("Dropped!"));
            
        }

        [TearDown]
        public void EndTest()
        {
            d.Close();
        }
    }
}
