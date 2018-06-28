using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{
    [Table("GerenciarEntradaPortarias")]
    public class GerenciarEntradaPortaria
    {
        [Key]
        public int GerenciarEntradaPortariaId { get; set; }

        [Display(Name = "Nome do Visitante*")]
        public string Nome { get; set; }

        [Display(Name = "RG*")]
        public string RG { get; set; }

        [Display(Name = "Data e Hora Entrada*")]
        [DataType(DataType.DateTime)]
        public DateTime DataHoraEntrada { get; set; }               

        [Display(Name = "Data e Hora Saída*")]
        [DataType(DataType.DateTime)]
        public DateTime DataHoraSaida { get; set; }        

        [Display(Name = "Placa do Veículo")]
        public string PlacaDoVeiculo { get; set; }

        [Display(Name = "Telefone Contato*")]
        public string TelefoneContato { get; set; }

        public string FlagParaControle { get; set; }               

        public string Observacao { get; set; }

        public virtual CadastroVeiculoFuncionario FuncionarioDentroPortaria { get; set; }

        public virtual CadastroVeiculoProprietario ProprietarioDentroPortaria { get; set; }

        public virtual CadastroVeiculoInquilino InquilinoDentroPortaria { get; set; }

        public virtual Visitante VisitanteDentroPortaria { get; set; }

    }
}