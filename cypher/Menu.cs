/*                                                          *
 * Adam Rushby - Dagon Interactive Media - PassCypher 2018  *
 *                                                          */

using System;

namespace PassCypher
{
    internal class MainMenu
    {
        public static void Mainmenu()
        {
            string choice = "";
            while (true)
            {
                MenuContext();
                try
                {
                    choice = Reader.ReadLine(120000);
                }
                catch (TimeoutException)
                {
                    Format.Clear();
                    Console.WriteLine("Session has Timed Out");
                    Console.ReadLine();
                    Format.Dash();
                    return;
                }

                switch (choice)
                {
                    case "test":
                        Console.WriteLine(CryptoRand.RandomString(50));
                        Console.ReadLine();
                        break;
                    case "new":
                        Format.Clear();
                        ReadWrite.Passwordgen();
                        break;
                    case "load":
                        Load();
                        break;
                    case "Synq":
                        Format.Clear();
                        Filereceiver.SendFile();
                        break;
                    case "Update":
                        Format.Clear();
                        Filereceiver.RecFile();
                        break;
                    case "lock":
                        Format.Clear();
                        return;
                    case "help":
                        Help.GetHelp();
                        Format.Clear();
                        break;
                    case "exit":
                        Environment.Exit(0);
                        break;
                    default:
                        InvalidContext();
                        Console.ReadLine();
                        break;
                }
            }
        }

        private static void Load()
        {
            Format.Clear();
            ReadWrite.Passwordload();
            Console.WriteLine("Press Enter key to continue");
            try
            {
                EnterKey.EnterToContinue(120000);
            }
            catch (TimeoutException)
            {
                Format.Clear();
                Console.WriteLine("Session has Timed Out");
                Console.ReadLine();
                Format.Dash();
            }
            Format.Clear();
            return;
        }

        private static void InvalidContext()
        {
            Format.Clear();
            Console.WriteLine("Invalid entry\n" +
                                "Type new, load or exit");
            Format.Dash();
            Console.Write("Press ENTER: ");
        }

        private static void MenuContext()
        {
            Format.Clear();
            Console.WriteLine("Create a new password\n" +
                                "Load saved passwords");
            Format.Dash();
            if (Memmappedfile.Read("SerStat") == false)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            Console.WriteLine("Backup Database to Server\n" +
                                "Update Database from Server");
            Console.ForegroundColor = ConsoleColor.White;
            Format.Dash();
            Console.WriteLine("Lock Program\n" +
                                "Help\n" +
                                "Exit");
            Format.Dash();
        }
    }
}