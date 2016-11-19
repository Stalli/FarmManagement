using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using FarmManagement.Lib;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FarmManagement.Models.ViewModel;

namespace FarmManagement.Controllers
{
    public class ReportController : BaseController
    {
        public ActionResult TaskAssignmentReport()
        {
            return View();
        }
        public ActionResult GetTaskAssignmentReport()
        {
            var filter = GetFilter();
            var status = Request.Params["filter[Status]"].ParseIntegerToEnum<TaskStatus>();
            var remarks = Request.Params["filter[Remarks]"].ParseIntegerToEnum<TaskRemarks>();

            var allTaskAssignments = FarmManagementEntities.TaskAssignments.AsQueryable();
            allTaskAssignments = allTaskAssignments.Where(x => x.InsertDate >= filter.StartDate && x.InsertDate < filter.EndDate);

            if (filter.UserId > 0)
            {
                allTaskAssignments = allTaskAssignments.Where(x => x.StaffNameId == filter.UserId);
            }
            if (!string.IsNullOrEmpty(status))
            {
                allTaskAssignments = allTaskAssignments.Where(x => x.Status == status);
            }
            if (!string.IsNullOrEmpty(remarks))
            {
                allTaskAssignments = allTaskAssignments.Where(x => x.Remarks == remarks);
            }

            var taskAssignments = allTaskAssignments.Select(x => new
            {
                x.TaskName,
                x.DeadlineStartDate,
                x.DeadlineEndDate,
                StaffName = x.User.Name,
                TaskStatusReport = x.Status,
                TaskRemarkReport = x.Remarks
            }).ToList<object>();

            ReportDocument report = GetReportDocument<object>(taskAssignments, "TaskAssignment");
            DownloadReport(report, filter.PrintType, "TaskAssignment");

            return ShowSuccessMessage("Successful");
        }

        public ActionResult SeedReport()
        {
            return View();
        }
        public ActionResult GetSeedReport()
        {
            var filter = GetFilter();

            var allSeeds = FarmManagementEntities.Seeds.AsQueryable();
            allSeeds = allSeeds.Where(x => x.InsertDate >= filter.StartDate && x.InsertDate < filter.EndDate);

            if (filter.UserId > 0)
            {
                allSeeds = allSeeds.Where(x => x.UserId == filter.UserId);
            }
            if (!string.IsNullOrEmpty(filter.Quality))
            {
                allSeeds = allSeeds.Where(x => x.Quality == filter.Quality);
            }
            if (!string.IsNullOrEmpty(filter.Quantity))
            {
                allSeeds = allSeeds.Where(x => x.Quantity == filter.Quantity);
            }

            var seeds = allSeeds.Select(x => new
            {
                x.DatePurchase,
                x.CropManagement.CropName,
                x.Quality,
                x.Quantity,
                x.User.Name,
                x.TotalPricePer,
                x.QuantityInNumber,
                VendorName = x.VendorManagement.Name,
            }).ToList<object>();

            ReportDocument report = GetReportDocument<object>(seeds, "Seed");
            DownloadReport(report, filter.PrintType, "Seed");

            return ShowSuccessMessage("Successful");
        }

        public ActionResult FertilizerReport()
        {
            return View();
        }
        public ActionResult GetFertilizerReport()
        {
            var filter = GetFilter();
            var type = Request.Params["filter[Type]"].ParseIntegerToEnum<FertilizerType>();

            var allFertilizers = FarmManagementEntities.Fertilizers.AsQueryable();
            allFertilizers = allFertilizers.Where(x => x.InsertDate >= filter.StartDate && x.InsertDate < filter.EndDate);

            if (filter.UserId > 0)
            {
                allFertilizers = allFertilizers.Where(x => x.UserId == filter.UserId);
            }
            if (!string.IsNullOrEmpty(filter.Quality))
            {
                allFertilizers = allFertilizers.Where(x => x.Quality == filter.Quality);
            }
            if (!string.IsNullOrEmpty(filter.Quantity))
            {
                allFertilizers = allFertilizers.Where(x => x.Quantity == filter.Quantity);
            }
            if (!string.IsNullOrEmpty(type))
            {
                allFertilizers = allFertilizers.Where(x => x.Type == type);
            }

            var fertilizers = allFertilizers.Select(x => new
            {
                x.DatePurchase,
                x.Brand,
                x.FertilizerName,
                x.Type,
                x.Quantity,
                x.Quality,
                x.User.Name,
                x.Price,
                x.QuantityInNumber,
                x.Farm.FarmName,
                VendorName = x.VendorManagement.Name
            }).ToList<object>();

            ReportDocument report = GetReportDocument<object>(fertilizers, "Fertilizer");
            DownloadReport(report, filter.PrintType, "Fertilizer");

            return ShowSuccessMessage("Successful");
        }

