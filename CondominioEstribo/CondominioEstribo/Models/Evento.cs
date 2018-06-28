using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CondominioEstribo.Models
{
    [Table("Eventos")]
    public class Evento
    {
        [Key]
        public int EventoId { get; set; }

        [Display(Name = "Titulo do Evento")]
        public string Titulo { get; set; }

        [Display(Name = "Data Inicial")]
        public DateTime DataInicial { get; set; }

        [Display(Name = "Data Final")]
        public DateTime DataFinal { get; set; }
                
        public bool DiaTodo { get; set; }
    }
}