using FarmManagement.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FarmManagement.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult DashBoard()
        {
            return View(GetEmployeeDashBoard());
        }

        private List<DashBoard> GetEmployeeDashBoard()
        {
            var dashBoards = new List<DashBoard>();

            var totalSeeds = FarmManagementEntities.Seeds.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                totalSeeds = totalSeeds.Where(x => x.UserId == LoggedInUser.UserId);
            }
            if (totalSeeds.Count() > 0)
            {
                var lastSeed = totalSeeds.OrderByDescending(x => x.InsertDate).FirstOrDefault();
                var lastSeedBuyDate = lastSeed == null ? (DateTime?)null : lastSeed.InsertDate;
                dashBoards.Add(new DashBoard { Name = "Seed", Amount = totalSeeds.Sum(x => x.TotalPricePer), Total = totalSeeds.Count(), LastBuyDate = lastSeedBuyDate, Color = "cyan" });
            }

            var totalFuels = FarmManagementEntities.Fuels.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                totalFuels = totalFuels.Where(x => x.UserId == LoggedInUser.UserId);
            }

            if (totalFuels.Count() > 0)
            {
                var lastFuel = totalFuels.OrderByDescending(x => x.InsertDate).FirstOrDefault();
                var lastFuelBuyDate = lastFuel == null ? (DateTime?)null : lastFuel.InsertDate;
                dashBoards.Add(new DashBoard { Name = "Fuel", Amount = totalFuels.Sum(x => x.TotalPrice), Total = totalFuels.Count(), LastBuyDate = lastFuelBuyDate, Color = "red" });
            }

            var totalFertilizers = FarmManagementEntities.Fertilizers.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                totalFertilizers = totalFertilizers.Where(x => x.UserId == LoggedInUser.UserId);
            }
            if (totalFertilizers.Count() > 0)
            {
                var lastFertilizer = totalFertilizers.OrderByDescending(x => x.InsertDate).FirstOrDefault();
                var lastFertilizerBuyDate = lastFertilizer == null ? (DateTime?)null : lastFertilizer.InsertDate;
                dashBoards.Add(new DashBoard { Name = "Fertilizer", Amount = totalFertilizers.Sum(x => x.Price), Total = totalFertilizers.Count(), LastBuyDate = lastFertilizerBuyDate, Color = "green" });
            }

            var totalAnimals = FarmManagementEntities.Animals.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                totalAnimals = totalAnimals.Where(x => x.UserId == LoggedInUser.UserId);
            }
            if (totalAnimals.Count() > 0)
            {
                var lastAnimal = totalAnimals.OrderByDescending(x => x.InsertDate).FirstOrDefault();
                var lastAnimalBuyDate = lastAnimal == null ? (DateTime?)null : lastAnimal.InsertDate;
                dashBoards.Add(new DashBoard { Name = "Animal", Amount = totalAnimals.Sum(x => x.Price), Total = totalAnimals.Count(), LastBuyDate = lastAnimalBuyDate, Color = "cyan" });
            }

            var totalMachineries = FarmManagementEntities.Machineries.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                totalMachineries = totalMachineries.Where(x => x.UserId == LoggedInUser.UserId);
            }
            if (totalMachineries.Count() > 0)
            {
                var lastMachinery = totalMachineries.OrderByDescending(x => x.InsertDate).FirstOrDefault();
                var lastMachineryBuyDate = lastMachinery == null ? (DateTime?)null : lastMachinery.InsertDate;
                dashBoards.Add(new DashBoard { Name = "Machinery", Amount = totalMachineries.Sum(x => x.Price), Total = totalMachineries.Count(), LastBuyDate = lastMachineryBuyDate, Color = "red" });
            }

            var totalOtherItems = FarmManagementEntities.OtherItems.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                totalOtherItems = totalOtherItems.Where(x => x.UserId == LoggedInUser.UserId);
            }
            if (totalOtherItems.Count() > 0)
            {
                var lastOtherItem = totalOtherItems.OrderByDescending(x => x.InsertDate).FirstOrDefault();
                var lastOtherItemBuyDate = lastOtherItem == null ? (DateTime?)null : lastOtherItem.InsertDate;
                dashBoards.Add(new DashBoard { Name = "OtherItem", Amount = totalOtherItems.Sum(x => x.Price), Total = totalOtherItems.Count(), LastBuyDate = lastOtherItemBuyDate, Color = "green" });
            }

            var totalPermanentEmployee = FarmManagementEntities.Users.Where(x => !x.IsTemporaryEmployee).AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                totalPermanentEmployee = totalPermanentEmployee.Where(x => x.Id == LoggedInUser.UserId);
            }
            dashBoards.Add(new DashBoard { Name = "Permanent Employee", Total = totalPermanentEmployee.Count(), Color = "cyan" });

            var totalTemporaryEmloyee = FarmManagementEntities.Users.Where(x => x.IsTemporaryEmployee).AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                totalTemporaryEmloyee = totalTemporaryEmloyee.Where(x => x.Id == LoggedInUser.UserId);
            }
            dashBoards.Add(new DashBoard { Name = "Temporary Employee", Total = totalTemporaryEmloyee.Count(), Color = "red" });

            var transAccount = FarmManagementEntities.TransectionPersonalAccounts.Where(x => x.UserId == LoggedInUser.UserId)
                                                     .OrderByDescending(x => x.InsertDate).FirstOrDefault();
            var balance = transAccount != null ? transAccount.Balance : 0;

            dashBoards.Add(new DashBoard { Name = "Balance", Total = balance, Color = "green" });

            return dashBoards;
        }
    }
}