        public ActionResult FuelReport()
        {
            return View();
        }
        public ActionResult GetFuelReport()
        {
            var filter = GetFilter();

            var allFuel = FarmManagementEntities.Fuels.AsQueryable();
            allFuel = allFuel.Where(x => x.InsertDate >= filter.StartDate && x.InsertDate < filter.EndDate);

            if (filter.UserId > 0)
            {
                allFuel = allFuel.Where(x => x.UserId == filter.UserId);
            }

            var fuels = allFuel.Select(x => new
            {
                x.DatePurchase,
                x.FuelName,
                x.QuantityInLiter,
                x.PricePerLiter,
                x.TotalPrice,
                UserName = x.User.Name,
                x.Farm.FarmName,
                VendorName = x.VendorManagement.Name
            }).ToList<object>();

            ReportDocument report = GetReportDocument<object>(fuels, "Fuel");
            DownloadReport(report, filter.PrintType, "Fuel");

            return ShowSuccessMessage("Successful");
        }

        public ActionResult AnimalReport()
        {
            return View();
        }
        public ActionResult GetAnimalReport()
        {
            var filter = GetFilter();

            var allAnimals = FarmManagementEntities.Animals.AsQueryable();
            allAnimals = allAnimals.Where(x => x.InsertDate >= filter.StartDate && x.InsertDate < filter.EndDate);

            var category = Request.Params["filter[Category]"].ParseIntegerToEnum<AnimalCategory>();
            var gender = Request.Params["filter[Gender]"].ParseIntegerToEnum<Gender>();

            if (filter.UserId > 0)
            {
                allAnimals = allAnimals.Where(x => x.UserId == filter.UserId);
            }
            if (!string.IsNullOrEmpty(category))
            {
                allAnimals = allAnimals.Where(x => x.Category == category);
            }
            if (!string.IsNullOrEmpty(gender))
            {
                allAnimals = allAnimals.Where(x => x.Category == gender);
            }

            var animals = allAnimals.Select(x => new
            {
                x.PurchaseDate,
                x.AnimalName,
                x.Category,
                x.Price,
                x.Sex,
                x.Age,
                x.Color,
                x.User.Name,
                x.Farm.FarmName,
                VendorName = x.VendorManagement.Name
            }).ToList<object>();

            ReportDocument report = GetReportDocument<object>(animals, "Animal");
            DownloadReport(report, filter.PrintType, "Animal");

            return ShowSuccessMessage("Successful");
        }

        public ActionResult MachineryReport()
        {
            return View();
        }
        public ActionResult GetMachineryReport()
        {
            var filter = GetFilter();

            var allMachineries = FarmManagementEntities.Machineries.AsQueryable();
            allMachineries = allMachineries.Where(x => x.InsertDate >= filter.StartDate && x.InsertDate < filter.EndDate);

            var condition = Request.Params["filter[Condition]"].ParseIntegerToEnum<Condition>();

            if (filter.UserId > 0)
            {
                allMachineries = allMachineries.Where(x => x.UserId == filter.UserId);
            }
            if (!string.IsNullOrEmpty(condition))
            {
                allMachineries = allMachineries.Where(x => x.Condition == condition);
            }

            var machineries = allMachineries.Select(x => new
            {
                x.PurchaseDate,
                x.Name,
                x.Color,
                x.Type,
                x.RegistrationNo,
                x.ModelNo,
                x.CompanyName,
                x.Price,
                x.Condition,
                UserName = x.User.Name,
                x.Farm.FarmName,
                VendorName = x.VendorManagement.Name,
            }).ToList<object>();

            ReportDocument report = GetReportDocument<object>(machineries, "Machinery");
            DownloadReport(report, filter.PrintType, "Machinery");

            return ShowSuccessMessage("Successful");
        }

