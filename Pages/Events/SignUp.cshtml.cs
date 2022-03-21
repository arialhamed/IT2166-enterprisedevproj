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

namespace enterprisedevproj.Pages.Events
{
    public class SignUpModel : PageModel
    {
        [BindProperty]
        public ApplicationUser HereUser { get; set; }
        [BindProperty]
        public Event HereEvent { get; set; }
        [BindProperty]
        public Need HereNeed { get; set; }
        [BindProperty]
        public EventParticipant HereParticipant { get; set; }
        private readonly ILogger<SignUpModel> _logger;
        private readonly EventService _svcEvent;
        private readonly NeedService _svcNeed;
        private readonly UserService _svcUser;
        private readonly ParticipantService _svcParticipant;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public SignUpModel(ILogger<SignUpModel> logger, EventService serviceEvent, NeedService serviceNeed, ParticipantService serviceParticipant, UserService serviceUser, SignInManager<ApplicationUser> managerSignIn, UserManager<ApplicationUser> managerUser)
        {
            _logger = logger;
            _svcEvent = serviceEvent;
            _svcNeed = serviceNeed;
            _svcParticipant = serviceParticipant;
            _svcUser = serviceUser;
            _signInManager = managerSignIn;
            _userManager = managerUser;
        }
        public IActionResult OnGet(string id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                HereEvent = _svcEvent.GetEventById(id);
                List<EventParticipant> allparticipants = new List<EventParticipant>();
                allparticipants = _svcParticipant.GetAllParticipants();
                foreach (var item in allparticipants)
                {
                    if (item.Email == _userManager.GetUserName(User) && (item.EventId == HereEvent.Id))
                    {
                        return RedirectToPage("/Errors/SignUpError", new { id = "exist" });
                    }
                    else
                    {
                        continue;
                    }
                }
                HereEvent = _svcEvent.GetEventById(id);
                HereNeed = _svcNeed.GetNeedByBeneficiaryId(_userManager.GetUserName(User));
                HereUser = _svcUser.GetUserByEmail(_userManager.GetUserName(User));
                return Page();

            }
            else
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }
        public IActionResult OnPost()
        {
            HereParticipant.Id = 1;
            HereParticipant.Email = _userManager.GetUserName(User);
            HereParticipant.EventId = HereEvent.Id;

            while (true)
            {
                if (_svcParticipant.ParticipantExists(HereParticipant.Id))
                {
                    HereParticipant.Id++;
                    continue;
                }
                else
                {
                    if (_svcParticipant.AddParticipant(HereParticipant))
                    {
                        return RedirectToPage("/Create/Confirmed", new { id = "su" });
                    }
                    else
                    {
                        return RedirectToPage("/Errors/SignUpFailed", new { id = "failed" });
                    }
                }
            }


        }
    }
}