using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCsDiner
{
    public class BusserTask
    {
        public Table Table { get; set; }
        public BusserTask(Table table)
        {
            Table = table;
        }
        public void DoTask()
        {
            Table.State = "clean";
            Console.WriteLine("Table Cleaned");
        }
    }
}
