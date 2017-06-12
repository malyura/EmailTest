using CsvHelper;
using System.Collections.Generic;
using System.IO;

namespace EmailTest
{
    public class DataFromCsv
    {
        protected string subject = "Message";
        protected string text = "Hello! How are you?";

        protected static IEnumerable<string[]> GetData()
        {
            var reader = new CsvReader(File.OpenText(@"d:\for_test\data.csv"));
            while (reader.Read())
            {
                yield return new string[] { reader.GetField<string>(0), reader.GetField<string>(1),
                    reader.GetField<string>(2), reader.GetField<string>(3) };
            }
        }

    }
}
