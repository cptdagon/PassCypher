using System;
using System.Diagnostics;
using System.Globalization;
using System.IO.MemoryMappedFiles;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace PassCypher
{
    class BootSequence
    {
        private const int mem_max = 20;
        private static bool boolean1;
        private static bool boolean2;
        private static MemoryMappedViewAccessor SSaccessor;
        private static MemoryMappedViewAccessor NSaccessor;

        //list of items used in boot sequence.
        enum Load { Memory, Encryption };
        private static string Lgen(int i)
        {
            return (((Load)i).ToString());
        }

        private static void Ping()
        {
            // Sends test data to the file server.
            // If data is recieved then the test has passed the ping test.
            // If no connection can be made then an exception occurs and is caught by the program.
            Socket clientSock_client = null;
            try
            {
                IPEndPoint ipEnd_client = new IPEndPoint(IPAddress.Parse("192.168.1.109"), 12345);
                clientSock_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                clientSock_client.Connect(ipEnd_client);
                Green();
            }
            // Boolean return values used to indicate success  
            catch 
            {
                //clientSock_client.Close();
                Red();
                //return; 
            }
            finally
            {
                clientSock_client.Close();
            }            
            return;
        }

        private static void Memory()
        {
            PerformanceCounter RAM = null;
            try
            {
                RAM = new PerformanceCounter("Memory", "Available MBytes");
                float memory = (float)Convert.ToInt64(Process.GetCurrentProcess().PrivateMemorySize64.ToString(
                    CultureInfo.CreateSpecificCulture("en-GB")), 
                    CultureInfo.CreateSpecificCulture("en-GB")
                    ) / 1000000;
                if ((memory + mem_max) < RAM.NextValue())
                {
                    Green();
                    return;
                }
                
            }
            catch { }
            finally
            {
                RAM.Dispose();
            }
            Red();
            return;
        }

        private static void Green()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Green");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void Red()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Red");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (client.OpenRead("http://clients3.google.com/generate_204"))
                    {
                        Green();
                        NSaccessor.Write(1, 1);
                        return;
                    }
                }
            }
            catch
            {
                Red();
                NSaccessor.Write(1, 0);
                return;
            }
        }

        public static AutoResetEvent Sequence { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static void Main()
        {          
            Console.ForegroundColor = ConsoleColor.White;
            int k = 0;
            string item = "";
            Thread.Sleep(250);
            Console.WriteLine("BOOTING SYSTEM");
            Format.Dash();
            Thread.Sleep(250);
            Console.WriteLine("Commencing Initialisation:\n");
            Thread.Sleep(250);

            //creates a memory mapped file to monitor server status 
            MemoryMappedFile mmf1 = null;
            MemoryMappedFile mmf2 = null;
            try
            {
                mmf1 = MemoryMappedFile.CreateNew("SerStat", 1000);
                SSaccessor = mmf1.CreateViewAccessor();
            }
            catch
            {
                //mmf1.Dispose();
            }
            try
            {
                mmf2 = MemoryMappedFile.CreateNew("NetStat", 1000);
                NSaccessor = mmf2.CreateViewAccessor();
            }
            catch
            {
                //mmf2.Dispose();
            }

            for (int j = 0; j <= 100; j++)
            {
                Console.Write("\rMapping to Memory: {0}%", j);
                Thread.Sleep(10);
            }
            Console.WriteLine();

            //validates the existance of memory mapped files
            SSaccessor.Write(1, 1);
            SSaccessor.Read(1, out boolean1);
            NSaccessor.Write(1, 1);
            NSaccessor.Read(1, out boolean2);
            for (int j = 0; j <= 100; j++)
            {
                Console.Write("\rVerifying File Integrity: {0}%", j);
                Thread.Sleep(10);
            }
            Thread.Sleep(100);
            Console.Write("\rVerifying File Integrity: ");
            if ((boolean1 & boolean2) == true)
            {
                Green();
            }
            else
            {
                Red();
            }

            //checks available memeory
            for (int i = 0; i < 2; i++)
            {
                item = Lgen(k);
                k++;
                Console.Write("{0} Stream: ", item);
                Memory();              
                Thread.Sleep(800);
            }

            //runs a ping test to server to see if server is running.
            Console.Write("Server Connection: ");
            Ping();
            Thread.Sleep(800);

            //runs a ping test to server to see if server is running.
            Console.Write("Network Connection: ");
            CheckForInternetConnection();
            Thread.Sleep(800);

            Console.WriteLine("\nInitialisation Complete\n");
            NSaccessor.Read(1, out bool boolean);
            if (boolean == true)
            {
                Console.WriteLine("STARTING IN ONLINE MODE");
            }
            else
            {
                Console.WriteLine("STARTING IN OFFLINE MODE");
            }
            Thread.Sleep(200);
            Sequence.Set();
            Thread.Sleep(Timeout.Infinite);
            mmf1.Dispose();
            mmf2.Dispose();
        }
    }
}

 
