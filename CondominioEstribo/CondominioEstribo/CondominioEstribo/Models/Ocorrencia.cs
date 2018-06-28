using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{
    [Table("Ocorrencias")]
    public class Ocorrencia
    {
        [Key]
        public int OcorrenciaId { get; set; }

        [Display(Name = "Descrição*")] 
        public string Descricao { get; set; }

        [Display(Name = "Tipo*")] 
        public string Tipo { get; set; }

        [Display(Name = "Situação*")] 
        public string Situacao { get; set; }       

        [Display(Name = "Desfecho*")] 
        public string Desfecho { get; set; }

        [Display(Name = "Data da Abertura*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime? DataAberturaOcorrencia { get; set; }

        [Display(Name = "Data Do Desfecho*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime? DataDesfechoOcorrencia { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? FlagData { get; set; }
        
        public string QuemRegistrouOcorrencia { get; set; }

        public int ProprietarioDentroDeOcorrencia { get; set; }

        public int InquilinoDentroDeOcorrencia { get; set; }

    }
}