namespace AuctionHouseServer
{
    public class Client
    {
            public string Name { get; set; }
            public string Ip { get; set; }

        public Client(string name, string ip)
        {
            Name = name;
            Ip = ip;
        }
    }
}