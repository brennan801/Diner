namespace JCsDiner
{
    public class HostTask
    {
        public Party Party { get; set; }
        public int Time { get; set; }
        public HostTask(Party party)
        {
            Party = party;
            Time = 2000;
        }
        public void DoTask()
        {
            System.Console.WriteLine($"Host seated party {Party.ID}");
        }
        public void StartTask()
        {
            System.Console.WriteLine($"Host is seating party {Party.ID}");
        }
    }
}