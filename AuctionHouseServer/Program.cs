using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace AuctionHouseServer
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 20001);
            TcpClient client;
            listener.Start();
            Console.WriteLine("Server ready!");
            try
            {
                while (true)
                {
                    client = listener.AcceptTcpClient();
                    Console.WriteLine($">> Client has connected!");
                    ClientHandler cHandler = new ClientHandler();
                    cHandler.StartClient(client);
                }
                
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
                listener.Stop();
                Console.WriteLine(">> Exit");
                Console.ReadLine();
        }
    }
}
