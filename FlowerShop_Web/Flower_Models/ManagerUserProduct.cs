using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_Models
{
    public class ManagerUserProduct
    {
        [Key]
        public int ID_MUP { get; set; }
        public string ID_Customer { get; set; }
        public int ID_Product {  get; set; }
        public int ViewCount { get; set; }

        [ForeignKey("ID_Product")]
        public Product Product { get; set; }

        [ForeignKey("ID_Customer")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
