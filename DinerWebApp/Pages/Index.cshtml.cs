using DinerWebApp.Services;
using JCsDiner;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DinerWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IDbService dbService;

        public Simulator WebSimulator { get; set; }
        public SimulatorResults Results { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IDbService dbService)
        {
            _logger = logger;
            this.dbService = dbService;
            WebSimulator = new Simulator();
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            string name = Request.Form["name"];
            int customers = int.Parse(Request.Form["customers"]);
            int waiters = int.Parse(Request.Form["waiters"]);
            int cooks = int.Parse(Request.Form["cooks"]);
            int tables = int.Parse(Request.Form["tables"]);
            int averagePartySize = int.Parse(Request.Form["partySize"]);
            int entryTime = int.Parse(Request.Form["entryTime"]);
            int eatingTime = int.Parse(Request.Form["eatingTime"]);
            var simArgs = new SimulatorArguments()
            {
                Customers = customers,
                NumberOfWaiters = waiters,
                NumberOfCooks = cooks,
                NumberOfTables = tables,
                AveragePartySize = averagePartySize,
                AveragePartyEntryTime = entryTime,
                AverageEatingTime = eatingTime
            };
            SimulatorResults results = WebSimulator.Run(simArgs);
            Results = results;
            results.Name = name;
            dbService.addRun(results);


        }
    }
}
