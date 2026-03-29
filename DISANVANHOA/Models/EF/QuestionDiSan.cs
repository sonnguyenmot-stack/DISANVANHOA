using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DISANVANHOA.Models.EF
{
    [Table("tb_QuestionDiSan")]// thử thách 2,3
    public class QuestionDiSan
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id {  get; set; }
        [Required(ErrorMessage ="Không được để trống tên")]
        public string Name { get; set; }
        public string Img1 {  get; set; }
        public string Img2 { get; set; }
        public string Img3 { get; set; }
        public string Img4 { get; set; }
        public  string result {  get; set; }
    }
}