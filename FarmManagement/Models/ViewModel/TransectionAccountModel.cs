using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmManagement.Models.ViewModel
{
    public class TransectionAccountModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        [Display(Name="Pay By User")]
        public int PayByUserId { get; set; }

        [Required]
        [Display(Name = "User")]
        public int UserId { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public System.DateTime Date { get; set; }
    }
}