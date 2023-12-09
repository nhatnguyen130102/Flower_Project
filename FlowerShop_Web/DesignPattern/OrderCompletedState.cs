﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    public class OrderCompletedState : IOrderState
    {
        public string CancelOrder(Order order)
        {
            return $"Không thể huỷ đơn hàng {order.ID_Bill}.";
        }

        public string ConfirmOrder(Order order)
        {
            return $"Không thể xác nhận đơn hàng {order.ID_Bill}.";
        }

        public string ShipOrder(Order order)
        {
            return $"Không vận chuyển đơn hàng {order.ID_Bill}.";

        }

        public string CompleteOrder(Order order)
        {
            return $"Không thể hoàn thành đơn hàng {order.ID_Bill}.";
        }
    }
}
