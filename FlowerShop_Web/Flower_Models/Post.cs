using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Flower_Models
{
    public class Post
    {
        [Key]
        public int ID_Post { get; set; }
        public int ID_Category { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public int? ViewCount { get; set; }

        // Mối quan hệ với Category
        [ForeignKey("ID_Category")]
        public Category? Category { get; set; }

        public Memento CreateStored(Post post)
        {
            return new Memento(post.ID_Post, post.ID_Category, post.Title, post.Content, post.CreatedAt, post.CreatedBy, ViewCount);
        }

        public void RestorePost(Memento memento)
        {
            ID_Post = memento.ID_Post;
            ID_Category = memento.ID_Category;
            Title = memento.Title;
            Content = memento.Content;
            CreatedAt = memento.CreatedAt;
            CreatedBy = memento.CreatedBy;
            ViewCount = memento.ViewCount;
        }

        public Post ShallowCopy()
        {
            return (Post)this.MemberwiseClone();
        }

        public Post DeepCopy()
        {
            Post clone = (Post)this.MemberwiseClone();
            clone.Category = new Category(Category.ID_Category);
            clone.Title = string.Copy(Title);
            return clone;
        }
    }
}