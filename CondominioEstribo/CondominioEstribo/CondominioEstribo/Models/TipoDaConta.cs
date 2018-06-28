using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{
    public class TipoDaConta
    {

        [Key]
        public int TipoDaContaId { get; set; }

        [Display(Name = "Conta ex: Água, Luz , telefone...*")]
        public string Tipo { get; set; }

        [Display(Name = "Observação*")]
        public string Observacao { get; set; }


    }
}