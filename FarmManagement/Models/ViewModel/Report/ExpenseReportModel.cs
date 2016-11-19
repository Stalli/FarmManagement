using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmManagement.Models.ViewModel
{
    public class ExpenseReportModel
    {
        public string AccountName { get; set; }
        public string FarmName { get; set; }
        public string PayeeName { get; set; }
        public string Description { get; set; }
        public string ExpenseName { get; set; }
        public Int32 UserId { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
    }
}