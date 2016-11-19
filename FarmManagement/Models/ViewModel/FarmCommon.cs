using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmManagement.Models.ViewModel
{
    public class FarmCommon
    {
        [Display(Name="Farm")]
        public Int32 FarmId { get; set; }

        [Display(Name = "Area")]
        public Int32 AreaId { get; set; }

        [Display(Name = "Account")]
        public Int32 AccountId { get; set; }

        [Display(Name = "Vendor")]
        public Int32 VendorId { get; set; }

        [Display(Name = "Crop Name")]
        public Int32 CropId { get; set; }

        public string FarmName { get; set; }
        public string VendorName { get; set; }
    }
}