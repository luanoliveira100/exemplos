using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CondominioEstribo.Models
{
    [Table("Lotes")]
    public class Lote
    {

        [Key]
        public int LoteId { get; set; }

        [Display(Name = "Número do Lote*")]
        public string NumeroLote { get; set; }

        [Display(Name = "Nome do Proprietário*")]
        public string NomeProprietario { get; set; }

        [Display(Name = "Possui Área Construida*")]
        public string AreaConstruida { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        public virtual Proprietario ProprietarioDentroDeLote { get; set; }


    }
}