using System;
using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Models.ViewModel
{
    public class AnimalModel : FarmCommon
    {
        public string AnimalGuid { get; set; }

        public Int32 Id { get; set; }

        [Required]
        [Display(Name = "Employee")]
        public Int32 UserId { get; set; }

        [Required]
        [Display(Name = "Animal Category")]
        public Int32 AnimalCategory { get; set; }

        [Required]
        [Display(Name = "Animal Name")]
        public string AnimalName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public Int32 Age { get; set; }

        [Required]
        public string Color { get; set; }

        public string Description { get; set; }

        public string Photo { get; set; }

        [Required]
        [Display(Name = "Family Name")]
        public string FamilyName { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public Int32 AnimalGender { get; set; }

        public string Name { get; set; }
        public string Category { get; set; }
        public string Sex { get; set; }

    }
}