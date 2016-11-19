using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmManagement.Models.ViewModel
{
    public class LoanReceivableFromEmployeeModel
    {
        public Int32 Id { get; set; }

        [Required]
        [Display(Name="Loan Id")]
        public Int32 LoanId { get; set; }

        [Required]
        [Display(Name = "Amount Receive")]
        public decimal AmountReceive { get; set; }

        [Required]
        [Display(Name = "Account")]
        public Int32 AccountId { get; set; }

        [Required]
        [Display(Name = "User")]
        public Int32 UserId { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}