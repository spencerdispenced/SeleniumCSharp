using ClosedXML.Excel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumBasics.Pages.SwagPages;
using SeleniumExtras.PageObjects;
using System;

namespace SeleniumBasics.Advanced
{
    [TestFixture]
    public class PomLoginsSwag : Setup.BaseTestSwag
    {
        private string excelPath = "SwagLogins.xlsx";
        private string scrnPath = "SeleniumBasics\\SeleniumBasics\\Screenshots\\FailSwagPOM.PNG";


        [Test, Order(1)]
        public void TestA()
        {
            try
            {
                var book = new XLWorkbook(excelPath);
                var sheet = book.Worksheet("logins");


                // Initialize login page objects
                SwagLogin login = new SwagLogin();
                PageFactory.InitElements(d, login);

                // Initialize logout page objects
                SwagHome logout = new SwagHome();
                PageFactory.InitElements(d, logout);



                var rowCount = sheet.LastRowUsed().RowNumber();
                WebDriverWait wait = new WebDriverWait(d, TimeSpan.FromSeconds(10));

                for (int i = 1; i < rowCount; i++)
                {
                    try
                    {
                        var username = sheet.Cell(i + 1, 1).Value.ToString();
                        var password = sheet.Cell(i + 1, 2).Value.ToString();


                        // Login using POM objects
                        login.LoginName.SendKeys(username);
                        login.Password.SendKeys(password);
                        login.LoginBtn.Click();

                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(d.FindElement(By.Id("react-burger-menu-btn"))));
                        sheet.Cell(i + 1, 3).Value = "Pass";

                        // Logout using POM objects
                        logout.BrugerBtn.Click();
                        logout.LogoutBtn.Click();
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
    }
}
