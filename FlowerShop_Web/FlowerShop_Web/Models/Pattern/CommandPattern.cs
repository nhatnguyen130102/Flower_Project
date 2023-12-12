using FloweShop_Web.Models.Flower_Repository;

namespace FlowerShop_Web.Models.Pattern
{
    public class CommandPattern
    {
    }
    public interface ICommand
    {
        string Execute(int? id);
    }
    public class StatusOrderCommand : ICommand
    {
        private readonly ApplicationDbContext _context;
        public StatusOrderCommand(ApplicationDbContext context)
        {
            _context = context;
        }
        public string Execute(int? id) 
        {
            var bill = _context.Bills.FirstOrDefault(x => x.ID_Bill == id);
            if (bill == null)
                return "Failed";
            bill.HandleStatus = !bill.HandleStatus;
            _context.Bills.Update(bill);
            _context.SaveChanges();
            return "Pass";
        }
    }
    public class DeliveredOrderCommand : ICommand
    {
        private readonly ApplicationDbContext _context;
        public DeliveredOrderCommand(ApplicationDbContext context)
        {
            _context = context;
        }
        public string Execute(int? id)
        {
            var bill = _context.Bills.FirstOrDefault(x => x.ID_Bill == id);
            if (bill == null)
                return "Failed";
            bill.DeliveredStatus = !bill.DeliveredStatus;
            _context.Bills.Update(bill);
            _context.SaveChanges();
            return "Pass";
        }
    }
    public class CompletedOrderCommand : ICommand
    {
        private readonly ApplicationDbContext _context;
        public CompletedOrderCommand(ApplicationDbContext context)
        {
            _context = context;
        }
        public string Execute(int? id)
        {
            var bill = _context.Bills.FirstOrDefault(x => x.ID_Bill == id);
            if (bill == null)
                return "Failed";
            bill.BillStatus = !bill.BillStatus;
            _context.Bills.Update(bill);
            _context.SaveChanges();
            return "Pass";
        }
    }
}