        public ActionResult VehicleReport()
        {
            return View();
        }
        public ActionResult GetVehicleReport()
        {
            var filter = GetFilter();

            var allVehicles = FarmManagementEntities.Vehicles.AsQueryable();
            allVehicles = allVehicles.Where(x => x.InsertDate >= filter.StartDate && x.InsertDate < filter.EndDate);

            var type = Request.Params["filter[Type]"].ParseIntegerToEnum<VehicleType>();
            var condition = Request.Params["filter[Condition]"].ParseIntegerToEnum<Condition>();

            if (filter.UserId > 0)
            {
                allVehicles = allVehicles.Where(x => x.UserId == filter.UserId);
            }
            if (!string.IsNullOrEmpty(type))
            {
                allVehicles = allVehicles.Where(x => x.Type == type);
            }
            if (!string.IsNullOrEmpty(condition))
            {
                allVehicles = allVehicles.Where(x => x.Condition == condition);
            }

            var vehicles = allVehicles.Select(x => new
            {
                x.PurchaseDate,
                x.Name,
                x.Color,
                x.Type,
                x.Power,
                x.RegistrationNo,
                x.ModelNo,
                x.CompanyName,
                x.Price,
                x.Condition,
                x.Farm.FarmName,
                UserName = x.User.Name,
                VendorName = x.VendorManagement.Name,
            }).ToList<object>();

            ReportDocument report = GetReportDocument<object>(vehicles, "Vehicle");
            DownloadReport(report, filter.PrintType, "Vehicle");

            return ShowSuccessMessage("Successful");
        }

        public ActionResult OtherItemReport()
        {
            return View();
        }
        public ActionResult GetOtherItemReport()
        {
            var filter = GetFilter();

            var allOtherItems = FarmManagementEntities.OtherItems.AsQueryable();
            allOtherItems = allOtherItems.Where(x => x.InsertDate >= filter.StartDate && x.InsertDate < filter.EndDate);

            if (filter.UserId > 0)
            {
                allOtherItems = allOtherItems.Where(x => x.UserId == filter.UserId);
            }

            var otherItems = allOtherItems.Select(x => new
            {
                x.TitleExpense,
                x.Price,
                x.Description,
                x.DatePurchase,
                x.Farm.FarmName,
                UserName = x.User.Name,
            }).ToList<object>();

            ReportDocument report = GetReportDocument<object>(otherItems, "OtherItem");
            DownloadReport(report, filter.PrintType, "OtherItem");

            return ShowSuccessMessage("Successful");
        }

