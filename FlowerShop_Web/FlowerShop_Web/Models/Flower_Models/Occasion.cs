using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop_Web.Models.Flower_Models
{
    public class Occasion
    {
        [Key]
        public int ID_Occasion { get; set; }
        public string Name_Occasion { get; set; }

        public List<Product>? Products { get; set; }
    }
}
