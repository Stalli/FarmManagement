using FarmManagement.Lib;
using FarmManagement.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarmManagement.Controllers
{
    public class CommonDbController : BaseController
    {
        public ActionResult LeftNavigation()
        {
            if (System.Web.HttpContext.Current.Session["Menu"] == null)
            {
                GetFormSetting();
            }

            var menus = System.Web.HttpContext.Current.Session["Menu"] as Dictionary<String, List<FormSettingModel>>;

            return PartialView("LeftNavigationPartial", menus);
        }

        public ActionResult GetFarmName()
        {
            var farms = FarmManagementEntities.Farms.Select(x => new
            {
                Name = x.FarmName,
                Id = x.Id
            }).ToList();

            farms.Add(new { Name = "Select", Id = 0 });
            farms = farms.OrderBy(x => x.Id).ToList();

            return Json(farms, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPersonalAccountName(int userId)
        {
            var personalAccounts = FarmManagementEntities.PersonalAccounts.Where(pa => pa.UserId == userId).Select(x => new
                {
                    Balance = x.OpeningBalance,
                    CreatedDate = x.CreatedDate,
                    Id = x.Id
                })
                .AsEnumerable()
                .Select(x => new
                {
                    Name = String.Format("Balance: {0}, date: {1}", x.Balance.ToString(), x.CreatedDate),
                    Id = x.Id
                }).ToList();

            personalAccounts.Add(new { Name = "Select", Id = 0 });
            personalAccounts = personalAccounts.OrderBy(x => x.Id).ToList();

            return Json(personalAccounts, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAreaName()
        {
            var farmAreas = FarmManagementEntities.FarmAreas.Select(x => new
            {
                Name = x.AreaName,
                Id = x.Id
            }).ToList();

            farmAreas.Add(new { Name = "Select", Id = 0 });
            farmAreas = farmAreas.OrderBy(x => x.Id).ToList();

            return Json(farmAreas, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAccount()
        {
            var accounts = FarmManagementEntities.Accounts.Select(x => new
            {
                x.Name,
                x.Id
            }).ToList();

            accounts.Add(new { Name = "Select", Id = 0 });
            accounts = accounts.OrderBy(x => x.Id).ToList();

            return Json(accounts, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetVendor()
        {
            var vendors = FarmManagementEntities.VendorManagements.Select(x => new
            {
                Name = x.Name,
                Id = x.Id
            }).ToList();

            vendors.Add(new { Name = "Select", Id = 0 });
            vendors = vendors.OrderBy(x => x.Id).ToList();

            return Json(vendors, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCrop()
        {
            var vendors = FarmManagementEntities.CropManagements.Select(x => new
            {
                Name = x.CropName,
                Id = x.Id
            }).ToList();

            vendors.Add(new { Name = "Select", Id = 0 });
            vendors = vendors.OrderBy(x => x.Id).ToList();

            return Json(vendors, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPayByEmployees()
        {
            var payByEmployees = FarmManagementEntities.PersonalAccounts.Where(x => x.UserId != LoggedInUser.UserId)
                                                        .Select(x => new { x.User.Name, PayByUserId = x.UserId }).ToList();

            return Json(payByEmployees, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLoanReturns(Int32 loanId)
        {
            var loanManagements = FarmManagementEntities.LoanManagements.Select(x => x.Id);
            var loanReturnIds = FarmManagementEntities.ReturnLoans.Where(x => x.LoanId != loanId).Select(x => x.LoanId);
            var returnLoanIds = loanManagements.Where(x => !loanReturnIds.Contains(x))
                                               .Select(x => new { Name = x, LoanId = x }).ToList();

            return Json(returnLoanIds, JsonRequestBehavior.AllowGet);
        }
    }
}
