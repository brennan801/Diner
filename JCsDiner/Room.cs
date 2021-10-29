using System.Collections.Generic;

namespace JCsDiner
{
    public class Room
    {
        public List<Table> Tables { get; internal set; }
        public Room(int numberOfTables)
        {
            Tables = new List<Table>();
            for (int i = 0; i < numberOfTables; i++)
            {
                Tables.Add(new Table());
            }
        }
        public int GetCapasity()
        {
            int openTables = 0;
            foreach (Table table in Tables)
            {
                if (!table.isOccupied)
                {
                    openTables++;
                }
            }
            return openTables;
        }

    }
}