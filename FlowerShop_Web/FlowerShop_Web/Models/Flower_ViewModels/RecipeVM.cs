using FlowerShop_Web.Models.Flower_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop_Web.Models.Flower_ViewModels
{
    public class RecipeVM
    {
        public int ID_Recipe { get; set; }
        public int ID_Product { get; set; }
        public int ID_Material { get; set; }
        public int Material_Quantity { get; set; }

        public List<Material>? Materials { get; set; }
        public List<Recipe>? Recipes { get; set; }
    }
}
