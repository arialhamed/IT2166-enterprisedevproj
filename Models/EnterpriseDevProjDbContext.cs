using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using enterprisedevproj.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace enterprisedevproj.Models
{
    // DbContext comes from EntityFrameworkCore
    public class EnterpriseDevProjDbContext: IdentityDbContext<ApplicationUser>
    {
        // Inject Iconfig to access appsettings.json
        private readonly IConfiguration _config;
        public EnterpriseDevProjDbContext(IConfiguration configuration)
        {
            _config = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Get conn string from the value of "TheConn" in appsettings and
            // configure context to connect to microsoft sql server database
            string connectionString = _config.GetConnectionString("TheConn");
            optionsBuilder.UseSqlServer(connectionString);
        }
        // Map Alert entity to Alerts table in database
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Need> Needs { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }
    }
}
