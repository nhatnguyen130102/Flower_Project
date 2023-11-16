using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_Models
{
    public class Shop
    {
        [Key]
        public int ID_Shop { get; set; }
        public string Name_Shop { get; set; }
        public string Address_Shop { get; set; }
        public string Phone_Shop { get; set; }
        public int ID_Locations { get; set; }

        public ICollection<MaterialWarehouse>? MaterialWarehouses { get; set; }
        public ICollection<ProductWarehouse>? ProductWarehouses { get; set; }
        public ICollection<StockReceivedDocket>? StockReceivedDockets { get; set; }

        // Mối quan hệ với Locations
        [ForeignKey("ID_Locations")]
        public Locations? Locations { get; set; }
    }

}
