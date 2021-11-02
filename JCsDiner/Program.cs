using System;

namespace JCsDiner
{
    class Program
    {
        static void Main(string[] args)
        {
            Resturant resturant = new();
            Lobby lobby = new(50);
            resturant.Lobby = lobby;
            Waiter waiter0 = new(resturant.Rooms[0]);
            Waiter waiter1 = new(resturant.Rooms[1]);
            Waiter waiter2 = new(resturant.Rooms[2]);
            Busser busser = new();
        }
    }
}
