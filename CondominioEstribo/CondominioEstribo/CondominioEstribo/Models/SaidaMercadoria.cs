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

        [Display(Name = "Produto*")]
        public string ProdutoSaidaMercadoria { get; set; }

        [Display(Name = "Quantidade*")]
        public int QuantidadeRetirada { get; set; }

        [Display(Name = "Data Da Retirada*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DataRetirada { get; set; }

        [Display(Name = "Funcionario*")]
        public string FuncionarioSaidaMercadoria { get; set; }

        [Display(Name = "Observação*")]
        public string Observacao { get; set; }
        
        public virtual Produto ProdutoDentroSaidaMercadoria { get; set; }

        public virtual Funcionario FuncionarioDentroSaidaMercadoria { get; set; }

        public virtual EntradaMercadoria EntradaDeMercadoriaDentroDeSaidaDeMercadoria { get; set; }

    }
}