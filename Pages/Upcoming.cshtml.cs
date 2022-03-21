using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace enterprisedevproj.Pages
{
    public class UpcomingModel : PageModel
    {
        // this page me be used for redirection instead
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}
