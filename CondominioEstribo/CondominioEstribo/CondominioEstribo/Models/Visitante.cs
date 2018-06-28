using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{
    [Table("Visitantes")]
    public class Visitante
    {
        [Key]
        public int VisitanteId { get; set; }

        [Display(Name = "Nome do Visitante*")]
        public string NomeVisitante { get; set; }
     
        [Display(Name = "Previsão de Entrada*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DataEntrada { get; set; }

        [Display(Name = "Previsão de Saída*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DataSaida { get; set; }

        [Display(Name = "Placa do Veículo*")]
        public string PlacaVeiculoVisitante { get; set; }

        [Display(Name = "Nome do Proprietário / Inquilino*")]
        public string NomeDoCondomino { get; set; }

        [Display(Name = "Observação*")]
        public string Observacao { get; set; }

        public virtual Proprietario ProprietarioDentroDeVisitante { get; set; }

    }
}