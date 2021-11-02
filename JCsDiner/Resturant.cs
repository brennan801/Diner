using System.Collections.Generic;

namespace JCsDiner
{
    public class Resturant
    {
        public List<Room> Rooms = new List<Room> { new Room(4), new Room(4), new Room(4) };
        public Lobby Lobby { get; set; }

        public Resturant()
        {
        }
    }
}