using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace enterprisedevproj.Models
{
    public class Need
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string BeneficiaryId { get; set; }
        [Required]
        public string AssistantId { get; set; }
        [Required, MaxLength(500)]
        public string MedicalHistory { get; set; }
        [Required, MaxLength(500)]
        public string Medications { get; set; }
        [Required, MaxLength(500)]
        public string Conditions { get; set; }
        [Required]
        public int Vegetarian { get; set; }
        [Required]
        public string FoodAllergies { get; set; }

    }
}