using FloviaRestaurant.Models;
using FloviaRestaurant.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FloviaRestaurant.Controllers.Admin
{
    public class AdminHomeController : Controller
    {
        // GET: AdminHome
        DataContext db = new DataContext();
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            User usercontrol = new User();
            usercontrol = db.User.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password && x.IsDelete == false);
            if (usercontrol != null)
            {
                if (usercontrol.IsActive == false)
                {
                    ViewBag.Message = "Bu Kullanıcı Aktif Değil";
                    return View();
                }
                FormsAuthentication.SetAuthCookie(usercontrol.UserName, true);
                if (usercontrol.RoleId == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return Redirect("~/AdminHome/Index");
                }
            }
            else
            {
                ViewBag.Message = "Kullanıcı Adı veya Şifre Yanlış!!!";
                return View();
            }


        }
    }
}