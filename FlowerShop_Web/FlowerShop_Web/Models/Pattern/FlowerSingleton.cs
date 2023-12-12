using FlowerShop_Web.Models.Flower_Models;
using FloweShop_Web.Models.Flower_Repository;
using FlowerShop_Web.Models.Flower_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop_Web.Models.DesignPattern
{
    public class FlowerSingleton
    {
        private readonly ApplicationDbContext _context;
        public static volatile FlowerSingleton Instance;
        private static readonly object _instanceLock = new object();

        public FlowerSingleton(ApplicationDbContext context)
        {
            _context = context;
        }

        public FlowerSingleton()
        {
        }

        public static FlowerSingleton GetInstance()
        {
            if(Instance == null)
            {
                lock (_instanceLock) ;
                if(Instance == null)
                {
                    Instance = new FlowerSingleton();
                }
            }
            return Instance;
        }

        public void AddProductType(ProductType productType)
        {
            _context.ProductTypes.Add(productType);
            _context.SaveChanges();
        }

        public void RemoveProductType(int? id)
        {
            ProductType item = _context.ProductTypes.Where(x=>x.ID_ProductType == id).FirstOrDefault();
            _context.ProductTypes.Remove(item);
            _context.SaveChanges();
        }

        public void EditProductType(ProductType productType)
        {
            _context.Entry(productType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

    }
}
