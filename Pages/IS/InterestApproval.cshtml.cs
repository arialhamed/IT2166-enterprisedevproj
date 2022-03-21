using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using enterprisedevproj.Models;
using enterprisedevproj.Services;
using Microsoft.Extensions.Logging;
using enterprisedevproj.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace enterprisedevproj.Pages.IS
{
    public class InterestApprovalModel : PageModel
    {
        private readonly ILogger<InterestApprovalModel> _logger;
        private readonly InterestService _svc;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public Interest HereInterest { get; set; }
        public InterestApprovalModel(ILogger<InterestApprovalModel> logger, InterestService service, SignInManager<ApplicationUser> managerSignIn, UserManager<ApplicationUser> managerUser)
        {
            _logger = logger;
            _svc = service;
            _signInManager = managerSignIn;
            _userManager = managerUser;
        }
        public IActionResult OnGet(string id)
        {
            // userManager check is manual, only one it staff available
            if (_signInManager.IsSignedIn(User) && _userManager.GetUserName(User) == "admin@resurface.org.sg")
            {
                // codes to check user privilege, as is the rest of the pages in IS
                if (id == null)
                {
                    return RedirectToPage("/Errors/IDMissing");
                }
                else
                {
                    if (id[0].ToString().ToUpper() != "I")
                    {
                        return RedirectToPage("/Errors/WrongID");
                    }
                    else
                    {
                        if (_svc.InterestExists(id))
                        {
                            HereInterest = _svc.GetInterestById(id);
                            if (HereInterest.Approved == 0)
                            {
                                HereInterest.Approved = 1;
                            }
                            else
                            {
                                HereInterest.Approved = 0;
                            }
                            _svc.UpdateInterest(HereInterest);
                            return RedirectToPage("/IS/Main", new { id = "interests" });
                        }
                        else
                        {
                            return RedirectToPage("/Errors/ItemMissing");
                        }
                    }
                }
            } else
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }
    }
}
