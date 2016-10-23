using System;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Rechnungsversand
{
    public class send_mail
    {
        public static async Task send(string adresse, string betreff, string anhang, string rnr)
        {
            using (MailMessage wunschreich_mail = new MailMessage())
            using (SmtpClient client = new SmtpClient())
            {
                Attachment attachment = null;
                client.Port = 25;
                client.Host = "smtp.server.com";
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("send@mail.com", "password");
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                wunschreich_mail.Subject = betreff;
                wunschreich_mail.IsBodyHtml = true;
                wunschreich_mail.AlternateViews.Add(getEmbeddedImage("W:\\Rechnungsversand\\assets\\signatur.png"));
                wunschreich_mail.From = new MailAddress("send@mail.com");
                wunschreich_mail.To.Add(new MailAddress(adresse));
                wunschreich_mail.Subject = "yourSubject";
                attachment = new System.Net.Mail.Attachment(anhang);
                wunschreich_mail.Attachments.Add(attachment);

                int retry = 0;
                while (retry < 3)
                    try
                    {
                        await client.SendMailAsync(wunschreich_mail);
                        await dbconnect.updaterecord(rnr, true);
                        attachment.Dispose();
                        break;
                    }
                    catch
                    {
                        var err = dbconnect.updaterecord(rnr, false);
                        retry++;
                    }
            }
        }


        private static AlternateView getEmbeddedImage(string filePath)
        {
            LinkedResource inline = new LinkedResource(filePath);
            inline.ContentId = Guid.NewGuid().ToString();
            string htmlBody = @"<img src='cid:" + inline.ContentId + @"'/>
";
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(inline);
            return alternateView;
        }
    }
}
