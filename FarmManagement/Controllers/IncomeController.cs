using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FarmManagement.Lib;
using FarmManagement.Models;
using FarmManagement.Models.ViewModel;
using KendoGridUtilities;

namespace FarmManagement.Controllers
{
    public class IncomeController : BaseController
    {
        public ActionResult LoanFromEmployee()
        {
            return View();
        }
        public ActionResult LoanFromEmployeeAjax(Int32 skip, Int32 take)
        {
            var allEmployeeLoans = FarmManagementEntities.LoanReceivableFromEmployees.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                allEmployeeLoans = allEmployeeLoans.Where(x => x.UserId == LoggedInUser.UserId);
            }

            var employeeLoans = allEmployeeLoans.OrderBy(x => x.Id)
                                                .Select(x => new
                                                {
                                                    x.Id,
                                                    x.LoanId,
                                                    x.AmountReceive,
                                                    Expense = x.Account.Name,
                                                    x.User.Name,
                                                    x.Date
                                                });

            var result = employeeLoans.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateUpdateEmployeeLoan(Int32 id)
        {
            var employeeLoanModel = new LoanReceivableFromEmployeeModel();
            employeeLoanModel.Date = DateTime.Now;

            return PartialView("EmployeeLoanPartial", employeeLoanModel);
        }
        [HttpPost]
        public ActionResult CreateUpdateEmployeeLoan(LoanReceivableFromEmployeeModel employeeLoanModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            var employeeLoan = new LoanReceivableFromEmployee();
            employeeLoan.LoanId = employeeLoanModel.LoanId;
            employeeLoan.AmountReceive = employeeLoanModel.AmountReceive;
            employeeLoan.AccountId = employeeLoanModel.AccountId;
            employeeLoan.UserId = employeeLoanModel.UserId;
            employeeLoan.Date = employeeLoanModel.Date;

            var userAccount = FarmManagementEntities.TransectionPersonalAccounts.Where(x => x.UserId == employeeLoanModel.UserId)
                                                                                .OrderByDescending(x => x.InsertDate).ToList().First();
            var userPersonalAccount = new TransectionPersonalAccount();
            userPersonalAccount.Credit = employeeLoanModel.AmountReceive;
            userPersonalAccount.Balance = userAccount.Balance + employeeLoanModel.AmountReceive;
            userPersonalAccount.Date = employeeLoanModel.Date;
            userPersonalAccount.InsertDate = DateTime.Now;
            userPersonalAccount.IsAddedByAdmin = false;
            userPersonalAccount.UserId = employeeLoanModel.UserId;
            FarmManagementEntities.TransectionPersonalAccounts.Add(userPersonalAccount);

            if (employeeLoanModel.Id == 0)
            {
                employeeLoan.InsertDate = DateTime.Now;
                FarmManagementEntities.LoanReceivableFromEmployees.Add(employeeLoan);
            }

            FarmManagementEntities.SaveChanges();

            var message = string.Format(Constant.SuccessMessage, employeeLoanModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }
        public ActionResult DeleteEmployeeLoan(Int32 id)
        {
            var employeeLoan = FarmManagementEntities.LoanReceivableFromEmployees.Single(x => x.Id == id);
            FarmManagementEntities.LoanReceivableFromEmployees.Remove(employeeLoan);
            FarmManagementEntities.SaveChanges();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }

        public ActionResult LoanFromBank()
        {
            return View();
        }
        public ActionResult LoanFromBankAjax(Int32 skip, Int32 take)
        {
            var allBankLoans = FarmManagementEntities.LoanReceivableFromBanks.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                allBankLoans = allBankLoans.Where(x => x.UserId == LoggedInUser.UserId);
            }

            var bankLoans = allBankLoans.OrderBy(x => x.Id).Select(x => new
            {
                x.Id,
                x.LoanId,
                x.Amount,
                x.NoOfInstallment,
                x.InterestRate,
                x.TotalAmountTobePaid,
                Expense = x.Account.Name,
                x.User.Name,
                x.LoanReceiveDate,
                x.LoanEndDate,
                x.Description
            });

            var result = bankLoans.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateUpdateBankLoan(Int32 id)
        {
            var bankLoanModel = new LoanReceivableFromBankModel();
            bankLoanModel.LoanReceiveDate = bankLoanModel.LoanEndDate = DateTime.Now;

            return PartialView("BankLoanPartial", bankLoanModel);
        }
        [HttpPost]
        public ActionResult CreateUpdateBankLoan(LoanReceivableFromBankModel bankLoanModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            var bankLoan = new LoanReceivableFromBank();
            bankLoan.LoanId = bankLoanModel.LoanId;
            bankLoan.Amount = bankLoanModel.Amount;
            bankLoan.NoOfInstallment = bankLoanModel.NoOfInstallment;
            bankLoan.InterestRate = bankLoanModel.InterestRate;
            bankLoan.TotalAmountTobePaid = bankLoanModel.TotalAmountTobePaid;
            bankLoan.AccountId = bankLoanModel.AccountId;
            bankLoan.UserId = bankLoanModel.UserId;
            bankLoan.LoanReceiveDate = bankLoanModel.LoanReceiveDate;
            bankLoan.LoanEndDate = bankLoanModel.LoanEndDate;
            bankLoan.Description = bankLoanModel.Description;

            var userAccount = FarmManagementEntities.TransectionPersonalAccounts.Where(x => x.UserId == bankLoanModel.UserId)
                                                                                .OrderByDescending(x => x.InsertDate).ToList().First();
            var userPersonalAccount = new TransectionPersonalAccount();
            userPersonalAccount.Credit = bankLoanModel.Amount;
            userPersonalAccount.Balance = userAccount.Balance + bankLoanModel.Amount;
            userPersonalAccount.Date = bankLoanModel.LoanReceiveDate;
            userPersonalAccount.InsertDate = DateTime.Now;
            userPersonalAccount.IsAddedByAdmin = false;
            userPersonalAccount.UserId = bankLoanModel.UserId;
            FarmManagementEntities.TransectionPersonalAccounts.Add(userPersonalAccount);

            if (bankLoanModel.Id == 0)
            {
                bankLoan.InsertDate = DateTime.Now;
                FarmManagementEntities.LoanReceivableFromBanks.Add(bankLoan);
            }

            FarmManagementEntities.SaveChanges();

            var message = string.Format(Constant.SuccessMessage, bankLoanModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }
        public ActionResult DeleteBankLoan(Int32 id)
        {
            var bankLoan = FarmManagementEntities.LoanReceivableFromBanks.Single(x => x.Id == id);
            FarmManagementEntities.LoanReceivableFromBanks.Remove(bankLoan);
            FarmManagementEntities.SaveChanges();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }

    }
}
