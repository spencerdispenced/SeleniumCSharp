using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumBasics.Pages.SwagPages
{
    
    public class SwagHome
    {
        [FindsBy(How = How.Id, Using = "react-burger-menu-btn")]
        public IWebElement BrugerBtn { get; set; }

        [FindsBy(How = How.Id, Using = "logout_sidebar_link")]
        public IWebElement LogoutBtn { get; set; }
    }
}
