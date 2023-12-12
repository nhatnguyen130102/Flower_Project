using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop_Web.Models.Flower_ViewModels
{
    public class MomoPaymentRequest
    {
        public string partnerCode { get; set; }
        public string accessKey { get; set; }
        public string requestId { get; set; }
        public string amount { get; set; }
        public string orderId { get; set; }
        public string orderInfo { get; set; }
        public string returnUrl { get; set; }
        public string notifyUrl { get; set; }
        public string extraData { get; set; }
        public string requestType { get; set; }
        public string signature { get; set; }
    }
}
