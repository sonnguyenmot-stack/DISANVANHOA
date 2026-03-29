using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DISANVANHOA.Models.EF
{

    public class Post// cộng đồng chat
    {
        public int Id { get; set; }

        [Required]
        public string Author { get; set; } // Người đăng

        [Required]
        public string Content { get; set; } // Nội dung bài viết

        public string MediaPath { get; set; } // Ảnh hoặc video

        public int Likes { get; set; } = 0;
        public int Dislikes { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Password { get; set; } // mật khẩu để sửa/xóa bài
                                             // Thêm danh sách file
       
        public bool IsApproved { get; set; } = false;//Trạng thái duyệt
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostMedia> MediaFiles { get; set; }
    }
}