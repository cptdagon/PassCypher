using System;
using System.IO;

namespace PassCypher
{
    internal class Setup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        private static string[] Paths { get; set; }
        internal static void NewUser()
        {
            Format.Clear();
            Console.WriteLine("PASSCYPHER makes use of Google Authenticate two-factor Authentication");
            Format.Dash();
            while (true)
            {
                Console.WriteLine("Enter a user name");
                string Username = Console.ReadLine();
                string Userpath = Username;
                Userpath = Path.Combine("Users", Userpath);
                if(!Directory.Exists(Userpath))
                {                   
                    Directory.CreateDirectory(Userpath);                        // creates a seperate directory for files
                    Paths[0] = Path.Combine(Userpath, Username + "_Master.txt");    // path stores master key for user
                    Paths[1] = Path.Combine(Userpath, Username + "_Keys.txt");      // path stores passwords
                    Paths[2] = Path.Combine(Userpath, Username + "_Config.txt");    // path stores misc data such as 2FA key and email                                            
                    foreach(string path in Paths)
                    {
                        File.Create(path).Close();
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("User name taken\nPlease pick a new name");
                }
            }
            Console.WriteLine("Enter an Email Address");
            string email = Console.ReadLine();
            string password = PasswordStrength();
            string masterkey = CryptoRand.RandomString(40);
            string securekey = _2FA.New2FA(email);
            Encrypt(securekey, masterkey, Paths[2]);
            Encrypt(email, masterkey, Paths[2]);
            Encrypt(masterkey, password, Paths[0]);
        }

        private static string PasswordStrength()
        {
            Format.Dash();
            Console.WriteLine("\t\t  Very Weak     Medium    Very String ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\t\t█████████████");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("████████████");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("█████████████");
            Console.ForegroundColor = ConsoleColor.White;
            Format.Dash();
            Console.WriteLine("Enter a Password");
            return PasswordReadLine();
        }

        private static void Encrypt(string plaintext, string key, string path)
        {
            string encryptedstring = StringCipher.RjndlEncrypt(plaintext, key);
            try
            {
                StreamWriter sw = new StreamWriter(path, true);
                sw.WriteLine(encryptedstring);
                sw.Close();
            }
            catch { }
        }

        public static string PasswordReadLine()
        {
            ConsoleKeyInfo key;
            string password = "";
            int accetablescore = 0;
            do
            {
                key = Console.ReadKey(true);
                while (key.Key != ConsoleKey.Enter) {                                      
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        password += key.KeyChar;
                        accetablescore = PasswordDisplay(password);
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                        {
                            password = password.Substring(0, (password.Length - 1));
                            accetablescore = PasswordDisplay(password);
                        }
                    }
                    key = Console.ReadKey(true);
                }
            }
            while (accetablescore <= 3);
            Console.WriteLine();
            return (password);
        }

        private static int PasswordDisplay(string password)
        {
            int accetablescore = PasswordScore.Score(password);
            Console.Write("\r{0} ", password);
            Console.ForegroundColor = ConsoleColor.White;
            return accetablescore;
        }
    }
}