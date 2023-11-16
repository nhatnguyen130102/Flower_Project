using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
    }

}
