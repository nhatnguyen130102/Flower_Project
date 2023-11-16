using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_Models
{
    public class Locations
    {
        [Key]
        public int ID_Locations { get; set; }
        public string Name_Locations { get; set; }

        public ICollection<Shop>?  Shops { get; set; }
    }

}
