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

namespace enterprisedevproj.Pages
{
    public class InterestModel : PageModel
    {
        [BindProperty]
        public Interest HereInterest { get; set; }
        [BindProperty]
        public Review HereReview { get; set; }
        [BindProperty]
        public List<Review> HereReviews { get; set; }
        [BindProperty]
        public string Message1 { get; set; }
        private readonly ILogger<InterestModel> _logger;
        private readonly InterestService _svcInterest;
        private readonly ReviewService _svcReview;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly Random rnd = new Random();
        public InterestModel(ILogger<InterestModel> logger, InterestService serviceInterest, ReviewService serviceReview, SignInManager<ApplicationUser> managerSignIn, UserManager<ApplicationUser> managerUser)
        {
            _logger = logger;
            _svcInterest = serviceInterest;
            _svcReview = serviceReview;
            _signInManager = managerSignIn;
            _userManager = managerUser;
        }
        public IActionResult OnGet(string id)
        {
            
            if(id == null)
            {
                return RedirectToPage("/Errors/IDMissing");
            } else
            {
                var idType = id[0].ToString().ToUpper();
                if (idType != "I")
                {
                    return RedirectToPage("/Errors/404");
                } else
                {
                    if (!(_svcInterest.InterestExists(id)))
                    {
                        return RedirectToPage("/Errors/ItemMissing");
                    } else
                    {
                        HereInterest = _svcInterest.GetInterestById(id);
                        if (HereInterest.Approved == 0)
                        {
                            return RedirectToPage("/Errors/InterestNotApproved");
                        }
                        else
                        {
                            HereInterest.Views += 1;
                            _svcInterest.UpdateInterest(HereInterest);
                            HereReviews = _svcReview.GetAllReviewsByInterestId(HereInterest.Id);
                            // more codes here to compile rating, views and likes
                            return Page();
                        }
                        
                    }
                }
            }
            
        }
        public IActionResult OnPost(string id)
        {
            do
            {
                HereReview.Id = "R" + (rnd.Next(100000, 999999)).ToString();
            } while (_svcReview.ReviewExists(HereInterest.Id));
            HereReview.ReviewerId = _userManager.GetUserId(User);
            HereReview.ReviewDate = DateTime.Now;
            HereReview.HelpfulRate = 0;
            HereReview.Rating = 0;
            HereReview.ItemId = HereInterest.Id;
            if (!_svcReview.AddReview(HereReview))
            {
                Message1 = "Error occured while trying to post your review";
            }
            return RedirectToPage("/Interest", new { id = HereReview.ItemId });
        }
        /*
        detect change,
        if click on like
            if like is true 
                unlike and change to <i class="bi bi-hand-thumbs-up"></i>
            else
                like and change to <i class="bi bi-hand-thumbs-up-fill"></i>
        if click on dislike
            if dislike is true
                unlike and change to <i class="bi bi-hand-thumbs-down"></i>
            else
                dislike and change to <i class="bi bi-hand-thumbs-down-fill"></i>
        like = 1
        unlike = 0
        dislike -1

        */
    }
}
