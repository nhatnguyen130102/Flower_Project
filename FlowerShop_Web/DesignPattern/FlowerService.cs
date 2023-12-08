using Flower_Models;
using Flower_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flower_Services;

namespace DesignPattern
{
    public class FlowerService : IFlowerService
    {
        private readonly ApplicationDbContext _context;

        public FlowerService(ApplicationDbContext context)
        {
            _context = context;
        }

        IEnumerable<Product> IFlowerService.GetAllFlowers()
        {
            var getList = _context.Products.ToList();
            return getList;
        }

        Product IFlowerService.GetFlowerById(int id)
        {
            var getItem = _context.Products.Where(x => x.ID_Product == id).FirstOrDefault();
            return getItem;
        }
    }

}
