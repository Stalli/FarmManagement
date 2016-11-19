using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoGridUtilities;
using FarmManagement.Models.ViewModel;
using FarmManagement.Models;
using FarmManagement.Lib;

namespace FarmManagement.Controllers
{
    public class SalaryController : BaseController
    {
        public ActionResult PermanentEmployeeSalary()
        {
            return View();
        }
        public ActionResult PermanentEmployeeSalaryAjax(Int32 skip, Int32 take)
        {
            var allPermanentSalaries = FarmManagementEntities.PermanentEmployeeSalaries.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                allPermanentSalaries = allPermanentSalaries.Where(x => x.UserId == LoggedInUser.UserId);
            }

            var employeeSalaries = allPermanentSalaries.OrderBy(x => x.Id)
                                                         .Select(x => new
                                                         {
                                                             x.Id,
                                                             x.Year,
                                                             x.Month,
                                                             x.Date,
                                                             x.User.Name,
                                                             x.BasicSalary,
                                                             x.MedicalAllowance,
                                                             x.TA_DA,
                                                             x.Bonus,
                                                             x.HouseRent,
                                                             x.UtilityAllowance,
                                                             x.ProvidentFund,
                                                             x.Penality
                                                         });

            var result = employeeSalaries.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateUpdatePermanentEmployeeSalary(Int32 id)
        {
            var empSalaryModel = new PermanentEmployeeSalaryModel();

            if (id > 0)
            {
                var empSalary = FarmManagementEntities.PermanentEmployeeSalaries.Single(x => x.Id == id);
                empSalaryModel = empSalary.ToType<PermanentEmployeeSalary, PermanentEmployeeSalaryModel>();

                var basicSalaryFromEmployeeTable = FarmManagementEntities.Users.Where(u => u.Id == empSalary.UserId).Select(u => u.Salary).FirstOrDefault();
                if (basicSalaryFromEmployeeTable != null)
                {
                    decimal numericSalary;

                    if (Decimal.TryParse(basicSalaryFromEmployeeTable, out numericSalary))
                        empSalaryModel.BasicSalary = numericSalary;
                }

                var loanReceived =
                    FarmManagementEntities.LoanManagements.Where(lm => lm.UserId == empSalary.UserId)
                        .Sum(lm => lm.LoanAmount);

                empSalaryModel.LoanReceived = loanReceived;

                var loanReturned =
                    FarmManagementEntities.ReturnLoans.Where(lm => lm.UserId == empSalary.UserId)
                        .Sum(lm => lm.InstallmentAmount);

                empSalaryModel.LoanReturned = loanReturned;

                empSalaryModel.LoanRemaining = loanReceived - loanReturned;

                empSalaryModel.EmployeeId = empSalary.UserId;
            }

            return PartialView("PermanentEmployeeSalaryPartial", empSalaryModel);
        }
        [HttpPost]
        public ActionResult CreateUpdatePermanentEmployeeSalary(PermanentEmployeeSalaryModel empModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            var empSalary = new PermanentEmployeeSalary();
            if (empModel.Id > 0)
            {
                empSalary = FarmManagementEntities.PermanentEmployeeSalaries.Single(x => x.Id == empModel.Id);
            }

            empSalary.Year = empModel.Year;
            empSalary.Month = empModel.Month;
            empSalary.Date = empModel.Date;
            empSalary.UserId = empModel.EmployeeId;
            empSalary.BasicSalary = empModel.BasicSalary;
            empSalary.MedicalAllowance = Convert.ToDecimal(empModel.MedicalAllowance);
            empSalary.TA_DA = Convert.ToDecimal(empModel.TA_DA);
            empSalary.HouseRent = Convert.ToDecimal(empModel.HouseRent);
            empSalary.UtilityAllowance = Convert.ToDecimal(empModel.UtilityAllowance);
            empSalary.ProvidentFund = Convert.ToDecimal(empModel.ProvidentFund);
            empSalary.Bonus = Convert.ToDecimal(empModel.Bonus);
            empSalary.Penality = empModel.Penality;
            empSalary.IsAllowFund = empModel.IsAllowFund;

            if (empModel.Id == 0)
            {
                empSalary.InsertDate = DateTime.Now;
                FarmManagementEntities.PermanentEmployeeSalaries.Add(empSalary);
            }
            else
            {
                empSalary.UpdateDate = DateTime.Now;
            }

            var user = FarmManagementEntities.Users.FirstOrDefault(u => u.Id == empModel.EmployeeId);
            if (user != null)
                user.Salary = empModel.BasicSalary.ToString();

            FarmManagementEntities.SaveChanges();

            var message = string.Format(Constant.SuccessMessage, empModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }
        public ActionResult DeletePermanentEmployeeSalary(Int32 id)
        {
            var empSalary = FarmManagementEntities.PermanentEmployeeSalaries.Single(x => x.Id == id);
            FarmManagementEntities.PermanentEmployeeSalaries.Remove(empSalary);
            FarmManagementEntities.SaveChanges();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }


        public ActionResult TemporaryEmployeeWages()
        {
            return View();
        }
        public ActionResult TemporaryEmployeeWagesAjax(Int32 skip, Int32 take)
        {
            var employeeSalaries = FarmManagementEntities.TemporaryEmployeeSalaries.OrderBy(x => x.Id)
                                                         .Select(x => new
                                                         {
                                                             x.Id,
                                                             x.Year,
                                                             x.Month,
                                                             x.MorningWages,
                                                             x.NightWages
                                                         });

            var result = employeeSalaries.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateUpdateTemporaryEmployeeSalary(Int32 id)
        {
            var empSalaryModel = new TemporaryEmployeeSalaryModel();

            if (id > 0)
            {
                var empSalary = FarmManagementEntities.TemporaryEmployeeSalaries.Single(x => x.Id == id);
                empSalaryModel = empSalary.ToType<TemporaryEmployeeSalary, TemporaryEmployeeSalaryModel>();
            }

            return PartialView("TemporaryEmployeeSalaryPartial", empSalaryModel);
        }
        [HttpPost]
        public ActionResult CreateUpdateTemporaryEmployeeSalary(TemporaryEmployeeSalaryModel tempEmpModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            var tempEmpSalary = new TemporaryEmployeeSalary();
            if (tempEmpModel.Id > 0)
            {
                tempEmpSalary = FarmManagementEntities.TemporaryEmployeeSalaries.Single(x => x.Id == tempEmpModel.Id);
            }

            tempEmpSalary.Year = tempEmpModel.Year;
            tempEmpSalary.Month = tempEmpModel.Month;
            tempEmpSalary.MorningWages = tempEmpModel.MorningWages;
            tempEmpSalary.NightWages = tempEmpModel.NightWages;

            if (tempEmpModel.Id == 0)
            {
                tempEmpSalary.InsertDate = DateTime.Now;
                FarmManagementEntities.TemporaryEmployeeSalaries.Add(tempEmpSalary);
            }
            else
            {
                tempEmpSalary.UpdateDate = DateTime.Now;
            }

            FarmManagementEntities.SaveChanges();

            var message = string.Format(Constant.SuccessMessage, tempEmpModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }

        public ActionResult DeleteTemporaryEmployeeSalary(Int32 id)
        {
            var empSalary = FarmManagementEntities.TemporaryEmployeeSalaries.Single(x => x.Id == id);
            FarmManagementEntities.TemporaryEmployeeSalaries.Remove(empSalary);
            FarmManagementEntities.SaveChanges();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }
    }
}
