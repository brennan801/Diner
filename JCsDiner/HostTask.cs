using System.Collections.Generic;

namespace JCsDiner
{
    public class HostTask
    {
        public Party Party { get; set; }
        public int Time { get; set; }
        public Table Table { get; set; }
        public Restaurant Restaurant { get; set; }
        public HostTask(Party party, Restaurant restaurant)
        {
            Party = party;
            Time = 2000;
            Restaurant = restaurant;
        }
        public void DoTask()
        {
            Party.State = new PartyDecidingOrder(Party);
            System.Console.WriteLine($"Host seated party {Party.ID} to table {Table.ID}");
        }
        public void StartTask()
        {
            System.Console.WriteLine($"Host is seating party {Party.ID} with size {Party.Customers}");
            Table = getNumTablesNeeded();
            Table.SetParty(Party);
            Party.Table = Table;
        }

        public Table getNumTablesNeeded()
        {
            var customersInParty = Party.Customers;
            if(customersInParty < 7)
            {
                return Restaurant.GetFreeTables()[0];
            }
            if(customersInParty < 11)
            {
                return Restaurant.CombineTables(2);
            }
            if (customersInParty < 14)
            {
                return Restaurant.CombineTables(3);
            }
            else return Restaurant.CombineTables(4);
        }
    }
}