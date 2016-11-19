using System;
using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Models.ViewModel
{
    public class TransectionPersonalAccountModel
    {
		public Int32 Id { get; set; }		
		
        [Required]
        [Display(Name = "Employee")]
		public Int32 EmployeeId { get; set; }
		
		[Required]
        public decimal Amount { get; set; }
		
		[Required]
		[Display(Name = "Transection Type")]
        public Int32 AccountTransectionType { get; set; }
		
		[Required]
        public DateTime Date { get; set; }

        [Display(Name = "Is Added By Admin")]
        public bool IsAddedByAdmin { get; set; }

        public double Credit { get; set; }
        public double Debit { get; set; }
        public string Name { get; set; }
		
	}
}