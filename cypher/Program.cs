/*                                                          *
 * Adam Rushby - Dagon Interactive Media - PassCypher 2018  *
 *                                                          */

using System;
using System.Threading;

namespace PassCypher
{
    class Program
    {
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Boot.BootSetup();
            Threads.Netproc = new Thread(NetTest.Main)
            {
                IsBackground = true
            };
            Threads.Netproc.Start();
            Session.Setmode();
            LockMenu.Lockmenu();
            //Notification.EmailUser();           
        }
    }
}