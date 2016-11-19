using System;
using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Models.ViewModel
{
    public class FuelExpenseModel : FarmCommon
    {
        public Int32 Id { get; set; }

        [Required]
        [Display(Name = "Fuel")]
        public Int32 FuelId { get; set; }

        [Required]
        public Int32 Quantity { get; set; }

        [Required]
        [Display(Name = "Used By")]
        public Int32 UserId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Description { get; set; }

    }
}