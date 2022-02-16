using System;
using System.Collections.Generic;

namespace JCsDiner
{
    class Program
    {
        static void Main(string[] args)
        {
            var sim = new Simulator(1,1,1);
            sim.Run();
        }
    }
}
