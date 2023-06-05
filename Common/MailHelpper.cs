using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace Common
{
    public class MailHelpper
    {
        public static string password = ConfigurationManager.AppSettings["PasswordEmail"];
        public static string email = ConfigurationManager.AppSettings["Email"];
        public static bool SendMail(string name,string subject,string content,string toMail)
		{
            bool rs = false;
			try
			{
                MailMessage message = new MailMessage();
                var smtp = new System.Net.Mail.SmtpClient();
				{
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(email, password);
                    smtp.Timeout = 20000;
				}
                MailAddress fromAddress = new MailAddress(email, name);
                message.From = fromAddress;
                message.To.Add(toMail);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = content;
                smtp.Send(message);
                rs = true;
			}
           catch(Exception ex)
			{
                Console.WriteLine(ex.Message);
                rs = false;
			}
            return rs;
        }
    }
}
