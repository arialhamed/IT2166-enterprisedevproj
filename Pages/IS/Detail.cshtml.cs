using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using enterprisedevproj.Models;
using enterprisedevproj.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using enterprisedevproj.Models.Users;

namespace enterprisedevproj.Pages.IS
{
    public class DetailModel : PageModel
    {
        [BindProperty]
        public string DetailIndex { get; set; }
        [BindProperty]
        public string ID { get; set; }
        private readonly ILogger<DetailModel> _logger;
        private readonly AlertService _svcAlert;
        private readonly ReviewService _svcReview;
        private readonly EventService _svcEvent;
        private readonly InterestService _svcInterest;
        private readonly NeedService _svcNeed;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        [BindProperty]
        public Alert HereAlert { get; set; }
        [BindProperty]
        public Review HereReview { get; set; }
        [BindProperty]
        public Event HereEvent { get; set; }
        [BindProperty]
        public Interest HereInterest { get; set; }
        [BindProperty]
        public Need HereNeed { get; set; }
        public DetailModel(ILogger<DetailModel> logger, AlertService serviceAlert, ReviewService serviceReview, EventService serviceEvent, InterestService serviceInterest, NeedService serviceNeed, SignInManager<ApplicationUser> managerSignIn, UserManager<ApplicationUser> managerUser)
        {
            _logger = logger;
            _svcAlert = serviceAlert;
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
                if (id == null)
                {
                    return RedirectToPage("/Errors/IDMissing");
                }
                else if ("arein".Contains(id[0].ToString()))
                {
                    var newid = id[0].ToString().ToUpper() + id[1..];
                    return RedirectToPage("/IS/Detail", new { id = id });
                }
                else
                {
                    ID = id;
                    switch (id[0])
                    {
                        case 'A':
                            if (_svcAlert.AlertExists(id))
                            {
                                DetailIndex = "A";
                                HereAlert = _svcAlert.GetAlertById(id);
                                //HereAlertTargetId = HereAlert.TargetId[]
                                /*switch (HereAlert.TargetId[0])
                                {
                                    case 'I':
                                        HereInterest = _svcInterest.GetInterestById(HereAlert.TargetId);
                                        break;
                                    default;
                                        break;
                                }*/
                            }
                            break;
                        case 'R':
                            if (_svcReview.ReviewExists(id))
                            {
                                DetailIndex = "R";
                                HereReview = _svcReview.GetReviewById(id);
                            }
                            break;
                        case 'E':
                            if (_svcEvent.EventExists(id))
                            {
                                DetailIndex = "E";
                                HereEvent = _svcEvent.GetEventById(id);
                            }
                            break;
                        case 'I':
                            if (_svcInterest.InterestExists(id))
                            {
                                DetailIndex = "I";
                                HereInterest = _svcInterest.GetInterestById(id);
                            }
                            break;
                        case 'N':
                            if (_svcNeed.NeedExists(id))
                            {
                                DetailIndex = "N";
                                HereNeed = _svcNeed.GetNeedById(id);
                            }
                            break;
                        default:
                            return RedirectToPage("/Errors/IDMissing");
                    }
                    return Page();
                }

            } else
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }
        
    }
}
