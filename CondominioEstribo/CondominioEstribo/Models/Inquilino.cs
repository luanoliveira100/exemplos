using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{
    [Table("Inquilinos")]
    public class Inquilino : Usuario
    {
        
        [Display(Name = "Nome*")]
        public string NomeInquilino { get; set; }

        [Display(Name = "CPF*")]
        public string CPFInquilino { get; set; }

        [Display(Name = "RG")]
        public string RGInquilino { get; set; }

        [Display(Name = "Telefone Celular*")]
        public string TelefoneInquilino { get; set; }

        [Display(Name = "Email")]      
        public string EmailInquilino { get; set; }

        public virtual Lote lote { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        [Display(Name = "Status")]
        public string FlagStatus { get; set; }

        public virtual Proprietario proprietario { get; set; }

    }
}