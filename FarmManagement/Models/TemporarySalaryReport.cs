using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmManagement.Models
{
    public class TemporarySalaryReport
    {
        public Int32 NoOfMorning { get; set; }
        public Int32 NoOfNight { get; set; }
        public decimal NoOfMorningSum { get; set; }
        public decimal NoOfNightSum { get; set; }
        public decimal Penalities { get; set; }
        public string UserName { get; set; }
        public string FatherName { get; set; }
        public decimal TotalShift { get; set; }
        public decimal TotalShiftSum { get; set; }
        public decimal NetSalary { get; set; }
    }
}