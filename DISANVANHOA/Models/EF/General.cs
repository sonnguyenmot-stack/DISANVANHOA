using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DISANVANHOA.Models.EF
{
    [Table("tb_General")]// giáo trình
    public class General:CommonAbstract
    {
        public General()
        {
            this.Documents = new HashSet<Document>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên địa điểm không được để trống!!!")]

        public string Title { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        [AllowHtml]
        public string Detail {  get; set; }
        public string Icon { get; set; }
        public string FilePath { get; set; }
        public int ViewCount {  get; set; }
        public ICollection<Document> Documents { get; set; }
    }
}