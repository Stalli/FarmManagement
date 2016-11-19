using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmManagement.Models.ViewModel
{
    public class VendorModel
    {
        public Int32 Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }

        [Required]
        [Display(Name = "Personal Phone No")]
        public string PersonalPhoneNo { get; set; }

        [Required]
        [Display(Name = "Address Info")]
        public string AddressInfo { get; set; }

        [Required]
        [Display(Name = "Opening Balance")]
        public decimal OpeningBalance { get; set; }

        [Display(Name = "Description")]
        public string OtherDescription { get; set; }

        [Required]
        [Display(Name = "Type")]
        public Int32 VendorType { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}