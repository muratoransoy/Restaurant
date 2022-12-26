using FloviaRestaurant.Models.Entity;
using FloviaRestaurant.Models.ViewModels;
using FloviaRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FloviaRestaurant.Controllers.Admin
{
    public class AdminOrderController : Controller
    {
        DataContext db = new DataContext();
        // GET: AdminUser
        [Authorize]
        public ActionResult Index()
        {
            OrderMultiModel orderMulti = new OrderMultiModel();
            orderMulti.Users = db.User.Where(x => x.IsDelete == false && x.IsActive == true).ToList();
            orderMulti.FoodItems = db.FoodItem.Where(x => x.IsDelete == false && x.IsActive == true).ToList();
            orderMulti.Orders = db.Order.Where(x => x.IsDelete == false && x.IsActive == true).ToList();

            return View(orderMulti);
        }
        [HttpGet]
        public ActionResult Create()
        {
            OrderMultiModel orderMulti = new OrderMultiModel();
            orderMulti.Users = db.User.Where(x => x.IsDelete == false && x.IsActive == true).ToList();
            orderMulti.FoodItems = db.FoodItem.Where(x => x.IsDelete == false && x.IsActive == true).ToList();
            return View(orderMulti);
        }
        [HttpPost]
        public ActionResult Create(Order order)
        {

            Order newOrder = new Order();
            newOrder = order;
            FoodItem f = db.FoodItem.FirstOrDefault(x => x.Id == newOrder.FoodItemId);
            newOrder.TotalPrice = newOrder.UnitPrice * newOrder.Quantity * f.Quantity;
            db.Order.Add(newOrder);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            OrderMultiModel om = new OrderMultiModel();
            om.Order = db.Order.FirstOrDefault(x => x.Id == Id);
            om.Users = db.User.Where(x => x.IsDelete == false && x.IsActive == true).ToList();
            om.FoodItems = db.FoodItem.Where(x => x.IsDelete == false && x.IsActive == true).ToList();
            if (om.Users == null)
            {
                return RedirectToAction("index");
            }
            return View(om);
      

        }
        [HttpPost]
        public ActionResult Edit(Order order, bool? IsActive)
        {

            Order orderItem = db.Order.FirstOrDefault(x => x.Id == order.Id && x.IsDelete == false);
            if (orderItem == null)
            {
                return RedirectToAction("Index");

            }
            orderItem.OrderDate = order.OrderDate;
            orderItem.UserId = order.UserId;
            orderItem.FoodItemId = order.FoodItemId;
            orderItem.Quantity = order.Quantity;
            orderItem.UnitPrice = order.UnitPrice;
            orderItem.TotalPrice = order.TotalPrice;
            orderItem.PickupDate = order.PickupDate;
            orderItem.IsActive = order.IsActive;
            FoodItem f = db.FoodItem.FirstOrDefault(x => x.Id == orderItem.FoodItemId);
            orderItem.TotalPrice = order.Quantity * f.UnitPrice * orderItem.Quantity;
            if (IsActive == null)
            {
                orderItem.IsActive = false;
            }
            else
            {
                orderItem.IsActive = true;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {


            OrderMultiModel orderModel = new OrderMultiModel();
            orderModel.Users = db.User.Where(x => x.IsDelete == false && x.IsActive == true).ToList();
            orderModel.Order = db.Order.FirstOrDefault(x => x.Id == id && x.IsDelete == false);
            orderModel.FoodItems = db.FoodItem.Where(x => x.IsDelete == false && x.IsActive == true).ToList();
            if (orderModel.Order == null)
            {
                return RedirectToAction("index");
            }
            return View(orderModel);




        }
        [HttpPost]
        public ActionResult Delete(Order order)
        {

            Order delOrder = new Order();
            delOrder = db.Order.Find(order.Id);
            delOrder.IsDelete = true;
            db.SaveChanges();
            return RedirectToAction("Index");



        }
    }
}