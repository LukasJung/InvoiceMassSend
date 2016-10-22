using ConsoleApplication1.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Rechnungsversand
{
    public static class dbconnect
    {
        private static string myConnectionString;
        public static MySqlConnection connection;
        public static async Task updaterecord(string rechnungsnr, bool status)
        {
            MySqlCommand command = new MySqlCommand();
            string SQL = string.Format("INSERT INTO `rechnungsversand` (`rechnungsnr`, `status` ) VALUES ('{0}', '{1}')", rechnungsnr, status);
            command.CommandText = SQL;
            
            command.Connection = connection;
            await command.ExecuteNonQueryAsync();
            connection.Close();
        }

        public static void OpenConnection()
        {
            try
            {
                myConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
                connection = new MySqlConnection(myConnectionString);
                connection.Open();
                Console.WriteLine("[TL] MySQL Connection established.");
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
        }

        public static void CloseConnection()
        {
            try
            {
                if(connection.State != System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
	}
}
