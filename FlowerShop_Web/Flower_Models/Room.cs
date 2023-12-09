﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_Models
{
    public class Room
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }

        public ICollection<Message> Messages { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }
    }
}
