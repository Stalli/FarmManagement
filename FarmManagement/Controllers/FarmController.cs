using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoGridUtilities;
using FarmManagement.Lib;
using FarmManagement.Models.ViewModel;
using FarmManagement.Models;

namespace FarmManagement.Controllers
{
    public class FarmController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FarmAjax(Int32 skip, Int32 take)
        {
            var farms = FarmManagementEntities.Farms.OrderBy(x => x.Id).Select(x => new
                                                                       {
                                                                           x.Id,
                                                                           x.FarmName,
                                                                           x.Description,
                                                                           x.Address,
                                                                           x.TotalArea,
                                                                           x.ActualPrice,
                                                                           x.TaxAmount,
                                                                           x.TotalValue,
                                                                           x.PaperValue,
                                                                           x.Rent,
                                                                           x.Owner,
                                                                           x.Status,
                                                                           x.PurchasedDate,
                                                                           x.StartDate,
                                                                           x.EndDate,
                                                                           x.TotalMonths
            });

            var result = farms.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateUpdateFarm(Int32 id)
        {
            var farmModel = new FarmModel();
            farmModel.PurchasedDate = farmModel.StartDate = farmModel.EndDate = DateTime.Now;

            if (id > 0)
            {
                var farm = FarmManagementEntities.Farms.Single(x => x.Id == id);
                farmModel = farm.ToType<Farm, FarmModel>();

                farmModel.FarmStatus = Convert.ToInt32(farm.Status.ParseEnum<FarmStatus>());
            }

            return PartialView("FarmPartial", farmModel);
        }
        [HttpPost]
        public ActionResult CreateUpdateFarm(FarmModel farmModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            var farm = new Farm();
            if (farmModel.Id > 0)
            {
                farm = FarmManagementEntities.Farms.Single(x => x.Id == farmModel.Id);
            }

            farm.FarmName = farmModel.FarmName;
            farm.Description = farmModel.Description;
            farm.Address = farmModel.Address;
            farm.TotalArea = farmModel.TotalArea;
            farm.ActualPrice = farmModel.ActualPrice;
            farm.TaxAmount = farmModel.TaxAmount;
            farm.TotalValue = farmModel.TotalValue;
            farm.PaperValue = farmModel.PaperValue;
            farm.Rent = farmModel.Rent;
            farm.Owner = farmModel.Owner;
            farm.PurchasedDate = farmModel.PurchasedDate;
            farm.StartDate = farmModel.StartDate;
            farm.EndDate = farmModel.EndDate;
            farm.TotalMonths = farmModel.TotalMonths;

            farm.Status = farmModel.FarmStatus.ToNullIfEmptyEnum<FarmStatus>();

            if (farmModel.Id == 0)
            {
                farm.InsertDate = DateTime.Now;
                FarmManagementEntities.Farms.Add(farm);
            }
            else
            {
                farm.UpdateDate = DateTime.Now;
            }

            FarmManagementEntities.SaveChanges();

            var message = string.Format(Constant.SuccessMessage, farmModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }

        public ActionResult FarmArea()
        {
            return View();
        }

        public ActionResult FarmAreaAjax(Int32 skip, Int32 take)
        {
            var farms = FarmManagementEntities.FarmAreas.OrderBy(x => x.Id)
                                                        .Select(x => new
                                                        {
                                                            x.Id,
                                                            x.Farm.FarmName,
                                                            x.AreaName,
                                                            x.AreaInAcars,
                                                            x.StartingPoint,
                                                            x.EndingPoint,
                                                            x.Date,
                                                            IsDelete = x.CropManagements.Any() || x.SeedExpenses.Any()
                                                                    || x.FertilizerExpenses.Any() || x.FuelExpenses.Any()
                                                        });

            var result = farms.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateUpdateFarmArea(Int32 id)
        {
            var farmAreaModel = new FarmAreaModel();
            farmAreaModel.Date = DateTime.Now;

            if (id > 0)
            {
                var farm = FarmManagementEntities.FarmAreas.Single(x => x.Id == id);
                farmAreaModel = farm.ToType<FarmArea, FarmAreaModel>();
            }

            return PartialView("FarmAreaPartial", farmAreaModel);
        }
        [HttpPost]
        public ActionResult CreateUpdateFarmArea(FarmAreaModel farmAreaModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            var farmArea = new FarmArea();
            if (farmAreaModel.Id > 0)
            {
                farmArea = FarmManagementEntities.FarmAreas.Single(x => x.Id == farmAreaModel.Id);
            }

            farmArea.FarmId = farmAreaModel.FarmId;
            farmArea.AreaName = farmAreaModel.AreaName;
            farmArea.AreaInAcars = farmAreaModel.AreaInAcars;
            farmArea.StartingPoint = farmAreaModel.StartingPoint;
            farmArea.EndingPoint = farmAreaModel.EndingPoint;
            farmArea.Date = farmAreaModel.Date;
            farmArea.RemaningArea = farmAreaModel.RemaningArea;

            if (farmAreaModel.Id == 0)
            {
                farmArea.InsertDate = DateTime.Now;
                FarmManagementEntities.FarmAreas.Add(farmArea);
            }
            else
            {
                farmArea.UpdateDate = DateTime.Now;
            }

            FarmManagementEntities.SaveChanges();

            var message = string.Format(Constant.SuccessMessage, farmAreaModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }

        public ActionResult DeleteFarmArea(Int32 id)
        {
            var farmArea = FarmManagementEntities.FarmAreas.Single(x => x.Id == id);
            FarmManagementEntities.FarmAreas.Remove(farmArea);
            FarmManagementEntities.SaveChanges();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }

        public ActionResult FarmAccount()
        {
            return View();
        }

        public ActionResult FarmAccountAjax(Int32 skip, Int32 take)
        {
            var farms = FarmManagementEntities.Accounts.OrderBy(x => x.Id)
                                                       .Select(x => new
                                                        {
                                                            x.Id,
                                                            x.Name,
                                                            x.InsertDate
                                                        });

            var result = farms.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateUpdateFarmAccount(Int32 id)
        {
            var accountModel = new AccountViewModel();

            if (id > 0)
            {
                var account = FarmManagementEntities.Accounts.Single(x => x.Id == id);
                accountModel = account.ToType<Account, AccountViewModel>();
            }

            return PartialView("FarmAccountPartial", accountModel);
        }
        [HttpPost]
        public ActionResult CreateUpdateFarmAccount(AccountViewModel accountModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            var account = new Account();
            if (accountModel.Id > 0)
            {
                account = FarmManagementEntities.Accounts.Single(x => x.Id == accountModel.Id);
            }

            account.Name = accountModel.Name;

            if (accountModel.Id == 0)
            {
                account.InsertDate = DateTime.Now;
                FarmManagementEntities.Accounts.Add(account);
            }
            else
            {
                account.UpdateDate = DateTime.Now;
            }

            FarmManagementEntities.SaveChanges();

            var message = string.Format(Constant.SuccessMessage, accountModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }

        public ActionResult DeleteFarmAccount(Int32 id)
        {
            var account = FarmManagementEntities.Accounts.Single(x => x.Id == id);
            FarmManagementEntities.Accounts.Remove(account);
            FarmManagementEntities.SaveChanges();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }
    }
}
