using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_Models
{
    public class FlashSale
    {
        [Key]
        public int ID_FlashSale { get; set; }
        public double Price_FlashSale { get; set; }

        public List<Product>? Products { get; set; }
    }
}
