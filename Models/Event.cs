using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace enterprisedevproj.Models
{
    public class Event
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string CreatorId { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime StartTime { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime EndTime { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Sponsors { get; set; }
    }
}
