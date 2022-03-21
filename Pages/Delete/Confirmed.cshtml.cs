using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using enterprisedevproj.Services;
using enterprisedevproj.Models;
using enterprisedevproj.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace enterprisedevproj.Pages.Delete
{
    public class ConfirmedModel : PageModel
    {
        [BindProperty]
        public string DetailIndex { get; set; }
        [BindProperty]
        public string RedirectValue { get; set; }
        private readonly ILogger<ConfirmedModel> _logger;
        private readonly UserService _svcUser;
        public ConfirmedModel(ILogger<ConfirmedModel> logger, UserService userService)
        {
            _logger = logger;
            _svcUser = userService;
        }
        public IActionResult OnGet(string id)
        {
            DetailIndex = id;
            if (DetailIndex == null)
            {
                return RedirectToPage("/Errors/IDMissing");
            } else
            {
                if (_svcUser.UserExists(id))
                {
                    RedirectValue = "users";
                }
                else
                {
                    switch (id[0])
                    {
                        case 'A':
                            RedirectValue = "alerts";
                            break;
                        case 'R':
                            RedirectValue = "reviews";
                            break;
                        case 'E':
                            RedirectValue = "events";
                            break;
                        case 'I':
                            RedirectValue = "interests";
                            break;
                        case 'N':
                            RedirectValue = "needs";
                            break;
                        default:
                            break;
                    }
                }
            }
            return Page();
        }
    }
}
