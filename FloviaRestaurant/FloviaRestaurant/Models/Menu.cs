using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FloviaRestaurant.Models
{
    public class Menu : BaseClass
    {
        public int Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int FoodItemId { get; set; }
        public FoodItem FoodItem { get; set; }
    }
}