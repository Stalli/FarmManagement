using System;
using System.ComponentModel.DataAnnotations;

namespace FarmManagement.Models.ViewModel
{
    public class EmployeeModel
    {
        public string EmployeeGuid { get; set; }

        public Int32 Id { get; set; }

        public bool IsUser { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public string Password { get; set; }

        [Required]
        [Display(Name = "Role")]
        public Int32 RoleId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Father Name")]
        public string FatherName { get; set; }

        [Required]
        public string CNIC { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        [Display(Name = "Farm")]
        public Int32 FarmId { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public Int32 EmployeeGender { get; set; }

        [Required]
        [Display(Name = "Marital Status")]
        public Int32 EmployeeMaritalStatus { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Place Of Birth")]
        public string PlaceOfBirth { get; set; }

        [Required]
        [Display(Name = "Contact No")]
        public string ContactNo { get; set; }

        public string Landline { get; set; }

        public string Nationality { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "{0} should be valid.")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Permanent Address")]
        public string PermanentAddress { get; set; }

        [Display(Name = "Current Address")]
        public string CurrentAddress { get; set; }

        [Required]
        [Display(Name = "Date Of Joining")]
        public DateTime DateOfJoining { get; set; }

        [Display(Name = "Resignation Date")]
        public DateTime? ResignationDate { get; set; }

        public Int32 EmployeeStatus { get; set; }

        public string Picture { get; set; }

        public string Position { get; set; }

        [Required]
        [Display(Name = "Is Temporary Employee")]
        public Int32 TemporaryEmployee { get; set; }

        [Required]
        [Display(Name = "Basic Salary")]
        public string Salary { get; set; }

        [Display(Name = "Scanned Document")]
        public string ScannedDocument { get; set; }

        [Display(Name = "CNIC FrontSide")]
        public string CNICFrontDocument { get; set; }

        [Display(Name = "CNIC BackSide")]
        public string CNICBackDocument { get; set; }

        [Display(Name = "Agreement Document")]
        public string AgreementDocument { get; set; }

        [Display(Name = "Company Name")]
        public string Company { get; set; }

        [Display(Name = "Company Phone Number")]
        public string CompanyPhoneNumber { get; set; }

        [Display(Name = "Vendor Name")]
        public string VendorName { get; set; }

        [Display(Name = "Account")]
        public int AccountId { get; set; }

        public string Status { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string RoleName { get; set; }
        public string IsTemporaryEmployee { get; set; }
        public string IsUserEmployee { get; set; }
        public string Type { get; set; }
        public string Security { get; set; }
    }
}