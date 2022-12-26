using FloviaRestaurant.Models;
using FloviaRestaurant.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace FloviaRestaurant.Controllers.Admin
{
    public class RoleController : Controller
    {
        // GET: Role
        DataContext db=new DataContext();
        [Authorize]

        public ActionResult Index()
        {
            return View(db.Role.Where(x => x.IsDelete == false).ToList());
        }
        [HttpGet]
        public ActionResult Create() 
        { 
            return View();
        }
        [HttpPost]
        public ActionResult Create(Role role) 
        {
            Role newRole = db.Role.FirstOrDefault(x => x.Name == role.Name && x.IsDelete == false);
            if(newRole != null)
            {
                ViewBag.Message = "Aynı isimde role tanımlayamazsınız";
                return Redirect(Request.UrlReferrer.ToString());

            }
            newRole = new Role();
            newRole.Name = role.Name;
            newRole.IsActive= role.IsActive;
            db.Role.Add(newRole);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            if (Id == 1 || Id == 2)
            {
                return RedirectToAction("Index");
            }
            Role editRole = db.Role.FirstOrDefault(x => x.Id == Id && x.IsDelete == false);
            if (editRole == null)
            {
                return RedirectToAction("Index");

            }
            return View(editRole);

        }
        [HttpPost]
        public ActionResult Edit(int Id, string Name, bool? IsActive)
        {

            Role editRole = db.Role.FirstOrDefault(x => x.Id == Id && x.IsDelete == false);
            if (editRole == null)
            {
                return RedirectToAction("Index");

            }
            Role roleControl = db.Role.FirstOrDefault(x => x.Name == Name && x.Id != Id && x.IsDelete == false);
            if (roleControl != null)
            {
                ViewBag.Mesaj = "aynı isimde role tanımlayamazsınız";
                return View(editRole);//sayfayı yeniler
            }

            editRole.Name = Name;
            if (IsActive == null)
            {
                editRole.IsActive = false;
            }
            else
            {
                editRole.IsActive = true;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int Id)
        {
            if (Id == 1 || Id == 2)
            {
                return RedirectToAction("Index");
            }
            Role delRole = db.Role.FirstOrDefault(x => x.Id == Id);
            if (delRole == null)
            {
                return RedirectToAction("Index");
            }
            delRole.IsDelete = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}