using Flower_Models;
using Flower_Repository;
using Flower_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern
{
    public class ConcretePost : PostService
    {
        Category category;
        int id;
        private readonly ApplicationDbContext _context;

        public ConcretePost(ApplicationDbContext context)
        {
            _context = context;
        }
        public ConcretePost(Category Post)
        {
            this.category = category;
        }
        public ConcretePost(int id)
        {
            this.id = id;
        }
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
