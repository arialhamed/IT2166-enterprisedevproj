using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace enterprisedevproj.Pages.Create
{
    public class ConfirmedModel : PageModel
    {
        [BindProperty]
        public string DetailIndex { get; set; }
        public IActionResult OnGet(string id)
        {
            DetailIndex = id;
            if(DetailIndex == null)
            {
                return RedirectToPage("/Errors/IDMissing");
            }
            return Page();
        }
    }
}
