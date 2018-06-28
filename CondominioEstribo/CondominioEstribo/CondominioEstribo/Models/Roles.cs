using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace CondominioEstribo.Models
{
    // Classe que cria e gerencia as Roles, ela faz uso da FormsAuthentication
    // para criar e gerenciar a autorização do usuário dentro da aplicação.
    public class Roles : RoleProvider
    {
        // Este método adiciona vários usuários a várias Roles.
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        // A interface pede a declaração explícita de ApplicationName, mas a 
        // implementação é opcional. Apenas declare para evitar erros.
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        // Este método cria uma Role.
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        // Este método exclui uma Role.
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        // Este método encontra usuários que batam com uma determinada expressão
        // passada por parâmetro.
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        // Este método devolve todas as Roles.
        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        // Este método devolve todas as Roles de um usuário.
        public override string[] GetRolesForUser(string username)
        {
            Context db = new Context();

            string sRoles = db.Usuarios.Where(p => p.LoginUsuario == username).FirstOrDefault().PerfilUsuario;
            string[] retorno = { sRoles };
            return retorno;

            throw new NotImplementedException();
        }

        // Este método devolve todos os usuários de uma Role.
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        // Este método verifica se um usuário pertence a uma Role.
        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        // Este método remove os usuários das Roles passadas como parâmetro.
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        // Este método verifica se uma Role existe.
        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}