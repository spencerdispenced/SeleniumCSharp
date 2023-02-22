using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;


using ClosedXML.Excel;
using OpenQA.Selenium.Support.UI;

namespace SeleniumBasics.Basics
{
    [TestFixture]
    public class ExceptionHandling
    {
        IWebDriver d = new ChromeDriver();
        string url = "https://www.saucedemo.com/";
        string path = "SwagLogins.xlsx";
        string scrnPath = "FailSwag.PNG";

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
            try
            {
                var book = new XLWorkbook(path);
                var sheet = book.Worksheet("logins");

                // Get used row count
                var rowCount = sheet.LastRowUsed().RowNumber();

                WebDriverWait wait = new WebDriverWait(d, TimeSpan.FromSeconds(10));

                // Iterate through username/passwords, start with 1 to skip titles
                for (int i = 1; i < rowCount; i++)
                {
                    try
                    {
                        // Get user/pass from excel
                        var username = sheet.Cell(i + 1, 1).Value.ToString();
                        var password = sheet.Cell(i + 1, 2).Value.ToString();

                        // Login
                        d.FindElement(By.Id("user-name")).SendKeys(username);
                        d.FindElement(By.Id("password")).SendKeys(password);
                        d.FindElement(By.Id("login-button")).Click();

                        // Wait for alert to appear
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(d.FindElement(By.Id("react-burger-menu-btn"))));

                        sheet.Cell(i + 1, 3).Value = "Pass";
                        // Logout
                        d.FindElement(By.Id("react-burger-menu-btn")).Click();
                        d.FindElement(By.Id("logout_sidebar_link")).Click();
                    }
                    catch (Exception e)
                    {
                        HandleError(e);
                        sheet.Cell(i + 1, 3).Value = "Fail";
                        d.Navigate().Refresh(); // Refresh the page
                        continue;
                    }
                }

                book.Save();
            }
            catch (Exception e)
            {
                HandleError(e);
                Assert.Fail();
                d.Quit();
            }
        }

        private void HandleError(Exception e)
        {
            Screenshot scrnSt = ((ITakesScreenshot)d).GetScreenshot();
            scrnSt.SaveAsFile(scrnPath);

            Console.WriteLine("FAIL: " + e.Message);
        }

        [TearDown]
        public void EndTest()
        {
            d.Quit();
        }
    }
}
