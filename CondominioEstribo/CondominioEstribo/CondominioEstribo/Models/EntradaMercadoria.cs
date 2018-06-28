using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{   
    [Table("EntradaMercadorias")]
    public class EntradaMercadoria
    {
        [Key]
        public int EntradaMercadoriaId { get; set; }

        [Display(Name = "Produto*")]
        public string ProdutoEntradaMercadoria { get; set; }

        [Display(Name = "Fornecedor*")]
        public string FornecedorEntradaMercadoria { get; set; }

        [Display(Name = "Quantidade*")]
        public int QuantidadeInserida { get; set; }

        [Display(Name = "Valor Total Da Nota Fiscal*")]
        public double ValorTotalDaNF { get; set; }

        [Display(Name = "Data Da Entrada*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DataDaEntrada { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        public virtual Produto ProdutoDentroDeEntradaMercadoria { get; set; }

        public virtual Fornecedor FornecedorDentroDeEntradaMercadoria { get; set; }


    }
}