using System;
using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Models.ViewModel
{
    public class FertilizerModel : FarmCommon
    {
        public string FertilizerGuid { get; set; }
		public Int32 Id { get; set; }		
		
		[Required]
		public string Brand { get; set; }
		
		[Required]
		[Display(Name = "Employee")]
        public Int32 UserId { get; set; }
		
		[Required]
		[Display(Name = "Fertilizer Name")]
        public string FertilizerName { get; set; }
		
		[Required]
        [Display(Name = "Fertilizer Type")]
        public Int32 FertilizerType { get; set; }
		
		[Required]
        [Display(Name = "Fertilizer Quality")]
        public Int32 FertilizerQuality { get; set; }

        [Required]
        [Display(Name = "Quantity In No")]
        public Int32 QuantityInNumber { get; set; }
		
		[Required]
        [Display(Name = "Quantity")]
        public Int32 FertilizerQuantity { get; set; }
		
		[Required]		
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Price Per Item")]
        public decimal PricePerItem { get; set; }
		
		[Required]
		[Display(Name = "Date Purchase")]
        public DateTime DatePurchase { get; set; }
		
		
		[Display(Name = "Payment Bill")]
        public string PaymentBill { get; set; }
		
		
		[Display(Name = "Other Description")]
        public string OtherDescription { get; set; }

        public string Type { get; set; }
        public string Quantity { get; set; }
        public string Quality { get; set; }
        public string Name { get; set; }
	}
}