        public ActionResult ExpenseReport()
        {
            return View();
        }
        public ActionResult GetExpenseReport()
        {
            var filter = GetFilter();
            var expense = Request.Params["filter[Expense]"];
            if (!string.IsNullOrEmpty(expense))
            {
                expense = ((Expense)Request.Params["filter[Expense]"].ParseEnum<Expense>()).ToString();
            }


            var expenses = new List<ExpenseReportModel>();

            var allSeedExpenses = FarmManagementEntities.SeedExpenses.AsQueryable();
            allSeedExpenses = allSeedExpenses.Where(x => x.Date >= filter.StartDate && x.Date < filter.EndDate);

            allSeedExpenses.ToList().ForEach(x =>
            {
                expenses.Add(new ExpenseReportModel
                {
                    AccountName = x.Account.Name,
                    FarmName = x.Farm.FarmName,
                    PayeeName = x.User.Name,
                    UserId = x.UserId,
                    Description = x.Description,
                    Date = x.Date,
                    Total = x.QuantityInNumber * x.Seed.PricePerItem,
                    ExpenseName = Expense.Seed.ToString()
                });
            });

            var allFuelExpenses = FarmManagementEntities.FuelExpenses.AsQueryable();
            allFuelExpenses = allFuelExpenses.Where(x => x.Date >= filter.StartDate && x.Date < filter.EndDate);

            allFuelExpenses.ToList().ForEach(x =>
            {
                expenses.Add(new ExpenseReportModel
                {
                    AccountName = x.Account.Name,
                    FarmName = x.Farm.FarmName,
                    PayeeName = x.User.Name,
                    UserId = x.UserId,
                    Description = x.Description,
                    Date = x.Date,
                    Total = x.Quantity * x.Fuel.PricePerLiter,
                    ExpenseName = Expense.Fuel.ToString()
                });
            });

            var allFertilizerExpenses = FarmManagementEntities.FertilizerExpenses.AsQueryable();
            allFertilizerExpenses = allFertilizerExpenses.Where(x => x.Date >= filter.StartDate && x.Date < filter.EndDate);

            allFertilizerExpenses.ToList().ForEach(x =>
            {
                expenses.Add(new ExpenseReportModel
                {
                    AccountName = x.Account.Name,
                    FarmName = x.Farm.FarmName,
                    PayeeName = x.User.Name,
                    UserId = x.UserId,
                    Description = x.Description,
                    Date = x.Date,
                    Total = x.QuantityInNumber * x.Fertilizer.PricePerItem,
                    ExpenseName = Expense.Fertilizer.ToString()
                });
            });

            var allOtherItemExpenses = FarmManagementEntities.OtherExpenses.AsQueryable();
            allOtherItemExpenses = allOtherItemExpenses.Where(x => x.Date >= filter.StartDate && x.Date < filter.EndDate);

            allOtherItemExpenses.ToList().ForEach(x =>
            {
                expenses.Add(new ExpenseReportModel
                {
                    AccountName = x.Account.Name,
                    FarmName = x.Farm.FarmName,
                    PayeeName = x.User.Name,
                    UserId = x.UserId,
                    Description = x.Description,
                    Date = x.Date,
                    Total = x.Amount,
                    ExpenseName = Expense.OtherItem.ToString()
                });
            });

            if (filter.UserId > 0)
            {
                expenses = expenses.Where(x => x.UserId == filter.UserId).ToList();
            }

            if (!string.IsNullOrEmpty(expense))
            {
                expenses = expenses.Where(x => x.ExpenseName.Contains(expense)).ToList();
            }

            ReportDocument report = GetReportDocument<ExpenseReportModel>(expenses, "ExpenseSheet");

            var txtStartDate = (TextObject)(report.ReportDefinition.ReportObjects["txtStartDate"]);
            txtStartDate.Text = filter.StartDate.ToShortDateString();

            var txtEndDate = (TextObject)(report.ReportDefinition.ReportObjects["txtEndDate"]);
            txtEndDate.Text = filter.EndDate.AddDays(1).ToShortDateString();

            DownloadReport(report, filter.PrintType, "ExpenseSheet");

            return ShowSuccessMessage("Successful");
        }

        public ActionResult LoanManagementReport()
        {
            return View();
        }
        public ActionResult GetLoanManagementReport()
        {
            var filter = GetFilter();

            var allLoanManagements = FarmManagementEntities.LoanManagements.AsQueryable();
            allLoanManagements = allLoanManagements.Where(x => x.InsertDate >= filter.StartDate && x.InsertDate < filter.EndDate);

            if (filter.UserId > 0)
            {
                allLoanManagements = allLoanManagements.Where(x => x.UserId == filter.UserId);
            }

            var loanManagements = allLoanManagements.Select(x => new
            {
                x.LoanAmount,
                x.IntersetRate,
                x.NoOfInstallment,
                x.LoanStartDate,
                x.LoanEndDate,
                LoanStatus = x.Status,
                x.User.Name,
            }).ToList<object>();

            ReportDocument report = GetReportDocument<object>(loanManagements, "LoanManagement");
            DownloadReport(report, filter.PrintType, "LoanManagement");

            return ShowSuccessMessage("Successful");
        }

