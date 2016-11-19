using System;
using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Models.ViewModel
{
    public class FertilizerExpenseModel : FarmCommon
    {
		public Int32 Id { get; set; }		
		
		[Required]
        [Display(Name = "Fertilizer Name")]
		public Int32 FertilizerId { get; set; }
		
        public string Type { get; set; }
		
        public string Quality { get; set; }
				
        public string Quantity { get; set; }
		
		[Required]
		[Display(Name = "Quantity In Number")]
        public Int32 QuantityInNumber { get; set; }
				
        public string Brand { get; set; }
		
		[Required]
        public DateTime Date { get; set; }
		
		[Required]
        [Display(Name = "Used By")]
        public Int32 UserId { get; set; }		
		
        public string Description { get; set; }
		
	}
}