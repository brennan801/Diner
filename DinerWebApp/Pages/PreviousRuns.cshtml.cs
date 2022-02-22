using DinerWebApp.Services;
using JCsDiner;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace DinerWebApp.Pages
{
    public class PreviousRunsModel : PageModel
    {
        private readonly IDbService dbSevice;
        public List<SimulatorResults> PrevRuns { get; set; }

        public PreviousRunsModel(IDbService dbSevice)
        {
            this.dbSevice = dbSevice;
        }
        public void OnGet()
        {
            PrevRuns = dbSevice.GetPreviousRuns();
        }
    }
}
