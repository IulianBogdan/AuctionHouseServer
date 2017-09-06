using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionHouseServer
{
    class Item
    {

        public string Name { get; set; }
        public double Price { get; set; }
        public double Bid { get; set; }

        public override string ToString()
        {
            return string.Format("Name: {0}, Price: {1}, Bid: {2}", Name, Price, Bid);
        }
    }

}

