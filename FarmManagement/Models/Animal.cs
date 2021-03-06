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
    
    public partial class Animal
    {
        public int Id { get; set; }
        public int FarmId { get; set; }
        public string AnimalName { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public System.DateTime PurchaseDate { get; set; }
        public int Age { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public int VendorId { get; set; }
        public string FamilyName { get; set; }
        public string Sex { get; set; }
        public System.DateTime InsertDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public int AccountId { get; set; }
        public int UserId { get; set; }
    
        public virtual Farm Farm { get; set; }
        public virtual VendorManagement VendorManagement { get; set; }
        public virtual Account Account { get; set; }
        public virtual User User { get; set; }
    }
}
