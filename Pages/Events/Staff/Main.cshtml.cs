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
    public class MainModel : PageModel
    {
                [BindProperty]
        public List<Event> allevents { get; set; }

        private readonly ILogger<MainModel> _logger;
        private EventService _svc;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public MainModel(ILogger<MainModel> logger, EventService service, SignInManager<ApplicationUser> managerSignIn, UserManager<ApplicationUser> managerUser)
        {
            _logger = logger;
            _svc = service;
            _signInManager = managerSignIn;
            _userManager = managerUser;

        }

        public IActionResult OnGet()
        {
            if (_signInManager.IsSignedIn(User) && _userManager.GetUserName(User).Contains("eventmanager") && _userManager.GetUserName(User).Contains("@resurface.org.sg"))
            {
                allevents = _svc.GetAllEvents();
                return Page();
            }
            else
            {
                return RedirectToPage("/Errors/AccessDenied");
            }
        }
    }
}
