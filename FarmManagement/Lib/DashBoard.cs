using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmManagement.Lib
{
    public class DashBoard
    {
        public string Name { get; set; }
        public decimal Total { get; set; }
        public decimal Amount { get; set; }
        public DateTime? LastBuyDate { get; set; }
        public string  Color { get; set; }
    }
}