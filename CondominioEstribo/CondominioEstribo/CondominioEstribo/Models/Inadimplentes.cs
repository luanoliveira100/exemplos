using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{
    public class Inadimplentes
    {

        [Key]
        public int ContasId { get; set; }

        public int Proprietarioid { get; set; }

        public DateTime data { get; set; }

    }
}