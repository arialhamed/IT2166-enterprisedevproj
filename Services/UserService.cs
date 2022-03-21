using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using enterprisedevproj.Models.Users;

namespace enterprisedevproj.Services
{
    public class UserService
    {
        private readonly Models.EnterpriseDevProjDbContext _context;
        public UserService(Models.EnterpriseDevProjDbContext context)
        {
            _context = context;
        }
        public List<ApplicationUser> GetAllUsers()
        {
            List<ApplicationUser> AllUsers = new List<ApplicationUser>();
            AllUsers = _context.Users.ToList();
            AllUsers = AllUsers.OrderBy(e => e.accountCreatedTime).ToList();
            return AllUsers;
        }
        public ApplicationUser GetUserById (string id)
        {
            ApplicationUser user = _context.Users.Where(e => e.Id == id).FirstOrDefault();
            return user;
        }
        public ApplicationUser GetUserByEmail(string email)
        {
            ApplicationUser user = _context.Users.Where(e => e.Email == email).FirstOrDefault();
            return user;
        }
        public bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
        // add user available in identity area
        // update user available in identity area
        // delete user available in identity area

        // this service is for pages not in identity to read
    }
}
