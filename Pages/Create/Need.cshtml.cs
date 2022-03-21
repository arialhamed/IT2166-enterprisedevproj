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
    public class NeedModel : PageModel
    {
        [BindProperty]
        public string Message1 { get; set; }
        private readonly ILogger<NeedModel> _logger;
        private readonly NeedService _svc;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Random rnd = new Random();

        public NeedModel(ILogger<NeedModel> logger, NeedService services, SignInManager<ApplicationUser> managerSignIn, UserManager<ApplicationUser> managerUser)
        {
            _logger = logger;
            _svc = services;
            _signInManager = managerSignIn;
            _userManager = managerUser;
        }
        [BindProperty]
        public Need HereNeed { get; set; }
        public IActionResult OnGet()
        {
            if (_signInManager.IsSignedIn(User))
            {
                List<Need> allneeds = new List<Need>();
                allneeds = _svc.GetAllNeeds();
                foreach (var item in allneeds)
                {
                    if (item.BeneficiaryId == _userManager.GetUserName(User))
                    {
                        return RedirectToPage("/Errors/NeedExist");
                    }
                    else
                    {
                        continue;
                    }
                }

                return Page();
            }
            else
            {
                return RedirectToPage("/Identity/Account/Login");
            }
        }

        public IActionResult OnPost()
        {
            do
            {
                HereNeed.Id = "N" + (rnd.Next(100000, 999999).ToString());
            }
            while (_svc.NeedExists(HereNeed.Id));
            HereNeed.BeneficiaryId = _userManager.GetUserName(User);
            HereNeed.AssistantId = _userManager.GetUserId(User); // rn no way to get staff assigned to specific user

            if (_svc.AddNeed(HereNeed))
            {
                return RedirectToPage("Confirmed", new { id = "n" });
            }
            else
            {
                Message1 = "Error with adding need, please try again";
                return Page();
            }
        }
    }
    
}
