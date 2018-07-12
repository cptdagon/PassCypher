/*                                                          *
 * Adam Rushby - Dagon Interactive Media - PassCypher 2018  *
 *                                                          */

using System.Net;
using System.Net.Mail;
using System.Reflection;
using System;
namespace PassCypher
{
    internal class Notification
    {
        private const string password = "";
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        internal static void EmailUser()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type type = typeof(AssemblyDescriptionAttribute);      
            AssemblyDescriptionAttribute a = (AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(assembly, type);
            NetworkCredential credential = new NetworkCredential(a.Description, password);
            using (MailMessage mail = new MailMessage(a.Description, ""))
            {
                SmtpClient client = null;
                try
                {
                    client = new SmtpClient
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
                }
                catch{}
                finally
                {
                    client.Dispose();
                    mail.Dispose();
                }
                return;
            }
        }
    }
}