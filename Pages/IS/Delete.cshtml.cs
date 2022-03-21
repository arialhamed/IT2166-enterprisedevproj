using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using enterprisedevproj.Models;
using enterprisedevproj.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using enterprisedevproj.Models.Users;

namespace enterprisedevproj.Pages.IS
{
    public class DeleteModel : PageModel
    {
        public string TITLE = "Resurface Notification System";
        [BindProperty]
        public string DetailIndex { get; set; }
        [BindProperty]
        public string DetailForCancel { get; set; }



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

        private readonly ILogger<DeleteModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly AlertService _svcAlert;
        private readonly UserService _svcUser;
        private readonly ReviewService _svcReview;
        private readonly EventService _svcEvent;
        private readonly InterestService _svcInterest;
        private readonly NeedService _svcNeed;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteModel(ILogger<DeleteModel> logger, IEmailSender senderEmail, AlertService serviceAlert, UserService serviceUser, ReviewService serviceReview, EventService serviceEvent, InterestService serviceInterest, NeedService serviceNeed, SignInManager<ApplicationUser> managerSignIn, UserManager<ApplicationUser> managerUser)
        {
            _logger = logger;
            _emailSender = senderEmail;
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
                DetailIndex = id[0].ToString();
                switch (DetailIndex)
                {
                    case "A":
                        if (_svcAlert.AlertExists(id))
                        {
                            HereAlert = _svcAlert.GetAlertById(id);
                            DetailForCancel = "alerts";
                        }
                        break;
                    case "R":
                        if (_svcReview.ReviewExists(id))
                        {
                            HereReview = _svcReview.GetReviewById(id);
                            DetailForCancel = "reviews";
                        }
                        break;
                    case "E":
                        if (_svcEvent.EventExists(id))
                        {
                            HereEvent = _svcEvent.GetEventById(id);
                            DetailForCancel = "events";
                        }
                        break;
                    case "I":
                        if (_svcInterest.InterestExists(id))
                        {
                            HereInterest = _svcInterest.GetInterestById(id);
                            DetailForCancel = "interests";
                        }
                        break;
                    case "N":
                        if (_svcNeed.NeedExists(id))
                        {
                            HereNeed = _svcNeed.GetNeedById(id);
                            DetailForCancel = "needs";
                        }
                        break;
                    default:
                        return RedirectToPage("/Errors/IDMissing");
                }
                return Page();
            } else
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }
        public IActionResult OnPost(string id)
        {
            DetailIndex = id[0].ToString();
            //SendEmailNotification(id);
            switch (DetailIndex)
            {
                case "A":
                    HereAlert = _svcAlert.GetAlertById(id);
                    try
                    {
                        var inEmail = _svcUser.GetUserById(HereAlert.AlerterId).Email;
                        _emailSender.SendEmailAsync(
                            inEmail,
                            TITLE,
                            $"An alert that you created, under the ID {HereAlert.Id} has been resolved by the IT Staff. Thank you for your contribution!"
                        );
                    } catch (Exception ex)
                    {
                        _logger.LogInformation(ex.ToString());
                    }
                    _svcAlert.DeleteAlert(HereAlert);
                    //more functions to send notif to whoever it may concern
                    //return RedirectToPage("/Delete/Confirmed", new { id = id });
                    break;
                case "R":
                    HereReview = _svcReview.GetReviewById(id);
                    try
                    {
                        var inEmail = _svcUser.GetUserById(HereReview.ReviewerId).Email;
                        _emailSender.SendEmailAsync(
                            inEmail,
                            TITLE,
                            $"A review, under the ID {HereReview.Id}, that you have created for item ID {HereReview.ItemId}, has been removed by the IT Staff. Please reach out to us if you want your review to stay, by emailing {null}"
                        );
                    } catch (Exception ex) {
                        _logger.LogInformation(ex.ToString());
                    }
                    _svcReview.DeleteReview(HereReview);
                    //return RedirectToPage("/Delete/Confirmed", new { id = id });
                    break;
                case "E":
                    HereEvent = _svcEvent.GetEventById(id);
                    try
                    { // does not include emailing to participants
                        var inEmail = _svcUser.GetUserById(HereEvent.CreatorId).Email;
                        _emailSender.SendEmailAsync(
                            inEmail,
                            TITLE,
                            $"An event, under the ID {HereEvent.Id}, has been deleted, whether by the IT Staff or by you. Thank you for your contribution!"
                        );
                    } catch (Exception ex) {
                        _logger.LogInformation(ex.ToString());
                    }
                    _svcEvent.DeleteEvent(HereEvent);
                    //return RedirectToPage("/Delete/Confirmed", new { id = id });
                    break;
                case "I":
                    HereInterest = _svcInterest.GetInterestById(id);
                    try
                    { // does not include emailing to all reviewers, likers, just only email creator
                        var inEmail = _svcUser.GetUserById(HereInterest.CreatorId).Email;
                        _emailSender.SendEmailAsync(
                            inEmail,
                            TITLE,
                            $"An interest, under the ID {HereInterest.Id}, has been deleted by the IT Staff. If you want to keep your interest in the website, please review our Social Media Policy and contact us"
                        );
                    } catch (Exception ex) {
                        _logger.LogInformation(ex.ToString());
                    }
                    _svcInterest.DeleteInterest(HereInterest);
                    //return RedirectToPage("/Delete/Confirmed", new { id = id });
                    break;
                case "N":
                    HereNeed = _svcNeed.GetNeedById(id);
                    try
                    {
                        var inEmail = _svcUser.GetUserById(HereNeed.AssistantId).Email;
                        _emailSender.SendEmailAsync(
                            inEmail,
                            TITLE,
                            $"A need, under the ID {HereNeed.Id}, that you have been assigned to to deliver has been removed from the database. Thank you for your cooperation."
                        );
                        inEmail = _svcUser.GetUserById(HereNeed.BeneficiaryId).Email;
                        _emailSender.SendEmailAsync(
                            inEmail,
                            TITLE,
                            $"A need, under the ID {HereNeed.Id}, that you have been assigned to receiver has been removed. It may have violated our Donation Policy, or your medical history may have it required to be removed/changed"
                        );
                    } catch(Exception ex) {
                        _logger.LogInformation(ex.ToString());
                    }
                    _svcNeed.DeleteNeed(HereNeed);
                    //return RedirectToPage("/Delete/Confirmed", new { id = id });
                    break;
                default:
                    return Page();
                
            }
            return RedirectToPage("/Delete/Confirmed", new { id = id });

            //return RedirectToPage("Main");
        }
        /*private void SendEmailNotification(string id)
        {
            DetailIndex = id[0].ToString();
            switch (id)
            {
                case "A":
                    var HereAlert = 
                    var inEmail = 
            }
        }*/
    }
}
