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
                    Tables.Remove(freeTables[0]);
                    Tables.Remove(freeTables[1]);
                    break;
                case 3:
                    newTable.numOfTables = 3;
                    newTable.numOfChairs = 13;
                    Tables.Remove(freeTables[0]);
                    Tables.Remove(freeTables[1]);
                    Tables.Remove(freeTables[2]);
                    break;
                case 4:
                    newTable.numOfTables = 4;
                    newTable.numOfChairs = 16;
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
            Tables.Remove(combinedTable);
            for (int i = 0; i < combinedTable.numOfTables; i++)
            {
                Tables.Add(new Table { State = "dirty" });
            }
        }
    }
}