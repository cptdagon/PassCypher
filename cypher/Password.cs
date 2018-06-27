using System;
using System.IO;

namespace PassCypher
{
    internal class Password
    {
        public static string Passcheck()
        {
            ConsoleKeyInfo key;
            string password = "";
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            }
            while (key.Key != ConsoleKey.Enter);
            return (password);
        }

        public static string Passworddecrypt(string password, string user)
        {
            string decryptedstring = "";
            string path = "";
            string encryptedstring;            
            path = Path.Combine("Users", user);
            path = Path.Combine(path, user + "_Master.txt");
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(path);
                while((encryptedstring = sr.ReadLine())!= null)
                {
                    decryptedstring = StringCipher.RjndlDecrypt(encryptedstring, password);
                }
            }                            
            catch (Exception e)
            {
                Console.WriteLine("exception: " + e.Message);
                decryptedstring =  "null";
            }
            finally
            {
                sr.Close();
            }
            return (decryptedstring);
        }
    }
}