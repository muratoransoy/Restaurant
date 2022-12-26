using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FloviaRestaurant.Models.Entity
{
    public class DataContext:DbContext
    {
        public DataContext(): base("RestaurantConnection") { }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<FoodItem> FoodItem { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
    }
}