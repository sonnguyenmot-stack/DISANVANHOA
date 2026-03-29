using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DISANVANHOA.Models.EF
{
    [Table("tb_DocumentCategory")]//địa điểm di sản
    public class DocumentCategory:CommonAbstract
    {
        public DocumentCategory()
        {
            this.Documents = new HashSet<Document>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required (ErrorMessage ="Tên địa điểm không được để trống!!!")]
        
        public string Title { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public ICollection<Document> Documents { get; set; }
    }
}