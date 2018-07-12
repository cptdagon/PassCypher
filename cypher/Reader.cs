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

        // resets events
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static Reader()
        {
            getInput = new AutoResetEvent(false);
            gotInput = new AutoResetEvent(false);           
        }
        //waits for event to be set
        //sets new event upon successful readline within time limit
        private static void Read()
        {
            while (true)
            {
                getInput.WaitOne();
                ConsoleKeyInfo key;
                Input = "";
                do
                {
                    key = Console.ReadKey(true);
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        Input += key.KeyChar;
                        Console.Write(key.KeyChar);
                    }
                    else
                    {
                        if (key.Key == ConsoleKey.Backspace && Input.Length > 0)
                        {
                            Input = Input.Substring(0, (Input.Length - 1));
                            Console.Write("\b \b");
                        }
                    }
                }
                while (key.Key != ConsoleKey.Enter);
                gotInput.Set();
            }
        }

        // Spawns background thread
        // sets event and waits for new event to be set within X amount milliseconds
        // if succesful returns readline value. 
        // omit the parameter to read a line without a timeout
        public static string ReadLine(int timeOutMillisecs = Timeout.Infinite)
        {
            inputThread = new Thread(Read)
            {
                IsBackground = true
            };
            inputThread.Start();
            getInput.Set();
            bool success = gotInput.WaitOne(timeOutMillisecs);
            inputThread.Abort();
            if (success)
                return Input;
            else
                throw new TimeoutException("User did not provide input within the time-limit.");
        }

        
    }
}