using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmManagement.Lib
{
    public static class ClientSession
    {
        public static void Clear()
        {
            HttpContext.Current.Session.Remove("ClientSession");
        }
    }
}