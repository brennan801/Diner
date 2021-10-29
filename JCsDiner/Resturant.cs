using System.Collections.Generic;

namespace JCsDiner
{
    public class Resturant
    {
        public List<Room> Rooms = new List<Room> { new Room(6), new Room(6), new Room(6) };
        public Lobby Lobby { get; set; }

        public Resturant()
        {
        }
    }
}