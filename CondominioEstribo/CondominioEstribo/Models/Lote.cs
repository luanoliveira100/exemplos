using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{
    public class Lote
    {
        [Key]
        public int LoteId { get; set; }

        [Display(Name = "Número do Lote*")]
        public string NumeroLote { get; set; }       

        [Display(Name = "Possui Área Construida*")]
        public string AreaConstruida { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        public virtual Proprietario proprietario { get; set; }

    }
}