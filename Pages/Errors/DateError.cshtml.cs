using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace enterprisedevproj.Pages.Errors
{
    public class DateErrorModel : PageModel
    {
        [BindProperty]
        public string ConfirmIndex { get; set; }
        public IActionResult OnGet(string id)
        {
            ConfirmIndex = id;
            if (ConfirmIndex == null)
            {
                return RedirectToPage("/Errors/UpdateFailed");
            }
            return Page();
        }
    }
}
