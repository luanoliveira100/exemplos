using CondominioEstribo.Controllers;
using CondominioEstribo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CondominioEstribo.WS
{
    public class RetornoCliente
    {
        private Context db = new Context();
        public List<EntidadeRetornaPessoa> RetornaPessoa()
        {

            //EntidadeRetornaPessoa entidadeRetorna = new EntidadeRetornaPessoa();
            //List<EntidadeRetornaPessoa> listaRetorno = new List<EntidadeRetornaPessoa>();
            //List<Usuario> usuarios = new List<Usuario>();
            //usuarios = db.Usuarios.ToList();

            //foreach (var A in usuarios)
            //{
            //    if (A.PerfilUsuario == "Proprietario")
            //    {
            //      //  entidadeRetorna.InquilinoDentroDeUsuario = A.InquilinoDentroDeUsuario;
            //        entidadeRetorna.LoginUsuario = A.LoginUsuario;
            //        entidadeRetorna.NovaSenhaUsuario = A.NovaSenhaUsuario;
            //        entidadeRetorna.PerfilUsuario = A.PerfilUsuario;
            //      //  entidadeRetorna.ProprietarioDentroDeUsuario = A.ProprietarioDentroDeUsuario;
            //        entidadeRetorna.SenhaUsuario = A.SenhaUsuario;
            //        entidadeRetorna.StatusUsuario = A.StatusUsuario;
            //        listaRetorno.Add(entidadeRetorna);
            //    }
            //} 




            //return listaRetorno;

            return null;

        }

    }
}