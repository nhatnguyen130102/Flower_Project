using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop_Web.Models.Flower_Models
{
    public class MaterialType
    {
        [Key]
        public int ID_MaterialType { get; set; }
        public string Name_MaterialType { get; set; }

        public List<Material>? Materials { get; set; }
    }
}
