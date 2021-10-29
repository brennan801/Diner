using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCsDiner
{
    public class Busser
    {
        public string State { get; set; }

        public Table cleanTable(Table table)
        {
            //TODO:
            return null;
        }

        public Table CombineTables(List<Table> tables)
        {
            Table newTable = new Table();
            switch (tables.Count())
            {
                case 2:
                    newTable.numOfTables = 2;
                    newTable.numOfChairs = 10;
                    break;
                case 3:
                    newTable.numOfTables = 3;
                    newTable.numOfChairs = 13;
                    break;
                case 4:
                    newTable.numOfTables = 4;
                    newTable.numOfChairs = 16;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return newTable;
        }

        public List<Table> SeperateTables(Table combinedTable)
        {
            List<Table> tables = new List<Table>();
            for(int i = 0; i < combinedTable.numOfTables; i++)
            {
                tables.Add(new Table(1, 6));
            }
            return tables;
        }
    }
}
