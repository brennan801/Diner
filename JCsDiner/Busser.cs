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
        public Room CurrentRoom { get; set; }
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

        public (Party, Table) CombineTablesForParty(Party party, Room room)
        {
            Console.WriteLine($"\t\t\t\tbusser combined tables");
            if (party.Customers.Count() < 11)
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

        public void Run1(Restaurant restaurant, int beatNumber)
        {
            if(CurrentTable is null)
            {
                var dirtyTables = new List<(Table, Room)>();
                foreach(Room room in restaurant.Rooms)
                {
                    foreach(Table table in room.Tables)
                    {
                        if (table.State == "dirty")
                        {
                            dirtyTables.Add((table, room));
                        }
                    }
                }

                if (dirtyTables.Count() > 0)
                {
                    (CurrentTable, CurrentRoom) = dirtyTables.First();
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
                        SeperateTables(CurrentRoom, CurrentTable);
                    }
                    CurrentTable.State = "clean";
                    Console.WriteLine($"\t\t\t\tbusser cleaned table with {CurrentTable.numOfTables} tables");
                    CurrentTable = null;
                }
            }
        }

        public void SeperateTables(Room room, Table table)
        {
            Console.WriteLine($"\t\t\t\tbusser seperated tables");
            room.SeperateTables(table);
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
