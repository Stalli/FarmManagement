using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FarmManagement.Lib;

namespace FarmManagement.Controllers
{
    public class FileUploadController : BaseController
    {
        public ActionResult AddFileUpload(HttpPostedFileBase file, string fileUploadPath, string guid)
        {
            if (file == null)
                return Json(new { status = "OK" }, "text/plain");

            var fileUpload = GetFileUpload();

            var folderPath = Path.Combine(System.IO.Path.GetTempPath(), fileUploadPath, guid);
            folderPath.EnusreDirectoryExist();

            var fileName = string.Format("{0}_{1}{2}", Path.GetFileNameWithoutExtension(file.FileName), DateTime.Now.ToString("yyyyMMddHHmmssfff"), Path.GetExtension(file.FileName));
            var filePath = Path.Combine(folderPath, fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            file.SaveAs(filePath);

            var newFileUpload = new FileUpload
            {
                FileUploadGuid = guid,
                FileUploadId = Guid.NewGuid().ToString(),
                FilePath = filePath,
                IsNewlyAdded = true,
                FileUploadName = (FileUploadPath)fileUploadPath.ParseEnum<FileUploadPath>()
            };
            fileUpload.Add(newFileUpload);

            return Json(new { status = "OK" }, "text/plain");
        }

        public List<FileUpload> GetFileUpload()
        {
            if (ClientSession.FileUploads == null)
            {
                ClientSession.FileUploads = new List<FileUpload>();
            }

            return ClientSession.FileUploads;
        }

        public JsonResult GetFiles(FileUploadPath? fileUploadPath)
        {
            var files = GetFileUpload().Where(x => !x.HasRemoved);
            if (fileUploadPath != null)
            {
                files = files.Where(x => x.FileUploadName == fileUploadPath);
            }

            var allFiles = files.ToList();

            var table = "<table class='table table-hover'><tr><th>File#</th><th>File Name</th><th>Action</th></tr>";

            var count = 1;
            foreach (var file in allFiles)
            {
                var actionButton = string.Format("<a href='javascript:;' title='Download File' onclick='downloadFile(\"{0}\");' class='btn btn-success btn-sm'><span class='glyphicon glyphicon-download-alt'></span> </a> &nbsp;", file.FileUploadId);
                actionButton += string.Format("<a href='javascript:;' title='Delete File' onclick='deleteFile(\"{0}\");' class='btn btn-danger btn-sm'><span class='glyphicon glyphicon glyphicon-trash'></span> </a>", file.FileUploadId);

                var row = "<tr><td>" + count + "</td><td>" + Path.GetFileName(file.FilePath) + "</td><td>" + actionButton + "</td></tr>";
                table += row;
                count++;
            }

            table += "</table>";

            return Json(table, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DownloadFile(string id)
        {
            var file = GetFileUpload().Single(x => x.FileUploadId == id);
            return File(file.FilePath, "application/octet-stream", Path.GetFileName(file.FilePath));
        }

        public JsonResult DeleteFile(string id)
        {
            var file = GetFileUpload().Single(x => x.FileUploadId == id);
            file.HasRemoved = true;
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public void DeleteAllFiles()
        {
            ClientSession.FileUploads = null;
        }
    }
}
