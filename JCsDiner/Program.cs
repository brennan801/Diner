using System;
using System.Collections.Generic;

namespace JCsDiner
{
    class Program
    {
        static void Main(string[] args)
        {
            var sim = new Simulator(10,2,2);
            sim.Run();
        }
    }
}
