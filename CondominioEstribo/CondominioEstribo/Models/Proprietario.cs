using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{
    [Table("Proprietarios")]
    public class Proprietario : Pessoa
    {

        [Display(Name = "Telefone Comercial*")]
        public string TelefoneComercial { get; set; }

        [Display(Name = "Observação*")]
        public string Observacao { get; set; }

        [Display(Name = "Status")]
        public string FlagStatus { get; set; }



    }
}