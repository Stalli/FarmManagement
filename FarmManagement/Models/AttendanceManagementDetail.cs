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
    
    public partial class AttendanceManagementDetail
    {
        public int Id { get; set; }
        public int AttendanceManagementId { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<bool> IsPresentMorning { get; set; }
        public Nullable<bool> IsAbsentMorning { get; set; }
        public Nullable<bool> IsLeaveMorning { get; set; }
        public Nullable<bool> IsPresentNight { get; set; }
        public Nullable<bool> IsAbsentNight { get; set; }
        public Nullable<bool> IsLeaveNight { get; set; }
        public int UserId { get; set; }
        public System.DateTime InsertDate { get; set; }
    
        public virtual AttendanceManagement AttendanceManagement { get; set; }
        public virtual User User { get; set; }
    }
}
