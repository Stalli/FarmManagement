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
    
    public partial class Seed
    {
        public Seed()
        {
            this.SeedExpenses = new HashSet<SeedExpense>();
        }
    
        public int Id { get; set; }
        public int FarmId { get; set; }
        public string Quality { get; set; }
        public string Quantity { get; set; }
        public int QuantityInNumber { get; set; }
        public int VendorId { get; set; }
        public decimal TotalPricePer { get; set; }
        public decimal PricePerItem { get; set; }
        public System.DateTime DatePurchase { get; set; }
        public int CropId { get; set; }
        public string PaymentBill { get; set; }
        public string OtherDescription { get; set; }
        public System.DateTime InsertDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public int AccountId { get; set; }
        public int UserId { get; set; }
    
        public virtual CropManagement CropManagement { get; set; }
        public virtual Farm Farm { get; set; }
        public virtual ICollection<SeedExpense> SeedExpenses { get; set; }
        public virtual VendorManagement VendorManagement { get; set; }
        public virtual Account Account { get; set; }
        public virtual User User { get; set; }
    }
}
