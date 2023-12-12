using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop_Web.Models.Flower_ViewModels
{
    public class CustomerTypeVM
    {
        public int ID_CustomerType { get; set; }
        public string Name_CustomerType { get; set; }
        public int MinSpend { get; set; }
        public int MaxSpend { get; set; }
        public double Discount { get; set; }
    }
}
