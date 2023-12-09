using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_Models
{
    public class Category
    {
     
        [Key]
        public int ID_Category { get; set; }
        public string Name_Category { get; set; }

        public List<Post>? Posts { get; set; }
    }
}
