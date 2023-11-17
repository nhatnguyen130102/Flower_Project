using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_Models
{
    public class FavoriteProductDetails
    {
        [Key]
        public int ID { get; set; }
        public int ID_FavoriteProduct { get; set; }
        public int? ID_Product { get; set; }

        // Mối quan hệ với Product
        [ForeignKey("ID_Product")]
        public Product? Product { get; set; }

        // Mối quan hệ với Product
        [ForeignKey("ID_FavoriteProduct")]
        public FavoriteProduct? FavoriteProducts { get; set; }
    }
}