        public ActionResult PaidLoanReport()
        {
            return View();
        }
        public ActionResult GetPaidLoanReport()
        {
            var filter = GetFilter();

            var allPaidLoans = FarmManagementEntities.PaidLoans.AsQueryable();
            var year = Request.Params["filter[Year]"].ToInt32();
            var month = Request.Params["filter[Month]"].ToInt32();

            allPaidLoans = allPaidLoans.Where(x => x.Year == year && x.Month == month);

            if (filter.UserId > 0)
            {
                allPaidLoans = allPaidLoans.Where(x => x.UserId == filter.UserId);
            }

            var paidLoans = allPaidLoans.Select(x => new
            {
                x.Date,
                x.Year,
                x.Month,
                x.Amount,
                x.LoanId,
                x.User.Name,
            }).ToList<object>();

            ReportDocument report = GetReportDocument<object>(paidLoans, "PaidLoan");
            DownloadReport(report, filter.PrintType, "PaidLoan");

            return ShowSuccessMessage("Successful");
        }

        public ActionResult TransectionPersonalReport()
        {
            return View();
        }
        public ActionResult GetTransectionPersonalReport()
        {
            var filter = GetFilter();

            var allTransectionPersonal = FarmManagementEntities.TransectionPersonalAccounts.AsQueryable();
            allTransectionPersonal = allTransectionPersonal.Where(x => x.Date >= filter.StartDate && x.Date < filter.EndDate);

            if (filter.UserId > 0)
            {
                allTransectionPersonal = allTransectionPersonal.Where(x => x.UserId == filter.UserId);
            }

            var transectionPersonal = allTransectionPersonal.Select(x => new
            {
                x.Date,
                Amount = x.Balance,
                Credit = x.Credit ?? 0,
                Debit = x.Debit ?? 0,
                x.User.Name,
            }).ToList<object>();

            ReportDocument report = GetReportDocument<object>(transectionPersonal, "TransectionPersonal");

            var txtFromDate = (TextObject)(report.ReportDefinition.ReportObjects["txtFromDate"]);
            var txtToDate = (TextObject)(report.ReportDefinition.ReportObjects["txtToDate"]);

            txtFromDate.Text = filter.StartDate.ToShortDateString();
            txtToDate.Text = filter.EndDate.AddDays(-1).ToShortDateString();

            DownloadReport(report, filter.PrintType, "TransectionPersonal");

            return ShowSuccessMessage("Successful");
        }

