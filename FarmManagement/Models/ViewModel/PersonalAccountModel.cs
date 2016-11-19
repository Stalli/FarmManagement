using System;
using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Models.ViewModel
{
	public class PersonalAccountModel
    {
		public Int32 Id { get; set; }
		
		[Required]
        [Display(Name = "Employee")]
		public Int32 EmployeeId { get; set; }
		
		[Required]
		[Display(Name = "Opening Balance")]
        public decimal OpeningBalance { get; set; }
		
		[Required]
		[Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }		
		
		[Display(Name = "Closing Date")]
        public DateTime? ClosingDate { get; set; }
        
        public string Name { get; set; }
		
	}
}