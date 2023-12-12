using FlowerShop_Web.Models.Flower_Models;
using FloweShop_Web.Models.Flower_Repository;
using FlowerShop_Web.Models.Flower_Services;
using FlowerShop_Web.Models.Flower_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop_Web.Models.DesignPattern
{
    public class ConcretePost : PostService
    {
        Category category;
        int id;
        private Category category1;
        private readonly ApplicationDbContext _context;

        public ConcretePost(ApplicationDbContext context)
        {
            _context = context;
        }
        public ConcretePost(Category Post)
        {
            this.Category = category;
        }
        public ConcretePost(int id)
        {
            this.id = id;
        }

        //public ConcretePost(Category category1)
        //{
        //    this.category1 = category1;
        //}

        public override void AddPost()
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        public override void EditPost()
        {
            _context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }
        public override void RemovePost()
        {
            Category item = _context.Categories.Where(x => x.ID_Category == id).FirstOrDefault();
            _context.Categories.Remove(item);
            _context.SaveChanges();
        }
    }
}
