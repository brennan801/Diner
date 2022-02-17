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
        public Restaurant Restaurant { get; set; }
        public BusserTask(Table table, Restaurant restaurant)
        {
            Restaurant = restaurant;
            Table = table;
        }
        public void DoTask()
        {
            Restaurant.SeperateTables(Table);
            Table.State = "clean";
            Console.WriteLine($"\t\t\t\t Busser has cleaned table {Table.ID}");
        }
        public void StartTask()
        {
            Console.WriteLine($"\t\t\t\t Busser is cleaning table {Table.ID}");
        }
    }
}
