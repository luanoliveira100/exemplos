using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{
    [Table("Obras")]
    public class Obra
    {
        [Key]
        public int ObraId { get; set; }

        [Display(Name = "Tipo da Obra*")]
        public string TipoObra { get; set; }

        [Display(Name = "Descrição da Obra*")]
        public string DescricaoObra { get; set; }

        [Display(Name = "Status*")]
        public string StatusObra { get; set; }

        [Display(Name = "Valor Aproximado*")]
        public string ValorObra { get; set; }

        [Display(Name = "Previsão de Início*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DataInicioObra { get; set; }

        [Display(Name = "Previsão de Termíno*")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DataTerminoObra { get; set; }       


    }

    



}