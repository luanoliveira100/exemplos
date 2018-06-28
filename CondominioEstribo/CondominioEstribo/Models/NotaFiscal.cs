using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{
    public class NotaFiscal
    {
        [Key]
        public int Id { get; set; }

        public DateTime Data { get; set; }

        public string NumeroDaNota { get; set; }

        public double Valor { get; set; }
    }
}