using System;
using System.Collections.Generic;

namespace JCsDiner
{
    class Program
    {
        static void Main(string[] args)
        {
            var sim = new Simulator();
            var simArgs = new SimulatorArguments()
            {
                Customers = 10,
                NumberOfWaiters = 2,
                NumberOfCooks = 1,
                AveragePartySize = 3,
                AveragePartyEntryTime = 3,
                NumberOfTables = 12
            };
            sim.Run(simArgs);
        }
    }
}
