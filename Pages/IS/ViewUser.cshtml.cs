using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using enterprisedevproj.Models.Users;
using enterprisedevproj.Models;
using enterprisedevproj.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;

namespace enterprisedevproj.Pages.IS
{
    public class ViewUserModel : PageModel
    {
        public string TITLE = "Resurface Notification System";

        [BindProperty]
        public ApplicationUser HereUser { get; set; }
        private readonly ILogger<ViewUserModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly UserService _svcUser;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public ViewUserModel(ILogger<ViewUserModel> logger, IEmailSender senderEmail, UserService serviceUser, SignInManager<ApplicationUser> managerSignIn, UserManager<ApplicationUser> managerUser)
        {
            _logger = logger;
            _emailSender = senderEmail;
            _svcUser = serviceUser;
            _signInManager = managerSignIn;
            _userManager = managerUser;
        }
        public IActionResult OnGet(string id)
        {
            // userManager check is manual, only one it staff available
            if (_signInManager.IsSignedIn(User) && _userManager.GetUserName(User) == "admin@resurface.org.sg")
            {
                if (id != null)
                {
                    HereUser = _svcUser.GetUserById(id);
                    return Page();
                } else
                {
                    return RedirectToPage("/Errors/IDMissing");
                }
            }
            else
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }
        public IActionResult OnPost(string id)
        {
            if (id != null)
            {
                _emailSender.SendEmailAsync(
                    _svcUser.GetUserById(id).Email,
                    TITLE,
                    "This is a warning. Your account has been reviewed by the IT Staff, and it is vulnerable to being suspended, or having your email banned."
                );
            }
            return Page();
        }
    }
}
