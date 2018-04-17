using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PassCypher
{
    internal class Filereceiver
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static void SendFile()
        {
            Socket clientSock_client = null;
            try
            {
                string fileName = SessionParameters.Keypath;
                byte[] fileData = File.ReadAllBytes(fileName);
                byte[] clientData = new byte[fileData.Length];
                byte[] fileNameByte = Encoding.UTF8.GetBytes(fileName);
                if (fileNameByte.Length > 5000 * 1024)
                {
                    Console.WriteLine("File size is more than 5Mb, please try with small file.");
                    return;
                }

                IPEndPoint ipEnd_client = new IPEndPoint(IPAddress.Parse("192.168.1.109"), 12345);
                clientSock_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

                Console.WriteLine("Buffering ...");
                fileData.CopyTo(clientData, 0);

                Console.WriteLine("Connection to server...");
                clientSock_client.Connect(ipEnd_client);

                Console.WriteLine("File sending...");
                clientSock_client.Send(clientData, 0, clientData.Length, 0);

                Console.WriteLine("File [" + fileName + "] transferred...");
            }
            catch (Exception e)
            {
                if (e.Message == "No connection could be made because the target machine actively refused it")
                    Console.WriteLine("File Sending fail. Because server not running.");
                else
                    Console.WriteLine("File Sending fail. " + e.Message);
            }
            finally
            {
                Console.WriteLine("Disconnecting...");
                clientSock_client.Close();
            }
           return;
        }

        public static void RecFile()
        {
            Socket clientSock_client = null;
            try
            {
                IPEndPoint ipEnd_client = new IPEndPoint(IPAddress.Parse("192.168.1.109"), 12345);
                clientSock_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                byte[] flag = Encoding.UTF8.GetBytes("!!!");
                byte[] recdata = new byte[1024];

                Console.WriteLine("Connection to server...");
                clientSock_client.Connect(ipEnd_client);

                Console.WriteLine("Sending...");
                clientSock_client.Send(flag, 0, flag.Length, 0);
                
                Console.WriteLine("Waiting for server...");
                int k;
                string data = "";
                clientSock_client.ReceiveTimeout = 10000;
                do
                {
                    k = clientSock_client.Receive(recdata);
                    data += Encoding.ASCII.GetString(recdata, 0, k);
                } while (k > 0);

                Console.WriteLine("Received..."); 
                Console.WriteLine(data);  
                
                Console.WriteLine("Disconnecting...");
            }
            catch (Exception e)
            {
                if (e.Message == "No connection could be made because the target machine actively refused it")
                    Console.WriteLine("File Receiving fail. Because server not running.");
                else
                    Console.WriteLine("File Receiving fail. " + e.Message);
            }
            finally
            {
                clientSock_client.Close();
            }
            return;
        }
    }
}
