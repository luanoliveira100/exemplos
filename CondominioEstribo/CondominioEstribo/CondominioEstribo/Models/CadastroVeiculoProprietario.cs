using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{
    [Table("CadastroVeiculoProprietarios")]
    public class CadastroVeiculoProprietario
    {
        [Key]
        public int CadastroVeiculoProprietarioId { get; set; }

        [Display(Name = "Nome do Proprietario*")]
        public string NomeDoProprietario { get; set; }

        [Display(Name = "Placa do Veículo*")]
        public string PlacaVeiculoProprietario { get; set; }
                
        public int ProprietarioDentroCadastroVeiculo { get; set; }


    }
}