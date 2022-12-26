using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FloviaRestaurant.Models
{
    public class Role : BaseClass
    {
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}