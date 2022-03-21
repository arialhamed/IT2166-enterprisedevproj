using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using enterprisedevproj.Models;
using enterprisedevproj.Services;
using enterprisedevproj.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace enterprisedevproj.Pages.Create
{
    public class AlertModel : PageModel
    {
        [BindProperty]
        public string Message1 { get; set; }
        private readonly Random rnd = new Random();
        private readonly ILogger<AlertModel> _logger;
        private readonly AlertService _svc;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AlertModel(ILogger<AlertModel> logger, AlertService service, SignInManager<ApplicationUser> managerSignIn, UserManager<ApplicationUser> managerUser)
        {
            _logger = logger;
            _svc = service;
            _signInManager = managerSignIn;
            _userManager = managerUser;
        }
        [BindProperty]
        public Alert HereAlert { get; set; }
        [BindProperty]
        public string Target { get; set; }
        public IActionResult OnGet(string id)
        {
            if (_signInManager.IsSignedIn(User)){ 
                if (id != null)
                {
                    //continue as normal, set Target
                    Target = id;
                    //Target = "AUREIN"[rnd.Next(5)] + (rnd.Next(100000, 999999)).ToString(); // PLACEHOLDER. when asp-route-id is implemented, make the appropiate changes
                    return Page();
                }
                else
                {
                    //
                    Target = "";
                    return RedirectToPage("/Errors/IDMissing");
                }
            } else
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }
        public IActionResult OnPost(string id)
        {
            do
            {
                HereAlert.Id = "A" + (rnd.Next(100000, 999999)).ToString();
            } while (_svc.AlertExists(HereAlert.Id));
            //HereAlert.AlerterId = "U" + (rnd.Next(100000, 999999)).ToString(); // PLACEHOLDER to be removed when session is added
            HereAlert.AlerterId = _userManager.GetUserId(User);
            HereAlert.TargetId = id; 
            HereAlert.AlertTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy MMMM dd, HH:mm:ss"));
            //Console.WriteLine(HereAlert.AlerterId);

            if (_svc.AddAlert(HereAlert))
            {
                return RedirectToPage("/Create/Confirmed", new { id = 'a' });
            }
            else
            {
                Message1 = "Error with adding alert, please try again, else, directly contact web staff.";
                return Page();
            }
        }
        /*public IActionResult Confirm()
        {
            return Partial("_Modal", "report");
        }*/
    }
}
