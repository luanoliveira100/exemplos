using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{
    [Table("CadastroVeiculoFuncionarios")]
    public class CadastroVeiculoFuncionario
    {
        [Key]
        public int CadastroVeiculoFuncionarioId { get; set; }

        [Display(Name = "Nome do Funcionario*")]
        public string NomeDoFuncionario { get; set; }

        [Display(Name = "Placa do Veículo*")]
        public string PlacaVeiculoFuncionario { get; set; }
        
        public int FuncionarioDentroCadastroVeiculo { get; set; }

    }
}