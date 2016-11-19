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
    public class DepartmentController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DepartmentAjax(Int32 skip, Int32 take)
        {
            var departments = FarmManagementEntities.Departments.OrderBy(x => x.Id)
                                                        .Select(x => new
                                                        {
                                                            x.Id,
                                                            x.Name,
                                                            x.Status,
                                                            x.Date,
                                                            x.Description,
                                                            IsDelete = x.Positions.Any()
                                                        });

            var result = departments.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateUpdateDepartment(Int32 id)
        {
            var departmentsModel = new DepartmentModel();
            departmentsModel.Date = DateTime.Now;

            if (id > 0)
            {
                var farm = FarmManagementEntities.Departments.Single(x => x.Id == id);
                departmentsModel = farm.ToType<Department, DepartmentModel>();
            }

            return PartialView("DepartmentPartial", departmentsModel);
        }
        [HttpPost]
        public ActionResult CreateUpdateDepartment(DepartmentModel departmentModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            var department = new Department();
            if (departmentModel.Id > 0)
            {
                department = FarmManagementEntities.Departments.Single(x => x.Id == departmentModel.Id);
            }

            department.Id = departmentModel.Id;
            department.Name = departmentModel.Name;
            department.Status = departmentModel.Status;
            department.Date = departmentModel.Date;
            department.Description = departmentModel.Description;

            if (departmentModel.Id == 0)
            {
                department.Date = DateTime.Now;
                FarmManagementEntities.Departments.Add(department);
            }

            FarmManagementEntities.SaveChanges();


            var message = string.Format(Constant.SuccessMessage, departmentModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }

        public ActionResult DeleteDepartment(Int32 id)
        {
            var departments = FarmManagementEntities.Departments.Single(x => x.Id == id);
            FarmManagementEntities.Departments.Remove(departments);
            FarmManagementEntities.SaveChanges();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }


    }
}
