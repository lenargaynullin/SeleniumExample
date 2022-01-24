using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestSelenium.patterns
{
    public class RegistrationPage
    {
        private WebDriver _driver;
        private WebDriverWait _wait;

        public RegistrationPage(WebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
        }

        public RegistrationPage SetEmail(string email)
        {
            _wait.Until(e => e.FindElement(By.Name("email"))).SendKeys(email);
            return this;
        }

        public void SubmitRegistration()
        {
            _driver.FindElement(By.CssSelector("button[data-ui='submitBtn']")).Click();
        }

        public bool IsSuccessRegistration()
        {
            return _wait.Until(e =>
                e.FindElement(By.XPath("//h3[text()='Вы зарегистрированы!']"))).Displayed;
        }
    }
}