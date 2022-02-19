using System;
using System.Collections.Generic;

namespace JCsDiner
{
    public class Restaurant
    {
        public List<Table> Tables { get; set; }
        public List<Order> CurrentOrders;
        public List<Party> CurrentParties;

        public Restaurant()
        {
            CurrentOrders = new List<Order>();
            CurrentParties = new List<Party>();
            Tables = new List<Table>();
            for (int i = 0; i < 12; i++)
            {
                Tables.Add(new Table(i));
            }

        }

        public Restaurant(int numberOfTables)
        {
            CurrentOrders = new List<Order>();
            CurrentParties = new List<Party>();
            Tables = new List<Table>();
            for (int i = 0; i < numberOfTables; i++)
            {
                Tables.Add(new Table(i));
            }
        }

        public int GetCapasity()
        {
            int capasity = 0;
            foreach(Table table in Tables)
            {
                if(table.State == "clean")
                {
                    capasity += table.numOfChairs;
                }
            }
            return capasity;
        }
        public List<Table> GetFreeTables()
        {
            List<Table> freeTables = new List<Table>();
            foreach (Table table in Tables)
            {
                if (table.State == "clean")
                {
                    freeTables.Add(table);
                }
            }
            return freeTables;
        }

        public Table CombineTables(int numTablesNeeded)
        {
            Table newTable = new Table();
            List<Table> freeTables = GetFreeTables();

            if (freeTables.Count < numTablesNeeded)
            {
                throw new IndexOutOfRangeException("not enough tables");
            }
            switch (numTablesNeeded)
            {
                case 2:
                    newTable.numOfTables = 2;
                    newTable.numOfChairs = 10;
                    newTable.InsideTables = new() { freeTables[0].ID, freeTables[1].ID };
                    newTable.ID = freeTables[0].ID * 13;
                    Tables.Remove(freeTables[0]);
                    Tables.Remove(freeTables[1]);
                    break;
                case 3:
                    newTable.numOfTables = 3;
                    newTable.numOfChairs = 13;
                    newTable.InsideTables = new() { freeTables[0].ID, freeTables[1].ID , freeTables[2].ID};
                    newTable.ID = freeTables[0].ID * 13;
                    Tables.Remove(freeTables[0]);
                    Tables.Remove(freeTables[1]);
                    Tables.Remove(freeTables[2]);
                    break;
                case 4:
                    newTable.numOfTables = 4;
                    newTable.numOfChairs = 16;
                    newTable.InsideTables = new() { freeTables[0].ID, freeTables[1].ID, freeTables[2].ID, freeTables[3].ID };
                    newTable.ID = freeTables[0].ID * 13;
                    Tables.Remove(freeTables[0]);
                    Tables.Remove(freeTables[1]);
                    Tables.Remove(freeTables[2]);
                    Tables.Remove(freeTables[3]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Tables.Add(newTable);
            return newTable;
        }

        public void SeperateTables(Table combinedTable)
        {
            if (combinedTable.InsideTables is not null)
            {
                Tables.Remove(combinedTable);
                foreach (int tableID in combinedTable.InsideTables)
                {
                    Tables.Add(new Table(tableID) { State = "clean" });
                }
            }
            
        }
    }
}