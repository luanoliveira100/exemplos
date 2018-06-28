using CondominioEstribo.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace CondominioEstribo.Controllers
{
    public class AccountController : Controller
    {
        /// <param name="returnURL"></param>
        /// <returns></returns>
        public ActionResult Login(string returnURL)
        {
            /*Recebe a url que o usuário tentou acessar*/
            ViewBag.ReturnUrl = returnURL;
            return View(new Usuario());
        }

        /// <param name="login"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuario login, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (Context db = new Context())
                {                    

                    var vLogin = db.Usuarios.Where(p => p.LoginUsuario.Equals(login.LoginUsuario)).FirstOrDefault();
                    /*Verificar se a variavel vLogin está vazia. Isso pode ocorrer caso o usuário não existe. 
              Caso não exista ele vai cair na condição else.*/
                    if (vLogin != null)
                    {
                        /*Código abaixo verifica se o usuário que retornou na variavel tem está 
                          ativo. Caso não esteja cai direto no else*/
                        if (vLogin.StatusUsuario)
                        {
                            /*Código abaixo verifica se a senha digitada no site é igual a senha que está sendo retornada 
                             do banco. Caso não cai direto no else*/
                            if (Equals(vLogin.SenhaUsuario, login.SenhaUsuario))
                            {
                                //if (vLogin.SenhaUsuario.Equals("123456"))
                                //{
                                //    return RedirectToAction("MudarSenha", "Account");
                                //}                                
                                FormsAuthentication.SetAuthCookie(vLogin.LoginUsuario, false);
                                if (Url.IsLocalUrl(returnUrl)
                                && returnUrl.Length > 1
                                && returnUrl.StartsWith("/")
                                && !returnUrl.StartsWith("//")
                                && returnUrl.StartsWith("/\\"))
                                {
                                    return Redirect(returnUrl);
                                }
                                if (vLogin.PerfilUsuario == "Inquilino")
                                {
                                    /*código abaixo cria uma session para armazenar o nome do usuário*/
                                    //Session["Nome"] = vLogin.PerfilUsuario;
                                    var nome = db.Inquilinos.Where(x => x.CPFInquilino.Equals(vLogin.LoginUsuario)).FirstOrDefault();
                                    Session["Nome"] = nome.NomeInquilino;
                                    /*código abaixo cria uma session para armazenar o perfil do usuário*/
                                    Session["Perfil"] = vLogin.PerfilUsuario;
                                }
                                else
                                {
                                    if (vLogin.PerfilUsuario == "Proprietário")
                                    {
                                        /*código abaixo cria uma session para armazenar o nome do usuário*/
                                        var nome = db.Proprietarios.Where(x => x.CPF.Equals(vLogin.LoginUsuario)).FirstOrDefault();
                                        Session["Nome"] = nome.Nome;
                                        /*código abaixo cria uma session para armazenar o perfil do usuário*/
                                        Session["Perfil"] = vLogin.PerfilUsuario;
                                    }
                                    else
                                    {
                                        if (vLogin.PerfilUsuario == "Funcionário")
                                        {
                                            /*código abaixo cria uma session para armazenar o nome do usuário*/
                                            var nome = db.Funcionarios.Where(x => x.CPF.Equals(vLogin.LoginUsuario)).FirstOrDefault();
                                            Session["Nome"] = nome.Nome;
                                            /*código abaixo cria uma session para armazenar o perfil do usuário*/

                                            var temp = db.Funcionarios.Where(x => x.CPF.Equals(vLogin.LoginUsuario)).FirstOrDefault();
                                            Session["Perfil"] = temp.Cargo.ToString();

                                            //Session["Perfil"] = vLogin.PerfilUsuario;
                                        }
                                        else
                                        {
                                            if (vLogin.PerfilUsuario == "Administrador")
                                            {
                                                var nome = db.Usuarios.Where(x => x.LoginUsuario.Equals(vLogin.LoginUsuario)).FirstOrDefault();
                                                Session["Nome"] = nome.PerfilUsuario;
                                                Session["Perfil"] = nome.PerfilUsuario;
                                            }
                                        }
                                    }
                                }        
                                                       
                                return RedirectToAction("Index", "Home");
                            }
                            /*Else responsável da validação da senha*/
                            else
                            {
                                /*Escreve na tela a mensagem de erro informada*/
                                ModelState.AddModelError("", "Senha informada Inválida!!!");
                                /*Retorna a tela de login*/
                                return View(new Usuario());
                            }
                        }
                        /*Else responsável por verificar se o usuário está ativo*/
                        else
                        {
                            /*Escreve na tela a mensagem de erro informada*/
                            ModelState.AddModelError("", "Usuário sem acesso para usar o sistema!!!");
                            /*Retorna a tela de login*/
                            return View(new Usuario());
                        }
                    }
                    /*Else responsável por verificar se o usuário existe*/
                    else
                    {
                        /*Escreve na tela a mensagem de erro informada*/
                        ModelState.AddModelError("", "Usuário informado inválido!!!");
                        /*Retorna a tela de login*/
                        return View(new Usuario());
                    }
                }
            }
            /*Caso os campos não esteja de acordo com a solicitação retorna a tela de login com as mensagem dos campos*/
            return View(login);
        }

        // Método que faz Logoff do usuário no sistema.
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult MudarSenha()
        {


            return View();

        }



    }
}