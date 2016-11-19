using System;
using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Models.ViewModel
{
    public class TemporaryEmployeeSalaryModel
    {
        public Int32 Id { get; set; }

        [Required]
        public Int32 Year { get; set; }

        [Required]
        public Int32 Month { get; set; }

        public decimal MorningWages { get; set; }

        public decimal NightWages { get; set; }
    }
}