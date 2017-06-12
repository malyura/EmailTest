using System.Collections.Generic;
using System.Data.SqlClient;

namespace EmailTest
{
    class DataFromDB
    {
        protected string subject = "Message";
        protected string text = "Hello! How are you?";

        static SqlConnection connection;

        static DataFromDB()
        {
            string connectionStr = @"Data Source =(local); Initial Catalog = Accounts; Integrated Security = true";
            connection = new SqlConnection(connectionStr);
        }

        protected static IEnumerable<string[]> GetData()
        {
            SqlCommand command = new SqlCommand("SELECT acc_sender, pass_sender, acc_receiver, pass_receiver FROM Accounts", connection);
            connection.Open();
            SqlDataReader dr = command.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    yield return new string[] { dr.GetString(0), dr.GetString(1), dr.GetString(2), dr.GetString(3) };
                }
                connection.Close();
            }
        }
    }
}
