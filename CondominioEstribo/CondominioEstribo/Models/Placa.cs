using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{
    public class Placa
    {
        [Key]
        public int placaId { get; set; }

        public string  numeroPlaca { get; set; }

        public virtual Proprietario proprietario { get; set; }

        public virtual Funcionario funcionario { get; set; }

        public virtual Inquilino inquilino { get; set; }

        public virtual Visitante visitante { get; set; }
        
    }
}