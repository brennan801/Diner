using System;
using System.Collections.Generic;

namespace JCsDiner
{
    class Program
    {
        static void Main(string[] args)
        {
            var sim = new Simulator(20,2,3);
            sim.Run();
        }
    }
}
