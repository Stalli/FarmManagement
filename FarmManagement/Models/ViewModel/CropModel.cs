using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmManagement.Models.ViewModel
{
    public class CropModel
    {
        public Int32 Id { get; set; }

        [Required]
        [Display(Name="Farm Name")]
        public Int32 FarmId { get; set; }

        [Required]
        [Display(Name = "Area Name")]
        public Int32 AreaId { get; set; }

        [Required]
        [Display(Name = "Crop Name")]
        public string CropName { get; set; }

        [Required]
        [Display(Name = "Crop Duration")]
        public string CropDuration { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime CropStartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime CropEndDate { get; set; }
    }
}