using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoGridUtilities;
using FarmManagement.Models;
using FarmManagement.Models.ViewModel;
using FarmManagement.Lib;

namespace FarmManagement.Controllers
{
    public class VendorController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VendorAjax(Int32 skip, Int32 take)
        {
            var vendors = FarmManagementEntities.VendorManagements.OrderBy(x => x.Id)
                                                .Select(x => new
                                                {
                                                    x.Id,
                                                    x.Name,
                                                    x.CompanyName,
                                                    x.PhoneNo,
                                                    x.PersonalPhoneNo,
                                                    x.AddressInfo,
                                                    x.OpeningBalance,
                                                    x.OtherDescription,
                                                    x.Type,
                                                    x.Date,
                                                    IsDelete = x.Fertilizers.Any() || x.Fuels.Any() || x.Machineries.Any()
                                                            || x.Seeds.Any() || x.Vehicles.Any() || x.Animals.Any()
                                                });

            var result = vendors.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateUpdateVendor(Int32 id)
        {
            var vendorModel = new VendorModel();
            vendorModel.Date = DateTime.Now;

            if (id > 0)
            {
                var vendor = FarmManagementEntities.VendorManagements.Single(x => x.Id == id);
                vendorModel = vendor.ToType<VendorManagement, VendorModel>();

                vendorModel.VendorType = Convert.ToInt32(vendor.Type.ParseEnum<VendorType>());
            }

            return PartialView("VendorPartial", vendorModel);
        }
        [HttpPost]
        public ActionResult CreateUpdateVendor(VendorModel vendorModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(GetModelErrors(ModelState));
            }

            var vendor = new VendorManagement();
            if (vendorModel.Id > 0)
            {
                vendor = FarmManagementEntities.VendorManagements.Single(x => x.Id == vendorModel.Id);
            }

            vendor.Name = vendorModel.Name;
            vendor.CompanyName = vendorModel.CompanyName;
            vendor.PhoneNo = vendorModel.PhoneNo;
            vendor.PersonalPhoneNo = vendorModel.PersonalPhoneNo;
            vendor.AddressInfo = vendorModel.AddressInfo;
            vendor.OpeningBalance = vendorModel.OpeningBalance;
            vendor.OtherDescription = vendorModel.OtherDescription;
            vendor.Date = vendorModel.Date;

            vendor.Type = vendorModel.VendorType.ToNullIfEmptyEnum<VendorType>();

            if (vendorModel.Id == 0)
            {
                vendor.InsertDate = DateTime.Now;
                FarmManagementEntities.VendorManagements.Add(vendor);
            }
            else
            {
                vendor.UpdateDate = DateTime.Now;
            }

            FarmManagementEntities.SaveChanges();

            var message = string.Format(Constant.SuccessMessage, vendorModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }

        public ActionResult DeleteVendor(Int32 id)
        {
            var vendor = FarmManagementEntities.VendorManagements.Single(x => x.Id == id);
            FarmManagementEntities.VendorManagements.Remove(vendor);
            FarmManagementEntities.SaveChanges();

            return ShowSuccessMessage(Constant.DeletedMessage);
        }

    }
}
