using FlowerShop_Web.Models.Flower_Models;
using FlowerShop_Web.Models.Flower_Services;
using FlowerShop_Web.Models.Flower_ViewModels;

namespace FlowerShop_Web.Models.DesignPattern
{
    public class PostProxyPattern : PostService
    {
        public PostService Post;
        public Category Category;
        int id;


        public PostProxyPattern(Category category)
        {
            this.Category = category;
        }

        public PostProxyPattern(int id)
        {
            this.id = id;
        }

        public override void AddPost()
        {
            if (Post == null)
            {
                Post = new ConcretePost(Category);
            }
            Post.AddPost();
        }

        public override void RemovePost()
        {
            if(Post == null)
            {
                Post = new ConcretePost(id);
            }
            Post.RemovePost();
        }

        public override void EditPost()
        {
            if(Post == null)
            {
                Post = new ConcretePost(Category);
            }
            Post.EditPost();
        }
    }
}
