using Flower_Models;
using Flower_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    public class PromotionFlowerDecorator : FlowerDecoratorBase
    {
        public PromotionFlowerDecorator(IFlowerService flowerService) : base(flowerService)
        {
        }

        private readonly int productIdToPromote;

        public PromotionFlowerDecorator(IFlowerService flowerService, int productIdToPromote) : base(flowerService)
        {
            this.productIdToPromote = productIdToPromote;
        }

        public override IEnumerable<Product> GetAllFlowers()
        {
            // Thêm logic để áp dụng khuyến mãi vào danh sách hoa
            var flowers = base.GetAllFlowers();
            foreach (var flower in flowers)
            {
                flower.Name_Product += " (Sale)";
                // Thêm logic khuyến mãi khác nếu cần
              
            }
            return flowers;
        }

        public override Product GetFlowerById(int id)
        {
            var flower = base.GetFlowerById(id);
          
                    flower.Name_Product += " (Sale)";
                    // Thêm logic khuyến mãi khác nếu cần
                    flower.Price_Product += 100;
        
            return flower;
        }
    }

}
