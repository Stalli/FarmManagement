using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmManagement.Models.ViewModel
{
    public class DepartmentModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name= "Department Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

    }
}