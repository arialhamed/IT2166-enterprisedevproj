using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using enterprisedevproj.Models;
using enterprisedevproj.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using enterprisedevproj.Models.Users;

namespace enterprisedevproj.Pages.Create
{
    public class EventModel : PageModel
    {
        [BindProperty]
        public string Message1 { get; set; }
        private readonly ILogger<EventModel> _logger;
        private readonly EventService _svc;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Random rnd = new Random();

        public EventModel(ILogger<EventModel> logger, EventService services, SignInManager<ApplicationUser> managerSignIn, UserManager<ApplicationUser> managerUser)
        {
            _logger = logger;
            _svc = services;
            _signInManager = managerSignIn;
            _userManager = managerUser;
        }
        [BindProperty]
        public Event HereEvent { get; set; }
        public IActionResult OnGet()
        {
            if (_signInManager.IsSignedIn(User) && _userManager.GetUserName(User).Contains("eventmanager") && _userManager.GetUserName(User).Contains("@resurface.org.sg"))
            {
                return Page();
            } else
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }
        // WES ATTENTION: need codes here to update to HereEvent
        public IActionResult OnPost()
        {
            do
            {
                HereEvent.Id = "E" + (rnd.Next(100000, 999999).ToString());
            }
            while (_svc.EventExists(HereEvent.Id));
            HereEvent.CreatorId = _userManager.GetUserId(User);
            HereEvent.Sponsors = "Singtel"; //hardcoded
            
            if (HereEvent.StartTime > DateTime.Today.Date)
            {
                if (HereEvent.EndTime >= HereEvent.StartTime)
                {
                    if (_svc.AddEvent(HereEvent))
                    {
                        return RedirectToPage("Confirmed", new { id = "e" });
                    }
                    else
                    {
                        Message1 = "Error with adding event, please try again";
                        return Page();
                    }
                }
                else
                {
                    return RedirectToPage("/Errors/DateError", new { id = "end" });
                }
            }
            else
            {
                return RedirectToPage("/Errors/DateError", new { id = "start" });
            }

        }
    }
}
