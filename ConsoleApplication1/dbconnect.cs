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
        public static void updaterecord(string rechnungsnr, bool status)
        {
            string myConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand();
            string SQL = "INSERT INTO `rechnungsversand` (`rechnungsnr`, `status` ) VALUES ('@rechnungsnr', '@status')";
            command.CommandText = SQL;
            command.Parameters.AddWithValue("@rechnungsnr", rechnungsnr);
            command.Parameters.AddWithValue("@status", status);
            command.Connection = connection;
            command.ExecuteNonQuery();
            connection.Close();
        }

	}
}
