using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{
    public class DadosRetorno <T>
    {

        public bool sucesso { get; set; }

        public string mensagem { get; set; }

        public T conteudo { get; set; }

    }
}