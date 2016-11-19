using System;
using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Models.ViewModel
{
	public class LoanManagementModel
    {
		public Int32 Id { get; set; }		
		
		[Required]
        [Display(Name = "Employee")]
        public Int32 EmployeeId { get; set; }
		
		[Required]
		[Display(Name = "Loan Amount")]
        public decimal LoanAmount { get; set; }		
		
		[Display(Name = "No Of Installment")]
        public Int32 NoOfInstallment { get; set; }
		
		
		[Display(Name = "Per Month Or Year")]
        public bool PerMonthOrYear { get; set; }		
		
		[Required]
		[Display(Name = "Start Date")]
        public DateTime LoanStartDate { get; set; }
		
		[Required]
		[Display(Name = "End Date")]
        public DateTime LoanEndDate { get; set; }

        public string Description { get; set; }

        [Display(Name = "Interset Rate")]
        public decimal IntersetRate { get; set; }

        [Required]
        [Display(Name = "Status")]
        public Int32 LoanStatus { get; set; }        

        public bool IsTemporaryEmployee { get; set; }

        public string LoanPerMonthOrYear { get; set; }
        public string Name { get; set; }

        [Display(Name = "Debit Salary")]
        public bool DebtSalary { get; set; }
    }
}