namespace DinerBlazorServer.Data
{
    public class DrawnTable
    {
        public int ID { get; private set; }
        public int LeftPos { get; private set; }
        public int RightPos { get; private set; }
        public int TopPos { get; private set; }
        public List<(int x, int y)> SeatPositions { get; private set; }

        public DrawnTable(int id, int leftPos, int rightPos, int topPos, int size)
        {
            ID = id;
            LeftPos = leftPos;
            RightPos = rightPos;
            TopPos = topPos;
            SeatPositions = new();
            AddSeatPositions(size);
        }

        private void AddSeatPositions(int tableSize)
        {
            switch (tableSize)
            {
                case 1:
                    AddSeatPositionsFor1Table();
                    break;
                case 2: AddSeatPositionsFor2Table();
                    break;
                case 3: AddSeatPositionsFor3Table();
                    break;
                default: AddSeatPositionsFor1Table();
                    break;
            }
        }

        private void AddSeatPositionsFor3Table()
        {
            throw new NotImplementedException();
        }

        private void AddSeatPositionsFor2Table()
        {
            SeatPositions = new List<(int x, int y)>();
            SeatPositions.Add((LeftPos - 2, TopPos));
            SeatPositions.Add((RightPos, TopPos));
            SeatPositions.Add((LeftPos - 2, TopPos + 9));
            SeatPositions.Add((RightPos, TopPos + 9));
            SeatPositions.Add((LeftPos - 2, TopPos + 18));
            SeatPositions.Add((RightPos, TopPos + 18));
            SeatPositions.Add((LeftPos + 4, TopPos - 2));
            SeatPositions.Add((RightPos - 4, TopPos - 2));
            SeatPositions.Add((LeftPos + 4, TopPos + 20));
            SeatPositions.Add((RightPos - 4, TopPos + 20));
        }

        private void AddSeatPositionsFor1Table()
        {
            SeatPositions = new List<(int x, int y)>();
            SeatPositions.Add((LeftPos - 2, TopPos));
            SeatPositions.Add((RightPos, TopPos));
            SeatPositions.Add((LeftPos - 2, TopPos + 9));
            SeatPositions.Add((RightPos, TopPos + 9));
            SeatPositions.Add((LeftPos - 2, TopPos + 18));
            SeatPositions.Add((RightPos, TopPos + 18));
        }
    }
}
