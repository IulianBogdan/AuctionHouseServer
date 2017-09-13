using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace AuctionHouseServer
{
    class ClientHandler
    {
        List<Client> subscribedClients = new List<Client>();
        public readonly AuctionHandler CurrentAuction;
        public readonly Socket client;
        public bool isRunning;

        public ClientHandler(Socket client, AuctionHandler auction)
        {
            this.client = client;
            CurrentAuction = auction;
            CurrentAuction.BroadcastEvent += CurrentAuctionEventBroadcaster;
        }

        public void CurrentAuctionEventBroadcaster(string message)
        {
            NetworkStream ns = new NetworkStream(client);
            StreamWriter sw = new StreamWriter(ns);
            sw.WriteLine(message);
            sw.Flush();
        }

        public void StartClient()
        {
            NetworkStream ns = new NetworkStream(client);
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.WriteLine("Please type in your username");
            sw.Flush();
            string clientName = sr.ReadLine();
            var clientobj = new Client(clientName, Convert.ToString(client.RemoteEndPoint));
            subscribedClients.Add(clientobj);
            sw.WriteLine("Welcome to the auction, " + clientName + " The item that is currently on the auction is : " + CurrentAuction.itemForAuction.ToString() + " Please type down the amount of money you want to bid for the item and press ENTER.");
                sw.Flush();
            isRunning = true;
            while (isRunning)
            {
                try
                {
                    //make one option for closing exiting the bid
                    double bidfromstream = Convert.ToDouble(sr.ReadLine());
                    CurrentAuction.PlaceBid(clientobj, bidfromstream);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    isRunning = false;
                }
            }
            client.Close();
            ns.Close();
            sw.Close();
            sr.Close();

        }
    }
}
