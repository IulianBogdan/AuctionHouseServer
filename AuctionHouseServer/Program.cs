using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionHouseServer
{
    class Program
    {
        public static AuctionHandler auction = new AuctionHandler();
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 2001);
            Socket clientSocket;
            listener.Start();
            Console.WriteLine("Server ready!");
            Thread auctionThread = new Thread(auction.StartAuction);
            auctionThread.Start();
            while (true)
            {
                clientSocket = listener.AcceptSocket();
                Console.WriteLine("Connection incoming...");
                ClientHandler clientHandler = new ClientHandler(clientSocket,auction);
                Thread clientThread = new Thread(clientHandler.StartClient);
                clientThread.Start();
            }

        }
    }
}
