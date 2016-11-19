using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FarmManagement.Lib;
using KendoGridUtilities;
using System.Globalization;
using System.Data.Objects;
using FarmManagement.Models;
using FarmManagement.Models.ViewModel;

namespace FarmManagement.Controllers
{
    public class AttendanceController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPermanentEmployee(Int32 skip, Int32 take, Int32 year, Int32 month, Int32 employeeId)
        {
            var permanentEmployees = FarmManagementEntities.AttendanceManagementDetails
                                                           .Where(x => x.AttendanceManagement.Year == year
                                                                    && x.AttendanceManagement.Month == month
                                                                    && x.UserId == employeeId
                                                                    && !x.User.IsTemporaryEmployee)
                                                           .OrderBy(x => x.Date)
                                                           .Select(x => new AttendanceManagementDetailModel
                                                           {
                                                               Id = x.Id,
                                                               Date = x.Date,
                                                               IsPresentMorning = x.IsPresentMorning ?? false,
                                                               IsLeaveMorning = x.IsLeaveMorning ?? false,
                                                               IsAbsentMorning = x.IsAbsentMorning ?? false,
                                                               AttendanceGuid = Guid.NewGuid()
                                                           });

            var allDatesInMonth = ExtensionMethods.GetDatesOfMonth(year, month).Except(permanentEmployees.Select(x => x.Date)).ToList();
            var employees = permanentEmployees.ToList();

            allDatesInMonth.ForEach(x =>
            {
                employees.Add(new AttendanceManagementDetailModel { Date = x.Date, AttendanceGuid = Guid.NewGuid() });
            });

            employees = employees.OrderBy(x => x.Date).ToList();

