using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop_Web.Models.DesignPattern
{
    public interface IOrderState
    {
        string ConfirmOrder(Order order);
        string ShipOrder(Order order);  
        string CompleteOrder(Order order);
        string CancelOrder(Order order);
    }
}
