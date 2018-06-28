using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{
    public class ContasPagar
    {


        [Key]
        public int ContasPagarId { get; set; }

        public virtual TipoDaConta TipodaConta {get; set;}

        public DateTime datatitulo { get; set; }


        public DateTime datapagamento { get; set; }


        public double valor { get; set; }


    }
}