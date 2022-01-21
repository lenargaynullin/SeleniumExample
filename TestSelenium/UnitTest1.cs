using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestSelenium
{

    public class NTests
    {
        private WebDriver driver;
        private WebDriverWait wait;
        private string login, password;
        private bool isAuth = false;
        
        const string Url = "https://old.kzn.opencity.pro/";
        
        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver(); // Запуск Chrome
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5)); // Задержка 5 секунд
            (login, password) = Utils.ClientRegistration(driver, wait);
        }

       [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit(); // Закрыть браузер
        }

        [Test]
        public void CheckAuthTesting()
        {
            Authorization(Url); // Открыть ссылку
            // Сравнить ссылки
            Assert.AreEqual("https://old.kzn.opencity.pro/cabinet/", driver.Url, "Не перешли в личный кабинет");
            isAuth = true;
        }
        
        [Test]
        public void CheckMyProfileTesting() // Ввод данных в поля профиля
        {
            if (!isAuth) Authorization(Url); // Если isAuth не true (Не авторизованы), то выполнить метод Authorization
            
            // Найти, Нажать Редактировать профиль (a.btn_edit_profile itemMenu):
            // driver.FindElement(By.CssSelector("a[class='btn_edit_profile itemMenu']")).Click();

            // Открыть ссылку редактировать профиль
            driver.Navigate().GoToUrl("https://old.kzn.opencity.pro/cabinet/myprofile");
            
            

            // Фамилия
            driver.FindElement(By.XPath("//input[@data-ui='lastname']")).SendKeys("Гайнуллин");

            // Имя
            driver.FindElement(By.XPath("//input[@data-ui='name']")).SendKeys("Ленар");
            
            // Отчество
            driver.FindElement(By.XPath("//input[@data-ui='patronymic']")).SendKeys("Талгатович");
            
            // Телефон
            driver.FindElement(By.XPath("//input[@data-ui='phone']")).SendKeys("89625522977");
            
            // Нажать сохранить
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            // Проверка. Появилось ли сообщение "Обновление профиля"
            var isExist1 = wait.Until(e =>
                e.FindElement(By.XPath("//h3[text()='Обновление профиля']"))).Displayed;
            Assert.IsTrue(isExist1, "Данные не сохранились - сообщение 'Обновление профиля' не появилось");
        }

        [Test]
        public void CheckMyProfileTestingDelete() // Удаление данных из полей профиля
        {
            if (!isAuth) Authorization(Url); // Если isAuth не true (Не авторизованы), то выполнить метод Authorization

            // Найти, Нажать Редактировать профиль (a.btn_edit_profile itemMenu):
            // driver.FindElement(By.CssSelector("a[class='btn_edit_profile itemMenu']")).Click();

            // Открыть ссылку редактировать профиль
            driver.Navigate().GoToUrl("https://old.kzn.opencity.pro/cabinet/myprofile");

            // Фамилия
            driver.FindElement(By.XPath("//input[@data-ui='lastname']")).SendKeys(Keys.Control + "a");
            driver.FindElement(By.XPath("//input[@data-ui='lastname']")).SendKeys(Keys.Delete);

            // Имя
            driver.FindElement(By.XPath("//input[@data-ui='name']")).SendKeys(Keys.Control + "a");
            driver.FindElement(By.XPath("//input[@data-ui='name']")).SendKeys(Keys.Delete);

            // Отчество
            driver.FindElement(By.XPath("//input[@data-ui='patronymic']")).SendKeys(Keys.Control + "a");
            driver.FindElement(By.XPath("//input[@data-ui='patronymic']")).SendKeys(Keys.Delete);

            // Телефон
            driver.FindElement(By.XPath("//input[@data-ui='phone']")).SendKeys("79876543210");
            
            // Нажать сохранить
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
                   
            // Проверка. Появилось ли сообщение "Обновление профиля"
            var isExist1 = wait.Until(e =>
                e.FindElement(By.XPath("//h3[text()='Обновление профиля']"))).Displayed;
            Assert.IsTrue(isExist1, "Данные не удалились - сообщение 'Обновление профиля' не появилось");

        }

        private void Authorization(string url)
        {
            driver.Navigate().GoToUrl(url); // Открыть ссылку
            driver.Manage().Window.Maximize(); // Окно во весь экран
            var auth = driver.FindElement(By.XPath("//a[@data-ui='auth']")); // Найти элемент a.auth (Войти),  (авторизация)
            auth.Click(); // Нажать Войти
            IWebElement inputEmail = wait.Until(e => e.FindElement(By.Name("username"))); // Найти элемент by Name "username" 
            inputEmail.SendKeys(login); // Ввести логин в поле inputEmail
            driver.FindElement(By.Name("password")).SendKeys(password); // Найти элемент по имени password, ввести пароль
            driver.FindElement(By.CssSelector("button.inputSubmit")).Click(); // Найти элемент по cssSelector button.inputSubmit (Войти), Нажать
            
            
        }


    }
}