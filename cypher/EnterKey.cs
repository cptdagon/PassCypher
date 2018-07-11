using System;
using System.Threading;

namespace PassCypher
{
    class EnterKey
    {
        private static Thread inputThread;
        private static AutoResetEvent getKey, gotKey;
        

        static EnterKey()
        {
            getKey = new AutoResetEvent(false);
            gotKey = new AutoResetEvent(false);           
        }

        private static void Enterkey()
        {            
            ConsoleKeyInfo key;              
            getKey.WaitOne();
            do
            {
                key = Console.ReadKey(true);
            }
            while (key.Key != ConsoleKey.Enter);
            gotKey.Set();
        }
        public static bool EnterToContinue(int timeOutMillisecs = Timeout.Infinite)
        {
            inputThread = new Thread(Enterkey)
            {
                IsBackground = true
            };
            inputThread.Start();
            getKey.Set();
            bool success = gotKey.WaitOne(timeOutMillisecs);
            inputThread.Abort();
            if (success)
                return true;
            else
                throw new TimeoutException("User did not provide input within the time-limit.");
        }
    }
}
