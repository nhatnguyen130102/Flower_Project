using Flower_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_Services
{
    public interface IFlowerService
    {
        IEnumerable<Product> GetAllFlowers();
        Product GetFlowerById(int id);
    }
}
