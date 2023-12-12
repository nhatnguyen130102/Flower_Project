using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop_Web.Models.Flower_ViewModels
{
    public class BillDetailVM
    {
        public int ID_Bill { get; set; }
        public string? ID_Customer { get; set; }
        public int? ID_Voucher { get; set; }
        public double Total_Bill { get; set; }
        public double Subtotal { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeliveredAt { get; set; }
        public bool? BillStatus { get; set; }
        public bool? DeliveredStatus { get; set; }
        public bool? HandleStatus { get; set; }
        public bool? Canceled { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Name_Order { get; set; }
        public string Phone_Order { get; set; }
        public int ID_Shop { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public string? Message { get; set; }

        
    }
}