            var result = employees.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            result = result.GroupBy(x => x.Date).Select(x => x.First());
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TemporaryEmployeeAttendance(Int32 skip, Int32 take, Int32 year, Int32 month, Int32 employeeId)
        {
            var temporaryEmployees = FarmManagementEntities.AttendanceManagementDetails
                                                           .Where(x => x.AttendanceManagement.Year == year
                                                                    && x.AttendanceManagement.Month == month
                                                                    && x.UserId == employeeId
                                                                    && x.User.IsTemporaryEmployee)
                                                           .OrderBy(x => x.Date)
                                                           .Select(x => new AttendanceManagementDetailModel
                                                           {
                                                               Id = x.Id,
                                                               Date = x.Date,
                                                               IsPresentMorning = x.IsPresentMorning ?? false,
                                                               IsLeaveMorning = x.IsLeaveMorning ?? false,
                                                               IsAbsentMorning = x.IsAbsentMorning ?? false,
                                                               IsPresentNight = x.IsPresentNight ?? false,
                                                               IsLeaveNight = x.IsLeaveNight ?? false,
                                                               IsAbsentNight = x.IsAbsentNight ?? false
                                                           });

            var allDatesInMonth = ExtensionMethods.GetDatesOfMonth(year, month).Except(temporaryEmployees.Select(x => x.Date)).ToList();
            var employees = temporaryEmployees.ToList();

            allDatesInMonth.ForEach(x =>
            {
                employees.Add(new AttendanceManagementDetailModel { Date = x.Date, AttendanceGuid = Guid.NewGuid() });
            });

            employees = employees.OrderBy(x => x.Date).ToList();

            var result = employees.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            result = result.GroupBy(x => x.Date).Select(x => x.First());
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetEmployeePartial(Int32 year, Int32 month, Int32 employeeType, Int32 employeeId)
        {
            var filters = new Dictionary<String, Int32>();
            filters.Add("Year", year);
            filters.Add("Month", month);
            filters.Add("EmployeeId", employeeId);

            if (employeeType == (int)EmployeeType.Permanent)
            {
                return PartialView("PermanentEmployeePartial", filters);
            }

            return PartialView("TemporaryEmployeePartial", filters);
        }

        public ActionResult UpdateEmployeeAttendance(AttendanceManagementDetail model)
        {
            var id = Convert.ToInt32(Request.Params["data[0][Id]"]);
            var attendance = Request.Params["data[0][Attendance]"].ParseEnum<Attendance>();
            var value = Convert.ToBoolean(Request.Params["data[0][Value]"]);
            var year = Convert.ToInt32(Request.Params["data[0][Year]"]);
            var month = Convert.ToInt32(Request.Params["data[0][Month]"]);

            var attendanceMgmt = FarmManagementEntities.AttendanceManagements.SingleOrDefault(x => x.Year == year && x.Month == month);
            if (attendanceMgmt == null)
            {
                attendanceMgmt = new AttendanceManagement { Year = year, Month = month, InsertDate = DateTime.Now };
            }

            var attendanceMgmtDetail = new AttendanceManagementDetail();
            if (id > 0)
            {
                attendanceMgmtDetail = FarmManagementEntities.AttendanceManagementDetails.Single(x => x.Id == id);
            }

            #region Assing Value
            switch (attendance)
            {
                case Attendance.IsPresentMorning:
                    attendanceMgmtDetail.IsPresentMorning = value;
                    break;
                case Attendance.IsLeaveMorning:
                    attendanceMgmtDetail.IsLeaveMorning = value;
                    break;
                case Attendance.IsAbsentMorning:
                    attendanceMgmtDetail.IsAbsentMorning = value;
                    break;
                case Attendance.IsPresentNight:
                    attendanceMgmtDetail.IsPresentNight = value;
                    break;
                case Attendance.IsLeaveNight:
                    attendanceMgmtDetail.IsLeaveNight = value;
                    break;
                case Attendance.IsAbsentNight:
                    attendanceMgmtDetail.IsAbsentNight = value;
                    break;
            }
            #endregion

            if (id == 0)
            {
                attendanceMgmtDetail.InsertDate = DateTime.Now;

                attendanceMgmtDetail.Date = Convert.ToDateTime(Request.Params["data[0][Date]"]);
                attendanceMgmtDetail.UserId = Convert.ToInt32(Request.Params["data[0][EmployeeId]"]);

                attendanceMgmt.AttendanceManagementDetails.Add(attendanceMgmtDetail);

                if (attendanceMgmt.Id == 0)
                {
                    FarmManagementEntities.AttendanceManagements.Add(attendanceMgmt);
                }
            }

            FarmManagementEntities.SaveChanges();

            var message = string.Format(Constant.SuccessMessage, id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }

        public ActionResult GetEmployee(Int32 employeeType)
        {
            bool isTemporaryEmployee = employeeType == (int)EmployeeType.Temporary;
            string active = EmployeeStatus.Active.ToString();

            var allEmployees = FarmManagementEntities.Users.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                allEmployees = allEmployees.Where(x => x.Id == LoggedInUser.UserId);
            }

            var employees = allEmployees.Where(x => x.Status == active && x.IsTemporaryEmployee == isTemporaryEmployee)
                                        .Select(x => new
                                        {
                                            x.Name,
                                            x.Id
                                        }).OrderBy(x => x.Id).ToList();

            employees.Add(new { Name = "Select", Id = 0 });
            employees = employees.OrderBy(x => x.Id).ToList();

            return Json(employees, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetEmployeeType()
        {
            var employeeTypes = ExtensionMethods.ToSelectList<EmployeeType>();
            return Json(employeeTypes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllEmployees()
        {
            string active = EmployeeStatus.Active.ToString();

            var allEmployees = FarmManagementEntities.Users.AsQueryable();
            if (!LoggedInUser.IsAdmin)
            {
                allEmployees = allEmployees.Where(x => x.Id == LoggedInUser.UserId);
            }

            var employees = allEmployees.Where(x => x.Status == active)
                                        .Select(x => new
                                        {
                                            x.Name,
                                            x.Id
                                        }).OrderBy(x => x.Id).ToList();

            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMonths()
        {
            var months = Enumerable.Range(1, 12).Select(i => new { Id = i, Name = DateTimeFormatInfo.CurrentInfo.GetMonthName(i) }).ToList();
            months.Add(new { Id = -1, Name = "Select" });

            months = months.OrderBy(x => x.Id).ToList();

            return Json(months, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetYears()
        {
            var years = Enumerable.Range(DateTime.Now.Year, 10).Select(x => new { Name = "" + x, Id = x }).ToList();
            years.Add(new { Name = "Select", Id = 0 });

            years = years.OrderBy(x => x.Id).ToList();

            return Json(years, JsonRequestBehavior.AllowGet);
        }
    }
}
