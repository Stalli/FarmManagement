using System;
using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Models.ViewModel
{
    public class MachineryModel : FarmCommon
    {
        public string MachineryGuid { get; set; }
		public Int32 Id { get; set; }
		
		[Required]
		[Display(Name = "Employee")]
        public Int32 UserId { get; set; }
		
		[Required]
		public string Name { get; set; }
		
		[Required]
        public string Type { get; set; }
		
		[Required]
		[Display(Name = "Registration No")]
        public string RegistrationNo { get; set; }
		
		[Required]
		[Display(Name = "Model No")]
        public string ModelNo { get; set; }		
		
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required]
        public string Color { get; set; }
		
		[Required]
        public decimal Price { get; set; }
		
		[Required]
		[Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; }

        [Display(Name = "Other Description")]
        public string OtherDescription { get; set; }
		
		[Required]		
		[Display(Name = "Condition")]
        public Int32 MachineCondition { get; set; }

        public string Picture { get; set; }
        public string LicenseFile { get; set; }

        public string UserName { get; set; }
        public string Condition { get; set; }

	}
}
