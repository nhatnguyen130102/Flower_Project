using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop_Web.Models.Flower_Models
{
    public class StockReceivedDocket
    {
        [Key]
        public int ID_StockReceivedDocket { get; set; }
        public int ID_Shop { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsActive { get; set; }
        public bool? Received { get; set; }
        public DateTime? ReceivedAt { get; set; }

        // Mối quan hệ với Shop
        [ForeignKey("ID_Shop")]
        public Shop? Shop { get; set; }

        public List<StockReceivedDocketDetails>? stockReceivedDocketDetails { get; set; }
    }

}
