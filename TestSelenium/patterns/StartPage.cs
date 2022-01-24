using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace TestSelenium.patterns
{
    public class StartPage
    {
        private WebDriver _driver;
        private WebDriverWait _wait;
        
        [FindsBy(How = How.XPath, Using = "//ul[@id='topItemsMenu']//li/a[contains(@href, 'information')]")]
        public IWebElement Information { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//ul[@id='topItemsMenu']//li/a[contains(@href, 'aboutproject')]")]
        public IWebElement About { get; set; }

        public Menu sendOrderMenu;
        public Menu informationMenu;
        

        public StartPage(WebDriver driver)
        {
            _driver = driver;
            _driver.Url = "https://old.kzn.opencity.pro/";
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            sendOrderMenu = new Menu(driver, "sendorder");
            informationMenu = new Menu(driver, "information");
        }

        public StartPage Open()
        {
            return this;
        }

        public RegistrationPage GetRegistrationPage()
        {
            _driver.FindElement(By.XPath("//a[@data-ui='registration']")).Click();
            return new RegistrationPage(_driver, _wait);
        }

        public LoginPage GetLoginPage()
        {
            _driver.FindElement(By.XPath("//a[@data-ui='auth']")).Click();
            return new LoginPage(_driver, _wait);
        }
    }
}