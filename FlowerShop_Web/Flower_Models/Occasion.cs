using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_Models
{
    public class Occasion
    {
        [Key]
        public int ID_Occasion { get; set; }
        public string Name_Occasion { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
