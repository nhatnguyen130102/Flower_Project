using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop_Web.Models.Flower_Models
{
    public class Recipe
    {
        [Key]
        public int ID_Recipe { get; set; }
        public int ID_Product { get; set; }
        public int ID_Material { get; set; }
        public int Material_Quantity { get; set; }

        // Mối quan hệ với Product       
        [ForeignKey("ID_Product")]
        public Product? Product { get; set; }

        // Mối quan hệ với Material
        [ForeignKey("ID_Material")]
        public Material? Material { get; set; }
    }

}
