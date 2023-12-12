using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop_Web.Models.Flower_Models
{
    public class CustomerType
    {
        [Key]
        public int ID_CustomerType { get; set; }
        public string Name_CustomerType { get; set; }
        public int MinSpend { get; set; }
        public int MaxSpend { get; set; }
        public double Discount { get; set; }

        public List<ApplicationUser>? ApplicationUser { get; set; }
    }

}
