using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace EmailTest
{
    class DataFromXml
    {
        protected string subject = "Message";
        protected string text = "Hello! How are you?";
        protected static IEnumerable<string[]> GetData()
        {
            using (XmlReader reader = XmlReader.Create(@"d:\for_test\data.xml"))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.Name == "Account")
                    {
                        XElement el = XElement.ReadFrom(reader) as XElement;
                        if (el != null)
                            yield return new string[] { el.Element("acc_sender").Value, el.Element("pass_sender").Value,
                                        el.Element("acc_receiver").Value, el.Element("pass_receiver").Value };
                    }
                }
            }
        }
    }
}
