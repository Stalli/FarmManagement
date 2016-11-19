using System;
using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Models.ViewModel
{
    public class OtherExpenseModel : FarmCommon
    {
		public Int32 Id { get; set; }		
		
		[Required]
        [Display(Name="Other Item")]
		public Int32 OtherItemId { get; set; }
		
		[Required]
        public decimal Amount { get; set; }
		
		public string Description { get; set; }
		
		[Required]
        public DateTime Date { get; set; }
		
		[Required]
        [Display(Name = "Amount Paid By")]
        public Int32 UserId { get; set; }
		
	}
}