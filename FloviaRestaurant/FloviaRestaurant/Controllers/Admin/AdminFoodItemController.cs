using FloviaRestaurant.Models;
using FloviaRestaurant.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FloviaRestaurant.Controllers.Admin
{
    public class AdminFoodItemController : Controller
    {
        // GET: AdminFoodItem
        DataContext db = new DataContext();
        [Authorize]
        public ActionResult Index()
        {
            return View(db.FoodItem.Where(x => x.IsDelete == false).ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FoodItem foodItem)
        {
            FoodItem newfood = db.FoodItem.FirstOrDefault(x => x.Name == foodItem.Name && x.IsDelete == false);
            if (newfood != null)
            {
                ViewBag.Message = "Aynı isimde FoodItem tanımlayamazsınız";
                return Redirect(Request.UrlReferrer.ToString());

            }
            newfood = new FoodItem();
            newfood.Name = foodItem.Name;
            newfood.Quantity = foodItem.Quantity;
            newfood.UnitPrice = foodItem.UnitPrice;
            newfood.ItemCategory = foodItem.ItemCategory;
            newfood.IsActive = foodItem.IsActive;
            db.FoodItem.Add(newfood);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            FoodItem editFoot = db.FoodItem.FirstOrDefault(x => x.Id == Id && x.IsDelete == false);
            if (editFoot == null)
            {
                return RedirectToAction("Index");

            }
            return View(editFoot);

        }
        [HttpPost]
        public ActionResult Edit(FoodItem food, bool? IsActive)
        {

            FoodItem foodItem = db.FoodItem.FirstOrDefault(x => x.Id == food.Id&& x.IsDelete == false);
            if (foodItem == null)
            {
                return RedirectToAction("Index");

            }
            Role roleControl = db.Role.FirstOrDefault(x => x.Name == food.Name && x.Id != food.Id && x.IsDelete == false);
            if (roleControl != null)
            {
                ViewBag.Mesaj = "aynı isimde ItemFood tanımlayamazsınız";
                return View(foodItem);
            }

            foodItem.Name = food.Name;
            foodItem.Quantity = food.Quantity;
            foodItem.UnitPrice = food.UnitPrice;
            foodItem.ItemCategory = food.ItemCategory;
            foodItem.IsActive = food.IsActive;
            if (IsActive == null)
            {
                foodItem.IsActive = false;
            }
            else
            {
                foodItem.IsActive = true;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int Id)
        {
            FoodItem delFood = db.FoodItem.FirstOrDefault(x => x.Id == Id);
            if (delFood == null)
            {
                return RedirectToAction("Index");
            }
            delFood.IsDelete = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}