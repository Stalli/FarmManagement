using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using FarmManagement.Models;
using FarmManagement.Lib;

namespace FarmManagement.Controllers
{
    public class AccountController : Controller
    {
        FarmManagementEntities dbEntity = new FarmManagementEntities();

        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View(new UserModal());
        }

        [HttpPost]
        public ActionResult Login(UserModal userModel, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                this.ShowErrorMessage(Constant.DefaultErrorMessage);
                return View();
            }

            var user = dbEntity.Users.SingleOrDefault(x => x.UserName == userModel.UserName);
            if (user == null)
            {
                this.ShowErrorMessage("Incorrect username or passsword");
                return View();
            }

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.UserName, DateTime.Now, DateTime.Now.AddDays(7), userModel.RememberMe, user.UserName);
            string hashCookies = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashCookies);
            Response.Cookies.Add(cookie);

            user.LastLogin = DateTime.Now;
            dbEntity.SaveChanges();

            HttpCookie myCookie = new HttpCookie("FarmCookies");
            //Add key-values in the cookie
            myCookie.Values.Add("UserId", user.Id.ToString());
            myCookie.Values.Add("Role", user.RoleId.ToString());
            myCookie.Values.Add("IsAdmin", user.IsAdmin.ToString());

            //set cookie expiry date-time. Made it to last for next 12 hours.
            myCookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(myCookie);

            LoggedInUser.Name = user.Name;
            LoggedInUser.UserId = user.Id;
            LoggedInUser.IsAdmin = user.IsAdmin;
            LoggedInUser.RoleId = user.RoleId;

            var baseController = new BaseController();
            baseController.GetFormSetting();

            if (!string.IsNullOrEmpty(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }

            return RedirectToAction("Dashboard", "Home");
        }

        public ActionResult LogOff()
        {
            Response.Cookies.Remove("FarmCookies");
            FormsAuthentication.SignOut();

            ClearAllSession();

            return RedirectToAction("Login", "Account");
        }

        public ActionResult Default()
        {
            return View();
        }

        private void ClearAllSession()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
        }
    }
}
