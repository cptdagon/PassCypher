using System;
using System.Threading;

namespace PassCypher
{
    internal class Threads
    {
        public static Thread Bootproc { get; set; }
        public static Thread Netproc { get; set; }
    }
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
            Notification.EmailUser();           
        }
    }
}