using System;
using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Models.ViewModel
{
    public class OtherItemModel : FarmCommon
    {
        public Int32 Id { get; set; }

        [Required]
        [Display(Name = "Employee")]
        public Int32 UserId { get; set; }

        [Required]
        [Display(Name = "Title Expense")]
        public string TitleExpense { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Date Purchase")]
        public DateTime DatePurchase { get; set; }

        public string UserName { get; set; }
    }
}
