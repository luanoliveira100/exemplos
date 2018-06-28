using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{
    [Table("Cargos")]
    public class Cargo
    {
        [Key]
        public int CargoId { get; set; }

        [Display(Name = "Cargo*")]
        public string NomeCargo { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        [Display(Name = "Status*")]
        public string FlagStatus { get; set; }

    }
}