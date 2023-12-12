using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop_Web.Models.DesignPattern
{
    public class OrderUnconfirmedState : IOrderState
    {
        public string CancelOrder(Order order)
        {
            order.Canceled = true;
            order.SetState(new OrderCancelState());
            return $"Đơn hàng {order.ID_Bill} đã được huỷ.";
        }

        public string ConfirmOrder(Order order)
        {
            order.BillStatus = true;
            order.SetState(new OrderConfirmedState());
            return $"Đơn hàng {order.ID_Bill} đã được xác nhận.";
        }

        public string ShipOrder(Order order)
        {
            return $"Không thể vận chuyển đơn hàng {order.ID_Bill}.";
        }

        public string CompleteOrder(Order order)
        {
            return $"Không thể hoàn thành đơn hàng {order.ID_Bill}.";
        }
    }
}
