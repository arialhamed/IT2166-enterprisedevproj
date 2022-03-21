using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //data annotation aka data validation

namespace enterprisedevproj.Models
{
    public class Interest
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        
        public string PosterURL { get; set; }
        [Range(typeof(int), "0", "5")]
        public int Rating { get; set; }
        [Required]
        public int Likes { get; set; }
        [Required]
        public int Views { get; set; }
        [Required]
        public string CreatorId { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime DateModified { get; set; }
        [Required]
        public int Approved { get; set; }
    }
}
