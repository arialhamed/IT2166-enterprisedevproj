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
using Microsoft.Extensions.Logging;

namespace enterprisedevproj.Pages.Events.Staff
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Event HereEvent { get; set; }

        private readonly ILogger<DeleteModel> _logger;
        private readonly EventService _svc;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteModel(ILogger<DeleteModel> logger, EventService service, SignInManager<ApplicationUser> managerSignIn, UserManager<ApplicationUser> managerUser)
        {
            _logger = logger;
            _svc = service;
            _signInManager = managerSignIn;
            _userManager = managerUser;
        }

        public IActionResult OnGet(string id)
        {
            if (_signInManager.IsSignedIn(User) && _userManager.GetUserName(User).Contains("eventmanager") && _userManager.GetUserName(User).Contains("@resurface.org.sg"))
            {
                if (_svc.EventExists(id))
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

        public IActionResult OnPost(string id)
        {
            _svc.DeleteEvent(HereEvent);
            return RedirectToPage("/Delete/Confirmed", new { id = "e" });
        }
    }
}
