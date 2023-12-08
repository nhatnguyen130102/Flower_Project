using Flower_Models;
using Flower_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    public abstract class FlowerDecoratorBase : IFlowerService
    {
        protected IFlowerService flowerService;

        protected FlowerDecoratorBase(IFlowerService flowerService)
        {
            this.flowerService = flowerService;
        }

        public virtual IEnumerable<Product> GetAllFlowers()
        {
            return flowerService.GetAllFlowers();
        }

        public virtual Product GetFlowerById(int id)
        {
            return flowerService.GetFlowerById(id);
        }
    }

}
