using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{
    [Table("Funcionarios")]
    public class Funcionario : Pessoa
    {
        [Display(Name = "Função*")]
        public string Cargo { get; set; }

        [Display(Name = "CTPS*")]
        public string NumeroCTPS { get; set; }

        [Display(Name = "Série*")]
        public string SerieCTPS { get; set; }

        [Display(Name = "Salário Base*")]
        public string SalarioBase { get; set; }

        [Display(Name = "Status*")]
        public string FlagStatus { get; set; }

        public Cargo CargoDentroDeFuncionario { get; set; }



    }
}