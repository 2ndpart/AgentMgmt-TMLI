using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MailSender
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string mailUserName = "tmconnect.no-reply@tokiomarine-life.co.id";
                string mailPass = "tmc0nn3ct!";

                string newUserName = "tmli\\tmconnect.no-reply";

                //string toEmail = args[0];

                SmtpClient smtp = new SmtpClient();
                MailMessage message = new MailMessage(mailUserName, "js.regar@gmail.com");//"rudiyanto.henriko@tokiomarine-life.co.id"
                message.Subject = "Welcome";
                message.Body = "Hello";

                smtp.Host = "192.168.1.27";
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(newUserName, mailPass);
                smtp.Port = 25;
                smtp.Send(message);
                Console.WriteLine("done");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
