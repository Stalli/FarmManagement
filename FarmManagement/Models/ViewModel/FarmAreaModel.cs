using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmManagement.Models.ViewModel
{
    public class FarmAreaModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name="Farm Name")]
        public int FarmId { get; set; }

        [Required]
        [Display(Name = "Area Name")]
        public string AreaName { get; set; }

        [Required]
        [Display(Name = "Area In Acars")]
        public string AreaInAcars { get; set; }

        [Required]
        [Display(Name = "Starting Point")]
        public string StartingPoint { get; set; }

        [Required]
        [Display(Name = "Ending Point")]
        public string EndingPoint { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Display(Name = "Remaning Area")]
        public string RemaningArea { get; set; }
}
}