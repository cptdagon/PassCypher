using System;
using System.Linq;

namespace PassCypher
{
    class LockMenu
    {
        public static void Lockmenu()
        {
            while (true)
            {
                MenuContext();
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "login":
                        bool b = false;
                        string user = "";
                        while (b != true)
                        {
                            Format.Clear();
                            string[] users = User.Fetchusers();
                            user = Console.ReadLine();
                            if (users.Contains(user) || user == "return")
                            {
                                b = true;
                            }
                            else
                            {
                                Console.WriteLine("User Does not exist");
                            }
                        }
                        if (user == "return")
                        {
                            break;
                        }
                        Lock(user);
                        break;
                    case "new":
                        Setup.NewUser();
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

        private static void InvalidContext()
        {
            Format.Clear();
            Console.WriteLine("Invalid entry\n" +
                                "Type new, login or exit");
            Format.Dash();
            Console.Write("Press ENTER: ");
        }

        private static void MenuContext()
        {
            Format.Clear();
            Console.WriteLine("Login to an existing user\n" +
                                "Create a new user\n" +
                                "Exit program");
            Format.Dash();
        }

        public static void Lock(string user)
        {
            Format.Clear();
            string check = "";
            int attempts = 0;
            while (attempts < 4)
            {
                Console.WriteLine("Please enter your password:");
                Format.Dash();
                check = Password.Passworddecrypt(Password.Passcheck(), user);
                if (check != "null")
                {
                    Format.Clear();
                    break;
                }
                else
                {
                    attempts++;
                    Format.Clear();
                    Console.WriteLine("Password incorrect\n" +
                                        "attempts remaining:" +
                                        (4 - attempts));
                }
            }
            while (true)
            {
                if (attempts < 4)
                {
                    //if (_2FA.Authenticate(check, user) == true)
                    Session.Setsession(check, user);
                    MainMenu.Mainmenu();
                    return;
                }
                else
                {
                    break;
                }
            }
        }
    }
}
