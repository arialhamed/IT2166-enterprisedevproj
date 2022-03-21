using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using enterprisedevproj.Models;
using enterprisedevproj.Models.Users;
using enterprisedevproj.Services;

namespace enterprisedevproj.Pages
{
    [BindProperties]
    public class SearchModel : PageModel
    {
        
        public List<Alert> HereAlerts { get; set; }
        public List<ApplicationUser> HereUsers { get; set; }
        public List<Review> HereReviews { get; set; }
        public List<Event> HereEvent { get; set; }
        public List<Interest> HereInterest { get; set; }
        public List<Need> HereNeeds { get; set; }

        private readonly ILogger<SearchModel> _logger;
        private readonly AlertService _svcAlert;
        private readonly UserService _svcUser;
        private readonly ReviewService _svcReview;
        private readonly EventService _svcEvent;
        private readonly InterestService _svcInterest;
        private readonly NeedService _svcNeed;

        public SearchModel(ILogger<SearchModel> logger, AlertService alertService, UserService userService, ReviewService reviewService, EventService eventService, InterestService interestService, NeedService needService)
        {
            _logger = logger;
            _svcAlert = alertService;
            _svcUser = userService;
            _svcReview = reviewService;
            _svcEvent = eventService;
            _svcInterest = interestService;
            _svcNeed = needService;
        }
        public void OnGet()
        {

        }
        public void search(string inValue)
        {

        }
    }
}
