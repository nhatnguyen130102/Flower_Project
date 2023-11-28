using Flower_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_ViewModels
{
    public class MaterialWHAdminVM
    {
        public int ID { get; set; }
        public int ID_Shop { get; set; }
        public int ID_Material { get; set; }
        public int InStock_Quantity { get; set; }
        public int Sold_Quantity { get; set; }

        public List<Shop>? Shop { get; set; }
    }
}
