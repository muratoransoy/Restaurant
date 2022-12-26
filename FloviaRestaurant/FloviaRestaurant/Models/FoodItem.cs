using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace FloviaRestaurant.Models
{
    public class FoodItem:BaseClass
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public string ItemCategory { get; set; }
        public List<Menu> Menus { get; set; }
        public List<Order> Orders { get; set; }
    }
}