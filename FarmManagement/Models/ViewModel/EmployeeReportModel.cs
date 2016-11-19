using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmManagement.Models.ViewModel
{
    public class EmployeeReportModel
    {
        public int SNo { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string FarmName { get; set; }
        public DateTime DateOfJoining { get; set; }
       
    }
}