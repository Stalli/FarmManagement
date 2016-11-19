using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoGridUtilities;
using FarmManagement.Lib;
using FarmManagement.Models;
using FarmManagement.Models.ViewModel;

namespace FarmManagement.Controllers
{
    public class TaskAssignmentController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TaskAssignmentAjax(Int32 skip, Int32 take)
        {
            var allTaskAssignments = FarmManagementEntities.TaskAssignments.AsQueryable();

            if (!LoggedInUser.IsAdmin)
            {
                allTaskAssignments = allTaskAssignments.Where(x => x.StaffNameId == LoggedInUser.UserId);
            }

            var taskAssignments = allTaskAssignments.Where(x => !x.User.IsTemporaryEmployee).OrderBy(x => x.Id)
                                                    .Select(x => new
                                                    {
                                                        x.Id,
                                                        x.TaskName,
                                                        x.User.Name,
                                                        x.Description,
                                                        x.DeadlineStartDate,
                                                        x.DeadlineEndDate,
                                                        x.Status,
                                                        x.Remarks
                                                    });

            var result = taskAssignments.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateUpdateTask(Int32 id)
        {
            var taskAssignModel = new TaskAssignmentModel();
            if (id > 0)
            {
                var taskAssign = FarmManagementEntities.TaskAssignments.Single(x => x.Id == id);
                taskAssignModel = taskAssign.ToType<TaskAssignment, TaskAssignmentModel>();

                taskAssignModel.TaskStatus = Convert.ToInt32(taskAssign.Status.ParseEnum<TaskStatus>());
                taskAssignModel.TaskRemarks = Convert.ToInt32(taskAssign.Remarks.ParseEnum<TaskRemarks>());
            }

            return PartialView("TaskAssignmentPartial", taskAssignModel);
        }

        [HttpPost]
        public ActionResult CreateUpdateTask(TaskAssignmentModel taskAssignModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(Constant.DefaultErrorMessage);
            }

            var taskAssign = new TaskAssignment();
            if (taskAssignModel.Id > 0)
            {
                taskAssign = FarmManagementEntities.TaskAssignments.Single(x => x.Id == taskAssignModel.Id);
            }

            taskAssign.TaskName = taskAssignModel.TaskName;
            taskAssign.StaffNameId = taskAssignModel.StaffNameId;
            taskAssign.Description = taskAssignModel.Description;
            taskAssign.DeadlineStartDate = taskAssignModel.DeadlineStartDate;
            taskAssign.DeadlineEndDate = taskAssignModel.DeadlineEndDate;

            taskAssign.Status = taskAssignModel.TaskStatus == null ? null : ((TaskStatus)taskAssignModel.TaskStatus).ToString();
            taskAssign.Remarks = taskAssignModel.TaskRemarks == null ? null : ((TaskRemarks)taskAssignModel.TaskRemarks).ToString();

            if (taskAssignModel.Id == 0)
            {
                taskAssign.InsertDate = DateTime.Now;
                FarmManagementEntities.TaskAssignments.Add(taskAssign);
            }
            else
            {
                taskAssign.UpdateDate = DateTime.Now;
            }

            FarmManagementEntities.SaveChanges();

            var message = string.Format(Constant.SuccessMessage, taskAssignModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);

        }

        public ActionResult DeleteTask(Int32 id)
        {
            var taskAssign = FarmManagementEntities.TaskAssignments.Single(x => x.Id == id);
            FarmManagementEntities.TaskAssignments.Remove(taskAssign);
            FarmManagementEntities.SaveChanges();
            return ShowSuccessMessage(Constant.DeletedMessage);
        }

        public ActionResult GetTaskStatus()
        {
            var status = ExtensionMethods.ToSelectList<TaskStatus>();
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTaskRemarks()
        {
            var remarks = ExtensionMethods.ToSelectList<TaskRemarks>();
            return Json(remarks, JsonRequestBehavior.AllowGet);
        }

    }
}
