using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoGridUtilities;
using FarmManagement.Models.ViewModel;
using FarmManagement.Models;
using FarmManagement.Lib;

namespace FarmManagement.Controllers
{
    public class SettingController : BaseController
    {
        public ActionResult FormSetting()
        {
            return View();
        }

        public ActionResult GetFormSettingPartial(Int32 role)
        {
            ViewBag.RoleId = role;
            return PartialView("FormSettingPartial");
        }
        public ActionResult FormSettingAjax(Int32 skip, Int32 take, Int32 roleId)
        {
            var forms = FarmManagementEntities.Forms.OrderBy(x => x.Id)
                                              .Select(x => new FormSettingModel
                                              {
                                                  FormGuid = Guid.NewGuid(),
                                                  FormId = x.Id,
                                                  Name = x.Name,
                                                  Type = x.Type
                                              });

            var result = forms.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            result = result.GroupBy(x => x.FormId).Select(x => x.First());

            var resultData = result.Skip(skip).Take(take).ToList();

            var assignForms = FarmManagementEntities.FormSettings.Where(x => x.RoleId == roleId).ToList();
            foreach (var data in assignForms)
            {
                var record = resultData.SingleOrDefault(x => x.FormId == data.FormId);
                if (record != null)
                {
                    record.IsAdd = data.IsAdd;
                    record.IsUpdate = data.IsUpdate;
                }
            }

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateFormSetting()
        {
            var formId = Convert.ToInt32(Request.Params["data[0][FormId]"]);
            var roleId = Convert.ToInt32(Request.Params["data[0][RoleId]"]);
            var setting = Request.Params["data[0][FormSetting]"];
            var value = Convert.ToBoolean(Request.Params["data[0][Value]"]);

            var formSetting = FarmManagementEntities.FormSettings.SingleOrDefault(x => x.RoleId == roleId && x.FormId == formId);
            if (formSetting == null) formSetting = new FormSetting();

            switch (setting)
            {
                case "IsAdd":
                    formSetting.IsAdd = value;
                    break;
                case "IsUpdate":
                    formSetting.IsUpdate = value;
                    break;
            }

            if (formSetting.Id == 0)
            {
                formSetting.RoleId = roleId;
                formSetting.FormId = formId;
                formSetting.InsertDate = DateTime.Now;
                FarmManagementEntities.FormSettings.Add(formSetting);
            }

            FarmManagementEntities.SaveChanges();

            GetFormSetting();

            var message = string.Format(Constant.SuccessMessage, formSetting.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }
    }
}
