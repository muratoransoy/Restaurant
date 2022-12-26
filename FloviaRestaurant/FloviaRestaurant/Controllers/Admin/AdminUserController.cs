using FloviaRestaurant.Models;
using FloviaRestaurant.Models.Entity;
using FloviaRestaurant.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FloviaRestaurant.Controllers.Admin
{
    public class AdminUserController : Controller
    {
        
        DataContext db = new DataContext();
        // GET: AdminUser
        [Authorize]
        public ActionResult Index()
        {
            UserMultiModel userModel = new UserMultiModel
            {
                Users = db.User.Where(x => x.IsDelete == false).ToList(),
                Roles = db.Role.Where(x => x.IsDelete == false && x.IsActive == true).ToList()
            };
            return View(userModel);
        }
        [HttpGet]
        public ActionResult Create()
        {
            List<Role> roleList = new List<Role>();
            roleList = db.Role.Where(x => x.IsDelete == false && x.IsActive == true).ToList();
            return View(roleList);
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            User userControl = db.User.FirstOrDefault(x => x.UserName == user.UserName || x.Email == user.Email);
            if (userControl != null)
            {
                return RedirectToAction("index");
            }
            User newUSer = new User();
            newUSer.UserName = user.UserName;
            newUSer.Email = user.Email;
            newUSer.Name = user.Name;
            newUSer.Surname = user.Surname;
            newUSer.Password = user.Password;
            newUSer.BirthDay = user.BirthDay;
            newUSer.Phone = user.Phone;
            newUSer.RoleId = user.RoleId;
            newUSer.IsActive = user.IsActive;
            db.User.Add(newUSer);
            db.SaveChanges();


            return RedirectToAction("index");
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            UserMultiModel userModel = new UserMultiModel();
            userModel.Roles = db.Role.Where(x => x.IsDelete == false && x.IsActive == true).ToList();
            userModel.User = db.User.FirstOrDefault(x => x.Id == Id && x.IsDelete == false);
            if (userModel.User == null)
            {
                return RedirectToAction("index");
            }
            return View(userModel);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            User EditUser = new User();
            EditUser = db.User.Find(user.Id);
            User userControl = new User();

            if (EditUser == null)
            {
                return RedirectToAction("index");
            }
            userControl = db.User.FirstOrDefault(x => x.UserName == user.UserName ||
            x.Email == user.Email && x.IsDelete == false && x.Id != user.Id);
            if (userControl != null)
            {
                UserMultiModel userModel = new UserMultiModel()
                {
                    Roles = db.Role.Where(x => x.IsDelete == false && x.IsActive == true).ToList(),
                    User = EditUser
                };

                ViewBag.mesaj = "aynı kullanıcı adı yada mail kullanılamaz";
                return View(userModel);
            }
            EditUser.UserName = user.UserName;
            EditUser.Email = user.Email;
            EditUser.Name = user.Name;
            EditUser.Surname = user.Surname;
            EditUser.RoleId = user.RoleId;
            EditUser.BirthDay = user.BirthDay;
            EditUser.Phone = user.Phone;
            EditUser.Password = user.Password;
            EditUser.IsActive = user.IsActive;
            db.SaveChanges();


            return RedirectToAction("index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {


            UserMultiModel userModel = new UserMultiModel();
            userModel.Roles = db.Role.Where(x => x.IsDelete == false && x.IsActive == true).ToList();
            userModel.User = db.User.FirstOrDefault(x => x.Id == id && x.IsDelete == false);
            if (userModel.User == null)
            {
                return RedirectToAction("index");
            }
            return View(userModel);




        }

        [HttpPost]
        public ActionResult Delete(User user)
        {

            User delUser = new User();
            delUser = db.User.Find(user.Id);
            delUser.IsDelete = true;
            db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}