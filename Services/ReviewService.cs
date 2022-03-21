using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using enterprisedevproj.Models;
using enterprisedevproj.Services;
using Microsoft.EntityFrameworkCore;

namespace enterprisedevproj.Services
{
    public class ReviewService
    {
        private readonly Models.EnterpriseDevProjDbContext _context;
        private readonly InterestService _svcInterest;
        public ReviewService(Models.EnterpriseDevProjDbContext context, InterestService interestService)
        {
            _context = context;
            _svcInterest = interestService;
        }
        public List<Review> GetAllReviews()
        {
            List<Review> AllReviews = new List<Review>();
            AllReviews = _context.Reviews.ToList();
            return AllReviews;
        }

        public List<Review> GetAllReviewsByInterestId(string id)
        {
            List<Review> AllReviews = new List<Review>();
            foreach (var r in _context.Reviews.ToList())
            {
                if (r.ItemId == id)
                {
                    AllReviews.Add(r);
                }
            }
            AllReviews = AllReviews.OrderByDescending(e => e.ReviewDate).ToList();
            return AllReviews;
        }
        public Review GetReviewById(string id)
        {
            Review review = _context.Reviews.Where(e => e.Id == id).FirstOrDefault();
            return review;
        }
        public bool ReviewExists(string id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
        public bool AddReview(Review newreview)
        {
            if (ReviewExists(newreview.Id))
            {
                return false;
            }
            _context.Add(newreview);
            _context.SaveChanges();
            return true;
        }
        public bool UpdateReview(Review thereview)
        {
            bool updated = true;
            _context.Attach(thereview).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                _context.SaveChanges();
                updated = true;
            } catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(thereview.Id))
                {
                    updated = false;
                } else
                {
                    throw;
                }
            }
            return updated;
        }
        public bool DeleteReview(Review thereview)
        {
            if (!ReviewExists(thereview.Id))
            {
                return false;
            }
            _context.Attach(thereview);
            _context.Remove(thereview);
            _context.SaveChanges();
            return true;
        }

        /*
         * [Required]
        public string Id { get; set; }
        [Required]
        public string ItemId { get; set; }
        [Required]
        public string ReviewerId { get; set; }
        [Required, MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public int HelpfulRate { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime ReviewDate { get; set; }*/

    }
}
