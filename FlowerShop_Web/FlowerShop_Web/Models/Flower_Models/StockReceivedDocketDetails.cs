using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop_Web.Models.Flower_Models
{
    public class StockReceivedDocketDetails
    {
        public int ID {  get; set; }
        public int ID_StockReceivedDocket { get; set; }
        public int ID_Material { get; set; }
        public int StockReceived_Quantity { get; set; }

        // Mối quan hệ với Material
        [ForeignKey("ID_Material")]
        public Material? Material { get; set; }

        // Mối quan hệ với StockReceivedDocker
        [ForeignKey("ID_StockReceivedDocket")]
        public StockReceivedDocket? StockReceivedDocket { get; set; }
    }

}
