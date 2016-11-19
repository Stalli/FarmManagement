using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FarmManagement.Lib
{
    public static class Constant
    {
        public const string DefaultErrorMessage = "Something went wrong.";

        public const string InsufficientBalance = "You don't have insufficent balance in your account to buy this product.";

        public const string SuccessMessage = "Record has been {0} successfully.";
        public const string DeletedMessage = "Record has been deleted successfully.";

        public const string FileUploadMessage = "File has been uploaded successfully;";
        public const string FileUploadPath = "~/App_Data/FileUpload";

    }
}