using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestSelenium.patterns
{
    public class LoginPage
    {
        private WebDriver _driver;
        private WebDriverWait _wait;

        public LoginPage(WebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
        }

        public void SetLogin(string login)
        {
            _wait.Until(e => e.FindElement(By.Name("username"))).SendKeys(login);
        }
        
        public void SetPassword(string psw)
        {
            _wait.Until(e => e.FindElement(By.Name("password"))).SendKeys(psw);
        }

        public void Login(string login, string psw)
        {
            SetLogin(login);
            SetPassword(psw);
            _driver.FindElement(By.CssSelector("button.inputSubmit")).Click();
        }
    }
}