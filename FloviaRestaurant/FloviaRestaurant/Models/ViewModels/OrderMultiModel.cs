using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FloviaRestaurant.Models.ViewModels
{
    public class OrderMultiModel
    {
        public List<FoodItem> FoodItems { get; set; }
        public List<User> Users { get; set; }
        public List<Order> Orders { get; set; }
        public Order Order { get; set; }
    }
}