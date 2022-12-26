using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FloviaRestaurant.Models
{
    public class Order : BaseClass
    {
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public int FoodItemId { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public DateTime PickupDate { get; set; }
        public int TotalPrice { get; set; }
        public User User { get; set; }
        public FoodItem FoodItem { get; set; }
    }
}