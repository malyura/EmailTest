using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace EmailTest
{
    public class Report
    {

        public string TakeScreenshot(IWebDriver driver, string nameScreen)   //метод для снятия скриншота при неудачном тесте
        {
            string dirScreen = Directory.CreateDirectory("screen").FullName;
            ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            string saveLocation = Path.Combine(dirScreen, nameScreen);
            screenshot.SaveAsFile(saveLocation, ImageFormat.Png);
            return Path.GetFullPath(saveLocation);
        }


        public void getReport(string acc, string result, int count, string reason = "", string screen = "")   //метод для формирования отчета
        {
            XElement report = new XElement("Report",
                           new XElement("Account", acc),
                           new XElement("Results", result),
                           new XElement("Failures_reason", reason),
                           new XElement("Screen", screen)
                           );
            Directory.CreateDirectory("report");
            string pathXml = @"d:\for_test\xml_rep.xml";
            if (count == 1)
            {
                XDocument doc = new XDocument(new XElement("Reports", report));
                doc.Save(pathXml);
            }
            else
            {
                XDocument document = XDocument.Load(pathXml);
                document.Element("Reports").Add(report);
                document.Save(pathXml);
            }

            if (count == 5)
            {

                XslCompiledTransform xslt = new XslCompiledTransform();
                string outputFile = @"d:\for_test\report_" + DateTime.Now.ToString("d.M.yy HH-mm-ss") + ".html";
                xslt.Load(@"d:\for_test\style_rep.xslt");
                xslt.Transform(pathXml, outputFile);
                Process.Start(outputFile);
            }
        }


    }
}
