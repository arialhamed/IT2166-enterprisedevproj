using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using enterprisedevproj.Models;
using enterprisedevproj.Models.Users;
using enterprisedevproj.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace enterprisedevproj.Pages.IS
{
    // thing isn't working so
    //[Authorize(Roles = "STAFF")]
    [BindProperties]
    public class MainModel : PageModel
    {
        public List<Alert> AllAlerts { get; set; }
        public List<Alert> AllActiveAlerts { get; set; }
        public List<Alert> AllResolvedAlerts { get; set; }
        public List<ApplicationUser> AllUsers { get; set; }
        public List<Review> AllReviews { get; set; }
        public List<Event> AllEvents { get; set; }
        public List<Interest> AllInterests { get; set; }
        public List<Need> AllNeeds { get; set; }
        private readonly ILogger<MainModel> _logger;
        private readonly AlertService _svcAlert;
        private readonly UserService _svcUser;
        private readonly ReviewService _svcReview;
        private readonly EventService _svcEvent;
        private readonly InterestService _svcInterest;
        private readonly NeedService _svcNeed;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public MainModel(ILogger<MainModel> logger, AlertService serviceAlert, UserService serviceUser, ReviewService serviceReview, EventService serviceEvent, InterestService serviceInterest, NeedService serviceNeed, SignInManager<ApplicationUser> managerSignIn, UserManager<ApplicationUser> managerUser)
        {
            _logger = logger;
            _svcAlert = serviceAlert;
            _svcUser = serviceUser;
            _svcReview = serviceReview;
            _svcEvent = serviceEvent;
            _svcInterest = serviceInterest;
            _svcNeed = serviceNeed;
            _signInManager = managerSignIn;
            _userManager = managerUser;
        }
        public IActionResult OnGet(string id)
        {
            // userManager check is manual, only one it staff available
            if (_signInManager.IsSignedIn(User) && _userManager.GetUserName(User) == "admin@resurface.org.sg")
            {
                switch (id)
                {
                    case null:
                        // default redirect to first of the backend nav
                        return RedirectToPage("Main", new { id = "alerts" });
                    case "alerts":
                        AllAlerts = _svcAlert.GetAllAlerts();
                        AllActiveAlerts = _svcAlert.GetActiveAlerts();
                        AllResolvedAlerts = _svcAlert.GetResolvedAlerts();
                        break;
                    case "users":
                        AllUsers = _svcUser.GetAllUsers();
                        break;
                    case "reviews":
                        AllReviews = _svcReview.GetAllReviews();
                        break;
                    case "events":
                        AllEvents = _svcEvent.GetAllEvents();
                        break;
                    case "interests":
                        AllInterests = _svcInterest.GetAllInterests();
                        break;
                    case "needs":
                        AllNeeds = _svcNeed.GetAllNeeds();
                        break;
                    default:
                        break;

                }
                return Page();
            } else
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }
    }

}
