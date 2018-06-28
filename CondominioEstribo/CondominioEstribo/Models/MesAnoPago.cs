using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{

    [Table("MesAnoPagos")]
    public class MesAnoPago
    {
        [Key]
        public int MesAnoPagoId { get; set; }

        public int mes { get; set; }

        public int ano { get; set; }


    }
}