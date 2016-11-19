using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoGridUtilities;
using System.Data.Objects.SqlClient;
using FarmManagement.Models.ViewModel;
using FarmManagement.Models;
using FarmManagement.Lib;

namespace FarmManagement.Controllers
{
    public class LoanManagementController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoanManagementAjax(Int32 skip, Int32 take)
        {
            var allLoanManagements = FarmManagementEntities.LoanManagements.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                allLoanManagements = allLoanManagements.Where(x => x.UserId == LoggedInUser.UserId);
            }

            var loanManagements = allLoanManagements.OrderBy(x => x.Id)
                                                        .Select(x => new
                                                        {
                                                            x.Id,
                                                            x.User.Name,
                                                            x.LoanAmount,
                                                            PerMonthOrYear = x.PerMonthOrYear ? "PerYear" : "PerMonth",
                                                            x.NoOfInstallment,
                                                            x.LoanStartDate,
                                                            x.LoanEndDate,
                                                            x.Description,
                                                            x.IntersetRate,
                                                            x.Status,
                                                            x.User.IsTemporaryEmployee,
                                                            IsDelete = x.PaidLoans.Any() || x.LoanReceivableFromBanks.Any()
                                                                    || x.LoanReceivableFromEmployees.Any() || x.ReturnLoans.Any()
                                                        });

