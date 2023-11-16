using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_Models
{
    public class MaterialType
    {
        [Key]
        public int ID_MaterialType { get; set; }
        public string Name_MaterialType { get; set; }

        public ICollection<Material>? Materials { get; set; }
    }
}
