using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace FarmManagement.Lib
{
    public static class NotificationMessage
    {
        public static void ShowSuccessMessage(this Controller controller, string message)
        {
            ShowNotificationMessage(controller, message, NotificationType.success);
        }

        public static void ShowErrorMessage(this Controller controller, string message)
        {
            ShowNotificationMessage(controller, message, NotificationType.error);
        }

        public static void ShowWarningMessage(this Controller controller, string message)
        {
            ShowNotificationMessage(controller, message, NotificationType.warning);
        }

        public static void ShowInformationMessage(this Controller controller, string message)
        {
            ShowNotificationMessage(controller, message, NotificationType.information);
        }

        public static void ShowNotificationMessage(Controller controller, string message, NotificationType notificationType)
        {
            if (controller.Request.IsAjaxRequest())
            {
                return; 
            }

            controller.TempData["NotificationMessage"] = string.Format("showNotificationMessage('{0}', '{1}', {2})", message, notificationType, 2000);
        }
    }

    public enum NotificationType
    {
        success,
        error,
        warning,
        information
    }
}