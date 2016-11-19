using System;
using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Models.ViewModel
{
    public class PaidLoanModel
    {
		public Int32 Id { get; set; }		
				
		[Required]
		public Int32 LoanId { get; set; }
		
		[Required]
        public Int32 EmployeeId { get; set; }

        [Required]
        public Int32 AccountId { get; set; }
		
		[Required]
        public DateTime Date { get; set; }
		
		[Required]
        public Int32 Year { get; set; }
		
		[Required]
        public Int32 Month { get; set; }
		
		[Required]
        public decimal Amount { get; set; }

        public string Name { get; set; }
		
	}
}