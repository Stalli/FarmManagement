using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmManagement.Lib
{
    public class SelectedFilter
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Int32 UserId { get; set; }
        public PrintType PrintType { get; set; }

        public string Quality { get; set; }
        public string Quantity { get; set; }
    }
}