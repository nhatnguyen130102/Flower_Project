using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_Models
{
    public class Cart
    {
        [Key]
        public int ID_Cart { get; set; }
        public string? ID_Customer { get; set; }

        // Mối quan hệ với Customer
        [ForeignKey("ID_Customer")]
        public ApplicationUser? ApplicationUser { get; set; }
        public ICollection<CartDetails>? CartDetails { get; set; }  
    }

}
