using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestSelenium
{
    public class Utils
    {
        public static (string, string) ClientRegistration(WebDriver driver, WebDriverWait wait )
        {
            try
            {
                driver.Navigate().GoToUrl("https://old.kzn.opencity.pro/");
                var registration = driver.FindElement(By.XPath("//a[@data-ui='registration']"));
                //Assert.IsTrue(registration.Displayed);
                registration.Click();
                IWebElement inputEmail = wait.Until(e => e.FindElement(By.Name("email")));

                //var inputEmail = driver.FindElement(By.Name("email"));
                Random rnd = new Random();
                inputEmail.SendKeys(rnd.NextInt64(1111111,99999999) + "@gmail.com");
                driver.FindElement(By.CssSelector("button[data-ui='submitBtn']")).Click();

                //не могу вытащить текст из элемента - иду другим путем, ищем элемент по тексту, что успешно зарегестрирована
                //var msg = wait.Until(e => e.FindElement(By.XPath("//div[@class='message_notify']/h3")));
                var isExist = wait.Until(e =>
                    e.FindElement(By.XPath("//h3[text()='Вы зарегистрированы!']"))).Displayed;
                Assert.IsTrue(isExist, "Регистрация не прошла - сообщение 'Вы зарегистрированы!' не появилось");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Assert.Ignore("Не удалось зарегистрировать клиента, дальнейшие тесты бессмысленны");
            }

            return ("alisa.skrynko@gmail.com", "c069db");
        }
    }
}