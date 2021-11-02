using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCsDiner
{
    public class Cook
    {
        public string State { get; private set; }

        public Order CookOrder(Order order)
        {
            order.State = "cooking";
            //random cook time
            order.State = "cooked";
            //throw order ready event
            return order;
        }
    }
}

