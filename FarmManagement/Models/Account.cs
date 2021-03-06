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
    
    public partial class Account
    {
        public Account()
        {
            this.Animals = new HashSet<Animal>();
            this.LoanReceivableFromBanks = new HashSet<LoanReceivableFromBank>();
            this.LoanReceivableFromEmployees = new HashSet<LoanReceivableFromEmployee>();
            this.Fertilizers = new HashSet<Fertilizer>();
            this.FertilizerExpenses = new HashSet<FertilizerExpense>();
            this.Fuels = new HashSet<Fuel>();
            this.FuelExpenses = new HashSet<FuelExpense>();
            this.Machineries = new HashSet<Machinery>();
            this.OtherExpenses = new HashSet<OtherExpense>();
            this.OtherItems = new HashSet<OtherItem>();
            this.PaidLoans = new HashSet<PaidLoan>();
            this.ReturnLoans = new HashSet<ReturnLoan>();
            this.Seeds = new HashSet<Seed>();
            this.SeedExpenses = new HashSet<SeedExpense>();
            this.Vehicles = new HashSet<Vehicle>();
            this.Users = new HashSet<User>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime InsertDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
    
        public virtual ICollection<Animal> Animals { get; set; }
        public virtual ICollection<LoanReceivableFromBank> LoanReceivableFromBanks { get; set; }
        public virtual ICollection<LoanReceivableFromEmployee> LoanReceivableFromEmployees { get; set; }
        public virtual ICollection<Fertilizer> Fertilizers { get; set; }
        public virtual ICollection<FertilizerExpense> FertilizerExpenses { get; set; }
        public virtual ICollection<Fuel> Fuels { get; set; }
        public virtual ICollection<FuelExpense> FuelExpenses { get; set; }
        public virtual ICollection<Machinery> Machineries { get; set; }
        public virtual ICollection<OtherExpense> OtherExpenses { get; set; }
        public virtual ICollection<OtherItem> OtherItems { get; set; }
        public virtual ICollection<PaidLoan> PaidLoans { get; set; }
        public virtual ICollection<ReturnLoan> ReturnLoans { get; set; }
        public virtual ICollection<Seed> Seeds { get; set; }
        public virtual ICollection<SeedExpense> SeedExpenses { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
