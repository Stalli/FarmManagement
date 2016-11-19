using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmManagement.Models.ViewModel
{
    public class PositionModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name= "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

    }
}