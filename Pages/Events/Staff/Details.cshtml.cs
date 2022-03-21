using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using enterprisedevproj.Models;
using enterprisedevproj.Models.Users;
using enterprisedevproj.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace enterprisedevproj.Pages.Events.Staff
{
    public class DetailsModel : PageModel
    {
        [BindProperty]
        public Event HereEvent { get; set; }
        private EventService _svc;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public DetailsModel(EventService service, SignInManager<ApplicationUser> managerSignIn, UserManager<ApplicationUser> managerUser)
        {
            _svc = service;
            _signInManager = managerSignIn;
            _userManager = managerUser;
        }
        public IActionResult OnGet(string id)
        {
            if (_signInManager.IsSignedIn(User) && _userManager.GetUserName(User).Contains("eventmanager") && _userManager.GetUserName(User).Contains("@resurface.org.sg"))
            {
                if (id != null)
                {
                    HereEvent = _svc.GetEventById(id);
                    return Page();
                }
                else
                {
                    return RedirectToPage("/Errors/IDMissing");
                }
            }
            else
            {
                return RedirectToPage("/Errors/AccessDenied");
            }
        }
    }
}
