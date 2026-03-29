using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DISANVANHOA.Models.EF
{
    [Table("tb_Type")]//loại hình di sản 
    public class DocType:CommonAbstract
    {
        public DocType()
        {
            this.Documents = new HashSet<Document>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public ICollection<Document> Documents { get; set; }
    }
}