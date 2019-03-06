/*                                                          *
 * Adam Rushby - Dagon Interactive Media - PassCypher 2018  *
 *                                                          */

using System;
using System.IO;

namespace PassCypher
{
    internal class ReadWrite
    {
        private ReadWrite() { }
        public static void Passwordgen()
        {
            string tag = CryptoRand.RandomString(20);

            Console.WriteLine("Please enter a User name for the password");
            Format.Dash();

            string username = Console.ReadLine();

            Format.Dash();
            Console.WriteLine("Please name the service that uses the UN/P");
            Format.Dash();

            string service = Console.ReadLine();
            
            string plaintext = service + ": " + username + " " + tag;
            string encryptedstring = StringCipher.RjndlEncrypt(plaintext, SessionParameters.Pass);
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(SessionParameters.Keypath, true);
                sw.WriteLine(encryptedstring);
                //sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                sw.Close();
            }
            Format.Dash();
            Console.WriteLine("Your password string is:");
            Format.Dash();
            Console.WriteLine(tag);
            Console.Write("Press ENTER: ");
            Console.ReadLine();
            return;
        }
        public static void Passwordload()
        {
            string encryptedstring;
            StreamReader sr = null;
            try
            {               
                sr = new StreamReader(SessionParameters.Keypath);
                while ((encryptedstring = sr.ReadLine()) != null)
                {                              
                    string decryptedstring = StringCipher.RjndlDecrypt(encryptedstring, SessionParameters.Pass);
                    Console.WriteLine("\n" + decryptedstring + "\n");
                    Format.Dash();
                }
                //sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("exception: " + e.Message);
            }
            finally
            {
                sr.Close();
            }
        }
    }
}