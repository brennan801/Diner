using System;
using System.Collections.Generic;

namespace JCsDiner
{
    class Program
    {
        static void Main(string[] args)
        {
            var sim = new Simulator(20,1);
            sim.Run();
        }
    }
}
