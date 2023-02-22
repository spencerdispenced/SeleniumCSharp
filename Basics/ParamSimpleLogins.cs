using Microsoft.Office.Interop.Excel;
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
    public class ParamSimpleLogins
    {
        IWebDriver d = new ChromeDriver();
        string url = "https://demoqa.com/login";
        string path = "ToolQALogins.xls";


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
            // Reference vars for app, book, and sheet
            Application app = new Application();
            app.Visible = true;
            Workbook book = app.Workbooks.Open(path);
            Worksheet sheet = book.Sheets["logins"];

            // Detect Last used Row - Ignore cells that contains formulas that result in blank values

            int rowCount = sheet.Cells.Find(
                    "*",
                    System.Reflection.Missing.Value,
                    XlFindLookIn.xlValues,
                    XlLookAt.xlWhole,
                    XlSearchOrder.xlByRows,
                    XlSearchDirection.xlPrevious,
                    false,
                    System.Reflection.Missing.Value,
                    System.Reflection.Missing.Value).Row;


            var expected = new List<string>() { "Billy", "Dave", "Bob", "Jill" };
            var actual = new List<string>();


            // Start at 2, excel has built in 'row', and avoid titles
            for (int i = 2; i <= rowCount; i++)
            {
                // Get Credentials from excel
                var username = sheet.Cells[i, 1].Text;
                var password = sheet.Cells[i, 2].Text;

                // Login to test page
                d.FindElement(By.Id("userName")).SendKeys(username);
                d.FindElement(By.Id("password")).SendKeys(password);
                d.FindElement(By.Id("login")).Click();

                // Get name of logged in user
                var name = d.FindElement(By.Id("userName-value")).Text;

                // Get name of logged in user
                actual.Add(name);

                // Logout
                d.FindElement(By.Id("submit")).Click();
            }

            book.Close();
            app.Quit();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TearDown]
        public void EndTest()
        {

            d.Close();
        }
    }
}
