using Flower_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_ViewModels
{
    public class SRDAdminVM
    {
        public int ID_StockReceivedDocket { get; set; }
        public int ID_Shop { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsActive { get; set; }
        public bool? Received { get; set; }
        public DateTime? ReceivedAt { get; set; }

        public List<Shop>? Shop { get; set; }
    }
}
