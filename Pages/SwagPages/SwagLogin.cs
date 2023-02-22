using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumBasics.Pages.SwagPages
{

    public class SwagLogin
    {
        // Username
        [FindsBy(How = How.Id, Using = "user-name")]
        public IWebElement LoginName { get; set; }

        // Password
        [FindsBy(How = How.Id, Using = "password")]
        public IWebElement Password { get; set; }

        // Login button
        [FindsBy(How = How.Id, Using = "login-button")]
        public IWebElement LoginBtn { get; set; }
    }
}
