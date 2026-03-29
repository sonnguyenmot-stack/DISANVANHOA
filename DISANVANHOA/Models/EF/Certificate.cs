using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DISANVANHOA.Models.EF
{
    [Table("tb_Certificate")]// chứng nhận di sản
    public class Certificate:CommonAbstract
    {
        public Certificate()
        {
            this.Documents = new HashSet<Document>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id {  get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }   
        public int Position { get; set; }
        public ICollection<Document> Documents { get; set; }

    }
}