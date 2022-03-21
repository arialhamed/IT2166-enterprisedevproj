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
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using enterprisedevproj.Models.Users;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace enterprisedevproj.Pages.Create
{
    public class InterestModel : PageModel
    {
        [BindProperty]
        public string Message1 { get; set; }
        private readonly ILogger<InterestModel> _logger;
        private readonly InterestService _svc;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly Random rnd = new Random();
        public InterestModel(ILogger<InterestModel> logger, InterestService service, SignInManager<ApplicationUser> managerSignIn, UserManager<ApplicationUser> managerUser)
        {
            _logger = logger;
            _svc = service;
            _signInManager = managerSignIn;
            _userManager = managerUser;
        }
        [BindProperty]
        public Interest HereInterest { get; set; }
        public IActionResult OnGet()
        {
            if (_signInManager.IsSignedIn(User))
            {
                //HereInterest.CreatorId = "U142634"; // hardcoded. when session is added, make the appropiate changes
                //HereInterest.CreatorId = _userManager.GetUserId(User);
                // if not logged in then go to idmissing
                return Page();
            } else
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }
        public IActionResult OnPost(IFormFile files)
        {
            do
            {
                HereInterest.Id = "I" + (rnd.Next(100000, 999999)).ToString();
            } while (_svc.InterestExists(HereInterest.Id));
            //HereInterest.CreatorId = "U142634"; // hardcoded. this needs to be repeated, just in case.
            HereInterest.CreatorId = _userManager.GetUserId(User);
            HereInterest.DateModified = Convert.ToDateTime(DateTime.Now.ToString("yyyy MMMM dd, HH:mm:ss"));
            HereInterest.Approved = 0; // will be changed to 1 by admin or it staff

            /*HereInterest.PosterURL = "";
            HereInterest.Rating = 0.0F;*/
            if (files != null)
            {
                if (files.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(files.FileName);

                    //Assigning Unique Filename (Guid)
                    var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);

                    // concatenating  FileName + FileExtension (new filename.ext)
                    var newFileName = String.Concat(myUniqueFileName, fileExtension);

                    HereInterest.PosterURL = "/img/interests/" + newFileName;

                    // Combines two strings into a path.
                    var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img")).Root + $@"\interests\{newFileName}";

                    using (FileStream fs = System.IO.File.Create(filepath))
                    {
                        files.CopyTo(fs);
                        fs.Flush();
                    }
                }
            }


            if (_svc.AddInterest(HereInterest))
            {
                return RedirectToPage("Confirmed", new { id = 'i' });
            }
            else
            {
                Message1 = "Error with adding interest, please try again, else, directly contact web staff.";
                return Page();
            }
        }

/*        public IActionResult Index(IFormFile files)
        {
            if (files != null)
            {
                if (files.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(files.FileName);

                    //Assigning Unique Filename (Guid)
                    var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);

                    // concatenating  FileName + FileExtension
                    var newFileName = String.Concat(myUniqueFileName, fileExtension);

                    // Combines two strings into a path.
                    var filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img")).Root + $@"\${_userManager.GetUserId(User)}\{newFileName}";

                    using (FileStream fs = System.IO.File.Create(filepath))
                    {
                        files.CopyTo(fs);
                        fs.Flush();
                    }
                }
            }
            return Page();
        }*/
    }
}
