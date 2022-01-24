using System;
using OpenQA.Selenium;

namespace TestSelenium.patterns
{
    public class Menu
    {
        private IWebElement menuItem;

        public Menu(WebDriver driver, string containsUrl)
        {
            this.menuItem = driver.FindElement(By.XPath($"//ul[@id='topItemsMenu']//li/a[contains(@href, '{containsUrl}')]"));
        }

        public void onClick()
        {
            this.menuItem.Click();
        }

        public string getText()
        {
            return menuItem.Text;
        }
    }
}