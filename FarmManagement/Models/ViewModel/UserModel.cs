using System;
using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Models.ViewModel
{
    public class UserModel
    {
        public Int32 Id { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Display(Name = "Is Admin")]
        public bool IsAdmin { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        public DateTime InsertDate { get; set; }
        public DateTime LastLogin { get; set; }
    }
}