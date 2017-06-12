using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmailTest
{
    class MailPage
    {
        private IWebDriver driver;
        public MailPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        By profileEmailLocator = By.CssSelector(".mail-User-Name");        // e-mail пользователя из профиля
        By writeMailLinkLocator = By.LinkText("Написать");
        By toFieldLocator = By.Name("to");     //поле Кому в окне создания нового сообщения
        By subjectFieldLocator = By.Name("subj");   //поле Тема в окне создания нового сообщения
        By textMailLocator = By.XPath("//*[@id='cke_1_contents']/div");   //поле для Текста письма
        By sendEmaiLocator = By.XPath("//*[@type='submit']");  //кнопка Отправить
        By sendConfirmMessageLocator = By.CssSelector(".mail-Done-Title.js-title-info");  //сообщение подтверждение "Письмо отправлено."

        By sentMailsLinkLocator = By.XPath("//span[text()='Отправленные']");
        By eMailLocator = By.XPath("//span[@class='mail-MessageSnippet-FromText']");
        By subjLocator = By.CssSelector(".mail-MessageSnippet-Item_subject");

        By presentMailsLinkLocator = By.XPath("//span[text()='Входящие']");

        By noMailsMessageLocator = By.XPath("//*[text()='В папке «Входящие» нет писем.']"); //сообщение входящих писем нет

        By quitLocator = By.XPath("//*[@data-metric='Меню сервисов:Выход']");

        public string getEmailFromProfile()                  //получить email владельца профиля
        {
            return FindElement(driver, profileEmailLocator, 5).Text;
        }

        public void clickProfileQuit()          //Выход из профиля
        {
            driver.FindElement(profileEmailLocator).Click();
            driver.FindElement(quitLocator).Click();
        }

        public void clickWriteMailButton()      //метод для клика Написать сообщение
        {
            FindElement(driver, writeMailLinkLocator, 10).Click();
        }


        public void userMailRecepient(string recepient) //метод для написания получателя письма
        {
            driver.FindElement(toFieldLocator).Click();
            driver.FindElement(toFieldLocator).Clear();
            driver.FindElement(toFieldLocator).SendKeys(recepient);
        }


        public void userMailSubject(string subject)  //метод для написания Темы письма
        {
            driver.FindElement(subjectFieldLocator).Click();
            driver.FindElement(subjectFieldLocator).Clear();
            driver.FindElement(subjectFieldLocator).SendKeys(subject);
        }

        public void userMailText(string text)  //метод для написания Текста письма
        {
            driver.FindElement(textMailLocator).Click();
            driver.FindElement(textMailLocator).Clear();
            driver.FindElement(textMailLocator).SendKeys(text);
        }

        public void clickSendButton()
        {
            driver.FindElement(sendEmaiLocator).Click();
        }

        public void userWriteAndSendEmail(string recepient, string subject, string text)
        {
            userMailSubject(subject);
            userMailText(text);
            userMailRecepient(recepient);
            clickSendButton();
        }


        public string getTextFromConfirmMessage()     //метод для получения текста из сообщения подтверждения об отправке
        {
            return driver.FindElement(sendConfirmMessageLocator).Text;
        }


        public bool getTextFromNoMailsMessage()     //метод для получения true если есть письма во входящих
        {
            if (driver.FindElements(noMailsMessageLocator).Count == 0)
                return true;
            return false;
        }

        public IWebElement FindElement(IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);

        }


        public void clickSentMailsLink()      //метод для клика кнопки Отправленные письма
        {
            driver.FindElement(sentMailsLinkLocator).Click();
        }

        public bool checkMail(string accName, string subject)          // метод для проверки письма в списках отправленных (для отправителя) или входящих (для получателя)
        {

            if (driver.FindElements(eMailLocator).First().Text == accName &&
                driver.FindElements(subjLocator).First().Text == subject)
            {
                return true;
            }

            return false;
        }


        public void clickPresentMailsLink()      //метод для клика кнопки Входящие письма
        {
            driver.FindElement(presentMailsLinkLocator).Click();
        }


    }
}
