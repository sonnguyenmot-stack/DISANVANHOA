using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DISANVANHOA.Models.EF
{
    [Table("tb_ChatFiles")]
    public class ChatFile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string User { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadTime { get; set; }
    }
}