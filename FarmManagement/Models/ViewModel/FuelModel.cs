using System;
using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Models.ViewModel
{
    public class FuelModel : FarmCommon
    {
        public string FuelGuid { get; set; }
        public Int32 Id { get; set; }

        [Required]
        [Display(Name = "Employee")]
        public Int32 UserId { get; set; }

        [Required]
        [Display(Name = "Fuel Name")]
        public string FuelName { get; set; }

        [Required]
        [Display(Name = "Date Purchase")]
        public DateTime DatePurchase { get; set; }

        [Required]
        [Display(Name = "Quantity In Liter")]
        public decimal QuantityInLiter { get; set; }

        [Required]
        [Display(Name = "Price Per Liter")]
        public decimal PricePerLiter { get; set; }

        [Required]
        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Payment Bill")]
        public string PaymentBill { get; set; }

        public string Phone { get; set; }

        [Display(Name = "Other Description")]
        public string OtherDescription { get; set; }

        public string UserName { get; set; }
    }
}
