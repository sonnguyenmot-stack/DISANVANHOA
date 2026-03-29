using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DISANVANHOA.Models.EF
{
    [Table("tb_Document")]// tài liệu di sản
    public class Document:CommonAbstract
    {
        public Document()
        {
            this.DocumentImages = new HashSet<DocumentImage>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên không được để trống!!!")]
        public string Title { get; set; }
        public string Alias { get; set; }
        public string Img { get; set; }

        public string FilePath { get; set; }

        public string Description { get; set; }
        [AllowHtml]
        public string Detail { get; set; }
        public string link { get; set; }
        public bool IsHome { get; set; }
        public bool IsActive { get; set; }
        public int CertificateId { get; set; }
        public int DocumentCategoryId { get; set; }
        public int DocTypeId {  get; set; }
        public int ViewCount { get; set; }
        public string SummaryAI { get; set; }        // Tóm tắt bài giảng
        public string WorksheetAI { get; set; }      // Phiếu học tập
        public string QuestionsAI { get; set; }     // Câu hỏi trắc nghiệm
        public string TestAI { get; set; }                 // Đề kiểm tra
        public string ActivitySuggestionsAI { get; set; }  // Hoạt động trải nghiệm
        public bool IsAIProcessed { get; set; }            // Đã sinh AI chưa
        public string VideoPath { get; set; }
        public string Author {  get; set; }
        public virtual DocumentCategory DocumentCategory { get; set; }
        public virtual Certificate GetCertificate { get; set; }
        public virtual DocType DocType { get; set; }
        public virtual General General { get; set; }
        public virtual ICollection<DocumentImage> DocumentImages { get; set; }
    }
}