using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCsDiner
{
    public class Busser : IRunable
    {
        public string State { get; set; }

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
                party.State = new WaitingToOrder(party);
                return (party, myTable);
            }
            if (party.Customers.Count() < 14)
            {
                Table myTable = room.CombineTables(3);
                myTable.SetParty(party);
                party.State = new WaitingToOrder(party);
                return (party, myTable);
            }
            else
            {
                Table myTable = room.CombineTables(4);
                myTable.SetParty(party);
                party.State = new WaitingToOrder(party);
                return (party, myTable);
            }
        }

        public void Run1()
        {
            throw new NotImplementedException();
        }

        public List<Table> SeperateTables(Room room, Table table)
        {
            return room.SeperateTables(table);
        }
    }
}
