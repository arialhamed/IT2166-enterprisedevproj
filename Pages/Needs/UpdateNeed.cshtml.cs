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

namespace enterprisedevproj.Pages.Needs
{
    public class UpdateNeedModel : PageModel
    {
        [BindProperty]
        public Need HereNeed { get; set; }

        private readonly ILogger<UpdateNeedModel> _logger;
        private NeedService _svc;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateNeedModel(ILogger<UpdateNeedModel> logger, NeedService services, SignInManager<ApplicationUser> managerSignIn, UserManager<ApplicationUser> managerUser)
        {
            _logger = logger;
            _svc = services;
            _signInManager = managerSignIn;
            _userManager = managerUser;
        }
        public IActionResult OnGet(string id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                HereNeed = _svc.GetNeedById(id);
                return Page();
            }
            else
            {
                return RedirectToPage("/Identity/Account/Login");
            }
        }

        public IActionResult OnPost(string id)
        {
            HereNeed.Id = id;
            HereNeed.BeneficiaryId = _userManager.GetUserName(User);
            HereNeed.AssistantId = _userManager.GetUserId(User);
            try
            {
                _svc.UpdateNeed(HereNeed);
                if (_svc.UpdateNeed(HereNeed) == true)
                {
                    return RedirectToPage("/Needs/UpdateConfirm", new { id = "success" });
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
