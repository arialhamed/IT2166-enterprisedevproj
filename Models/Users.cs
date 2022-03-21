using System;
using System.Net;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace enterprisedevproj.Models.Users {
    
    public class ApplicationUser : IdentityUser {
        [Required]
        [PersonalData]
        public virtual string FirstName { get; set; }

        [PersonalData]
        public virtual string LastName { get; set; }

        [Required]
        [PersonalData]
        public virtual string accountCreatedIp { get; set; }

        [Required]
        [PersonalData]
        public virtual DateTime accountCreatedTime { get; set; }

        [Required]
        [PersonalData]
        public virtual DateTime lastLoginTime { get; set; }

    }

    public class EmployeeUser: ApplicationUser {

        [Required]
        [PersonalData]
        public override string LastName { get; set; }
    }
}