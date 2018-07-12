/*                                                          *
 * Adam Rushby - Dagon Interactive Media - PassCypher 2018  *
 *                                                          */

using System;
using System.IO.MemoryMappedFiles;
using System.Net;
using System.Net.Sockets;

namespace PassCypher
{
    class NetTest
    {
        private static int Left { get; set; }
        private static int Top { get; set; }     
        private static bool ServerShiftReg1 { get; set; }
        private static bool ServerShiftReg2 { get; set; }
        public static void Main()
        {           
            ServerShiftReg1 = Ping();
            while (true)
            {
                ServerShiftReg2 = ServerShiftReg1;
                ServerShiftReg1 = Ping();
                if (ServerShiftReg1 != ServerShiftReg2)
                {
                    NetUpdate();
                }
                System.Threading.Thread.Sleep(5000);
            }
        }

        public static void NetUpdate()
        {
            Left = Console.CursorLeft; // gets position from left of window, 0 index
            Top = Console.CursorTop; // gets position from the top of screen down, 0 index                    
            Console.SetCursorPosition(0, 0);
            Console.Write("server status: ");
            if (ServerShiftReg1 == true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("online    ");
                AccessWrite(1);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("offline    ");
                AccessWrite(0);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(Left, Top);
        }

        private static Boolean Ping()
        {
            // Sends test data to the file server.
            // If data is recieved then the test has passed the ping test.
            // If no connection can be made then an exception occurs and is caught by the program.
            Socket clientSock_client = null;
            bool b = false;
            try
            {
                IPEndPoint ipEnd_client = new IPEndPoint(IPAddress.Parse("192.168.1.109"), 12345);
                clientSock_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                clientSock_client.Connect(ipEnd_client);
                b = true;
            }
            // Boolean return values used to indicate success  
            catch
            {
                //clientSock_client.Close();
                b = false;
            }
            finally
            {
                clientSock_client.Close();
            }
            return b;
        }

        private static void AccessWrite(int i)
        {
            using (MemoryMappedFile mmf = MemoryMappedFile.OpenExisting("SerStat"))
            {
                using (MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor())
                {
                    accessor.Write(1, i);
                }
            }
        }
    }
}
