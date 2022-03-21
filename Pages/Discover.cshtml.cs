using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Mvc.ViewMasterPage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using enterprisedevproj.Models;
using enterprisedevproj.Services;
using Microsoft.Extensions.Logging;

namespace enterprisedevproj.Pages
{
    public class DiscoverModel : PageModel
    {
        [BindProperty]
        public List<Interest> AllInterests { get; set; }
        /*[BindProperty]
        public List<List<Interest>> TabledInterest { get; set; }
        [BindProperty]
        public List<Interest> InterestList1 { get; set; }
        [BindProperty]
        public List<Interest> InterestList2 { get; set; }
        [BindProperty]
        public List<Interest> InterestList3 { get; set; }*/

        [BindProperty]
        public int ColumnsInt { get; set; }

        private readonly ILogger<DiscoverModel> _logger;
        private readonly InterestService _svcInterest;
        public DiscoverModel(ILogger<DiscoverModel> logger, InterestService serviceInterest)
        {
            _logger = logger;
            _svcInterest = serviceInterest;
        }
        public void OnGet()
        {
            AllInterests = _svcInterest.GetApprovedInterests();

/*            var count1 = 0;
            foreach (Interest i in _svcInterest.GetAllInterests())
            {
                switch (count1)
                {
                    case 0:
                        InterestList1.Add(i);
                        break;
                    case 1:
                        InterestList2.Add(i);
                        break;
                    case 2:
                        InterestList3.Add(i);
                        break;
                    default:
                        break;
                }
                if (count1 >= 2)
                {
                    count1 = -1;
                }
                count1 += 1;
            }*/

            int bruh = AllInterests.Count() / 3 + AllInterests.Count() % 3;
            ColumnsInt = bruh;


        }
    }
}
