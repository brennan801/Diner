namespace JCsDiner
{
    public class Order
    {
        public int Appetizers { get; set; }
        public int Platers { get; set; }
        public Table Table { get; set; }
        public string State { get; set; } //waitingToBeCooked, beingCooked, waitingoBeReturned, beingReturned, beingEaten
        public int WaitCounter { get; set; }

        public Order(Table table)
        {
            this.Table = table;
            WaitCounter = 0;
        }
        public Order()
        {
            WaitCounter = 0;
        }
    }
}