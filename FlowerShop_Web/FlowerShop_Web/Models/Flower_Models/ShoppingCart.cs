using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop_Web.Models.Flower_Models
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; }

        public ShoppingCart()
        {
            Items = new List<CartItem>();
        }
    }
}
