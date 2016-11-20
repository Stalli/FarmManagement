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
    public class ExpenseController : BaseController
    {
        public ActionResult SeedExpense()
        {
            return View();
        }
        public ActionResult SeedExpenseAjax(Int32 skip, Int32 take)
        {
            var allSeedExpenses = FarmManagementEntities.SeedExpenses.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                allSeedExpenses = allSeedExpenses.Where(x => x.UserId == LoggedInUser.UserId);
            }

            var seedExpenses = allSeedExpenses.OrderBy(x => x.Id)
                                              .Select(x => new
                                              {
                                                  x.Id,
                                                  x.Farm.FarmName,
                                                  x.FarmArea.AreaName,
                                                  Expense = x.Account.Name,
                                                  x.Seed.CropManagement.CropName,
                                                  x.Seed.Quality,
                                                  x.Seed.Quantity,
                                                  x.QuantityInNumber,
                                                  x.Description,
                                                  x.Date,
                                                  x.User.Name
                                              });

            var result = seedExpenses.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateUpdateSeedExpense(Int32 id)
        {
            var seedExpenseModel = new SeedExpenseModel();

            if (id > 0)
            {
                var seed = FarmManagementEntities.SeedExpenses.Single(x => x.Id == id);
                seedExpenseModel = seed.ToType<SeedExpense, SeedExpenseModel>();
            }

            return PartialView("SeedExpensePartial", seedExpenseModel);
        }
        [HttpPost]
        public ActionResult CreateUpdateSeedExpense(SeedExpenseModel seedExpenseModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            var seedExpense = new SeedExpense();
            if (seedExpenseModel.Id > 0)
            {
                seedExpense = FarmManagementEntities.SeedExpenses.Single(x => x.Id == seedExpenseModel.Id);
            }

            seedExpense.FarmId = seedExpenseModel.FarmId;
            seedExpense.AreaId = seedExpenseModel.AreaId;
            seedExpense.AccountId = seedExpenseModel.AccountId;
            seedExpense.SeedId = seedExpenseModel.SeedId;

            seedExpense.QuantityInNumber = seedExpenseModel.QuantityInNumber;
            seedExpense.Description = seedExpenseModel.Description;
            seedExpense.Date = seedExpenseModel.Date;

            if (seedExpenseModel.Id == 0)
            {
                seedExpense.UserId = seedExpenseModel.UserId;

                FarmManagementEntities.SeedExpenses.Add(seedExpense);
            }

            FarmManagementEntities.SaveChanges();

            var message = string.Format(Constant.SuccessMessage, seedExpenseModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }

        public ActionResult DeleteSeedExpense(Int32 id)
        {
            var seedExpense = FarmManagementEntities.SeedExpenses.Single(x => x.Id == id);
            FarmManagementEntities.SeedExpenses.Remove(seedExpense);
            FarmManagementEntities.SaveChanges();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }

        public ActionResult FertilizerExpense()
        {
            return View();
        }
        public ActionResult FertilizerExpenseAjax(Int32 skip, Int32 take)
        {
            var allFertilizerExpenses = FarmManagementEntities.FertilizerExpenses.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                allFertilizerExpenses = allFertilizerExpenses.Where(x => x.UserId == LoggedInUser.UserId);
            }

            var fertilizerExpenses = allFertilizerExpenses.OrderBy(x => x.Id)
                                                          .Select(x => new
                                                          {
                                                              x.Id,
                                                              x.Farm.FarmName,
                                                              x.FarmArea.AreaName,
                                                              Expense = x.Account.Name,
                                                              x.CropManagement.CropName,
                                                              x.Fertilizer.FertilizerName,
                                                              x.Fertilizer.Type,
                                                              x.Fertilizer.Brand,
                                                              x.Fertilizer.Quality,
                                                              x.Fertilizer.Quantity,
                                                              x.QuantityInNumber,
                                                              x.Date,
                                                              x.User.Name,
                                                              x.Description
                                                          });

            var result = fertilizerExpenses.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateUpdateFertilizerExpense(Int32 id)
        {
            var fertilizerExpenseModel = new FertilizerExpenseModel();

            if (id > 0)
            {
                var fertilizerExpense = FarmManagementEntities.FertilizerExpenses.Single(x => x.Id == id);
                fertilizerExpenseModel = fertilizerExpense.ToType<FertilizerExpense, FertilizerExpenseModel>();
            }

            return PartialView("FertilizerExpensePartial", fertilizerExpenseModel);
        }
        [HttpPost]
        public ActionResult CreateUpdateFertilizerExpense(FertilizerExpenseModel fertilizerExpenseModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            var fertilizerExpense = new FertilizerExpense();
            if (fertilizerExpenseModel.Id > 0)
            {
                fertilizerExpense = FarmManagementEntities.FertilizerExpenses.Single(x => x.Id == fertilizerExpenseModel.Id);
            }

            fertilizerExpense.FarmId = fertilizerExpenseModel.FarmId;
            fertilizerExpense.AreaId = fertilizerExpenseModel.AreaId;
            fertilizerExpense.AccountId = fertilizerExpenseModel.AccountId;
            fertilizerExpense.CropId = fertilizerExpenseModel.CropId;

            fertilizerExpense.FertilizerId = fertilizerExpenseModel.FertilizerId;
            fertilizerExpense.QuantityInNumber = fertilizerExpenseModel.QuantityInNumber;
            fertilizerExpense.Date = fertilizerExpenseModel.Date;
            fertilizerExpense.Description = fertilizerExpenseModel.Description;

            if (fertilizerExpense.Id == 0)
            {
                fertilizerExpense.UserId = fertilizerExpenseModel.UserId;
                FarmManagementEntities.FertilizerExpenses.Add(fertilizerExpense);
            }

            FarmManagementEntities.SaveChanges();

            var message = string.Format(Constant.SuccessMessage, fertilizerExpenseModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }

        public ActionResult DeleteFertilizerExpense(Int32 id)
        {
            var fertilizerExpense = FarmManagementEntities.FertilizerExpenses.Single(x => x.Id == id);
            FarmManagementEntities.FertilizerExpenses.Remove(fertilizerExpense);
            FarmManagementEntities.SaveChanges();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }

        public ActionResult FuelExpense()
        {
            return View();
        }
        public ActionResult FuelExpenseAjax(Int32 skip, Int32 take)
        {
            var allFuelExpenses = FarmManagementEntities.FuelExpenses.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                allFuelExpenses = allFuelExpenses.Where(x => x.UserId == LoggedInUser.UserId);
            }

            var fuels = allFuelExpenses.OrderBy(x => x.Id).Select(x => new
                                                          {
                                                              x.Id,
                                                              x.Farm.FarmName,
                                                              x.FarmArea.AreaName,
                                                              Expense = x.Account.Name,
                                                              x.CropManagement.CropName,
                                                              x.Fuel.FuelName,
                                                              x.Quantity,
                                                              UserName = x.User.Name,
                                                              x.Date,
                                                              x.Description
                                                          });

            var result = fuels.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateUpdateFuelExpense(Int32 id)
        {
            var fuelExpenseModel = new FuelExpenseModel();

            if (id > 0)
            {
                var fuelExpense = FarmManagementEntities.FuelExpenses.Single(x => x.Id == id);
                fuelExpenseModel = fuelExpense.ToType<FuelExpense, FuelExpenseModel>();
            }

            return PartialView("FuelExpensePartial", fuelExpenseModel);
        }
        [HttpPost]
        public ActionResult CreateUpdateFuelExpense(FuelExpenseModel fuelExpenseModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            var fuelExpense = new FuelExpense();
            if (fuelExpenseModel.Id > 0)
            {
                fuelExpense = FarmManagementEntities.FuelExpenses.Single(x => x.Id == fuelExpenseModel.Id);
            }

            fuelExpense.FarmId = fuelExpenseModel.FarmId;
            fuelExpense.AreaId = fuelExpenseModel.AreaId;
            fuelExpense.AccountId = fuelExpenseModel.AccountId;
            fuelExpense.CropId = fuelExpenseModel.CropId;

            fuelExpense.FuelId = fuelExpenseModel.FuelId;
            fuelExpense.Quantity = fuelExpenseModel.Quantity;
            fuelExpense.Date = fuelExpenseModel.Date;
            fuelExpense.Description = fuelExpenseModel.Description;

            if (fuelExpenseModel.Id == 0)
            {
                fuelExpense.UserId = fuelExpenseModel.UserId;
                FarmManagementEntities.FuelExpenses.Add(fuelExpense);
            }

            FarmManagementEntities.SaveChanges();

            var message = string.Format(Constant.SuccessMessage, fuelExpenseModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }

        public ActionResult DeleteFuelExpense(Int32 id)
        {
            var fuelExpense = FarmManagementEntities.FuelExpenses.Single(x => x.Id == id);
            FarmManagementEntities.FuelExpenses.Remove(fuelExpense);
            FarmManagementEntities.SaveChanges();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }

        public ActionResult OtherItemExpense()
        {
            return View();
        }
        public ActionResult OtherItemExpenseAjax(Int32 skip, Int32 take)
        {
            var allOtherExpenses = FarmManagementEntities.OtherExpenses.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                allOtherExpenses = allOtherExpenses.Where(x => x.UserId == LoggedInUser.UserId);
            }

            var otherItemExpenses = allOtherExpenses.OrderBy(x => x.Id).Select(x => new
                                                                       {
                                                                           x.Id,
                                                                           x.Farm.FarmName,
                                                                           Expense = x.Account.Name,
                                                                           x.OtherItem.TitleExpense,
                                                                           x.Amount,
                                                                           x.Description,
                                                                           x.Date,
                                                                           PayByName = x.User.Name
                                                                       });

            var result = otherItemExpenses.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateUpdateOtherItemExpense(Int32 id)
        {
            var otherItemExpenseModel = new OtherExpenseModel();

            if (id > 0)
            {
                var otherItemExpense = FarmManagementEntities.OtherExpenses.Single(x => x.Id == id);
                otherItemExpenseModel = otherItemExpense.ToType<OtherExpense, OtherExpenseModel>();
            }

            return PartialView("OtherItemExpensePartial", otherItemExpenseModel);
        }
        [HttpPost]
        public ActionResult CreateUpdateOtherItemExpense(OtherExpenseModel otherItemExpenseModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            var otherItemExpense = new OtherExpense();
            if (otherItemExpenseModel.Id > 0)
            {
                otherItemExpense = FarmManagementEntities.OtherExpenses.Single(x => x.Id == otherItemExpenseModel.Id);
            }

            otherItemExpense.FarmId = otherItemExpenseModel.FarmId;
            otherItemExpense.AccountId = otherItemExpenseModel.AccountId;

            otherItemExpense.OtherItemId = otherItemExpenseModel.OtherItemId;
            otherItemExpense.Amount = otherItemExpenseModel.Amount;
            otherItemExpense.Description = otherItemExpenseModel.Description;
            otherItemExpense.Date = otherItemExpenseModel.Date;

            if (otherItemExpenseModel.Id == 0)
            {
                otherItemExpense.UserId = otherItemExpenseModel.UserId;
                FarmManagementEntities.OtherExpenses.Add(otherItemExpense);
            }

            FarmManagementEntities.SaveChanges();

            var message = string.Format(Constant.SuccessMessage, otherItemExpenseModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }

        public ActionResult DeleteOtherItemExpense(Int32 id)
        {
            var otherItemExpense = FarmManagementEntities.OtherExpenses.Single(x => x.Id == id);
            FarmManagementEntities.OtherExpenses.Remove(otherItemExpense);
            FarmManagementEntities.SaveChanges();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }

        public ActionResult GetFertilizers()
        {
            var fertilizers = FarmManagementEntities.Fertilizers.Select(x => new { Name = x.FertilizerName, Id = x.Id }).ToList();

            fertilizers.Add(new { Name = "Select", Id = 0 });

            fertilizers = fertilizers.OrderBy(x => x.Id).ToList();

            return Json(fertilizers, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCropNames()
        {
            var crops = FarmManagementEntities.Seeds.Select(x => new { Name = x.CropManagement.CropName, Id = x.Id }).ToList();

            crops.Add(new { Name = "Select", Id = 0 });

            crops = crops.OrderBy(x => x.Id).ToList();

            return Json(crops, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetFuelName()
        {
            var fuels = FarmManagementEntities.Fuels.Select(x => new { Name = x.FuelName, Id = x.Id }).ToList();

            fuels.Add(new { Name = "Select", Id = 0 });

            fuels = fuels.OrderBy(x => x.Id).ToList();

            return Json(fuels, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTotalSeed(Int32 seedId)
        {
            var seed = FarmManagementEntities.Seeds.Single(x => x.Id == seedId);
            var totalSeedExpenseUsed = FarmManagementEntities.SeedExpenses.Where(x => x.SeedId == seedId).ToList().Sum(x => x.QuantityInNumber);

            var leftSeed = seed.QuantityInNumber - totalSeedExpenseUsed;

            return Json(new
            {
                Quantity = seed.Quantity,
                Quality = seed.Quality,
                TotalLeftSeed = leftSeed
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTotalFertilizer(Int32 fertilizerId)
        {
            var fertilizer = FarmManagementEntities.Fertilizers.Single(x => x.Id == fertilizerId);
            var totalFertilizerExpenseUsed = FarmManagementEntities.FertilizerExpenses.Where(x => x.FertilizerId == fertilizerId).ToList().Sum(x => x.QuantityInNumber);

            var leftFertilizer = fertilizer.QuantityInNumber - totalFertilizerExpenseUsed;

            return Json(new
            {
                Quantity = fertilizer.Quantity,
                Quality = fertilizer.Quality,
                Type = fertilizer.Type,
                Brand = fertilizer.Brand,
                TotalLeftFertilizer = leftFertilizer

            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTotalFuel(Int32 fuelId)
        {
            var fuel = FarmManagementEntities.Fuels.Single(x => x.Id == fuelId);
            var totalFuelExpenseUsed = FarmManagementEntities.FuelExpenses.Where(x => x.FuelId == fuelId).ToList().Sum(x => x.Quantity);

            var leftFuel = fuel.QuantityInLiter - totalFuelExpenseUsed;

            return Json(new { TotalLeftFuel = leftFuel }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTotalOtherItem(Int32 itemId)
        {
            var otherItem = FarmManagementEntities.OtherItems.Single(x => x.ItemId == itemId);
            var totalOtherItemExpenseUsed = FarmManagementEntities.OtherExpenses.Where(x => x.OtherItemId == itemId).ToList().Sum(x => x.Amount);

            var leftOtherItem = otherItem.Price - totalOtherItemExpenseUsed;

            return Json(new { TotalLeftOtherItem = leftOtherItem }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AnimalExpense()
        {
            return View("OtherItemExpense");
        }

        public ActionResult VehicleExpense()
        {
            return View("OtherItemExpense");
        }

        public ActionResult MachineryExpense()
        {
            return View("OtherItemExpense");
        }
        
        public ActionResult PesticidesExpense()
        {
            return View("OtherItemExpense");
        }

        public ActionResult GeneralExpense()
        {
            return View("OtherItemExpense");
        }
    }
}
