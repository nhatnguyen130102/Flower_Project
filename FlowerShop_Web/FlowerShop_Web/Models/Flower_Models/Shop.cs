using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop_Web.Models.Flower_Models
{
    public class Shop
    {
        [Key]
        public int ID_Shop { get; set; }
        public string Name_Shop { get; set; }
        public string Address_Shop { get; set; }
        public string Phone_Shop { get; set; }
        public int ID_Locations { get; set; }

        public List<MaterialWarehouse>? MaterialWarehouses { get; set; }
        public List<ProductWarehouse>? ProductWarehouses { get; set; }
        public List<StockReceivedDocket>? StockReceivedDockets { get; set; }

        // Mối quan hệ với Locations
        [ForeignKey("ID_Locations")]
        public Locations? Locations { get; set; }
    }

}
