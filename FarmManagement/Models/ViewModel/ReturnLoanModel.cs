using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmManagement.Models.ViewModel
{
    public class ReturnLoanModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name="Loan Id")]
        public int LoanId { get; set; }

        [Required]
        [Display(Name = "Installment Amount")]
        public decimal InstallmentAmount { get; set; }

        [Required]
        [Display(Name = "Account")]
        public int AccountId { get; set; }

        [Required]
        [Display(Name = "User")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Paid Date")]
        public DateTime PaidDate { get; set; }
    }
}