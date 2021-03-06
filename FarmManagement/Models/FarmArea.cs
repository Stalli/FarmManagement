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
    
    public partial class FarmArea
    {
        public FarmArea()
        {
            this.CropManagements = new HashSet<CropManagement>();
            this.FertilizerExpenses = new HashSet<FertilizerExpense>();
            this.FuelExpenses = new HashSet<FuelExpense>();
            this.SeedExpenses = new HashSet<SeedExpense>();
        }
    
        public int Id { get; set; }
        public int FarmId { get; set; }
        public string AreaName { get; set; }
        public string AreaInAcars { get; set; }
        public string StartingPoint { get; set; }
        public string EndingPoint { get; set; }
        public System.DateTime Date { get; set; }
        public System.DateTime InsertDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string RemaningArea { get; set; }
    
        public virtual ICollection<CropManagement> CropManagements { get; set; }
        public virtual Farm Farm { get; set; }
        public virtual ICollection<FertilizerExpense> FertilizerExpenses { get; set; }
        public virtual ICollection<FuelExpense> FuelExpenses { get; set; }
        public virtual ICollection<SeedExpense> SeedExpenses { get; set; }
    }
}
