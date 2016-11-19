using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmManagement.Models.ViewModel
{
    public class FormSettingModel
    {
        public Guid FormGuid { get; set; }
        public Int32 Id { get; set; }
        public Int32 FormId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public bool IsAdd { get; set; }
        public bool IsUpdate { get; set; }
    }
}