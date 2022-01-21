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
        public Simulator Simulator { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            Simulator = new Simulator();
        }

        public void OnGet()
        {

        }
    }
}
