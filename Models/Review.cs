using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace enterprisedevproj.Models
{
    public class Review
    {
        [Required]
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
        public DateTime ReviewDate { get; set; }
    }
}
