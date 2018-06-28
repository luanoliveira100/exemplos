using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{
    public class NotaFiscalItens
    {
        [Key]
        public int Id { get; set; }

        public virtual NotaFiscal notaFiscal { get; set; }

        public int Quantidade { get; set; }

        public virtual Produto Produto { get; set; }

        public double ValorUnitartio { get; set; }
    }
}