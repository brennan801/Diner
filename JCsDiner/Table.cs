using System;
using System.Collections.Generic;

namespace JCsDiner
{
    public class Table
    {
        public int ID { get; set; }
        public string State { get; set; } // "clean", "occupied", "dirty"
        public int numOfChairs { get; set; }
        public int numOfTables { get; internal set; }
        public Party Party{ get; private set; }
        public List<int> InsideTables { get; set; }
        public Table()
        {
            numOfChairs = 6;
            numOfTables = 1;
            State = "clean";
        }
        public Table(int id)
        {
            numOfChairs = 6;
            numOfTables = 1;
            State = "clean";
            ID = id;
        }
        public Table(int numOfTables, int numOfChairs)
        {
            this.numOfTables = numOfTables;
            this.numOfChairs = numOfChairs;
            State = "clean";
        }
        public Table SetParty(Party party)
        {
            if(party.Customers <= this.numOfChairs)
            {
                this.Party = party;
                this.State = "occupied";

                return this;
            }
            else
            {
                throw new IndexOutOfRangeException("There isn't room for this party at this table");
            }
        }
    }
}