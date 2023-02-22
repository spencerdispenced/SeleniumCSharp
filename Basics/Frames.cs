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
    public class Frames
    {
        public static IWebDriver d;
        public static string url;

        [OneTimeSetUp]
        public void Init()
        {
            d = new ChromeDriver();
            url = "https://demoqa.com/frames";
        }

        [SetUp]
        public void Initialize()
        {
            d.Navigate().GoToUrl(url); // Navigate to URL
            d.Manage().Window.Maximize(); // Maximize window
            d.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test]
        public void Test_SingleFrame()
        {
            d.SwitchTo().Frame("frame1");
            string bodyText = d.FindElement(By.TagName("body")).Text;

            Assert.That(bodyText, Is.EqualTo("This is a sample page"));
        }

        [Test]
        public void Test_MultipleFrames()
        {
            // Navigate to new url for better test
            d.Navigate().GoToUrl("http://the-internet.herokuapp.com/nested_frames"); 
            List<string> expected = new List<string>() { "MIDDLE", "RIGHT", "BOTTOM" };
            List<string> actual = new List<string>();

            // Switch to top, middle frame
            d.SwitchTo().Frame("frame-top");
            d.SwitchTo().Frame("frame-middle");

            // add text to list
            actual.Add(d.FindElement(By.TagName("body")).Text);

            // Move back to frame-top
            d.SwitchTo().ParentFrame();

            // Move to top, right frame
            d.SwitchTo().Frame("frame-right");
            actual.Add(d.FindElement(By.TagName("body")).Text);

            // Move back to main window frame
            d.SwitchTo().DefaultContent();

            // Move to bottom frame
            d.SwitchTo().Frame("frame-bottom");
            actual.Add(d.FindElement(By.TagName("body")).Text);

            Assert.That(expected, Is.EqualTo(actual));
        }

        [OneTimeTearDown]
        public void EndTest()
        {
            d.Close();
        }
    }
}
