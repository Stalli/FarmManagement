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
    public class PositionController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PositionAjax(Int32 skip, Int32 take)
        {
            var Positions = FarmManagementEntities.Positions.OrderBy(x => x.Id)
                                                        .Select(x => new
                                                        {
                                                            x.Id,
                                                            x.Name,
                                                            x.Description,
                                                            x.Date,
                                                            x.DepartmentId
                                                        });

            var result = Positions.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateUpdatePosition(Int32 id)
        {
            var PositionsModel = new PositionModel();
            PositionsModel.Date = DateTime.Now;

            if (id > 0)
            {
                var farm = FarmManagementEntities.Positions.Single(x => x.Id == id);
                PositionsModel = farm.ToType<Position, PositionModel>();
            }

            return PartialView("PositionPartial", PositionsModel);
        }
        [HttpPost]
        public ActionResult CreateUpdatePosition(PositionModel PositionModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            var Position = new Position();
            if (PositionModel.Id > 0)
            {
                Position = FarmManagementEntities.Positions.Single(x => x.Id == PositionModel.Id);
            }

            Position.Id = PositionModel.Id;
            Position.Name = PositionModel.Name;
            Position.DepartmentId = PositionModel.DepartmentId;
            Position.Date = PositionModel.Date;
            Position.Description = PositionModel.Description;

            if (PositionModel.Id == 0)
            {
                Position.Date = DateTime.Now;
                FarmManagementEntities.Positions.Add(Position);
            }

            FarmManagementEntities.SaveChanges();


            var message = string.Format(Constant.SuccessMessage, PositionModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }

        public ActionResult DeletePosition(Int32 id)
        {
            var Positions = FarmManagementEntities.Positions.Single(x => x.Id == id);
            FarmManagementEntities.Positions.Remove(Positions);
            FarmManagementEntities.SaveChanges();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }


    }
}
