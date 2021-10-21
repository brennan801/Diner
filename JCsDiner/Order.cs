namespace JCsDiner
{
    public class Order
    {
        private readonly int id;
        private Party party;
        private string state;
        public int Appetizers { get; set; }
        public int Platers { get; set; }
        public Party Party { get { return party; } }
        public string State { get { return state; } }

        public Order(Party party)
        {
            this.party = party;
        }
    }
}