using Flower_Models;
using Flower_Services;
using Flower_ViewModels;

namespace DesignPattern
{
    public class PostProxyPattern : PostService
    {
        PostService Post;
        Category Category;
        int id;

        public PostProxyPattern()
        {
            Post = null;
        }

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
