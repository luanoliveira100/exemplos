using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{
    [Table("SaidaMercadorias")]
    public class SaidaMercadoria
    {
        [Key]
        public int SaidaMercadoriaId { get; set; }
        
        [Display(Name = "Quantidade*")]
        public int quantidade { get; set; }

        [Display(Name = "Data Da Retirada*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime data { get; set; }     
        
        public virtual Produto produto { get; set; }

        public virtual Funcionario funcionario { get; set; }

        [Display(Name = "Observação*")]
        public string observacao { get; set; }

    }
}