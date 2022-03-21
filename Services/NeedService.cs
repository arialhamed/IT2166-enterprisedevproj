using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using enterprisedevproj.Models;
using Microsoft.EntityFrameworkCore;

namespace enterprisedevproj.Services
{
    public class NeedService
    {
        private readonly Models.EnterpriseDevProjDbContext _context;
        public NeedService(Models.EnterpriseDevProjDbContext context)
        {
            _context = context;
        }
        public List<Need> GetAllNeeds()
        {
            List<Need> AllNeeds = new List<Need>();
            AllNeeds = _context.Needs.ToList(); //ERROR HERE
            return AllNeeds;
        }
        public Need GetNeedById(string id)
        {
            Need needs = _context.Needs.Where(e => e.Id == id).FirstOrDefault();
            return needs;
        }
        public Need GetNeedByBeneficiaryId(string id)
        {
            Need needs = _context.Needs.Where(e => e.BeneficiaryId == id).FirstOrDefault();
            return needs;
        }
        public bool NeedExists(string id)
        {
            return _context.Needs.Any(e => e.Id == id);
        }
        public bool AddNeed(Need newNeed)
        {
            if (NeedExists(newNeed.Id))
            {
                return false;
            }
            _context.Add(newNeed);
            _context.SaveChanges();
            return true;
        }
        public bool UpdateNeed(Need theNeed)
        {
            bool updated = true;
            _context.Attach(theNeed).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            //POTENTIAL ERROR HERE
            try
            {
                _context.SaveChanges();
                updated = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NeedExists(theNeed.Id))
                {
                    updated = false;
                }
                else
                {
                    throw;
                }
            }
            return updated;
        }
        public bool DeleteNeed(Need theNeed)
        {
            if (!NeedExists(theNeed.Id))
            {
                return false;
            }
            _context.Attach(theNeed);
            _context.Remove(theNeed);
            _context.SaveChanges();
            return true;
        }
    }
}
