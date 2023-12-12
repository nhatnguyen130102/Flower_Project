using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop_Web.Models.Flower_ViewModels
{
    public class ProductServiceVM
    {
        public int ID_Product { get; set; }
        public int ID_Occasion { get; set; }
        public string Name_Product { get; set; }
        public double Price_Product { get; set; }
        public string? Img_Product { get; set; }
        public bool isAvailabled { get; set; }
        public bool isDiscontinued { get; set; }
        public DateTime CreatedAt { get; set; }
        public double? Rating { get; set; }
        public int? ViewCount { get; set; }
        public int? ID_FlashSale { get; set; }
        public int ID_ProductType { get; set; }
        public string? size { get; set; }
    }
}
