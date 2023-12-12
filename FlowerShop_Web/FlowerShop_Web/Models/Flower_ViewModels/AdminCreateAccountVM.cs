using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop_Web.Models.Flower_ViewModels
{
    public class AdminCreateAccountVM
    {
        public string? ID { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }

        public int? ID_CustomerType { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public int? ID_Shop { get; set; }
        public string? ID_Role { get; set; }
        public double? Spend { get; set; }

        public string? NewPassword { get; set; }
    }
}
