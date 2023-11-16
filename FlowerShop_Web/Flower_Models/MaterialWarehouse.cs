using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_Models
{
    public class MaterialWarehouse
    {
        public int ID { get; set; }
        public int ID_Shop { get; set; }
        public int ID_Material { get; set; }
        public int InStock_Quantity { get; set; }
        public int Sold_Quantity { get; set; }

        // Mối quan hệ với Shop
        [ForeignKey("ID_Shop")]
        public Shop? Shop { get; set; }

        // Mối quan hệ với Material
        [ForeignKey("ID_Material")]
        public Material? Material { get; set; }
    }

}
