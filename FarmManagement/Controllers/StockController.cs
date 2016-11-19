using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoGridUtilities;
using FarmManagement.Lib;
using FarmManagement.Models.ViewModel;
using FarmManagement.Models;
using System.Data.Objects.SqlClient;

namespace FarmManagement.Controllers
{
    public class StockController : BaseController
    {
        public ActionResult Seed()
        {
            return View();
        }
        public ActionResult SeedAjax(Int32 skip, Int32 take)
        {
            var allSeeds = FarmManagementEntities.Seeds.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                allSeeds = allSeeds.Where(x => x.UserId == LoggedInUser.UserId);
            }

            var seeds = allSeeds.OrderBy(x => x.Id).Select(x => new
                                                   {
                                                       x.Id,
                                                       x.Farm.FarmName,
                                                       x.Quality,
                                                       x.Quantity,
                                                       x.QuantityInNumber,
                                                       VendorName = x.VendorManagement.Name,
                                                       x.TotalPricePer,
                                                       x.PricePerItem,
                                                       x.DatePurchase,
                                                       x.CropManagement.CropName,
                                                       Expense = x.Account.Name,
                                                       x.PaymentBill,
                                                       x.OtherDescription,
                                                       TotalLeft = x.QuantityInNumber - (!x.SeedExpenses.Any() ? 0 : x.SeedExpenses.Sum(y => y.QuantityInNumber)),
                                                       IsDelete = x.SeedExpenses.Any()
                                                   });

