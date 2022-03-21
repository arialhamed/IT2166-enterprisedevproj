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
    public class EditModel : PageModel
    {
            [BindProperty]
        public Event HereEvent { get; set; }

        private readonly ILogger<EditModel> _logger;
        private EventService _svc;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(ILogger<EditModel> logger, EventService services, SignInManager<ApplicationUser> managerSignIn, UserManager<ApplicationUser> managerUser)
        {
            _logger = logger;
            _svc = services;
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
        public IActionResult OnPost(string id)
        {
            HereEvent.Id = id;
            HereEvent.CreatorId = _userManager.GetUserId(User);
            HereEvent.Sponsors = "Singtel"; //hardcoded
            try
            {
                _svc.UpdateEvent(HereEvent);
                if (_svc.UpdateEvent(HereEvent) == true)
                {
                    return RedirectToPage("/Events/Staff/Main");
                }
            }
            catch
            {
                return RedirectToPage("/Errors/UpdateFailed");
            }
            return Page();
        }
    }
}
