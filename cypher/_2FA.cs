using System;
using Google.Authenticator;
using System.IO;

namespace PassCypher
{
    internal class _2FA
    {
        public static string New2FA(string email)
        {
            string tfacode = CryptoRand.Random2faString();
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            var setupInfo = tfa.GenerateSetupCode("PASSCYPHER", email, tfacode, 300, 300);
            Console.WriteLine(setupInfo.ManualEntryKey);
            Console.Write("Press ENTER: ");
            Console.ReadLine();
            return tfacode;
        }

        public static bool Authenticate(string masterkey, string user)
        {
            string decryptedstring = "";
            string path = "";
            string encryptedstring;
            path = Path.Combine("Users", user);
            path = Path.Combine(path, user + "_Config.txt");
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(path);
                encryptedstring = sr.ReadLine();
                decryptedstring = StringCipher.RjndlDecrypt(encryptedstring, masterkey);
            }
            catch (Exception e)
            {
                Console.WriteLine("exception: " + e.Message);
                //sr.Close();
            }
            finally
            {
                sr.Close();
            }
            Console.WriteLine("input Google 2FA code:");
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            return tfa.ValidateTwoFactorPIN(decryptedstring, Console.ReadLine());
        }
    }
}