        public ActionResult PermanentSalaryReport()
        {
            return View();
        }
        public ActionResult GetPermanentSalaryReport()
        {
            var filter = GetFilter();

            var allPermanentSalaries = FarmManagementEntities.PermanentEmployeeSalaries.AsQueryable();
            var year = Request.Params["filter[Year]"].ToInt32();
            var month = Request.Params["filter[Month]"].ToInt32();

            allPermanentSalaries = allPermanentSalaries.Where(x => x.Year == year && x.Month == month);

            if (filter.UserId > 0)
            {
                allPermanentSalaries = allPermanentSalaries.Where(x => x.UserId == filter.UserId);
            }

            var permanentSalaries = allPermanentSalaries.Select(x => new
            {
                Year = x.Year,
                Month = x.Month,
                BasicSalary = x.BasicSalary,
                EmpBonus = x.Bonus,
                EmpHouseRent = x.HouseRent == null ? 0 : x.HouseRent,
                EmpAllowance = x.UtilityAllowance == null ? 0 : x.UtilityAllowance,
                EmpFund = x.ProvidentFund == null ? 0 : x.ProvidentFund,
                EmpMedical = x.MedicalAllowance == null ? 0 : x.MedicalAllowance,
                EmpTADA = x.TA_DA == null ? 0 : x.TA_DA,
                EmpPenality = x.Penality ?? 0,
                Name = x.User.Name,

                SumGrossSalary = x.BasicSalary + (x.HouseRent == null ? 0 : x.HouseRent) + (x.UtilityAllowance == null ? 0 : x.UtilityAllowance),
                TotalAllowance = x.Bonus + (x.MedicalAllowance == null ? 0 : x.MedicalAllowance) + (x.TA_DA == null ? 0 : x.TA_DA),
                TotalDeduction = (x.ProvidentFund == null ? 0 : x.ProvidentFund) + (x.Penality ?? 0),
                TotalSalary = x.BasicSalary + x.Bonus + (x.HouseRent == null ? 0 : x.HouseRent) + (x.TA_DA == null ? 0 : x.TA_DA) +
                                                     (x.UtilityAllowance == null ? 0 : x.UtilityAllowance) + (x.MedicalAllowance == null ? 0 : x.MedicalAllowance),
                NetSalary = (x.BasicSalary + x.Bonus + (x.HouseRent == null ? 0 : x.HouseRent) + (x.TA_DA == null ? 0 : x.TA_DA) +
                                                    (x.UtilityAllowance == null ? 0 : x.UtilityAllowance) + (x.MedicalAllowance == null ? 0 : x.MedicalAllowance)) -
                                                    ((x.ProvidentFund == null ? 0 : x.ProvidentFund) + (x.Penality ?? 0))

            }).ToList<object>();

            ReportDocument report = GetReportDocument<object>(permanentSalaries, "PermanentSalary");
            var txtFromDate = (TextObject)(report.ReportDefinition.ReportObjects["txtDate"]);
            txtFromDate.Text = DateTime.Now.ToShortDateString();

            DownloadReport(report, filter.PrintType, "PermanentSalary");

            return ShowSuccessMessage("Successful");
        }

        public ActionResult TemporarySalaryReport()
        {
            return View();
        }
        public ActionResult GetTemporarySalaryReport()
        {
            var filter = GetFilter();
            var year = Request.Params["filter[Year]"].ToInt32();
            var month = Request.Params["filter[Month]"].ToInt32();
            var penality = Request.Params["filter[Penality]"].ToInt32();

            var tempSalary = FarmManagementEntities.TemporaryEmployeeSalaries.SingleOrDefault(x => x.Year == year && x.Month == month);
            if (tempSalary == null)
            {
                tempSalary = new Models.TemporaryEmployeeSalary();
            }

            var employee = FarmManagementEntities.Users.Single(x => x.Id == filter.UserId);
            var attendance = FarmManagementEntities.AttendanceManagementDetails.Where(x => x.AttendanceManagement.Year == year
                                                                                        && x.AttendanceManagement.Month == month
                                                                                        && x.UserId == filter.UserId
                                                                                        && (x.IsPresentMorning == true || x.IsPresentNight == true)).ToList();


            var tempEmpSalaries = new List<Models.TemporarySalaryReport>();
            var tempEmpSalary = new Models.TemporarySalaryReport();
            tempEmpSalary.UserName = employee.Name;
            tempEmpSalary.FatherName = employee.FatherName;
            tempEmpSalary.NoOfMorning = attendance.Count(x => x.IsPresentMorning == true);
            tempEmpSalary.NoOfNight = attendance.Count(x => x.IsPresentNight == true);
            tempEmpSalary.NoOfMorningSum = Convert.ToDecimal(tempEmpSalary.NoOfMorning * tempSalary.MorningWages);
            tempEmpSalary.NoOfNightSum = Convert.ToDecimal(tempEmpSalary.NoOfNight * tempSalary.NightWages);

            tempEmpSalary.TotalShift = tempEmpSalary.NoOfMorning + tempEmpSalary.NoOfNight;
            tempEmpSalary.TotalShiftSum = tempEmpSalary.NoOfMorningSum + tempEmpSalary.NoOfNightSum;
            tempEmpSalary.Penalities = penality;
            tempEmpSalary.NetSalary = tempEmpSalary.TotalShiftSum - penality;

            tempEmpSalaries.Add(tempEmpSalary);

            ReportDocument report = GetReportDocument<Models.TemporarySalaryReport>(tempEmpSalaries, "TemporarySalary");
            DownloadReport(report, filter.PrintType, "TemporarySalary");

            return ShowSuccessMessage("Successful");
        }

