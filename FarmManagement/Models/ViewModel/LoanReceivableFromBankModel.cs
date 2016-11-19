using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmManagement.Models.ViewModel
{
    public class LoanReceivableFromBankModel
    {
        public Int32 Id { get; set; }

        [Required]
        [Display(Name="Loan Id")]
        public Int32 LoanId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "No Of Installement")]
        public decimal NoOfInstallment { get; set; }

        [Required]
        [Display(Name = "Interest Rate")]
        public decimal InterestRate { get; set; }

        [Required]
        [Display(Name = "Total Amount To be Paid")]
        public decimal TotalAmountTobePaid { get; set; }

        [Required]
        [Display(Name = "Account")]
        public Int32 AccountId { get; set; }

        [Required]
        [Display(Name = "User")]
        public Int32 UserId { get; set; }

        [Required]
        [Display(Name = "Loan Receive Date")]
        public DateTime LoanReceiveDate { get; set; }

        [Required]
        [Display(Name = "Loan End Date")]
        public DateTime LoanEndDate { get; set; }

        public string Description { get; set; }
    }
}