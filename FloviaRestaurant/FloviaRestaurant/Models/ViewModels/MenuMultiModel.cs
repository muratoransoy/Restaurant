using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FloviaRestaurant.Models.ViewModels
{
    public class MenuMultiModel
    {
        public List<FoodItem> foodItems { get; set; }
        public List<Menu> Menus { get; set; }
        public Menu Menu { get; set; }
    }
}