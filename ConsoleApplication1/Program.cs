using MySql.Data.MySqlClient;
using System.IO;
using System.Linq;
using System.Configuration;


namespace Rechnungsversand
{
    class Program
    {
        static void Main(string[] args)
        {

            string myConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            MySqlConnection connection = new MySqlConnection(myConnectionString);
            connection.Open();
            string[] pdfFiles = Directory.GetFiles(@"W:\\Rechnungsversand\\Rechnungen", "*.pdf").Select(path => Path.GetFileName(path)).ToArray();
            foreach (string filename in pdfFiles)
            {
                MySqlCommand command = connection.CreateCommand();
                command = new MySqlCommand("SELECT `SheetNr`,`AuftragsNr`,`AuftragsKennung`,`VorgangNr`,`Anschrift_Email` FROM `fk_auftrag` WHERE `AuftragsNr` = @Value AND `AuftragsKennung` = 3;", connection);
                command.Parameters.AddWithValue("@Value", filename.Replace(".pdf", ""));
                MySqlDataReader Reader;
                Reader = command.ExecuteReader();
                string adresse = Reader.GetString("Anschrift_Email");
                string betreff = "Ihre Rechnung mit der Rechnungsnr: " + filename.Replace(".pdf", "") + " von Wunschreich";
                string rnr = Reader.GetString("AuftragsNr");
                send_mail.send(adresse, betreff,filename, rnr);
                
            }
            
        }
    }
}
