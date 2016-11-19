using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KendoGridUtilities;
using FarmManagement.Lib;
using FarmManagement.Models.ViewModel;
using FarmManagement.Models;
using System.IO;

namespace FarmManagement.Controllers
{
    public class EmployeeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EmployeeAjax(Int32 skip, Int32 take)
        {
            var allEmployees = FarmManagementEntities.Users.AsQueryable();

            if (!LoggedInUser.IsAdmin)
            {
                allEmployees = allEmployees.Where(x => x.Id == LoggedInUser.UserId);
            }

            var employees = allEmployees.OrderBy(x => x.Id).Select(x => new
                                                           {
                                                               x.Id,
                                                               x.Role.RoleName,
                                                               x.Name,
                                                               x.FatherName,
                                                               x.CNIC,
                                                               x.Gender,
                                                               x.MaritalStatus,
                                                               x.DateOfBirth,
                                                               x.PlaceOfBirth,
                                                               x.ContactNo,
                                                               x.Landline,
                                                               x.Nationality,
                                                               x.Email,
                                                               x.PermanentAddress,
                                                               x.CurrentAddress,
                                                               x.DateOfJoining,
                                                               x.ResignationDate,
                                                               x.Status,
                                                               x.Position,
                                                               x.Department,                                                               
                                                               x.Farm.FarmName,
                                                               IsTemporaryEmployee = x.IsTemporaryEmployee ? "Yes" : "No",
                                                               x.Salary
                                                           });

            var result = employees.MultipleFilter(Request);
            result = result.MultipleSort(Request);
            var resultData = result.Skip(skip).Take(take).ToList();

