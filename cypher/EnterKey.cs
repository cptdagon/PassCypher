/*                                                          *
 * Adam Rushby - Dagon Interactive Media - PassCypher 2018  *
 *                                                          */

using System;
using System.Threading;

namespace PassCypher
{
    class EnterKey
    {
        private EnterKey() { }
        private static Thread inputThread;
        private static AutoResetEvent getKey, gotKey;

        // Resets Events
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static EnterKey()
        {
            getKey = new AutoResetEvent(false);
            gotKey = new AutoResetEvent(false);           
        }
        //waits for event to be set
        //sets new event upon successful readline within time limit
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

        // Spawns background thread
        // sets event and waits for new event to be set within X amount milliseconds
        // if succesful returns readline value. 
        // omit the parameter to read a line without a timeout
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
