using System;
using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Models.ViewModel
{
    public class SeedExpenseModel : FarmCommon
    {
		public Int32 Id { get; set; }		
		
		[Required]
        [Display(Name = "Crop Name")]
		public Int32 SeedId  { get; set; }
		
        public string Quantity { get; set; }

        public string Quality { get; set; }
		
		[Required]
		[Display(Name = "Quantity In Number")]
        public Int32 QuantityInNumber { get; set; }
		
		[Required]
        public DateTime Date { get; set; }
		
		[Required]
        [Display(Name = "Used By")]
        public Int32 UserId { get; set; }		
		
        public string Description { get; set; }
		
	}
}
