using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmManagement.Lib
{
    public class FileUpload
    {
        public string FileUploadGuid { get; set; }
        public string FileUploadId { get; set; }
        public string FilePath { get; set; }
        public bool HasRemoved { get; set; }
        public bool IsNewlyAdded { get; set; }
        public FileUploadPath FileUploadName { get; set; }
    }
}