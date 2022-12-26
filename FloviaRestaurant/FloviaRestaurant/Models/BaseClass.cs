using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FloviaRestaurant.Models
{
    public abstract class BaseClass
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

    }
}