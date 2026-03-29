using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DISANVANHOA.Models.EF
{
    public class PostMedia// hình ảnh video cuả cộng đồng chat
    {
        public int Id { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        public string FilePath { get; set; } // /Uploads/....

        // "image" hoặc "video" (tiện cho view)
        public string FileType { get; set; }

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
    }
}