using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

using Microsoft.Office.Interop.Excel;
using System.IO;

namespace SeleniumBasics.Basics
{
    [TestFixture]
    public class ExcelReadWriteDemo
    {
        // Reference vars for app, book, and sheet
        Application app = null;
        Workbook book = null;
        Worksheet sheet = null;

        [Test]
        public void ExecuteTest()
        {
            app = new Application();

            string path = "Students.xls";


            // Open workbook
            book = app.Workbooks.Open(path);

            // Open first sheet
            sheet = book.Worksheets.Item[1];

            // Determines if app is visible or invisible
            app.Visible = true;

            /*// Row count, These don't work correctly
            int rowCount = sheet.UsedRange.Rows.Count;
            int lastRow = sheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell, Type.Missing).Row;*/


            // Detect Last used Row - Ignore cells that contains formulas that result in blank values
            int lastRowIgnoreFormulas = sheet.Cells.Find(
                            "*",
                            System.Reflection.Missing.Value,
                            XlFindLookIn.xlValues,
                            XlLookAt.xlWhole,
                            XlSearchOrder.xlByRows,
                            XlSearchDirection.xlPrevious,
                            false,
                            System.Reflection.Missing.Value,
                            System.Reflection.Missing.Value).Row;

            string name;
            double mark1 = 0, mark2 = 0, mark3 = 0;

            // Iterate through each row
            for (int row = 2; row <= lastRowIgnoreFormulas; row++)
            {
                // Fetch values
                name = sheet.Cells[row, 2].value;
                mark1 = sheet.Cells[row, 3].value;
                mark2 = sheet.Cells[row, 4].value;
                mark3 = sheet.Cells[row, 5].value;

                Console.WriteLine("The scores of {0} are {1}, {2}, and {3}",
                    name, mark1, mark2, mark3);


                // Calculate total marks
                double totalMark = mark1 + mark2 + mark3;

                // Write back to sheet
                sheet.Cells[row, 6].value = totalMark;
            }

            book.Save();
        }

        [TearDown]
        public void EndTest()
        {
            // Close book and application
            book.Close();
            app.Quit();
        }
    }
}
