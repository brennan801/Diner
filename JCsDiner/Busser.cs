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

        public Busser()
        {
        }

        public Table cleanTable(Table table)
        {
            table.State = "clean";
            return table;
        }

        public (Party, Table) CombineTablesForParty(Party party, Room room)
        {
            if(party.Customers.Count() < 11)
            {
                Table myTable = room.CombineTables(2);
                myTable.SetParty(party);
                party.State = new PartyWaitingToOrder(party);
                return (party, myTable);
            }
            if (party.Customers.Count() < 14)
            {
                Table myTable = room.CombineTables(3);
                myTable.SetParty(party);
                party.State = new PartyWaitingToOrder(party);
                return (party, myTable);
            }
            else
            {
                Table myTable = room.CombineTables(4);
                myTable.SetParty(party);
                party.State = new PartyWaitingToOrder(party);
                return (party, myTable);
            }
        }

        public void Run1(Restaurant restaurant)
        {
            if(CurrentTable is null)
            {
                var dirtyTables = new List<Table>();
                foreach(Room room in restaurant.Rooms)
                {
                    foreach(Table table in room.Tables)
                    {
                        if (table.State == "dirty")
                        {
                            dirtyTables.Add(table);
                        }
                    }
                }

                if (dirtyTables.Count() > 0)
                {
                    CurrentTable = dirtyTables.First();
                    Console.WriteLine("\t\t\t\tBusser started cleaning a table");
                    CurrentTableTimeLeft = 5;
                    return;
                }
                else return;
            }
            else
            {
                CurrentTableTimeLeft--;
                if(CurrentTableTimeLeft == 0)
                {
                    CurrentTable.State = "clean";
                    Console.WriteLine($"\t\t\t\tbusser cleaned table");
                    CurrentTable = null;
                }
            }
        }

        public List<Table> SeperateTables(Room room, Table table)
        {
            return room.SeperateTables(table);
        }
    }
}
