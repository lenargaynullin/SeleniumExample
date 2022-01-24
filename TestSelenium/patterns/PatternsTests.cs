using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace TestSelenium.patterns
{
    public class PatternsTests
    {
        private WebDriver driver;
        private WebDriverWait wait;
        private StartPage _startPage;
        
        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        [SetUp]
        public void BeforeTest()
        {
            if (driver.FindElements(By.XPath("//a[@class='btnExit']")).Count > 0)
            {
                driver.FindElement(By.XPath("//a[@class='btnExit']")).Click();
            }
            _startPage = new StartPage(driver).Open();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void RegistrationTesting()
        {
            Random rnd = new Random();
            var registrationPage = _startPage.GetRegistrationPage();
            registrationPage.SetEmail(rnd.Next(1000,9999).ToString() +"dfsfsfs@sfdsfs.ru")
                .SubmitRegistration();
            Assert.IsTrue(registrationPage.IsSuccessRegistration());
        }
        
        [Test]
        public void LoginTesting()
        {
            var loginPage = _startPage.GetLoginPage();
            loginPage.Login("alisa.skrynko@gmail.com", "c069db");
            Assert.AreEqual("https://old.kzn.opencity.pro/cabinet/", driver.Url, "Не перешли в личный кабинет");
        }

        [Test]
        public void FActoryTest()
        {
            PageFactory.InitElements(driver, _startPage);
            _startPage.About.Click();
            Assert.AreEqual("https://old.kzn.opencity.pro/aboutproject", driver.Url);
        }


        [Test]
        public void PageElementTesting()
        {
            _startPage.sendOrderMenu.onClick();
            Assert.AreEqual("https://old.kzn.opencity.pro/sendorder", driver.Url);
        }
        
        public void BuilderTest()
        {
            var person = new Person.Builder()
                .WithSurname("Petrov")
                .WithName("Dima").build();
        }
    }
}