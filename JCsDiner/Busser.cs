using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCsDiner
{
    public class Busser : IRunable
    {
        public Table CurrentTable { get; set; }
        public int CurrentTableTimeLeft;
        public int FreeCounter { get; set; }

        public Busser()
        {
        }

        public Table cleanTable(Table table)
        {
            table.State = "clean";
            return table;
        }

        public void Run1(Restaurant restaurant, int beatNumber)
        {
            if(CurrentTable is null)
            {
                var dirtyTables = new List<Table>();
                foreach(Table table in restaurant.Tables)
                {
                    if (table.State == "dirty")
                    {
                        dirtyTables.Add(table);
                    }
                }
                

                if (dirtyTables.Count() > 0)
                {
                    CurrentTable = dirtyTables.First();
                    Console.WriteLine("\t\t\t\tBusser started cleaning a table");
                    CurrentTableTimeLeft = 5;
                    return;
                }
                else
                {
                    FreeCounter++;
                    return;
                }
            }
            else
            {
                CurrentTableTimeLeft--;
                if(CurrentTableTimeLeft == 0)
                {
                    if(CurrentTable.numOfTables > 1)
                    {
                        SeperateTables(CurrentTable, restaurant);
                    }
                    CurrentTable.State = "clean";
                    Console.WriteLine($"\t\t\t\tbusser cleaned table with {CurrentTable.numOfTables} tables");
                    CurrentTable = null;
                }
            }
        }

        public void SeperateTables(Table table, Restaurant restaurant)
        {
            Console.WriteLine($"\t\t\t\tbusser seperated tables");
            restaurant.SeperateTables(table);
        }

        public void PrintStats()
        {
            Console.WriteLine(
                $"Busser" +
                $"\n\tWastedTime: {FreeCounter}"
                );
        }
    }
}
