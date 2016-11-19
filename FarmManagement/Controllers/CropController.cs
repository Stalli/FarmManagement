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
    public class CropController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CropAjax(Int32 skip, Int32 take)
        {
            var farms = FarmManagementEntities.CropManagements.OrderBy(x => x.Id)
                                                              .Select(x => new
                                                              {
                                                                  x.Id,
                                                                  x.Farm.FarmName,
                                                                  x.FarmArea.AreaName,
                                                                  x.CropName,
                                                                  x.CropDuration,
                                                                  x.CropStartDate,
                                                                  x.CropEndDate,
                                                                  IsDelete = x.Seeds.Any() || x.FertilizerExpenses.Any() || x.FuelExpenses.Any()
                                                              });

            var result = farms.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateUpdateCrop(Int32 id)
        {
            var cropModel = new CropModel();
            cropModel.CropStartDate = cropModel.CropEndDate = DateTime.Now;

            if (id > 0)
            {
                var crop = FarmManagementEntities.CropManagements.Single(x => x.Id == id);
                cropModel = crop.ToType<CropManagement, CropModel>();
            }

            return PartialView("CropPartial", cropModel);
        }
        [HttpPost]
        public ActionResult CreateUpdateCrop(CropModel cropModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            var crop = new CropManagement();
            if (cropModel.Id > 0)
            {
                crop = FarmManagementEntities.CropManagements.Single(x => x.Id == cropModel.Id);
            }

            crop.FarmId = cropModel.FarmId;
            crop.AreaId = cropModel.AreaId;
            crop.CropName = cropModel.CropName;
            crop.CropDuration = cropModel.CropDuration;
            crop.CropStartDate = cropModel.CropStartDate;
            crop.CropEndDate = cropModel.CropEndDate;

            if (cropModel.Id == 0)
            {
                crop.InsertDate = DateTime.Now;
                FarmManagementEntities.CropManagements.Add(crop);
            }
            else
            {
                crop.UpdateDate = DateTime.Now;
            }

            FarmManagementEntities.SaveChanges();

            var message = string.Format(Constant.SuccessMessage, cropModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }

        public ActionResult DeleteCrop(Int32 id)
        {
            var crop = FarmManagementEntities.CropManagements.Single(x => x.Id == id);
            FarmManagementEntities.CropManagements.Remove(crop);
            FarmManagementEntities.SaveChanges();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }

    }
}
