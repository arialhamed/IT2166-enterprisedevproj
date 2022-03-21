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

namespace enterprisedevproj.Pages.Needs.Staff
{
    public class DetailsModel : PageModel
    {
        [BindProperty]
        public Need HereNeed { get; set; }
        private NeedService _svc;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public DetailsModel(NeedService service, SignInManager<ApplicationUser> managerSignIn, UserManager<ApplicationUser> managerUser)
        {
            _svc = service;
            _signInManager = managerSignIn;
            _userManager = managerUser;
        }

        public IActionResult OnGet(string id)
        {
            if (_signInManager.IsSignedIn(User) && _userManager.GetUserName(User) == "needsmanager@resurface.org.sg")
            {
                if (id != null)
                {
                    HereNeed = _svc.GetNeedById(id);
                    return Page();
                }
                else
                {
                    return RedirectToPage("/Errors/IDMissing");
                }
            }
            else
            {
                return RedirectToPage("/Errors/AccessDenied");
            }
        }
    }
}
