using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarmManagement.Models.ViewModel
{
    public class AccountViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}