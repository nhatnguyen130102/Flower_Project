using Flower_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_ViewModels
{
    public class SRDDVM
    {
        public int ID { get; set; }
        public int ID_StockReceivedDocket { get; set; }
        public int ID_Material { get; set; }
        public int StockReceived_Quantity { get; set; }

        public List<Material>? Materials { get; set; }
        public List<StockReceivedDocketDetails>? StockReceived_Details { get; set; }
    }
}
