using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_Models
{
    public class Memento
    {
        public int ID_Post { get; set; }
        public int ID_Category { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public int? ViewCount { get; set; }

        public Memento()
        {
        }

        public Memento(int iD_Post, int iD_Category, string title, string content, DateTime createdAt, string createdBy, int? viewCount)
        {
            ID_Post = iD_Post;
            ID_Category = iD_Category;
            Title = title;
            Content = content;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            ViewCount = viewCount;
        }
    }  
}
