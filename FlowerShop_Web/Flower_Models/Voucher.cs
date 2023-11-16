using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_Models
{
    public class Voucher
    {
        [Key]
        public int ID_Voucher { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public double Discount { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public int Voucher_Quantity { get; set; }
        public double MinimumAmount { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Bill>? Bills { get; set; }

    }
    public enum VoucherType
    {
        Percent,
        Currency,
    }
}
