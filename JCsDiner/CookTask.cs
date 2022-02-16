namespace JCsDiner
{
    public class CookTask { 
        public Order Order { get; set; }
        public int Time { get; set; }
        public CookTask(Order order)
        {
            Order = order;
            Time = Order.Platers * 2000 + Order.Appetizers * 1000;
        }
        public void DoTask(int id)
        {
            System.Console.WriteLine($"Cook {id} is finished cooking order for table {Order.Table}");
        }
        public void StartTask(int id)
        {
            System.Console.WriteLine($"Cook {id} is finished cooking order for table {Order.Table}");
        }
    }
}