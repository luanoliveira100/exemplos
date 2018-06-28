using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{
    [Table("Pessoas")]
    public class Pessoa : Usuario
    {


        [Display(Name = "Nome*")]
        public string Nome { get; set; }

        [Display(Name = "CPF*")]
        public string CPF { get; set; }

        [Display(Name ="RG")]
        public string RG { get; set; }

        [Display(Name = "Endereço*")]
        public string Logradouro { get; set; }

        [Display(Name = "Número*")]
        public int Numero { get; set; }

        [Display(Name = "CEP*")]
        public string CEP { get; set; }

        [Display(Name = "Telefone Celular*")]
        public string TelefoneCelular { get; set; }
     
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}