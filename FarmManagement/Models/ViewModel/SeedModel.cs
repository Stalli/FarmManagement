using System;
using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Models.ViewModel
{
    public class SeedModel : FarmCommon
    {
        public string SeedGuid { get; set; }
        public Int32 Id { get; set; }

        [Required]
        [Display(Name = "Employee")]
        public Int32 UserId { get; set; }

        [Required]
        [Display(Name = "Quality")]
        public Int32 SeedQuality { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public Int32 SeedQuantity { get; set; }

        [Required]
        [Display(Name = "Quantity In No")]
        public Int32 QuantityInNumber { get; set; }

        [Required]
        [Display(Name = "Total Price")]
        public decimal TotalPricePer { get; set; }

        [Display(Name = "Price Per Item")]
        public decimal PricePerItem { get; set; }

        [Required]
        [Display(Name = "Date Purchase")]
        public DateTime DatePurchase { get; set; }

        [Display(Name = "Payment Bill")]
        public string PaymentBill { get; set; }

        [Display(Name = "Other Description")]
        public string OtherDescription { get; set; }

        public string Quality { get; set; }
        public string Quantity { get; set; }
        public string Name { get; set; }
    }
}
