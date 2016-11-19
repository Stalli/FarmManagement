using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmManagement.Lib
{
    public static class LoggedInUser
    {
        public static string Name { get; set; }
        public static Int32 UserId { get; set; }
        public static bool IsAdmin { get; set; }
        public static Int32 RoleId { get; set; }
    }
}