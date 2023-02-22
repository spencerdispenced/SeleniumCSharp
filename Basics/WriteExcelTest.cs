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

using ClosedXML.Excel;

namespace SeleniumBasics.Basics
{
    [TestFixture]
    public class WriteExcelTest
    {
        IWebDriver d = new ChromeDriver();
        string url = "https://www.techlistic.com/p/demo-selenium-practice.html";
        string path = "SeleniumBasics\\data\\CustTestOpenXl.xlsx";

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
            var book = new XLWorkbook();
            var sheet = book.Worksheets.Add("New Customers");


            var table = d.FindElement(By.Id("customers"));
            var rows = table.FindElements(By.TagName("tr"));
            var headers = rows[0].FindElements(By.TagName("th"));

            // Write header values into excel
            for (int i = 0; i < headers.Count; i++)
            {
                sheet.Cell(1, i + 1).Value = headers[i].Text;
            }

            // Write table values into excel
            for (int i = 1; i < rows.Count; i++)
            {
                var cols = rows[i].FindElements(By.TagName("td"));

                for (int j = 0; j < cols.Count; j++)
                    sheet.Cell(i + 1, j + 1).Value = cols[j].Text;
            }

            book.SaveAs(path);
        }

        [TearDown]
        public void EndTest()
        {
            d.Close();
        }
    }
}
