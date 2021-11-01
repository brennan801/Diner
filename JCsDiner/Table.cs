using System;
using System.Collections.Generic;

namespace JCsDiner
{
    public class Table
    {
        private readonly bool isBooth;
        public string State { get; set; }
        public int numOfChairs { get; set; }
        public int numOfTables { get; internal set; }
        public Party Party{ get; private set; }
        public bool isOccupied { get; set; }
        public Table()
        {
            numOfChairs = 6;
            numOfTables = 1;
        }
        public Table(int numOfTables, int numOfChairs)
        {
            this.numOfTables = numOfTables;
            this.numOfChairs = numOfChairs;
            isOccupied = false;
        }
        public Table SetParty(Party party)
        {
            if(party.Customers.Count <= this.numOfChairs)
            {
                this.Party = party;
                this.isOccupied = true;

                return this;
            }
            else
            {
                throw new IndexOutOfRangeException("There isn't room for this party at this table");
            }
        }
    }
}