            var result = seeds.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateUpdateSeed(Int32 id)
        {
            var seedModel = new SeedModel();
            seedModel.SeedGuid = Guid.NewGuid().ToString();
            seedModel.DatePurchase = DateTime.Now;

            if (id > 0)
            {
                var seed = FarmManagementEntities.Seeds.Single(x => x.Id == id);
                seedModel = seed.ToType<Seed, SeedModel>();

                seedModel.SeedQuality = Convert.ToInt32(seed.Quality.ParseEnum<Quality>());
                seedModel.SeedQuantity = Convert.ToInt32(seed.Quantity.ParseEnum<Quantity>());

                GetUploadFile(FileUploadPath.Seed, seed.Id, seed.PaymentBill);
                seedModel.SeedGuid = seed.Id.ToString();
            }

            return PartialView("SeedPartial", seedModel);
        }
        [HttpPost]
        public ActionResult CreateUpdateSeed(SeedModel seedModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            if (!CheckEmployeeHasBalance(seedModel.TotalPricePer))
            {
                return ShowErrorMessage(Constant.InsufficientBalance);
            }

            var seed = new Seed();
            if (seedModel.Id > 0)
            {
                seed = FarmManagementEntities.Seeds.Single(x => x.Id == seedModel.Id);
            }

            seed.FarmId = seedModel.FarmId;
            seed.VendorId = seedModel.VendorId;
            seed.AccountId = seedModel.AccountId;
            seed.CropId = seedModel.CropId;

            seed.DatePurchase = seedModel.DatePurchase;
            seed.OtherDescription = seedModel.OtherDescription;

            seed.Quality = seedModel.SeedQuality.ToNullIfEmptyEnum<Quality>();
            seed.Quantity = seedModel.SeedQuantity.ToNullIfEmptyEnum<Quantity>();

            var fileUpload = ClientSession.FileUploads.Single(x => x.FileUploadGuid == seedModel.SeedGuid && x.FileUploadName == FileUploadPath.Seed && !x.HasRemoved);
            seed.PaymentBill = System.IO.Path.GetFileName(fileUpload.FilePath);

            if (seedModel.Id == 0)
            {
                seed.TotalPricePer = seedModel.TotalPricePer;
                seed.PricePerItem = seedModel.PricePerItem;
                seed.QuantityInNumber = seedModel.QuantityInNumber;

                seed.InsertDate = DateTime.Now;
                seed.UserId = seedModel.UserId;
                FarmManagementEntities.Seeds.Add(seed);

                ManageEmployeeBalance(seedModel.TotalPricePer);
            }
            else
            {
                seed.UpdateDate = DateTime.Now;
            }

            FarmManagementEntities.SaveChanges();

            UpdateFileUploadPath(FileUploadPath.Seed, fileUpload, seed.Id);
            ClearAllFileUpload();

            var message = string.Format(Constant.SuccessMessage, seedModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }

        public ActionResult DeleteSeed(Int32 id)
        {
            var seed = FarmManagementEntities.Seeds.Single(x => x.Id == id);
            FarmManagementEntities.Seeds.Remove(seed);
            FarmManagementEntities.SaveChanges();

            ExtensionMethods.GetFileUploadPath(FileUploadPath.Seed.ToString(), id.ToString()).DeleteUploadFile();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }

        public ActionResult Vehicle()
        {
            return View();
        }
        public ActionResult VehicleAjax(Int32 skip, Int32 take)
        {
            var allVehicles = FarmManagementEntities.Vehicles.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                allVehicles = allVehicles.Where(x => x.UserId == LoggedInUser.UserId);
            }

            var vehicles = allVehicles.OrderBy(x => x.Id).Select(x => new
                                                          {
                                                              x.Id,
                                                              x.Farm.FarmName,
                                                              Expense = x.Account.Name,
                                                              x.Name,
                                                              x.Color,
                                                              x.Type,
                                                              x.Power,
                                                              x.RegistrationNo,
                                                              x.CompanyName,
                                                              x.Price,
                                                              x.ModelNo,
                                                              x.PurchaseDate,
                                                              VendorName = x.VendorManagement.Name,
                                                              x.OtherDescription,
                                                              x.Condition
                                                          });

            var result = vehicles.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateUpdateVehicle(Int32 id)
        {
            var vehicleModel = new VehicleModel();
            vehicleModel.VehicleGuid = Guid.NewGuid().ToString();
            vehicleModel.PurchaseDate = DateTime.Now;

            if (id > 0)
            {
                var vehicle = FarmManagementEntities.Vehicles.Single(x => x.Id == id);
                vehicleModel = vehicle.ToType<Vehicle, VehicleModel>();

                vehicleModel.VehicleType = Convert.ToInt32(vehicle.Type.ParseEnum<VehicleType>());
                vehicleModel.VehicleCondition = Convert.ToInt32(vehicle.Condition.ParseEnum<Condition>());

                GetUploadFile(FileUploadPath.VehicleLicenseFile, vehicle.Id, vehicle.LicenseFile);
                GetUploadFile(FileUploadPath.VehiclePicture, vehicle.Id, vehicle.Picture);
                vehicleModel.VehicleGuid = vehicle.Id.ToString();
            }

            return PartialView("VehiclePartial", vehicleModel);
        }
        [HttpPost]
        public ActionResult CreateUpdateVehicle(VehicleModel vehicleModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            if (!CheckEmployeeHasBalance(vehicleModel.Price))
            {
                return ShowErrorMessage(Constant.InsufficientBalance);
            }

            var vehicle = new Vehicle();
            if (vehicleModel.Id > 0)
            {
                vehicle = FarmManagementEntities.Vehicles.Single(x => x.Id == vehicleModel.Id);
            }

            vehicle.FarmId = vehicleModel.FarmId;
            vehicle.VendorId = vehicleModel.VendorId;
            vehicle.AccountId = vehicleModel.AccountId;

            vehicle.Name = vehicleModel.Name;
            vehicle.Color = vehicleModel.Color;
            vehicle.Power = vehicleModel.Power;
            vehicle.RegistrationNo = vehicleModel.RegistrationNo;
            vehicle.CompanyName = vehicleModel.CompanyName;
            vehicle.ModelNo = vehicleModel.ModelNo;
            vehicle.PurchaseDate = vehicleModel.PurchaseDate;
            vehicle.OtherDescription = vehicleModel.OtherDescription;

            vehicle.Condition = vehicleModel.VehicleCondition.ToNullIfEmptyEnum<Condition>();
            vehicle.Type = vehicleModel.VehicleType.ToNullIfEmptyEnum<VehicleType>();

            var fileUploadLicense = ClientSession.FileUploads.Single(x => x.FileUploadGuid == vehicleModel.VehicleGuid
                                                                       && x.FileUploadName == FileUploadPath.VehicleLicenseFile
                                                                       && !x.HasRemoved);
            vehicle.LicenseFile = System.IO.Path.GetFileName(fileUploadLicense.FilePath);

            var fileUploadPicture = ClientSession.FileUploads.Single(x => x.FileUploadGuid == vehicleModel.VehicleGuid
                                                                       && x.FileUploadName == FileUploadPath.VehiclePicture
                                                                       && !x.HasRemoved);
            vehicle.Picture = System.IO.Path.GetFileName(fileUploadPicture.FilePath);

            if (vehicleModel.Id == 0)
            {
                vehicle.Price = vehicleModel.Price;
                vehicle.InsertDate = DateTime.Now;
                vehicle.UserId = vehicleModel.UserId;
                FarmManagementEntities.Vehicles.Add(vehicle);

                ManageEmployeeBalance(vehicleModel.Price);
            }
            else
            {
                vehicle.UpdateDate = DateTime.Now;
            }

            FarmManagementEntities.SaveChanges();

            UpdateFileUploadPath(FileUploadPath.VehicleLicenseFile, fileUploadLicense, vehicle.Id);
            UpdateFileUploadPath(FileUploadPath.VehiclePicture, fileUploadPicture, vehicle.Id);
            ClearAllFileUpload();

            var message = string.Format(Constant.SuccessMessage, vehicleModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }

        public ActionResult DeleteVehicle(Int32 id)
        {
            var vechicle = FarmManagementEntities.Vehicles.Single(x => x.Id == id);
            FarmManagementEntities.Vehicles.Remove(vechicle);
            FarmManagementEntities.SaveChanges();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }

        public ActionResult Animal()
        {
            return View();
        }
        public ActionResult AnimalAjax(Int32 skip, Int32 take)
        {
            var allAnimals = FarmManagementEntities.Animals.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                allAnimals = allAnimals.Where(x => x.UserId == LoggedInUser.UserId);
            }

            var animals = allAnimals.OrderBy(x => x.Id).Select(x => new
                                                        {
                                                            x.Id,
                                                            x.Farm.FarmName,
                                                            Expense = x.Account.Name,
                                                            x.AnimalName,
                                                            x.Category,
                                                            x.Price,
                                                            x.PurchaseDate,
                                                            x.Age,
                                                            x.Color,
                                                            x.Description,
                                                            x.Photo,
                                                            VendorName = x.VendorManagement.Name,
                                                            x.FamilyName,
                                                            x.Sex,
                                                        });

            var result = animals.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateUpdateAnimal(Int32 id)
        {
            var animalModel = new AnimalModel();
            animalModel.AnimalGuid = Guid.NewGuid().ToString();
            animalModel.PurchaseDate = DateTime.Now;

            if (id > 0)
            {
                var animal = FarmManagementEntities.Animals.Single(x => x.Id == id);
                animalModel = animal.ToType<Animal, AnimalModel>();

                animalModel.AnimalGender = Convert.ToInt32(animal.Sex.ParseEnum<Gender>());
                animalModel.AnimalCategory = Convert.ToInt32(animal.Category.ParseEnum<AnimalCategory>());

                GetUploadFile(FileUploadPath.Animal, animal.Id, animal.Photo);
                animalModel.AnimalGuid = animal.Id.ToString();
            }

            return PartialView("AnimalPartial", animalModel);
        }
        [HttpPost]
        public ActionResult CreateUpdateAnimal(AnimalModel animalModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            if (!CheckEmployeeHasBalance(animalModel.Price))
            {
                return ShowErrorMessage(Constant.InsufficientBalance);
            }

            var animal = new Animal();
            if (animalModel.Id > 0)
            {
                animal = FarmManagementEntities.Animals.Single(x => x.Id == animalModel.Id);
            }

            animal.FarmId = animalModel.FarmId;
            animal.VendorId = animalModel.VendorId;
            animal.AccountId = animalModel.AccountId;

            animal.AnimalName = animalModel.AnimalName;
            animal.PurchaseDate = Convert.ToDateTime(animalModel.PurchaseDate);
            animal.Age = animalModel.Age;
            animal.Color = animalModel.Color;
            animal.Description = animalModel.Description;
            animal.FamilyName = animalModel.FamilyName;

            animal.Category = animalModel.AnimalCategory.ToNullIfEmptyEnum<AnimalCategory>();
            animal.Sex = animalModel.AnimalGender.ToNullIfEmptyEnum<Gender>();

            var fileUpload = ClientSession.FileUploads.Single(x => x.FileUploadGuid == animalModel.AnimalGuid && x.FileUploadName == FileUploadPath.Animal && !x.HasRemoved);
            animal.Photo = System.IO.Path.GetFileName(fileUpload.FilePath);

            if (animalModel.Id == 0)
            {
                animal.Price = animalModel.Price;
                animal.InsertDate = DateTime.Now;
                animal.UserId = animalModel.UserId;
                FarmManagementEntities.Animals.Add(animal);

                ManageEmployeeBalance(animalModel.Price);
            }
            else
            {
                animal.UpdateDate = DateTime.Now;
            }

            FarmManagementEntities.SaveChanges();

            UpdateFileUploadPath(FileUploadPath.Animal, fileUpload, animal.Id);
            ClearAllFileUpload();

            var message = string.Format(Constant.SuccessMessage, animalModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }

        public ActionResult DeleteAnimal(Int32 id)
        {
            var animal = FarmManagementEntities.Animals.Single(x => x.Id == id);
            FarmManagementEntities.Animals.Remove(animal);
            FarmManagementEntities.SaveChanges();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }

        public ActionResult Fertilizer()
        {
            return View();
        }
        public ActionResult FertilizerAjax(Int32 skip, Int32 take)
        {
            var allFertilizers = FarmManagementEntities.Fertilizers.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                allFertilizers = allFertilizers.Where(x => x.UserId == LoggedInUser.UserId);
            }

            var fertilizers = allFertilizers.OrderBy(x => x.Id)
                                            .Select(x => new
                                            {
                                                x.Id,
                                                x.Farm.FarmName,
                                                Expense = x.Account.Name,
                                                x.PricePerItem,
                                                x.Brand,
                                                x.FertilizerName,
                                                x.QuantityInNumber,
                                                x.Type,
                                                x.Quality,
                                                x.Quantity,
                                                VendorName = x.VendorManagement.Name,
                                                x.Price,
                                                x.DatePurchase,
                                                x.OtherDescription,
                                                TotalLeft = x.QuantityInNumber - (!x.FertilizerExpenses.Any() ? 0 : x.FertilizerExpenses.Sum(y => y.QuantityInNumber)),
                                                IsDelete = x.FertilizerExpenses.Any()
                                            });

            var result = fertilizers.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateUpdateFertilizer(Int32 id)
        {
            var fertilizerModel = new FertilizerModel();
            fertilizerModel.FertilizerGuid = Guid.NewGuid().ToString();
            fertilizerModel.DatePurchase = DateTime.Now;

            if (id > 0)
            {
                var fertilize = FarmManagementEntities.Fertilizers.Single(x => x.Id == id);
                fertilizerModel = fertilize.ToType<Fertilizer, FertilizerModel>();

                fertilizerModel.FertilizerQuality = Convert.ToInt32(fertilize.Quality.ParseEnum<Quality>());
                fertilizerModel.FertilizerQuantity = Convert.ToInt32(fertilize.Quantity.ParseEnum<FertilizerQuantity>());
                fertilizerModel.FertilizerType = Convert.ToInt32(fertilize.Type.ParseEnum<FertilizerType>());

                GetUploadFile(FileUploadPath.Fertilizer, fertilize.Id, fertilize.PaymentBill);
                fertilizerModel.FertilizerGuid = fertilize.Id.ToString();
            }

            return PartialView("FertilizerPartial", fertilizerModel);
        }
        [HttpPost]
        public ActionResult CreateUpdateFertilizer(FertilizerModel fertilizerModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            if (!CheckEmployeeHasBalance(fertilizerModel.Price))
            {
                return ShowErrorMessage(Constant.InsufficientBalance);
            }

            var fertilizer = new Fertilizer();
            if (fertilizerModel.Id > 0)
            {
                fertilizer = FarmManagementEntities.Fertilizers.Single(x => x.Id == fertilizerModel.Id);
            }

            fertilizer.FarmId = fertilizerModel.FarmId;
            fertilizer.VendorId = fertilizerModel.VendorId;
            fertilizer.AccountId = fertilizerModel.AccountId;

            fertilizer.FertilizerName = fertilizerModel.FertilizerName;
            fertilizer.DatePurchase = fertilizerModel.DatePurchase;
            fertilizer.PaymentBill = fertilizerModel.PaymentBill;
            fertilizer.OtherDescription = fertilizerModel.OtherDescription;
            fertilizer.Brand = fertilizerModel.Brand;

            fertilizer.Quality = fertilizerModel.FertilizerQuality.ToNullIfEmptyEnum<Quality>();
            fertilizer.Quantity = fertilizerModel.FertilizerQuantity.ToNullIfEmptyEnum<FertilizerQuantity>();
            fertilizer.Type = fertilizerModel.FertilizerType.ToNullIfEmptyEnum<FertilizerType>();

            var fileUpload = ClientSession.FileUploads.Single(x => x.FileUploadGuid == fertilizerModel.FertilizerGuid && x.FileUploadName == FileUploadPath.Fertilizer
                                                                && !x.HasRemoved);
            fertilizer.PaymentBill = System.IO.Path.GetFileName(fileUpload.FilePath);

            if (fertilizerModel.Id == 0)
            {
                fertilizer.Price = fertilizerModel.Price;
                fertilizer.PricePerItem = fertilizerModel.PricePerItem;
                fertilizer.QuantityInNumber = fertilizerModel.QuantityInNumber;

                fertilizer.InsertDate = DateTime.Now;
                fertilizer.UserId = fertilizerModel.UserId;
                FarmManagementEntities.Fertilizers.Add(fertilizer);

                ManageEmployeeBalance(fertilizerModel.Price);
            }
            else
            {
                fertilizer.UpdateDate = DateTime.Now;
            }

            FarmManagementEntities.SaveChanges();

            UpdateFileUploadPath(FileUploadPath.Fertilizer, fileUpload, fertilizer.Id);
            ClearAllFileUpload();

            var message = string.Format(Constant.SuccessMessage, fertilizerModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }

        public ActionResult DeleteFertilizer(Int32 id)
        {
            var seed = FarmManagementEntities.Fertilizers.Single(x => x.Id == id);
            FarmManagementEntities.Fertilizers.Remove(seed);
            FarmManagementEntities.SaveChanges();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }

        public ActionResult Machinery()
        {
            return View();
        }
        public ActionResult MachineryAjax(Int32 skip, Int32 take)
        {
            var allMachineries = FarmManagementEntities.Machineries.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                allMachineries = allMachineries.Where(x => x.UserId == LoggedInUser.UserId);
            }

            var machineries = allMachineries.OrderBy(x => x.Id).Select(x => new
                                                               {
                                                                   x.Id,
                                                                   x.Farm.FarmName,
                                                                   Expense = x.Account.Name,
                                                                   x.Name,
                                                                   x.Type,
                                                                   x.Color,
                                                                   x.RegistrationNo,
                                                                   x.ModelNo,
                                                                   x.CompanyName,
                                                                   x.Price,
                                                                   x.PurchaseDate,
                                                                   x.OtherDescription,
                                                                   x.Condition,
                                                                   VendorName = x.VendorManagement.Name
                                                               });

            var result = machineries.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateUpdateMachinery(Int32 id)
        {
            var machineryModel = new MachineryModel();
            machineryModel.MachineryGuid = Guid.NewGuid().ToString();
            machineryModel.PurchaseDate = DateTime.Now;

            if (id > 0)
            {
                var machinery = FarmManagementEntities.Machineries.Single(x => x.Id == id);
                machineryModel = machinery.ToType<Machinery, MachineryModel>();

                machineryModel.MachineCondition = Convert.ToInt32(machinery.Condition.ParseEnum<Condition>());

                GetUploadFile(FileUploadPath.MachineLicenseFile, machinery.Id, machinery.LicenseFile);
                GetUploadFile(FileUploadPath.MachinePicture, machinery.Id, machinery.Picture);
                machineryModel.MachineryGuid = machinery.Id.ToString();
            }

            return PartialView("MachineryPartial", machineryModel);
        }
        [HttpPost]
        public ActionResult CreateUpdateMachinery(MachineryModel machineryModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            if (!CheckEmployeeHasBalance(machineryModel.Price))
            {
                return ShowErrorMessage(Constant.InsufficientBalance);
            }

            var machinery = new Machinery();
            if (machineryModel.Id > 0)
            {
                machinery = FarmManagementEntities.Machineries.Single(x => x.Id == machineryModel.Id);
            }

            machinery.FarmId = machineryModel.FarmId;
            machinery.VendorId = machineryModel.VendorId;
            machinery.AccountId = machineryModel.AccountId;

            machinery.Name = machineryModel.Name;
            machinery.Color = machineryModel.Color;
            machinery.Type = machineryModel.Type;
            machinery.RegistrationNo = machineryModel.RegistrationNo;
            machinery.ModelNo = machineryModel.ModelNo;
            machinery.CompanyName = machineryModel.CompanyName;
            machinery.PurchaseDate = machineryModel.PurchaseDate;
            machinery.OtherDescription = machineryModel.OtherDescription;

            machinery.Condition = machineryModel.MachineCondition.ToNullIfEmptyEnum<Condition>();

            var fileUploadLicense = ClientSession.FileUploads.Single(x => x.FileUploadGuid == machineryModel.MachineryGuid
                                                                       && x.FileUploadName == FileUploadPath.MachineLicenseFile
                                                                       && !x.HasRemoved);
            machinery.LicenseFile = System.IO.Path.GetFileName(fileUploadLicense.FilePath);

            var fileUploadPicture = ClientSession.FileUploads.Single(x => x.FileUploadGuid == machineryModel.MachineryGuid
                                                                       && x.FileUploadName == FileUploadPath.MachinePicture
                                                                       && !x.HasRemoved);
            machinery.Picture = System.IO.Path.GetFileName(fileUploadPicture.FilePath);

            if (machineryModel.Id == 0)
            {
                machinery.Price = machineryModel.Price;
                machinery.InsertDate = DateTime.Now;
                machinery.UserId = machineryModel.UserId;
                FarmManagementEntities.Machineries.Add(machinery);

                ManageEmployeeBalance(machineryModel.Price);
            }
            else
            {
                machinery.UpdateDate = DateTime.Now;
            }

            FarmManagementEntities.SaveChanges();

            UpdateFileUploadPath(FileUploadPath.MachineLicenseFile, fileUploadLicense, machinery.Id);
            UpdateFileUploadPath(FileUploadPath.MachinePicture, fileUploadPicture, machinery.Id);
            ClearAllFileUpload();

            var message = string.Format(Constant.SuccessMessage, machineryModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }

        public ActionResult DeleteMachinery(Int32 id)
        {
            var machinery = FarmManagementEntities.Machineries.Single(x => x.Id == id);
            FarmManagementEntities.Machineries.Remove(machinery);
            FarmManagementEntities.SaveChanges();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }

        public ActionResult Fuel()
        {
            return View();
        }
        public ActionResult FuelAjax(Int32 skip, Int32 take)
        {
            var allFuels = FarmManagementEntities.Fuels.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                allFuels = allFuels.Where(x => x.UserId == LoggedInUser.UserId);
            }
            var fuels = allFuels.OrderBy(x => x.Id).Select(x => new
                                                   {
                                                       x.Id,
                                                       x.Farm.FarmName,
                                                       Expense = x.Account.Name,
                                                       x.FuelName,
                                                       x.DatePurchase,
                                                       x.QuantityInLiter,
                                                       x.PricePerLiter,
                                                       x.TotalPrice,
                                                       x.PaymentBill,
                                                       VendorName = x.VendorManagement.Name,
                                                       x.OtherDescription,
                                                       TotalLeft = x.QuantityInLiter - (!x.FuelExpenses.Any() ? 0 : x.FuelExpenses.Sum(y => y.Quantity)),
                                                       IsDelete = x.FuelExpenses.Any()
                                                   });

            var result = fuels.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateUpdateFuel(Int32 id)
        {
            var fuelModel = new FuelModel();
            fuelModel.FuelGuid = Guid.NewGuid().ToString();
            fuelModel.DatePurchase = DateTime.Now;

            if (id > 0)
            {
                var fuel = FarmManagementEntities.Fuels.Single(x => x.Id == id);
                fuelModel = fuel.ToType<Fuel, FuelModel>();

                GetUploadFile(FileUploadPath.Fuel, fuel.Id, fuel.PaymentBill);
                fuelModel.FuelGuid = fuel.Id.ToString();
            }

            return PartialView("FuelPartial", fuelModel);
        }
        [HttpPost]
        public ActionResult CreateUpdateFuel(FuelModel fuelModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            if (!CheckEmployeeHasBalance(fuelModel.TotalPrice))
            {
                return ShowErrorMessage(Constant.InsufficientBalance);
            }

            var fuel = new Fuel();
            if (fuelModel.Id > 0)
            {
                fuel = FarmManagementEntities.Fuels.Single(x => x.Id == fuelModel.Id);
            }

            fuel.FarmId = fuelModel.FarmId;
            fuel.VendorId = fuelModel.VendorId;
            fuel.AccountId = fuelModel.AccountId;

            fuel.FuelName = fuelModel.FuelName;
            fuel.DatePurchase = fuelModel.DatePurchase;
            fuel.OtherDescription = fuelModel.OtherDescription;

            var fileUpload = ClientSession.FileUploads.Single(x => x.FileUploadGuid == fuelModel.FuelGuid && x.FileUploadName == FileUploadPath.Fuel
                                                                && !x.HasRemoved);
            fuel.PaymentBill = System.IO.Path.GetFileName(fileUpload.FilePath);

            if (fuelModel.Id == 0)
            {
                fuel.TotalPrice = fuelModel.TotalPrice;
                fuel.QuantityInLiter = fuelModel.QuantityInLiter;
                fuel.PricePerLiter = fuelModel.PricePerLiter;

                fuel.InsertDate = DateTime.Now;
                fuel.UserId = fuelModel.UserId;
                FarmManagementEntities.Fuels.Add(fuel);

                ManageEmployeeBalance(fuelModel.TotalPrice);
            }
            else
            {
                fuel.UpdateDate = DateTime.Now;
            }

            FarmManagementEntities.SaveChanges();

            UpdateFileUploadPath(FileUploadPath.Fuel, fileUpload, fuel.Id);
            ClearAllFileUpload();

            var message = string.Format(Constant.SuccessMessage, fuelModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }

        public ActionResult DeleteFuel(Int32 id)
        {
            var fuel = FarmManagementEntities.Fuels.Single(x => x.Id == id);
            FarmManagementEntities.Fuels.Remove(fuel);
            FarmManagementEntities.SaveChanges();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }

        public ActionResult OtherItem()
        {
            return View();
        }
        public ActionResult OtherItemAjax(Int32 skip, Int32 take)
        {
            var allOtherItems = FarmManagementEntities.OtherItems.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                allOtherItems = allOtherItems.Where(x => x.UserId == LoggedInUser.UserId);
            }

            var otherItems = allOtherItems.OrderBy(x => x.ItemId).Select(x => new
                                                              {
                                                                  x.ItemId,
                                                                  x.Farm.FarmName,
                                                                  Expense = x.Account.Name,
                                                                  x.TitleExpense,
                                                                  x.Description,
                                                                  x.Price,
                                                                  x.DatePurchase,
                                                                  TotalLeft = x.Price - (!x.OtherExpenses.Any() ? 0 : x.OtherExpenses.Sum(y => y.Amount)),
                                                                  IsDelete = x.OtherExpenses.Any()
                                                              });

            var result = otherItems.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateUpdateOtherItem(Int32 id)
        {
            var otherItemModel = new OtherItemModel();
            otherItemModel.DatePurchase = DateTime.Now;

            if (id > 0)
            {
                var otherItem = FarmManagementEntities.OtherItems.Single(x => x.ItemId == id);
                otherItemModel = otherItem.ToType<OtherItem, OtherItemModel>();
            }

            return PartialView("OtherItemPartial", otherItemModel);
        }
        [HttpPost]
        public ActionResult CreateUpdateOtherItem(OtherItemModel otherItemModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            if (!CheckEmployeeHasBalance(otherItemModel.Price))
            {
                return ShowErrorMessage(Constant.InsufficientBalance);
            }

            var otherItem = new OtherItem();
            if (otherItemModel.Id > 0)
            {
                otherItem = FarmManagementEntities.OtherItems.Single(x => x.ItemId == otherItemModel.Id);
            }

            otherItem.FarmId = otherItemModel.FarmId;
            otherItem.AccountId = otherItemModel.AccountId;

            otherItem.TitleExpense = otherItemModel.TitleExpense;
            otherItem.Description = otherItemModel.Description;
            otherItem.DatePurchase = otherItemModel.DatePurchase;

            if (otherItemModel.Id == 0)
            {
                otherItem.Price = otherItemModel.Price;

                otherItem.InsertDate = DateTime.Now;
                otherItem.UserId = otherItemModel.UserId;
                FarmManagementEntities.OtherItems.Add(otherItem);

                ManageEmployeeBalance(otherItemModel.Price);
            }
            else
            {
                otherItem.UpdateDate = DateTime.Now;
            }

            FarmManagementEntities.SaveChanges();

            var message = string.Format(Constant.SuccessMessage, otherItemModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }

        public ActionResult DeleteOtherItem(Int32 id)
        {
            var otherItem = FarmManagementEntities.OtherItems.Single(x => x.ItemId == id);
            FarmManagementEntities.OtherItems.Remove(otherItem);
            FarmManagementEntities.SaveChanges();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }

        public ActionResult GetOtherItem()
        {
            var otherItems = FarmManagementEntities.OtherItems.Select(x => new { Name = x.TitleExpense, ItemId = x.ItemId }).ToList();
            otherItems.Add(new { Name = "Select", ItemId = 0 });
            otherItems = otherItems.OrderBy(x => x.ItemId).ToList();

            return Json(otherItems, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAnimalCategory()
        {
            var animalCategory = ExtensionMethods.ToSelectList<AnimalCategory>();
            return Json(animalCategory, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAnimalGender()
        {
            var animalGender = ExtensionMethods.ToSelectList<Gender>();
            return Json(animalGender, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetQuantity()
        {
            var quantity = ExtensionMethods.ToSelectList<Quantity>();
            return Json(quantity, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetQuality()
        {
            var quality = ExtensionMethods.ToSelectList<Quality>();
            return Json(quality, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetFertilizerQuantity()
        {
            var fertilizerQuantity = ExtensionMethods.ToSelectList<FertilizerQuantity>();
            return Json(fertilizerQuantity, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetFertilizerType()
        {
            var fertilizerType = ExtensionMethods.ToSelectList<FertilizerType>();
            return Json(fertilizerType, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCondition()
        {
            var conditions = ExtensionMethods.ToSelectList<Condition>();
            return Json(conditions, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetVehicleType()
        {
            var vehicleTypes = ExtensionMethods.ToSelectList<VehicleType>();
            return Json(vehicleTypes, JsonRequestBehavior.AllowGet);
        }

        private bool CheckEmployeeHasBalance(decimal amount)
        {
            var account = FarmManagementEntities.TransectionPersonalAccounts.Where(x => x.UserId == LoggedInUser.UserId)
                                                .OrderByDescending(x => x.InsertDate).FirstOrDefault();
            if (account == null)
            {
                return false;
            }
            if (account.Balance < amount)
            {
                return false;
            }

            return true;
        }

        private void ManageEmployeeBalance(decimal amount)
        {
            var lastTransAccount = FarmManagementEntities.TransectionPersonalAccounts.Where(x => x.UserId == LoggedInUser.UserId)
                                                                                     .OrderByDescending(x => x.InsertDate).FirstOrDefault();

            var transectionAccount = new TransectionPersonalAccount
            {
                UserId = LoggedInUser.UserId,
                Balance = (lastTransAccount == null ? 0 : lastTransAccount.Balance) - amount,
                Debit = amount,
                Date = DateTime.Now,
                InsertDate = DateTime.Now
            };

            FarmManagementEntities.TransectionPersonalAccounts.Add(transectionAccount);
        }

        public void UpdateFileUploadPath(FileUploadPath uploadPath, FileUpload fileUpload, Int32 id)
        {
            var oldPath = Path.GetDirectoryName(fileUpload.FilePath);
            if (System.IO.Directory.Exists(oldPath))
            {
                var newPath = ExtensionMethods.GetFileUploadPath(uploadPath.ToString(), id.ToString());
                if (oldPath != newPath)
                {
                    newPath.EnusreDirectoryExist();
                    System.IO.File.Move(fileUpload.FilePath, Path.Combine(newPath, Path.GetFileName(fileUpload.FilePath)));
                }
            }
        }

        public void GetUploadFile(FileUploadPath uploadPath, Int32 id, string fileName)
        {
            var fileUploads = GetFileUploads();
            var newFileUpload = new FileUpload
            {
                FilePath = ExtensionMethods.GetFileUploadPath(uploadPath.ToString(), id.ToString()) + "\\" + fileName,
                FileUploadGuid = id.ToString(),
                FileUploadId = Guid.NewGuid().ToString(),
                FileUploadName = uploadPath
            };
            fileUploads.Add(newFileUpload);
        }

        public List<FileUpload> GetFileUploads()
        {
            if (ClientSession.FileUploads == null)
            {
                ClientSession.FileUploads = new List<FileUpload>();
            }

            return ClientSession.FileUploads;
        }

        private void ClearAllFileUpload()
        {
            var filePaths = GetFileUploads().Where(x => x.HasRemoved).ToList();
            if (filePaths.Count > 0)
            {
                filePaths.ForEach(x => { if (System.IO.File.Exists(x.FilePath)) System.IO.File.Delete(x.FilePath); });
            }

            FarmManagement.Lib.ClientSession.Clear();
        }
    }
}
