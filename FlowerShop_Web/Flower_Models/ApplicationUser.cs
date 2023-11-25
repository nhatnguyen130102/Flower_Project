using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string? LastName { get; set; }
        [PersonalData]
        public string? FirstName { get; set; }
        [PersonalData]
        public string? Address { get; set; }
        [PersonalData]
        public string? District { get; set; }
        [PersonalData]
        public string? Street { get; set; }
        [PersonalData]
        public string? Ward { get; set; }
        [PersonalData]
        public string? City { get; set; }
        [PersonalData]
        public int? ID_CustomerType { get; set; }

        public int? ID_Shop { get; set; }

        public double? Spend { get; set; }

        [ForeignKey("ID_CustomerType")]
        public CustomerType? CustomerType { get; set; }

        [ForeignKey("ID_Shop")]
        public Shop? Shop { get; set; }

        public List<FavoriteProduct>? FavoriteProducts { get; set; }
        public List<Bill>? Bills { get; set; }
        public List<Cart>? Carts { get; set; }
        public List<ManagerUserProduct>? ManagerUserProduct { get; set; }
    }
}
