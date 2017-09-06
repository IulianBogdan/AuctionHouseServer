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
        public int Price { get; set; }

        public int GetPrice()
        {
            return Price;
        }

        public bool UpdatePrice(int newP)
        {
            bool priceChanged = false;
            if (newP > Price)
            {
                Price = newP;
                priceChanged = true;
            }
            return priceChanged;
        }
    }

}