            return Json(new { Data = resultData, Total = result.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateUpdateEmployee(Int32 id)
        {
            var employeeModel = new EmployeeModel();
            employeeModel.EmployeeGuid = Guid.NewGuid().ToString();
            employeeModel.DateOfBirth = employeeModel.DateOfJoining = DateTime.Now;

            if (id > 0)
            {
                var employee = FarmManagementEntities.Users.Single(x => x.Id == id);
                employeeModel = employee.ToType<User, EmployeeModel>();

                employeeModel.EmployeeGender = Convert.ToInt32(employee.Gender.ParseEnum<Gender>());
                employeeModel.EmployeeMaritalStatus = Convert.ToInt32(employee.MaritalStatus.ParseEnum<MaritalStatus>());
                employeeModel.EmployeeStatus = Convert.ToInt32(employee.Status.ParseEnum<EmployeeStatus>());
                //employeeModel.TemporaryEmployee = employee.IsTemporaryEmployee ? (int)EmployeeType.Temporary : (int)EmployeeType.Permanent;
                employeeModel.TemporaryEmployee = employee.RelationType ?? (int)EmployeeType.Permanent;

                if (employee.IsTemporaryEmployee)
                {
                    GetUploadFile(FileUploadPath.EmployeeCNICFrontDocument, employee.Id, employee.CNICFrontSide);
                    GetUploadFile(FileUploadPath.EmployeeCNICBackDocument, employee.Id, employee.CNICBackSide);
                    GetUploadFile(FileUploadPath.EmployeeAgreementDocument, employee.Id, employee.AgreementDocument);
                }
                else
                {
                    GetUploadFile(FileUploadPath.EmployeeScannedDocument, employee.Id, employee.ScannedDocument);
                }

                GetUploadFile(FileUploadPath.EmployeePicture, employee.Id, employee.Picture);

                employeeModel.EmployeeGuid = employee.Id.ToString();
            }

            return PartialView("EmployeePartial", employeeModel);
        }
        [HttpPost]
        public ActionResult CreateUpdateEmployee(EmployeeModel empModel)
        {
            if (!ModelState.IsValid)
            {
                return ShowErrorMessage(Constant.DefaultErrorMessage);
            }

            var employee = new User();
            if (empModel.Id > 0)
            {
                employee = FarmManagementEntities.Users.Single(x => x.Id == empModel.Id);
            }

            employee.RoleId = empModel.RoleId;
            employee.Name = empModel.Name;
            employee.FatherName = empModel.FatherName;
            employee.CNIC = empModel.CNIC;
            employee.DateOfBirth = empModel.DateOfBirth;
            employee.PlaceOfBirth = empModel.PlaceOfBirth;
            employee.ContactNo = empModel.ContactNo;
            employee.Landline = empModel.Landline;
            employee.Nationality = empModel.Nationality;
            employee.Email = empModel.Email;
            employee.PermanentAddress = empModel.PermanentAddress;
            employee.CurrentAddress = empModel.CurrentAddress;
            employee.DateOfJoining = empModel.DateOfJoining;
            employee.ResignationDate = empModel.ResignationDate;
            employee.Picture = empModel.Picture;
            employee.Position = empModel.Position;
            employee.IsUser = empModel.IsUser;
            employee.FarmId = empModel.FarmId;
            employee.Department = empModel.Department;
            employee.Salary = empModel.Salary;
            

            if (employee.IsUser)
            {
                var isUserNameExist = FarmManagementEntities.Users.Any(x => x.Id != empModel.Id && x.UserName == empModel.UserName);
                if (isUserNameExist)
                {
                    return ShowErrorMessage("User name already exist, Please choose different user name.");
                }

                employee.UserName = empModel.UserName;
                employee.Password = empModel.Password;
            }
            else
            {
                employee.UserName = employee.Password = string.Empty;
            }

            var isTemporaryEmployee = empModel.TemporaryEmployee == (int)EmployeeType.Temporary;
            employee.IsTemporaryEmployee = isTemporaryEmployee;

            employee.Status = empModel.EmployeeStatus.ToNullIfEmptyEnum<EmployeeStatus>();
            employee.Gender = empModel.EmployeeGender.ToNullIfEmptyEnum<Gender>();
            employee.MaritalStatus = empModel.EmployeeMaritalStatus.ToNullIfEmptyEnum<MaritalStatus>();

            FileUpload cnicFrontDocument = null, cnicBackDocument = null, aggrementDocument = null, uploadScannedDocument = null;

            var uploadEmpPicture = ClientSession.FileUploads.FirstOrDefault(x => x.FileUploadGuid == empModel.EmployeeGuid
                                                                      && x.FileUploadName == FileUploadPath.EmployeePicture && !x.HasRemoved);
            employee.Picture = System.IO.Path.GetFileName(uploadEmpPicture.FilePath);

            if (isTemporaryEmployee)
            {
                cnicFrontDocument = ClientSession.FileUploads.SingleOrDefault(x => x.FileUploadGuid == empModel.EmployeeGuid
                                                                                && x.FileUploadName == FileUploadPath.EmployeeCNICFrontDocument && !x.HasRemoved);
                cnicBackDocument = ClientSession.FileUploads.SingleOrDefault(x => x.FileUploadGuid == empModel.EmployeeGuid
                                                                               && x.FileUploadName == FileUploadPath.EmployeeCNICBackDocument && !x.HasRemoved);
                aggrementDocument = ClientSession.FileUploads.SingleOrDefault(x => x.FileUploadGuid == empModel.EmployeeGuid
                                                                                && x.FileUploadName == FileUploadPath.EmployeeAgreementDocument && !x.HasRemoved);

                if (cnicFrontDocument != null)
                {
                    employee.CNICFrontSide = System.IO.Path.GetFileName(cnicFrontDocument.FilePath);
                }
                if (cnicBackDocument != null)
                {
                    employee.CNICBackSide = System.IO.Path.GetFileName(cnicBackDocument.FilePath);
                }
                if (aggrementDocument != null)
                {
                    employee.AgreementDocument = System.IO.Path.GetFileName(aggrementDocument.FilePath);
                }
            }
            else
            {
                uploadScannedDocument = ClientSession.FileUploads.FirstOrDefault(x => x.FileUploadGuid == empModel.EmployeeGuid
                                                                                    && x.FileUploadName == FileUploadPath.EmployeeScannedDocument && !x.HasRemoved);
                if (uploadScannedDocument != null)
                {
                    employee.ScannedDocument = System.IO.Path.GetFileName(uploadScannedDocument.FilePath);
                }
            }

            employee.Company = empModel.Company;
            employee.CompanyPhoneNumber = empModel.CompanyPhoneNumber;
            employee.Type = empModel.Type;
            employee.Security = empModel.Security;
            employee.VendorName = empModel.VendorName;
            employee.AccountId = empModel.AccountId == 0 ? (int?)null : empModel.AccountId;
            employee.RelationType = empModel.TemporaryEmployee;

            if (empModel.Id == 0)
            {
                employee.InsertDate = DateTime.Now;
                FarmManagementEntities.Users.Add(employee);
            }
            else
            {
                employee.UpdateDate = DateTime.Now;
            }

            FarmManagementEntities.SaveChanges();

            UpdateFileUploadPath(FileUploadPath.EmployeePicture, uploadEmpPicture, employee.Id);

            if (isTemporaryEmployee)
            {
                UpdateFileUploadPath(FileUploadPath.EmployeeAgreementDocument, aggrementDocument, employee.Id);
                UpdateFileUploadPath(FileUploadPath.EmployeeCNICFrontDocument, cnicFrontDocument, employee.Id);
                UpdateFileUploadPath(FileUploadPath.EmployeeCNICBackDocument, cnicBackDocument, employee.Id);
            }
            else
            {
                UpdateFileUploadPath(FileUploadPath.EmployeeScannedDocument, uploadScannedDocument, employee.Id);
            }
            ClearAllFileUpload();

            var message = string.Format(Constant.SuccessMessage, empModel.Id > 0 ? "updated" : "added");
            return ShowSuccessMessage(message);
        }

        public void UpdateFileUploadPath(FileUploadPath uploadPath, FileUpload fileUpload, Int32 id)
        {
            if (fileUpload == null)
            {
                return;
            }

            var oldPath = Path.GetDirectoryName(fileUpload.FilePath).TrimEnd('.');
            if (Directory.Exists(oldPath))
            {
                oldPath = oldPath.TrimEnd('.');
                oldPath = oldPath.TrimEnd('\\');

                var newPath = ExtensionMethods.GetFileUploadPath(uploadPath.ToString(), id.ToString());
                if (oldPath != newPath)
                {
                    newPath.EnusreDirectoryExist();
                    System.IO.File.Move(fileUpload.FilePath, Path.Combine(newPath, Path.GetFileName(fileUpload.FilePath)));
                }
            }
        }

        public void GetUploadFile(FileUploadPath uploadPath, Int32 id, string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }

            var fileUploads = GetFileUploads();
            var assignNewGuid = Guid.NewGuid().ToString();
            if (fileUploads.Any(x => x.FileUploadId == assignNewGuid))
            {
                return;
            }

            var newFileUpload = new FileUpload
            {
                FilePath = ExtensionMethods.GetFileUploadPath(uploadPath.ToString(), id.ToString()) + "\\" + fileName,
                FileUploadGuid = id.ToString(),
                FileUploadId = assignNewGuid,
                FileUploadName = uploadPath
            };
            fileUploads.Add(newFileUpload);
        }

        public List<FileUpload> GetFileUploads()
        {
            if (ClientSession.FileUploads == null)
            {
                ClientSession.FileUploads = new List<FileUpload>();
            }

            return ClientSession.FileUploads;
        }

        private void ClearAllFileUpload()
        {
            //var filePaths = GetFileUploads().Where(x => x.HasRemoved).ToList();
            //if (filePaths.Count > 0)
            //{
            //    filePaths.ForEach(x => { if (System.IO.File.Exists(x.FilePath)) System.IO.File.Delete(x.FilePath); });
            //}

            FarmManagement.Lib.ClientSession.Clear();
        }

        public ActionResult GetRoles()
        {
            var roles = FarmManagementEntities.Roles.Select(x => new { Name = x.RoleName, Id = x.Id }).ToList();

            roles.Add(new { Name = "Select", Id = 0 });

            roles = roles.OrderBy(x => x.Id).ToList();

            return Json(roles, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMaritalStatus()
        {
            var maritalStatus = ExtensionMethods.ToSelectList<MaritalStatus>();
            return Json(maritalStatus, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetStatus()
        {
            var status = ExtensionMethods.ToSelectList<EmployeeStatus>();
            return Json(status, JsonRequestBehavior.AllowGet);
        }
    }
}
