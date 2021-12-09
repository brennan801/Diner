using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCsDiner
{
    public class Cook : IRunable
    {
        public Order Order { get; set; }
        public int FreeTimeCount { get; private set; }
        public int TimeLeftToCook { get; private set; }
        public void Run1(Restaurant restaurant)
        {
            var orderquery =
                    from order in restaurant.CurrentOrders
                    where order.State == "ToBeCooked"
                    orderby order.WaitCounter
                    select order;
            foreach(Order order in orderquery)
            {
                order.WaitCounter++;
            }
            if (Order == null || Order.State != "BeingCooked")
            {
                if (orderquery.Count() > 0)
                {
                    Order = orderquery.First();
                    Order.State = "BeingCooked";
                    TimeLeftToCook = getTimeToCook(Order);
                    Console.WriteLine($"\t\t\tCook started cooking new order with {Order.Platers} platers and {Order.Appetizers} appetizers");
                    return;
                }
                else FreeTimeCount++;
            }
            else
            {
                if (TimeLeftToCook > 0)
                {
                    TimeLeftToCook--;
                }
                else
                {
                    Order.State = "ToBeReturned";
                    Console.WriteLine("\t\t\tcook finished an order");
                }
            }
        }

        private int getTimeToCook(Order order)
        {
            return 2 * order.Appetizers + 3 * order.Platers;
        }
    }
}