            var result = loanManagements.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateUpdateLoanManagement(Int32 id, bool isTempEmployee)
        {
            var loanManagementModel = new LoanManagementModel();
            loanManagementModel.LoanStartDate = loanManagementModel.LoanEndDate = DateTime.Now;

            if (id > 0)
            {
                var loanManagement = FarmManagementEntities.LoanManagements.Single(x => x.Id == id);
                loanManagementModel = loanManagement.ToType<LoanManagement, LoanManagementModel>();
                loanManagementModel.EmployeeId = loanManagement.UserId;

                loanManagementModel.LoanStatus = Convert.ToInt32(loanManagement.Status.ParseEnum<LoanStatus>());
            }

            loanManagementModel.IsTemporaryEmployee = isTempEmployee;
            return PartialView("PermanentEmployeeLoanPartial", loanManagementModel);
        }
        [HttpPost]
        public ActionResult CreateUpdateLoanManagement(LoanManagementModel loanManagementModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            var loanManagement = new LoanManagement();
            if (loanManagementModel.Id > 0)
            {
                loanManagement = FarmManagementEntities.LoanManagements.Single(x => x.Id == loanManagementModel.Id);
            }

            loanManagement.UserId = loanManagementModel.EmployeeId;
            loanManagement.LoanAmount = loanManagementModel.LoanAmount;
            loanManagement.NoOfInstallment = loanManagementModel.NoOfInstallment;
            loanManagement.PerMonthOrYear = loanManagementModel.PerMonthOrYear;
            loanManagement.LoanStartDate = loanManagementModel.LoanStartDate;
            loanManagement.LoanEndDate = loanManagementModel.LoanEndDate;
            loanManagement.IntersetRate = Convert.ToDecimal(loanManagementModel.IntersetRate);
            loanManagement.Description = loanManagementModel.Description;
            loanManagement.DebtSalary = loanManagementModel.DebtSalary;

            loanManagement.DeductedSalary = "0";

            //if (loanManagement.DebtSalary != null && (bool)loanManagement.DebtSalary)
            //{
            //    if (loanManagement.User.Salary != null)
            //        loanManagement.DeductedSalary = ((loanManagementModel.LoanAmount - decimal.Parse(loanManagement.User.Salary)) * 1).ToString();
            //}

            loanManagement.Status = loanManagementModel.LoanStatus.ToNullIfEmptyEnum<LoanStatus>();

            if (loanManagementModel.Id == 0)
            {
                loanManagement.InsertDate = DateTime.Now;
                FarmManagementEntities.LoanManagements.Add(loanManagement);
            }
            else
            {
                loanManagement.UpdateDate = DateTime.Now;
            }

            FarmManagementEntities.SaveChanges();

            var message = string.Format(Constant.SuccessMessage, loanManagementModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }
        public ActionResult DeleteLoanManagement(Int32 id)
        {
            var loanManagement = FarmManagementEntities.LoanManagements.Single(x => x.Id == id);
            FarmManagementEntities.LoanManagements.Remove(loanManagement);
            FarmManagementEntities.SaveChanges();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }

        public ActionResult PersonalAccount()
        {
            return View();
        }
        public ActionResult PersonalAccountAjax(Int32 skip, Int32 take)
        {
            var allPersonalAccounts = FarmManagementEntities.PersonalAccounts.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                allPersonalAccounts = allPersonalAccounts.Where(x => x.UserId == LoggedInUser.UserId);
            }

            var personalAccounts = allPersonalAccounts.OrderBy(x => x.Id).Select(x => new
            {
                x.Id,
                x.User.Name,
                x.OpeningBalance,
                x.CreatedDate,
                x.ClosingDate
            });

            var result = personalAccounts.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateUpdatePersonalAccount(Int32 id)
        {
            var personalAccountModel = new PersonalAccountModel();
            personalAccountModel.CreatedDate = DateTime.Now;

            if (id > 0)
            {
                var personalAccount = FarmManagementEntities.PersonalAccounts.Single(x => x.Id == id);
                personalAccountModel = personalAccount.ToType<PersonalAccount, PersonalAccountModel>();
                personalAccountModel.EmployeeId = personalAccount.UserId;
            }

            return PartialView("PersonalAccountPartial", personalAccountModel);
        }
        [HttpPost]
        public ActionResult CreateUpdatePersonalAccount(PersonalAccountModel personalAccountModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            var personalAccount = new PersonalAccount();
            if (personalAccountModel.Id > 0)
            {
                personalAccount = FarmManagementEntities.PersonalAccounts.Single(x => x.Id == personalAccountModel.Id);
            }

            personalAccount.UserId = personalAccountModel.EmployeeId;
            personalAccount.OpeningBalance = personalAccountModel.OpeningBalance;
            personalAccount.ClosingDate = personalAccountModel.ClosingDate;
            personalAccount.CreatedDate = personalAccountModel.CreatedDate;

            if (personalAccountModel.Id == 0)
            {
                personalAccount.InsertDate = DateTime.Now;
                FarmManagementEntities.PersonalAccounts.Add(personalAccount);
            }
            else
            {
                personalAccount.UpdateDate = DateTime.Now;
            }

            FarmManagementEntities.SaveChanges();

            var message = string.Format(Constant.SuccessMessage, personalAccountModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }
        public ActionResult DeletePersonalAccount(Int32 id)
        {
            var personalAccount = FarmManagementEntities.PersonalAccounts.Single(x => x.Id == id);
            FarmManagementEntities.PersonalAccounts.Remove(personalAccount);
            FarmManagementEntities.SaveChanges();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }

        public ActionResult PaidLoan()
        {
            return View();
        }
        public ActionResult PaidLoanAjax(Int32 skip, Int32 take)
        {
            var allPaidLoans = FarmManagementEntities.PaidLoans.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                allPaidLoans = allPaidLoans.Where(x => x.UserId == LoggedInUser.UserId);
            }

            var paidLoans = allPaidLoans.OrderBy(x => x.Id).Select(x => new
            {
                x.Id,
                x.LoanId,
                x.User.Name,
                x.Date,
                x.Year,
                x.Month,
                x.Amount
            });

            var result = paidLoans.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateUpdatePaidLoan(Int32 id)
        {
            var paidLoanModel = new PaidLoanModel();
            paidLoanModel.Date = DateTime.Now;

            if (id > 0)
            {
                var paidLoan = FarmManagementEntities.PaidLoans.Single(x => x.Id == id);
                paidLoanModel = paidLoan.ToType<PaidLoan, PaidLoanModel>();
                paidLoanModel.EmployeeId = paidLoan.UserId;
            }

            return PartialView("PaidLoanPartial", paidLoanModel);
        }
        [HttpPost]
        public ActionResult CreateUpdatePaidLoan(PaidLoanModel paidLoanModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            var paidLoan = new PaidLoan();
            if (paidLoanModel.Id > 0)
            {
                paidLoan = FarmManagementEntities.PaidLoans.Single(x => x.Id == paidLoanModel.Id);
            }

            paidLoan.LoanId = paidLoanModel.LoanId;
            paidLoan.UserId = paidLoanModel.EmployeeId;
            paidLoan.AccountId = paidLoanModel.AccountId;
            paidLoan.Date = paidLoanModel.Date;
            paidLoan.Year = paidLoanModel.Year;
            paidLoan.Month = paidLoanModel.Month;
            paidLoan.Amount = paidLoanModel.Amount;

            if (paidLoanModel.Id == 0)
            {
                FarmManagementEntities.PaidLoans.Add(paidLoan);
            }

            FarmManagementEntities.SaveChanges();

            var message = string.Format(Constant.SuccessMessage, paidLoanModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }
        public ActionResult DeletePaidLoan(Int32 id)
        {
            var paidLoan = FarmManagementEntities.PaidLoans.Single(x => x.Id == id);
            FarmManagementEntities.PaidLoans.Remove(paidLoan);
            FarmManagementEntities.SaveChanges();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }

        public ActionResult TransectionPersonalAccount()
        {
            return View();
        }
        public ActionResult TransectionPersonalAccountAjax(Int32 skip, Int32 take)
        {
            var allTransectionAccounts = FarmManagementEntities.TransectionPersonalAccounts.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                allTransectionAccounts = allTransectionAccounts.Where(x => x.UserId == LoggedInUser.UserId);
            }

            var transSectionAccounts = allTransectionAccounts.OrderByDescending(x => x.Id)
                                                             .Select(x => new
                                                             {
                                                                 x.Id,
                                                                 x.User.Name,
                                                                 x.Credit,
                                                                 x.Debit,
                                                                 x.Date,
                                                                 x.Balance
                                                             });

            var result = transSectionAccounts.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateUpdateTransectionPersonalAccount(Int32 id)
        {
            var personalAccountModel = new TransectionPersonalAccountModel();
            personalAccountModel.Date = DateTime.Now;

            return PartialView("TransectionPersonalAccountPartial", personalAccountModel);
        }
        [HttpPost]
        public ActionResult CreateUpdateTransectionPersonalAccount(TransectionPersonalAccountModel transectionAccountModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            var lastTransAccount = FarmManagementEntities.TransectionPersonalAccounts.Where(x => x.UserId == transectionAccountModel.EmployeeId)
                                                                                     .OrderByDescending(x => x.InsertDate).ToList().FirstOrDefault();
            var lastTransAccountBalance = (lastTransAccount == null ? 0 : lastTransAccount.Balance);

            var transectionAccount = new TransectionPersonalAccount();
            transectionAccount.UserId = transectionAccountModel.EmployeeId;
            transectionAccount.Date = transectionAccountModel.Date;
            transectionAccount.IsAddedByAdmin = true;

            var isCredit = (TransectionType)transectionAccountModel.AccountTransectionType == TransectionType.Credit;
            if (isCredit)
            {
                transectionAccount.Credit = transectionAccountModel.Amount;
                transectionAccount.Balance = lastTransAccountBalance + transectionAccountModel.Amount;
            }
            else
            {
                transectionAccount.Debit = transectionAccountModel.Amount;
                transectionAccount.Balance = lastTransAccountBalance - transectionAccountModel.Amount;
            }

            if (transectionAccountModel.Id == 0)
            {
                transectionAccount.InsertDate = DateTime.Now;
                FarmManagementEntities.TransectionPersonalAccounts.Add(transectionAccount);
            }

            FarmManagementEntities.SaveChanges();

            var message = string.Format(Constant.SuccessMessage, transectionAccountModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }
        public ActionResult DeleteTransectionPersonalAccount(Int32 id)
        {
            var transectionAccount = FarmManagementEntities.TransectionPersonalAccounts.Single(x => x.Id == id);
            FarmManagementEntities.TransectionPersonalAccounts.Remove(transectionAccount);
            FarmManagementEntities.SaveChanges();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }

        public ActionResult LoanReturn()
        {
            return View();
        }
        public ActionResult LoanReturnAjax(Int32 skip, Int32 take)
        {
            var allLoanReturns = FarmManagementEntities.ReturnLoans.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                allLoanReturns = allLoanReturns.Where(x => x.UserId == LoggedInUser.UserId);
            }

            var loanReturns = allLoanReturns.OrderBy(x => x.Id).Select(x => new
            {
                x.Id,
                x.LoanId,
                x.InstallmentAmount,
                Expense = x.Account.Name,
                x.User.Name,
                x.PaidDate
            });

            var result = loanReturns.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateUpdateLoanReturn(Int32 id)
        {
            var returnLoanModel = new ReturnLoanModel();
            returnLoanModel.PaidDate = DateTime.Now;

            if (id > 0)
            {
                var returnLoan = FarmManagementEntities.ReturnLoans.Single(x => x.Id == id);
                returnLoanModel = returnLoan.ToType<ReturnLoan, ReturnLoanModel>();
            }

            return PartialView("LoanReturnPartial", returnLoanModel);
        }
        [HttpPost]
        public ActionResult CreateUpdateLoanReturn(ReturnLoanModel returnLoanModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            var returnLoan = new ReturnLoan();
            if (returnLoanModel.Id > 0)
            {
                returnLoan = FarmManagementEntities.ReturnLoans.Single(x => x.Id == returnLoanModel.Id);
            }

            returnLoan.LoanId = returnLoanModel.LoanId;
            returnLoan.InstallmentAmount = returnLoanModel.InstallmentAmount;
            returnLoan.AccountId = returnLoanModel.AccountId;
            returnLoan.UserId = returnLoanModel.UserId;
            returnLoan.PaidDate = returnLoanModel.PaidDate;

            if (returnLoanModel.Id == 0)
            {
                returnLoan.InsertDate = DateTime.Now;
                FarmManagementEntities.ReturnLoans.Add(returnLoan);
            }

            FarmManagementEntities.SaveChanges();

            var message = string.Format(Constant.SuccessMessage, returnLoanModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }
        public ActionResult DeleteLoanReturn(Int32 id)
        {
            var returnLoan = FarmManagementEntities.ReturnLoans.Single(x => x.Id == id);
            FarmManagementEntities.ReturnLoans.Remove(returnLoan);
            FarmManagementEntities.SaveChanges();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }

        public ActionResult GetPersonalAccountEmployees()
        {
            var allPersonalAccountEmp = FarmManagementEntities.PersonalAccounts.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                allPersonalAccountEmp = allPersonalAccountEmp.Where(x => x.UserId == LoggedInUser.UserId);
            }

            var personalAccountEmp = allPersonalAccountEmp.Select(x => new { Name = x.User.Name, Id = x.UserId }).ToList();

            personalAccountEmp.Add(new { Name = "Select", Id = 0 });

            personalAccountEmp = personalAccountEmp.OrderBy(x => x.Id).ToList();

            return Json(personalAccountEmp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLoanEmployees()
        {
            var allLoanEmployees = FarmManagementEntities.LoanManagements.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                allLoanEmployees = allLoanEmployees.Where(x => x.UserId == LoggedInUser.UserId);
            }

            var loanEmployees = allLoanEmployees.Select(x => new
            {
                Name = System.Data.Objects.SqlClient.SqlFunctions.StringConvert((double)x.Id).Trim(),
                Id = x.Id
            }).ToList();

            loanEmployees.Add(new { Name = "Select", Id = 0 });
            loanEmployees = loanEmployees.OrderBy(x => x.Id).ToList();

            return Json(loanEmployees, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TransectionAccount()
        {
            return View();
        }
        public ActionResult TransectionAccountAjax(Int32 skip, Int32 take)
        {
            var allTransectionAccount = FarmManagementEntities.TransectionAccounts.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                allTransectionAccount = allTransectionAccount.Where(x => x.UserId == LoggedInUser.UserId);
            }

            var transectionAccount = allTransectionAccount.OrderBy(x => x.Id).Select(x => new
            {
                x.Id,
                x.User.Name,
                PayByName = x.User1.Name,
                x.Balance,
                x.Date
            });

            var result = transectionAccount.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateUpdateTransectionAccount(Int32 id)
        {
            var transectionAccountModel = new TransectionAccountModel();
            transectionAccountModel.Date = DateTime.Now;
            transectionAccountModel.UserId = LoggedInUser.UserId;
            transectionAccountModel.Name = LoggedInUser.Name;

            return PartialView("TransectionAccountPartial", transectionAccountModel);
        }
        [HttpPost]
        public ActionResult CreateUpdateTransectionAccount(TransectionAccountModel transectionAccountModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            var isPayByUserExist = FarmManagementEntities.PersonalAccounts.Any(x => x.UserId == transectionAccountModel.PayByUserId);
            if (!isPayByUserExist)
            {
                return ShowErrorMessage("Selected Pay By employee doesn't have personal account.");
            }

            var transectionAccount = new TransectionAccount();

            transectionAccount.Id = transectionAccountModel.Id;
            transectionAccount.UserId = transectionAccountModel.UserId;
            transectionAccount.PayByUserId = transectionAccountModel.PayByUserId;
            transectionAccount.Balance = transectionAccountModel.Balance;
            transectionAccount.Date = transectionAccountModel.Date;

            var userAccount = FarmManagementEntities.TransectionPersonalAccounts.Where(x => x.UserId == transectionAccountModel.UserId)
                                                                                .OrderByDescending(x => x.InsertDate).First();
            var userPersonalAccount = new TransectionPersonalAccount();
            userPersonalAccount.Debit = transectionAccountModel.Balance;
            userPersonalAccount.Balance = userAccount.Balance - transectionAccountModel.Balance;
            userPersonalAccount.Date = transectionAccountModel.Date;
            userPersonalAccount.InsertDate = DateTime.Now;
            userPersonalAccount.IsAddedByAdmin = false;
            userPersonalAccount.UserId = transectionAccountModel.UserId;
            FarmManagementEntities.TransectionPersonalAccounts.Add(userPersonalAccount);


            var payByUserAccount = FarmManagementEntities.TransectionPersonalAccounts.Where(x => x.UserId == transectionAccountModel.PayByUserId)
                                                                                     .OrderByDescending(x => x.InsertDate).FirstOrDefault();
            var payByUserPersonalAccount = new TransectionPersonalAccount();
            payByUserPersonalAccount.Credit = transectionAccountModel.Balance;
            payByUserPersonalAccount.Balance = (payByUserAccount == null ? 0 : payByUserAccount.Balance) + transectionAccountModel.Balance;
            payByUserPersonalAccount.Date = transectionAccountModel.Date;
            payByUserPersonalAccount.InsertDate = DateTime.Now;
            payByUserPersonalAccount.UserId = transectionAccountModel.PayByUserId;
            payByUserPersonalAccount.IsAddedByAdmin = false;
            FarmManagementEntities.TransectionPersonalAccounts.Add(payByUserPersonalAccount);

            if (transectionAccountModel.Id == 0)
            {
                transectionAccount.InsertDate = DateTime.Now;
                FarmManagementEntities.TransectionAccounts.Add(transectionAccount);
            }

            FarmManagementEntities.SaveChanges();

            var message = string.Format(Constant.SuccessMessage, transectionAccountModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }
        public ActionResult DeleteTransectionAccount(Int32 id)
        {
            var transectionAccount = FarmManagementEntities.TransectionAccounts.Single(x => x.Id == id);
            FarmManagementEntities.TransectionAccounts.Remove(transectionAccount);
            FarmManagementEntities.SaveChanges();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }

        public ActionResult LoanAmountAjax(int loanId)
        {
            var amount = FarmManagementEntities.LoanManagements.Where(lm => lm.Id == loanId).Select(lm => lm.LoanAmount).FirstOrDefault();

            return Json(new { Data = amount}, JsonRequestBehavior.AllowGet);
        }
    }
}
