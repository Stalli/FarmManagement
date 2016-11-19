using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FarmManagement.Lib;
using System.IO;
using FarmManagement.Models;
using System.Web.Script.Serialization;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FarmManagement.Models.ViewModel;

namespace FarmManagement.Controllers
{
    public class BaseController : Controller
    {
        protected readonly FarmManagementEntities FarmManagementEntities;

        public BaseController()
        {
            FarmManagementEntities = new FarmManagementEntities();
        }

        new void Dispose()
        {
            FarmManagementEntities.Dispose();
        }

        public EndPointSession ClientSession
        {
            get
            {
                if (HttpContext.Session["ClientSession"] == null)
                {
                    HttpContext.Session["ClientSession"] = new EndPointSession();
                    return (EndPointSession)HttpContext.Session["ClientSession"];
                }
                else
                {
                    return (EndPointSession)HttpContext.Session["ClientSession"];
                }

            }
            set
            {
                HttpContext.Session["ClientSession"] = value;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (LoggedInUser.UserId == 0)
            {
                GetLoggedInInformation();
                if (LoggedInUser.UserId == 0)
                {
                    filterContext.Result = new RedirectResult("~/Account/Login?ReturnUrl=" + Request.Path);
                }
            }
        }

        public void GetLoggedInInformation()
        {
            if (Request == null)
            {
                return;
            }

            HttpCookie myCookie = Request.Cookies["FarmCookies"];
            if (myCookie != null)
            {
                LoggedInUser.UserId = Convert.ToInt32(myCookie.Values["UserId"]);
                LoggedInUser.RoleId = Convert.ToInt32(myCookie.Values["Role"]);
                LoggedInUser.IsAdmin = Convert.ToBoolean(myCookie.Values["IsAdmin"]);
            }
        }

        public void GetFormSetting()
        {
            var forms = FarmManagementEntities.FormSettings.Where(x => x.RoleId == LoggedInUser.RoleId).Where(x => x.IsAdd && x.IsUpdate).OrderBy(x => x.Id)
                                                           .Select(x => new FormSettingModel
                                                           {
                                                               Id = x.Id,
                                                               Name = x.Form.Name,
                                                               Type = x.Form.Type,
                                                               ControllerName = x.Form.ControllerName,
                                                               ActionName = x.Form.ActionName,
                                                           }).GroupBy(x => x.Type).ToDictionary(x => x.Key, x => x.ToList());

            System.Web.HttpContext.Current.Session["Menu"] = forms;
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public JsonResult ShowSuccessMessage(string message)
        {
            return Json(new { IsSuccess = true, Type = "success", Message = message }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ShowErrorMessage(string message)
        {
            return Json(new { IsSuccess = false, Type = "error", Message = message }, JsonRequestBehavior.AllowGet);
        }

        public ReportDocument GetReportDocument<T>(List<T> records, string fileName)
        {
            ReportDocument report = new ReportDocument();
            string filePath = Server.MapPath(string.Format("../Reports/{0}.rpt", fileName));
            report.Load(filePath);
            report.SetDataSource(records);
            report.Refresh();

            return report;
        }
        public void DownloadReport(ReportDocument report, PrintType printType, string fileName)
        {
            Stream stream = null;

            switch (printType)
            {
                case PrintType.Pdf:
                    stream = report.ExportToStream(ExportFormatType.PortableDocFormat);
                    fileName += ".pdf";
                    break;
                case PrintType.Excel:
                    stream = stream = report.ExportToStream(ExportFormatType.Excel);
                    fileName += ".xls";
                    break;
            }

            stream.Seek(0, SeekOrigin.Begin);

            TempData["DownloadFile"] = stream;
            TempData["FileName"] = fileName;
        }

        public string GetModelErrors(ModelStateDictionary modelState)
        {
            return modelState.Keys.SelectMany(key => ModelState[key].Errors).ToModelErrors();
        }
    }
}
