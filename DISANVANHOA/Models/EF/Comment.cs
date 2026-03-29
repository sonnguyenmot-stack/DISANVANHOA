using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DISANVANHOA.Models.EF
{
    public class Comment// bình luận
    {
        public int Id { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Content { get; set; }

        public int Likes { get; set; } = 0;
        public int Dislikes { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
    }
}