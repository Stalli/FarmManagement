using FarmManagement.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarmManagement.Controllers
{
    public class CommonEnumController : Controller
    {
        public ActionResult GetFarmStatus()
        {
            var farmStatus = ExtensionMethods.ToSelectList<FarmStatus>();
            return Json(farmStatus, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetVendorType()
        {
            var vendorType = ExtensionMethods.ToSelectList<VendorType>();
            return Json(vendorType, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLoanStatus()
        {
            var loanStatus = ExtensionMethods.ToSelectList<LoanStatus>();
            return Json(loanStatus, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTransectionType()
        {
            var transectionTypes = ExtensionMethods.ToSelectList<TransectionType>();
            return Json(transectionTypes, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetExpense()
        {
            var expenseTypes = ExtensionMethods.ToSelectList<Expense>();
            return Json(expenseTypes, JsonRequestBehavior.AllowGet);
        }
    }
}
