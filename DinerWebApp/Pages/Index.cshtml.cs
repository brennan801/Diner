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
        public Simulator WebSimulator { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            WebSimulator = new Simulator();
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            int customers = int.Parse(Request.Form["customers"].ToString());
            int waiters = int.Parse(Request.Form["waiters"].ToString());
            int cooks = int.Parse(Request.Form["cooks"].ToString());
            int averagePartySize = int.Parse(Request.Form["partySize"].ToString());
            var simArgs = new SimulatorArguments()
            {
                Customers = customers,
                NumberOfWaiters = waiters,
                NumberOfCooks = cooks,
                AveragePartySize = averagePartySize
            };
            WebSimulator.Run(simArgs);
        }
    }
}
