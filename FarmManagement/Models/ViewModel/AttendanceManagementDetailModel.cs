using System;
using System.ComponentModel.DataAnnotations;
using FarmManagement.Lib;

namespace FarmManagement.Models.ViewModel
{
    public class AttendanceManagementDetailModel
    {
        public Guid AttendanceGuid { get; set; }

        public Int32 Id { get; set; }
        public Int32 AttendanceMgmtId { get; set; }

        [Required]
        public Int32 Year { get; set; }

        [Required]
        public Int32 Month { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Display(Name = "Is Present Morning")]
        public bool IsPresentMorning { get; set; }

        [Display(Name = "Is Absent Morning")]
        public bool IsAbsentMorning { get; set; }

        [Display(Name = "Is Leave Morning")]
        public bool IsLeaveMorning { get; set; }

        [Display(Name = "Is Present Night")]
        public bool IsPresentNight { get; set; }

        [Display(Name = "Is Absent Night")]
        public bool IsAbsentNight { get; set; }

        [Display(Name = "Is Leave Night")]
        public bool IsLeaveNight { get; set; }

        public string Name { get; set; }
        public Int32 UserId { get; set; }
        public string PresentMorning { get; set; }
        public string AbsentMorning { get; set; }
        public string LeaveMorning { get; set; }
        public string PresentNight { get; set; }
        public string AbsentNight { get; set; }
        public string LeaveNight { get; set; }
    }
}