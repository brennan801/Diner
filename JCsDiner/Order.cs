namespace JCsDiner
{
    public class Order
    {
        private readonly int id;
        public int Appetizers { get; set; }
        public int Platers { get; set; }
        public Party Party { get; private set; }
        public string State { get; set; }

        public Order(Party party)
        {
            this.Party = party;
        }
    }
}