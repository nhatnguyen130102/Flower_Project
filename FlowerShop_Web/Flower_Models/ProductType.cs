using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_Models
{
    public class ProductType
    {
        [Key]
        public int ID_ProductType { get; set; }
        public string Name_ProductType { get; set; }
       

        public List<Product>? Products { get; set; }
    }
}
