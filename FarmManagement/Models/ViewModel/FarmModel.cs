using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmManagement.Models.ViewModel
{
    public class FarmModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name="Farm Name")]
        public string FarmName { get; set; }

        public string Description { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Total Area")]
        public string TotalArea { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal ActualPrice { get; set; }

        [Required]
        [Display(Name = "Tax Amount")]
        public decimal TaxAmount { get; set; }

        [Required]
        [Display(Name = "Total Value")]
        public decimal TotalValue { get; set; }

        [Required]
        [Display(Name = "Paper Vauey")]
        public decimal PaperValue { get; set; }

        [Required]
        public decimal Rent { get; set; }

        [Required]
        public string Owner { get; set; }

        [Required]
        [Display(Name = "Farm Status")]
        public Int32 FarmStatus { get; set; }

        [Required]
        [Display(Name = "Purchase Date")]
        public DateTime PurchasedDate { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Total Months")]
        public string TotalMonths { get; set; }
    }
}