using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionHouseServer
{
    class ClientHandler
    {
        Dictionary<string, string> participants = new Dictionary<string, string>();

        private TcpClient _client;
        public void StartClient(TcpClient client)
        {
            _client = client;
            Thread clientThread = new Thread(Communication);
            clientThread.Start();
        }
        public void Communication()
        {
            bool isConn = true;
            NetworkStream ns = _client.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.WriteLine("Please insert your name...");
            sw.Flush();
            string message;
            try
            {

                while (isConn)
                {
                    message = sr.ReadLine();
                    if (message != null)
                    {
                        participants.Add(message, ((IPEndPoint) _client.Client.RemoteEndPoint).Address.ToString());
                        sw.WriteLine("Name valid");
                        sw.Flush();
                        foreach (var x in participants)
                        {
                            Console.WriteLine(x.ToString());
                        }
                    }
                    else
                    {
                        sw.WriteLine("Please enter a valid name...");
                        sw.Flush();
                    }

                }
            }
            catch (Exception)
            {
                Console.WriteLine("problem");
            }
            ns.Close();
            sr.Close();
            sw.Close();
        }
    }
}
