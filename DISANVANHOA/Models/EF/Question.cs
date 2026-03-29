using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DISANVANHOA.Models.EF
{
    [Table("tb_Question")]// thử thách 1
    public class Question
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Câu hỏi không được để trống")]
        public string Text { get; set; }          // Nội dung câu hỏi
        public string OptionA { get; set; }       // Đáp án A
        public string OptionB { get; set; }       // Đáp án B
        public string OptionC { get; set; }       // Đáp án C
        public string OptionD { get; set; }       // Đáp án D
        public string CorrectAnswer { get; set; } // "A", "B", "C", "D"
    }
}