        public ActionResult AttendanceReport()
        {
            return View();
        }
        public ActionResult GetAttendanceReport()
        {
            var filter = GetFilter();

            var allPermanentAttendance = FarmManagementEntities.AttendanceManagementDetails.AsQueryable();

            var isPermanentEmployee = Request.Params["filter[EmployeeType]"].ToInt32() == (int)EmployeeType.Permanent;
            var year = Request.Params["filter[Year]"].ToInt32();
            var month = Request.Params["filter[Month]"].ToInt32();

            allPermanentAttendance = allPermanentAttendance.Where(x => x.AttendanceManagement.Year == year && x.AttendanceManagement.Month == month);

            if (isPermanentEmployee)
            {
                allPermanentAttendance = allPermanentAttendance.Where(x => !x.User.IsTemporaryEmployee);
            }
            else
            {
                allPermanentAttendance = allPermanentAttendance.Where(x => x.User.IsTemporaryEmployee);
            }

            if (filter.UserId > 0)
            {
                allPermanentAttendance = allPermanentAttendance.Where(x => x.UserId == filter.UserId);
            }

            var permanentAttendance = allPermanentAttendance.Select(x => new AttendanceManagementDetailModel
            {
                Date = x.Date,
                UserId = x.UserId,
                Name = x.User.Name,
                PresentMorning = x.IsPresentMorning == true ? "Yes" : "No",
                PresentNight = x.IsPresentNight == true ? "Yes" : "No",
                LeaveMorning = x.IsLeaveMorning == true ? "Yes" : "No",
                LeaveNight = x.IsLeaveNight == true ? "Yes" : "No",
                AbsentMorning = x.IsAbsentMorning == true ? "Yes" : "No",
                AbsentNight = x.IsAbsentNight == true ? "Yes" : "No",
            });

            var distinctEmployees = permanentAttendance.Select(x => x.UserId).Distinct().ToList();
            var allDatesInMonth = ExtensionMethods.GetDatesOfMonth(year, month).Except(permanentAttendance.Select(x => x.Date)).ToList();

            var allEmployees = permanentAttendance.ToList();
            foreach (var employeeId in distinctEmployees)
            {
                var user = permanentAttendance.First(x => x.UserId == employeeId);
                allDatesInMonth.ForEach(x =>
                {
                    allEmployees.Add(new AttendanceManagementDetailModel
                    {
                        Name = user.Name,
                        UserId = user.UserId,
                        Date = x.Date,
                        PresentMorning = "No",
                        PresentNight = "No",
                        LeaveMorning = "No",
                        LeaveNight = "No",
                        AbsentMorning = "No",
                        AbsentNight = "No",
                    });
                });
            }

            allEmployees = allEmployees.OrderBy(x => x.UserId).ThenBy(x => x.Date).ToList();

            ReportDocument report;
            if (isPermanentEmployee)
            {
                report = GetReportDocument<AttendanceManagementDetailModel>(allEmployees, "PermanentAttendance");
            }
            else
            {
                report = GetReportDocument<AttendanceManagementDetailModel>(allEmployees, "TemporaryAttendance");
            }

            var txtYear = (TextObject)(report.ReportDefinition.ReportObjects["txtYear"]);
            var txtMonth = (TextObject)(report.ReportDefinition.ReportObjects["txtMonth"]);
            txtYear.Text = year.ToString();
            txtMonth.Text = month.ToString();

            DownloadReport(report, filter.PrintType, isPermanentEmployee ? "PermanentAttendance" : "TemporaryAttendance");

            return ShowSuccessMessage("Successful");
        }

