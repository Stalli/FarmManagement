
using System;
using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Models.ViewModel
{
    public class PermanentEmployeeSalaryModel
    {
        public Int32 Id { get; set; }

        [Required]
        public Int32 Year { get; set; }

        [Required]
        public Int32 Month { get; set; }

        [Required]
        [Display(Name = "Employee")]
        public Int32 EmployeeId { get; set; }

        [Required]
        [Display(Name = "Basic Salary")]
        public decimal BasicSalary { get; set; }

        public decimal? Bonus { get; set; }

        [Display(Name = "Medical Allowance")]
        public decimal? MedicalAllowance { get; set; }

        [Display(Name = "T.A/D.A")]
        public decimal? TA_DA { get; set; }

        [Display(Name = "House Rent")]
        public decimal? HouseRent { get; set; }

        [Display(Name = "Utility Allowance")]
        public decimal? UtilityAllowance { get; set; }

        [Display(Name = "Provident Fund")]
        public decimal? ProvidentFund { get; set; }

        public decimal? Penality { get; set; }

        [Display(Name = "Is Allow Fund")]
        public bool IsAllowFund { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Display(Name = "Loan received")]
        public decimal LoanReceived { get; set; }

        [Display(Name = "Loan returned")]
        public decimal LoanReturned { get; set; }

        [Display(Name = "Loan remaining")]
        public decimal LoanRemaining { get; set; }

        public string Name { get; set; }
        public decimal EmpBonus { get; set; }
        public decimal EmpMedical { get; set; }
        public decimal EmpTADA { get; set; }
        public decimal EmpHouseRent { get; set; }
        public decimal EmpAllowance { get; set; }
        public decimal EmpFund { get; set; }
        public decimal EmpPenality { get; set; }
        public decimal SumGrossSalary { get; set; }
        public decimal TotalAllowance { get; set; }
        public decimal TotalDeduction { get; set; }
        public decimal TotalSalary { get; set; }
        public decimal NetSalary { get; set; }

    }
}