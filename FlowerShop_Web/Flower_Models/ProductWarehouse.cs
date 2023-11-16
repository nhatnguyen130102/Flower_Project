using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_Models
{
    public class ProductWarehouse
    {
        
        public int ID { get; set; }
        public int ID_Shop { get; set; }
        public int ID_Product { get; set; }
        public int InStock_Quantity { get; set; }
        public int Sold_Quantity { get; set; }

        // Mối quan hệ với Shop
        [ForeignKey("ID_Shop")]
        public Shop? Shop { get; set; }

        // Mối quan hệ với Product
        [ForeignKey("ID_Product")]
        public Product? Product { get; set; }
    }
}
