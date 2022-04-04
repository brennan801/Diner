namespace JCsDiner
{
    public class CookTask { 
        public Order Order { get; set; }
        public int Time { get; set; }
        public CookTask(Order order)
        {
            Order = order;
            Time = Order.Platers * 2 + Order.Appetizers;
        }
        public void DoTask(int id)
        {
            Order.State = "waiting to be returned";
            System.Console.WriteLine($"\t\t\t Cook {id} is finished cooking order for party {Order.Table.Party.ID}");
        }
        public void StartTask(int id)
        {
            System.Console.WriteLine($"\t\t\t Cook {id} started cooking order for party {Order.Table.Party.ID}");
            Order.State = "being cooked";
        }
    }
}