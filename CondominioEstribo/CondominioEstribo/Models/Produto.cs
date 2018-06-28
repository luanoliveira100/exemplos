using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Display(Name = "Produto*")]
        public string NomeProduto { get; set; }

        [Display(Name = "Descrição*")]
        public string Descricao { get; set; }

        [Display(Name = "Margem De Segurança*")]
        public int QuantidadeMinima { get; set; }

        [Display(Name = "Quantidade Atual*")]
        public int QuantidadeAtual { get; set; }
                
        [Display(Name = "Observação*")]
        public string Observacao { get; set; }        
                
    }
}