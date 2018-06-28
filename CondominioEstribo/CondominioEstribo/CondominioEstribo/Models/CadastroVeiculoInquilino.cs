using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{
    [Table("CadastroVeiculoInquilinos")]
    public class CadastroVeiculoInquilino
    {
        [Key]
        public int CadastroVeiculoInquilinosId { get; set; }

        [Display(Name = "Nome do Inquilino*")]
        public string NomeDoInquilino { get; set; }

        [Display(Name = "Placa do Veículo*")]
        public string PlacaVeiculoInquilino { get; set; }      

        public int InquilinoDentroCadastroVeiculo { get; set; }



    }
}