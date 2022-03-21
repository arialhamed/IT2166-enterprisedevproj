using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //data annotation aka data validation

namespace enterprisedevproj.Models
{
    public class Alert
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string TargetId { get; set; }
        [Required]
        public string AlerterId { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime AlertTime { get; set; }
        [Required, MaxLength(300)]
        public string Description { get; set; }
    }
}
