using enterprisedevproj.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace enterprisedevproj.Services
{
    public class InterestService
    {
        private readonly Models.EnterpriseDevProjDbContext _context;
        public InterestService(Models.EnterpriseDevProjDbContext context)
        {
            _context = context;
        }
        public List<Interest> GetAllInterests()
        {
            List<Interest> AllInterests = new List<Interest>();
            AllInterests = _context.Interests.ToList(); //ERROR HERE
            AllInterests = AllInterests.OrderBy(e => e.DateModified).ToList();
            return AllInterests;
        }
        public Interest GetInterestById (string id)
        {
            Interest interest = _context.Interests.Where(e => e.Id == id).FirstOrDefault();
            return interest;
        }
        public List<Interest> GetInterestsForSearch(string inValue)
        {
            List<Interest> SomeInterests = new List<Interest>();
            SomeInterests = _context.Interests.Where(e => e.Description.Contains(inValue)).ToList();
            return SomeInterests;
        }
        public List<Interest> GetApprovedInterests()
        {
            List<Interest> SomeInterests = new List<Interest>();
            SomeInterests = _context.Interests.Where(e => e.Approved == 1).ToList();
            return SomeInterests;
        }
        public bool InterestExists(string id)
        {
            return _context.Interests.Any(e => e.Id == id);
        }
        public bool AddInterest(Interest newinterest)
        {
            if (InterestExists(newinterest.Id))
            {
                return false;
            }
            _context.Add(newinterest);
            _context.SaveChanges();
            return true;
        }
        public bool UpdateInterest(Interest theinterest)
        {
            bool updated = true;
            _context.Attach(theinterest).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            //POTENTIAL ERROR HERE
            try
            {
                _context.SaveChanges();
                updated = true;
            } catch (DbUpdateConcurrencyException)
            {
                if (!InterestExists(theinterest.Id))
                {
                    updated = false;
                } else
                {
                    throw;
                }
            }
            return updated;
        }
        public bool DeleteInterest(Interest theinterest)
        {
            if (!InterestExists(theinterest.Id))
            {
                return false;
            }
            _context.Attach(theinterest);
            _context.Remove(theinterest);
            _context.SaveChanges();
            return true;
        }
    }
}
