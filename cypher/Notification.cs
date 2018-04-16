using System.Net;
using System.Net.Mail;
using System.Reflection;
using System;
namespace PassCypher
{
    internal class Notification
    {
        private const string password = "";
        internal static void EmailUser()
        {

            Assembly assembly = Assembly.GetExecutingAssembly();
            Type type = typeof(AssemblyDescriptionAttribute);      
            AssemblyDescriptionAttribute a = (AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(assembly, type);
            NetworkCredential credential = new NetworkCredential(a.Description, password);
            MailMessage mail = new MailMessage(a.Description, "");
            SmtpClient client = new SmtpClient
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smtp-mail.outlook.com",
                Credentials = credential,
                EnableSsl = true
            };
            mail.Subject = "this is a test email.";
            mail.Body = "this is my test email body";
            client.Send(mail);
            client.Dispose();
            mail.Dispose();
        }
    }
}