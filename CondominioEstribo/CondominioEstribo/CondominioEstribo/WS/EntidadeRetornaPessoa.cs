using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CondominioEstribo.WS
{
    public class EntidadeRetornaPessoa
    {

        public int UsuarioId { get; set; }      
        public string LoginUsuario { get; set; }     
        public string SenhaUsuario { get; set; }      
        public string NovaSenhaUsuario { get; set; }     
        public bool StatusUsuario { get; set; }     
        public string PerfilUsuario { get; set; }
        public int ProprietarioDentroDeUsuario { get; set; }
        public int InquilinoDentroDeUsuario { get; set; }
        public int FuncionarioDentroDeUsuario { get; set; }



    }
}