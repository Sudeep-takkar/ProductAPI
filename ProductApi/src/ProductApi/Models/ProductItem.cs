using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductApi.Models
{
    public class ProductItem
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public string Discount { get; set; }
        public int Token { get; set; }
        public string IsAvailable { get; set; }
    }
}
