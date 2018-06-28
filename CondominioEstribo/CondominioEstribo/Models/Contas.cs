using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{
    public class Contas
    {

        [Key]
        public int ContasId { get; set; }

        public virtual Proprietario proprietario { get; set; }


        public DateTime datatitulo { get; set; }


        public DateTime datapagamento { get; set; }


        public double valor { get; set; }


        public string fonte { get; set; }


        public string codigo { get; set; }


        public string lote { get; set; }


        public string pago { get; set; }
    }

}
