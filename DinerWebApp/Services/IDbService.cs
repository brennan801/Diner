using JCsDiner;
using System.Collections.Generic;

namespace DinerWebApp.Services
{
    public interface IDbService
    {
        void addRun(SimulatorResults results);
        List<SimulatorResults> GetPreviousRuns();
    }
}
