using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DISANVANHOA.Models.EF
{
    [Table("tb_Historicalrelics")]// di tích lịch sử
    public class Historicalrelics:CommonAbstract
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Bạn không để trống tiêu đề ")]
        [StringLength(150)]
        public string Title { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        [AllowHtml]
        public string Detail { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public int ViewCount {  get; set; }
        public string FilePath {  get; set; }
        public string VideoPath {  get; set; }
        public string Author { get; set; }

    }
}
