using System;
using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Models.ViewModel
{
	public class RoleModel
    {
		public Int32 Id { get; set; }
		
		[Required]
		[Display(Name = "Role Name")]		
		public string RoleName { get; set; }
		
	}
}