        public ActionResult EmployeeReport()
        {
            return View();
        }
        public ActionResult GetEmployeeReport()
        {
            var filter = GetFilter();

            var allEmployees = FarmManagementEntities.Users.AsQueryable();
            allEmployees = allEmployees.Where(x => x.InsertDate >= filter.StartDate && x.InsertDate < filter.EndDate);

            var role = Request.Params["filter[Role]"].ToInt32();
            var status = Request.Params["filter[Status]"].ParseIntegerToEnum<EmployeeStatus>();

            if (role > 0)
            {
                allEmployees = allEmployees.Where(x => x.RoleId == role);
            }
            if (!string.IsNullOrEmpty(status))
            {
                allEmployees = allEmployees.Where(x => x.Status == status);
            }

            var employees = allEmployees.Select(x => new
            {
                x.Name,
                x.FatherName,
                x.CNIC,
                x.Gender,
                x.MaritalStatus,
                x.DateOfBirth,
                x.DateOfJoining,
                x.PlaceOfBirth,
                x.PermanentAddress,
                x.CurrentAddress,
                x.ContactNo,
                x.Landline,
                x.Email,
                x.Nationality,
                x.Position,
                x.Picture
            }).ToList<object>();

            ReportDocument report = GetReportDocument<object>(employees, "Employee");
            DownloadReport(report, filter.PrintType, "Employee");

            return ShowSuccessMessage("Successful");
        }

        public ActionResult EmployeeSummary()
        {
            return View();
        }
        public ActionResult GetEmployeeSummary()
        {
            var filter = GetFilter();

            var role = Request.Params["filter[Role]"].ToInt32();
            var status = Request.Params["filter[Status]"].ParseIntegerToEnum<EmployeeStatus>();

            var allEmployees = FarmManagementEntities.Users.AsQueryable();
            allEmployees = allEmployees.Where(x => x.InsertDate >= filter.StartDate && x.InsertDate < filter.EndDate);

            if (role > 0)
            {
                allEmployees = allEmployees.Where(x => x.RoleId == role);
            }
            if (!string.IsNullOrEmpty(status))
            {
                allEmployees = allEmployees.Where(x => x.Status == status);
            }

            var employees = allEmployees.Select(x => new EmployeeReportModel
            {
                Name = x.Name,
                Position = x.Position,
                DateOfJoining = x.DateOfJoining,
                Salary = x.PermanentEmployeeSalaries.FirstOrDefault(y => y.UserId == x.Id) == null
                                                          ? 0 : x.PermanentEmployeeSalaries.FirstOrDefault(y => y.UserId == x.Id).BasicSalary,
                Department = x.Department,
                FarmName = x.Farm.FarmName,
                SNo = x.Id
            }).ToList();

            ReportDocument report = GetReportDocument<EmployeeReportModel>(employees, "EmployeeSummary");
            DownloadReport(report, filter.PrintType, "EmployeeSummary");

            return ShowSuccessMessage("Successful");
        }

        public ActionResult Download()
        {
            var fileName = TempData["FileName"].ToString();
            var stream = TempData["DownloadFile"] as Stream;
            return File(stream, "application/octet-stream", fileName);
        }
        private SelectedFilter GetFilter()
        {
            var filter = new SelectedFilter();

            filter.StartDate = Convert.ToDateTime(Request.Params["filter[StartDate]"]);
            filter.EndDate = Convert.ToDateTime(Request.Params["filter[EndDate]"]).AddDays(1);
            filter.UserId = Request.Params["filter[Employee]"].ToInt32();
            filter.Quality = Request.Params["filter[Quality]"].ParseIntegerToEnum<Quality>();
            filter.Quantity = Request.Params["filter[Quantity]"].ParseIntegerToEnum<Quantity>();
            filter.PrintType = (PrintType)Request.Params["filter[PrintType]"].ParseEnum<PrintType>();

            return filter;
        }
    }
}


