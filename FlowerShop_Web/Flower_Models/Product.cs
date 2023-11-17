using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_Models
{
    public class Product
    {
        [Key]
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

        public ICollection<FavoriteProductDetails>? FavoriteProductDetails { get; set; }
        public ICollection<BillDetails>? BillDetails { get; set; }
        public ICollection<CartDetails>? CartDetails { get; set; }
        public ICollection<ProductWarehouse>? ProductWarehouses { get; set; }
        public ICollection<Recipe>? Recipes { get; set; }
        public ICollection<ManagerUserProduct>? ManagerUsers { get; set; }

        //[ForeignKey("ID_Shop")]
        //public Shop? Shop { get; set; }

        // Mối quan hệ với Occasion
        [ForeignKey("ID_Occasion")]
        public Occasion? Occasion { get; set; }

        // Mối quan hệ với FlashSale
        [ForeignKey("ID_FlashSale")]
        public FlashSale? FlashSale { get; set; }

        // Mối quan hệ với ProductType
        [ForeignKey("ID_ProductType")]
        public ProductType? ProductType { get; set; }
    }
    public enum size
    {
        Classic,
        Premium,
        Deluxe
    }
    public static class EnumExtensions
    {
        public static string ToEnumString(this size enumValue)
        {
            switch (enumValue)
            {
                case size.Classic:
                    return "Classic";
                case size.Premium:
                    return "Premium";
                case size.Deluxe:
                    return "Deluxe";
                default:
                    return "Giá trị không xác định";
            }
        }
    }
}
