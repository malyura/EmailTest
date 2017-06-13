using NUnit.Framework;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NLog;

namespace EmailTest
{
    [TestFixture]
    class SendEmailTest : DataFromDB      //наследуемся от класса с нужным типом хранилища данных (DataFromXml, DataFromDB или DataFromCsv)
    {
        IWebDriver driver;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        bool iExecTestGood = false; // переменная для проверки успешности теста
        string url = "https://mail.yandex.by/";
        Report rep = new Report();
        int countCase = 0;  //Счетчик тест-кейсов

        [Test, TestCaseSource("GetData")]
        public void UserSendEmail(string acc_sender, string pass_sender, string acc_receiver, string pass_recevier)
        {
            try
            {
                AccountPage ap = new AccountPage(driver);   //создаем новый экземпляр класса AccountPage 
                MailPage mp = ap.logInToAccount(acc_sender, pass_sender); //выполняем метод logInToYourAccount который вернет нам экземпляр mg класса MailPage
                Assert.AreEqual(acc_sender, mp.getEmailFromProfile());   //проверка email владельца профиля
                logger.Info("Log in to the sender profile completed");
                mp.clickWriteMailButton();  //нажимаем кнопку создания нового сообщения
                mp.userWriteAndSendEmail(acc_receiver, subject, text); //пишем и отправляем сообщение
                Assert.AreEqual("Письмо отправлено.", mp.getTextFromConfirmMessage());  //убеждаемся что сообщение отправлено (есть сообщение подтверждение)
                mp.clickSentMailsLink();     //идем в отправленные письма
                Assert.True(mp.checkMail(acc_receiver, subject));   //проверка наличия письма в отправленных
                logger.Info("message sent");
                mp.clickProfileQuit();   //Выход их профиля отправителя
                driver.Url = url;
                mp = ap.logInToAccount(acc_receiver, pass_recevier);   //вход в профиль получателя письма
                Assert.AreEqual(acc_receiver, mp.getEmailFromProfile());   //проверка email владельца профиля
                logger.Info("Log in to the receiver profile completed");
                mp.clickPresentMailsLink();   //вход во входящие
                Assert.True(mp.getTextFromNoMailsMessage(), "No messages in the inbox");   //проверка есть ли во входящих письма
                Assert.True(mp.checkMail(acc_sender, subject), "Message not found");  //проверка есть ли письмо от отправителя
                rep.getReport("Sender: " + acc_sender + " Receiver: " + acc_receiver, "Passed", countCase);  //формирование отчета об успешном тесте
                iExecTestGood = true;        //тест пройден успешно
            }
            catch (Exception e)
            {

                logger.ErrorException(e.Message, e);     //запись ошибки в лог
                string screenPath = @"d:\for_test\bug_" + acc_sender + "_" + acc_receiver + "_" + DateTime.Now.ToString("d.M.yy HH-mm-ss") + ".png";
                rep.TakeScreenshot(driver,screenPath);   //скриншот экрана
                rep.getReport("Sender: " + acc_sender + " Receiver: " + acc_receiver, "Failed", countCase, e.Message, screenPath);  //формирования отчета о неуспешном тесте
                iExecTestGood = false;     //тест не пройден
            }
            Assert.True(iExecTestGood, "Test falled");  //для верного отображения успешности/неуспешности теста
        }

        [SetUp]
        public void SetUp()
        {

            countCase++;
            logger.Info("Case #{0}", countCase);
            driver = new ChromeDriver();
            logger.Info("New driver instantiated");
            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 15));
            logger.Info("Implicit wait applied on the driver for 15 seconds");
            driver.Manage().Window.Maximize();
            logger.Info("Maximize browser window");
            driver.Navigate().GoToUrl(url);
            logger.Info("Web application launched");

        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            logger.Info("Browser closed");
        }


    }


}
