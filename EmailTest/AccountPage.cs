using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailTest
{
    class AccountPage
    {
        private IWebDriver driver;

        public AccountPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        By emailLocator = By.Name("login");
        By passLocator = By.Name("passwd");
        By signInLocator = By.XPath("//*[@type='submit']");



        public void emailInLoginForm(string email)    //email в форму
        {
            driver.FindElement(emailLocator).Click();
            driver.FindElement(emailLocator).Clear();
            driver.FindElement(emailLocator).SendKeys(email);
        }

        public void passInLoginForm(string pass)   //пароль в форму
        {
            driver.FindElement(passLocator).Click();
            driver.FindElement(passLocator).Clear();
            driver.FindElement(passLocator).SendKeys(pass);
        }

        public void clickSignInButton()   //метод для кнопки Войти
        {
            driver.FindElement(signInLocator).Click();
        }


        public MailPage logInToAccount(string email, string pass) //объединили 3 метода
        {
            emailInLoginForm(email);
            passInLoginForm(pass);
            clickSignInButton();
            return new MailPage(driver);
        }


    }
}
