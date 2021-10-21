namespace JCsDiner
{
    public class Table
    {
        private readonly bool isBooth;
        public string State { get; set; }
        public int numOfTabes { get; set; }
        public int numOfChairs { get; internal set; }
        public Party party { get; set; }
    }
}