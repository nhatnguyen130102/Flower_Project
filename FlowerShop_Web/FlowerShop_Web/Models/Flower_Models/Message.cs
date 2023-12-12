using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop_Web.Models.Flower_Models
{
    public class Message
    {
        public int Id { get; set; }
        public string IdRoom { get; set; } // Sử dụng cùng kiểu dữ liệu với IdRoom trong Room
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public UserType UserType { get; set; } // Sử dụng enum hoặc kiểu dữ liệu phù hợp

        [ForeignKey("IdRoom")]
        public Room Room { get; set; }
    }

    public enum UserType
    {
        User,
        Admin
    }

}
