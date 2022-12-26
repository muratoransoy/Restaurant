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
    public class AdminMenuController : Controller
    {
        DataContext db = new DataContext();
        // GET: AdminMenu
        [Authorize]
        public ActionResult Index()
        {
            MenuMultiModel menumodel = new MenuMultiModel
            {
                Menus = db.Menu.Where(x => x.IsDelete == false).ToList(),
                foodItems = db.FoodItem.Where(x => x.IsDelete == false && x.IsActive == true).ToList()
            };
            return View(menumodel);
        }
        [HttpGet]
        public ActionResult Create()
        {
            List<FoodItem> foodList = new List<FoodItem>();
            foodList = db.FoodItem.Where(x => x.IsDelete == false && x.IsActive == true).ToList();
            return View(foodList);
        }
        [HttpPost]
        public ActionResult Create(Menu menu)
        {
            Menu menuControl = db.Menu.FirstOrDefault(x => x.FoodItemId == menu.FoodItemId && x.IsDelete == false);
            if (menuControl != null)
            {
                return RedirectToAction("index");
            }
            Menu newMenu = new Menu();
            newMenu.Price = menu.Price;
            newMenu.StartDate = menu.StartDate;
            newMenu.EndDate = menu.EndDate;
            newMenu.FoodItemId = menu.FoodItemId;
            newMenu.IsActive = menu.IsActive;
            db.Menu.Add(newMenu);
            db.SaveChanges();


            return RedirectToAction("index");
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            MenuMultiModel menuModel = new MenuMultiModel();
            menuModel.foodItems = db.FoodItem.Where(x => x.IsDelete == false && x.IsActive == true).ToList();
            menuModel.Menu = db.Menu.FirstOrDefault(x => x.Id == Id && x.IsDelete == false);
            if (menuModel.Menu == null)
            {
                return RedirectToAction("index");
            }
            return View(menuModel);
        }
        [HttpPost]
        public ActionResult Edit(Menu menu)
        {
            Menu EditMenu = new Menu();
            EditMenu = db.Menu.Find(menu.Id);
            Menu menuControl = new Menu();

            if (EditMenu == null)
            {
                return RedirectToAction("index");
            }
            menuControl = db.Menu.FirstOrDefault(x => x.Price == menu.Price ||
            x.StartDate == menu.StartDate && x.IsDelete == false && x.Id != menu.Id);
            if (menuControl != null)
            {
                MenuMultiModel menuModel = new MenuMultiModel()
                {
                    foodItems = db.FoodItem.Where(x => x.IsDelete == false && x.IsActive == true).ToList(),
                    Menu = EditMenu
                };

                ViewBag.mesaj = "aynı Fiyat yada Başlangıç tarihi kullanılamaz";
                return View(menuModel);
            }
            EditMenu.Price = menu.Price;
            EditMenu.StartDate = menu.StartDate;
            EditMenu.EndDate = menu.EndDate;
            EditMenu.FoodItemId = menu.FoodItemId;
            EditMenu.IsActive = menu.IsActive;
            db.SaveChanges();


            return RedirectToAction("index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {


            MenuMultiModel menuModel = new MenuMultiModel();
            menuModel.foodItems = db.FoodItem.Where(x => x.IsDelete == false && x.IsActive == true).ToList();
            menuModel.Menu = db.Menu.FirstOrDefault(x => x.Id == id && x.IsDelete == false);
            if (menuModel.Menu == null)
            {
                return RedirectToAction("index");
            }
            return View(menuModel);




        }

        [HttpPost]
        public ActionResult Delete(Menu menu)
        {

            Menu delMenu = new Menu();
            delMenu = db.Menu.Find(menu.Id);
            delMenu.IsDelete = true;
            db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}