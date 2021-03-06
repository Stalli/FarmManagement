//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FarmManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.Animals = new HashSet<Animal>();
            this.AttendanceManagementDetails = new HashSet<AttendanceManagementDetail>();
            this.Fertilizers = new HashSet<Fertilizer>();
            this.FertilizerExpenses = new HashSet<FertilizerExpense>();
            this.Fuels = new HashSet<Fuel>();
            this.FuelExpenses = new HashSet<FuelExpense>();
            this.LoanManagements = new HashSet<LoanManagement>();
            this.LoanReceivableFromBanks = new HashSet<LoanReceivableFromBank>();
            this.LoanReceivableFromEmployees = new HashSet<LoanReceivableFromEmployee>();
            this.Machineries = new HashSet<Machinery>();
            this.OtherExpenses = new HashSet<OtherExpense>();
            this.OtherItems = new HashSet<OtherItem>();
            this.PaidLoans = new HashSet<PaidLoan>();
            this.PermanentEmployeeSalaries = new HashSet<PermanentEmployeeSalary>();
            this.PersonalAccounts = new HashSet<PersonalAccount>();
            this.ReturnLoans = new HashSet<ReturnLoan>();
            this.Seeds = new HashSet<Seed>();
            this.SeedExpenses = new HashSet<SeedExpense>();
            this.TaskAssignments = new HashSet<TaskAssignment>();
            this.TransectionAccounts = new HashSet<TransectionAccount>();
            this.TransectionAccounts1 = new HashSet<TransectionAccount>();
            this.TransectionPersonalAccounts = new HashSet<TransectionPersonalAccount>();
            this.Vehicles = new HashSet<Vehicle>();
        }
    
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string CNIC { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string ContactNo { get; set; }
        public string Landline { get; set; }
        public string Nationality { get; set; }
        public string Email { get; set; }
        public string PermanentAddress { get; set; }
        public string CurrentAddress { get; set; }
        public System.DateTime DateOfJoining { get; set; }
        public Nullable<System.DateTime> ResignationDate { get; set; }
        public string Status { get; set; }
        public string Picture { get; set; }
        public string Position { get; set; }
        public bool IsTemporaryEmployee { get; set; }
        public string ScannedDocument { get; set; }
        public string CNICFrontSide { get; set; }
        public string CNICBackSide { get; set; }
        public string AgreementDocument { get; set; }
        public bool IsUser { get; set; }
        public bool IsAdmin { get; set; }
        public System.DateTime InsertDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public int FarmId { get; set; }
        public string Department { get; set; }
        public string Salary { get; set; }
        public string Company { get; set; }
        public string CompanyPhoneNumber { get; set; }
        public string Type { get; set; }
        public string Security { get; set; }
        public string VendorName { get; set; }
        public Nullable<int> AccountId { get; set; }
        public Nullable<int> RelationType { get; set; }
    
        public virtual ICollection<Animal> Animals { get; set; }
        public virtual ICollection<AttendanceManagementDetail> AttendanceManagementDetails { get; set; }
        public virtual Farm Farm { get; set; }
        public virtual ICollection<Fertilizer> Fertilizers { get; set; }
        public virtual ICollection<FertilizerExpense> FertilizerExpenses { get; set; }
        public virtual ICollection<Fuel> Fuels { get; set; }
        public virtual ICollection<FuelExpense> FuelExpenses { get; set; }
        public virtual ICollection<LoanManagement> LoanManagements { get; set; }
        public virtual ICollection<LoanReceivableFromBank> LoanReceivableFromBanks { get; set; }
        public virtual ICollection<LoanReceivableFromEmployee> LoanReceivableFromEmployees { get; set; }
        public virtual ICollection<Machinery> Machineries { get; set; }
        public virtual ICollection<OtherExpense> OtherExpenses { get; set; }
        public virtual ICollection<OtherItem> OtherItems { get; set; }
        public virtual ICollection<PaidLoan> PaidLoans { get; set; }
        public virtual ICollection<PermanentEmployeeSalary> PermanentEmployeeSalaries { get; set; }
        public virtual ICollection<PersonalAccount> PersonalAccounts { get; set; }
        public virtual ICollection<ReturnLoan> ReturnLoans { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Seed> Seeds { get; set; }
        public virtual ICollection<SeedExpense> SeedExpenses { get; set; }
        public virtual ICollection<TaskAssignment> TaskAssignments { get; set; }
        public virtual ICollection<TransectionAccount> TransectionAccounts { get; set; }
        public virtual ICollection<TransectionAccount> TransectionAccounts1 { get; set; }
        public virtual ICollection<TransectionPersonalAccount> TransectionPersonalAccounts { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public virtual Account Account { get; set; }
        public virtual PersonalAccount PersonalAccount { get; set; }
    }
}
