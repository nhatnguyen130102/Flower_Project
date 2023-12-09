using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_ViewModels
{
    public class MaterialVM
    {
        public int ID_Material { get; set; }
        public int ID_MaterialType { get; set; }
        public string Name_Material { get; set; }
        public DateTime ImportAt { get; set; }
        public DateTime EXP_Material { get; set; }
        public double Price_Material { get; set; }
        public string? Img_Material { get; set; }
    }
}
