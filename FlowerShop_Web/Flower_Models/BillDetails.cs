using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_Models
{
    public class BillDetails
    {
        public int ID {  get; set; }
        public int ID_Bill { get; set; }
        public int ID_Product { get; set; }
        public int Product_Quantity { get; set; }
        public double Total { get; set; }

        // Mối quan hệ với Product
        [ForeignKey("ID_Product")]
        public Product? Product { get; set; }
        [ForeignKey("ID_Bill")]
        public Bill? Bill { get; set; }
    }

}
