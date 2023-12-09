namespace DesignPattern
{
    public class Order
    {
        private IOrderState _state;

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
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? Message { get; set; }

        public Order()
        {
            // Ban đầu, đơn hàng ở trạng thái chưa xác nhận.
            _state = new OrderUnconfirmedState();
        }

        public void SetState(IOrderState state)
        {
            _state = state;
        }

        public void ConfirmOrder()
        {
            _state.ConfirmOrder(this);
        }

        public void ShipOrder()
        {
            _state.ShipOrder(this);
        }

        public void CompleteOrder()
        {
            _state.CompleteOrder(this);
        }

        public void CancelOrder()
        {
            _state.CancelOrder(this);
        }
    }

}
