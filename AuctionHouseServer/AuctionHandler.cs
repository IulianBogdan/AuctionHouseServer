using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionHouseServer
{

    class AuctionHandler
    {
        public delegate void BroadcastAuction(string message);

        public event BroadcastAuction BroadcastEvent;
        public readonly object gravel = new object();
        public readonly object itemToLock = new object();
        public bool isRunning;
        //public int countdown;
        public Item itemForAuction = new Item{Name="Picasso Painting",Price = 10000, Bid = 0};

        public void StartAuction()
        {
            isRunning = true;
            if (BroadcastEvent != null)
            {
                BroadcastEvent("The item in the auction is: " + itemForAuction.ToString());
                while (isRunning)
                {
                    string currentBid = Convert.ToString(itemForAuction.Bid);
                    Thread.Sleep(1100);
                    
                        if (BroadcastEvent != null)
                        {
                            BroadcastEvent("Going once for: " + currentBid);

                        }
                      Thread.Sleep(800);
                        {
                            if (BroadcastEvent != null)
                            {
                                BroadcastEvent("Going twice for: " + currentBid);
                            }
                        }
                       Thread.Sleep(300);
                        {
                            if (BroadcastEvent != null)
                                BroadcastEvent("Item sold for: " + currentBid + "to the client: " );
                            isRunning = false;
                        }
                    
                }
            }
        }
        public void ResetTimer()
        {
            lock (gravel)
            {
                Thread t = new Thread(StartAuction);
                t.Start();
            }
        }

        public string PlaceBid(Client client, double clientBid)
        {
            lock (itemToLock)
            {
                if (itemForAuction.Bid < clientBid)
                {
                    BroadcastEvent(client.Name + "offered " + clientBid);
                    itemForAuction.Bid = clientBid;
                    ResetTimer();
                }
                else
                {
                    return "Your bid is too low. Please place a bid with a value higher than the current price!";
                }

            }
            return "Your bid is" + Convert.ToString(clientBid);
        }
    }

}
