﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_Models
{
    public class Material
    {
        [Key]
        public int ID_Material { get; set; }
        public int ID_MaterialType { get; set; }
        public string Name_Material { get; set; }
        public DateTime ImportAt { get; set; }
        public DateTime EXP_Material { get; set; }
        public double Price_Material { get; set; }
        public string? Img_Material { get; set; }

        public ICollection<MaterialWarehouse>? Warehouse_Warehouse { get; set; }
        public ICollection<Recipe>? Recipes { get; set; }
        public ICollection<StockReceivedDocketDetails>? StockReceivedDocketDetails { get; set; }

        // Mối quan hệ với MaterialType
        [ForeignKey("ID_MaterialType")]
        public MaterialType? MaterialType { get; set; }
    }
}
