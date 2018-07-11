/*                                                          *
 * Adam Rushby - Dagon Interactive Media - PassCypher 2018  *
 *                                                          */   

using System;
using System.Threading;

namespace PassCypher
{
    // Implementation of Console.Readline combined with a variable Timeout Exception
    class Reader
    {
        private static Thread inputThread;
        private static AutoResetEvent getInput, gotInput;
        private static string Input { get; set; }

        // resets events and spawns background threads
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static Reader()
        {
            getInput = new AutoResetEvent(false);
            gotInput = new AutoResetEvent(false);
            inputThread = new Thread(Read)
            {
                IsBackground = true
            };
            inputThread.Start();
        }
        //waits for event to be set
        //sets new event upon successful readline within time limit
        private static void Read()
        {
            Input = "";
            while (true)
            {
                getInput.WaitOne();
                Input = Console.ReadLine();
                gotInput.Set();
            }
        }

       
        // sets event and waits for new event to be set within X amount milliseconds
        // if succesful returns readline value. 
        // omit the parameter to read a line without a timeout
        public static string ReadLine(int timeOutMillisecs = Timeout.Infinite)
        {
            getInput.Set();
            bool success = gotInput.WaitOne(timeOutMillisecs);
            if (success)
                return Input;
            else
                throw new TimeoutException("User did not provide input within the time-limit.");
        }

        
    }
}