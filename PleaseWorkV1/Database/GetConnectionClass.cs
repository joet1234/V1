using MySql.Data.MySqlClient;
using System;
using System.Net;

namespace PleaseWorkV1.Database
{
    public class GetConnectionClass
    {
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    //Imagine a world were google doesn't return something. . . Exactly 
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public MySqlConnection GetConnection() 
        {
                MySqlConnection sqlconn;

                string connsqlstring = "Server=qub.cjw92whe4wuf.eu-west-2.rds.amazonaws.com;Port=3306;database=med;User Id=jTurkington;Password=emily1234;";

                sqlconn = new MySqlConnection(connsqlstring);

                sqlconn.Open();

                return sqlconn;
        }
    }
}