using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CondominioEstribo.Models;
using System.Web.Helpers;
using System.Web.Mail;
using System.Net.Mail;
using CondominioEstribo.Negocio;

namespace CondominioEstribo.Controllers
{
    public class ProprietarioController : Controller
    {

        private Context db = new Context();


        public JsonResult GetProrprietario(string term)
        {
            // retorna placas
            List<string> proprietarios;
            proprietarios = db.Proprietarios.Where(p => p.Nome.StartsWith(term)).
                 Select(e => e.Nome).Distinct().ToList();

            return Json(proprietarios, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string proprietario)
        {
            List<Proprietario> p;

            //if (!string.IsNullOrEmpty(txtNomeProprietario))
            if (proprietario == null)
            {
                p = db.Proprietarios.ToList();
            }
            else
            {
                p = db.Proprietarios.Where(x => x.Nome.Contains(proprietario)).ToList();
                // p = p.Where(x => x.Nome.Contains(proprietario) && x.FlagStatus == "Ativo");
                
            }
            //


            return View(p);

            // return View(db.Proprietarios.ToList());

        }

        public ActionResult ListarTodosProprietarios()
        {
           // List<Proprietario> prop = db.Proprietarios.ToList();
            return View();
        }

        // METODO QUE LISTA TODOS OS REGISTROS E ORDENA POR ORDEM ALFABETICA //   
        [HttpPost]     
        public ActionResult ListarTodosProprietarios(string proprietario)
        {
            // List<Proprietario> proprietarios = db.Proprietarios.to();
            List<Proprietario> prop = new List<Proprietario>();
            if (proprietario == null)

                prop = db.Proprietarios.ToList();
            else
            {
                prop = db.Proprietarios.Where(x => x.Nome.StartsWith(proprietario)).ToList();
            }
            

            return View(prop);
            
        }

        public Proprietario BuscarPorNome(string Nome)
        {
           
            Proprietario proprietario = new Proprietario();

            proprietario = db.Proprietarios.FirstOrDefault(x => x.Nome.Equals(Nome));

            if (proprietario == null)
            {
                return null;
            }
            else
            {
                return proprietario;
            }


        }


        public List<Proprietario> listarTodosOsProprietarios()
        {
            List<Proprietario> listaDeProprietarios = new List<Proprietario>();
            listaDeProprietarios = db.Proprietarios.ToList();
            return listaDeProprietarios;

        }



        public ActionResult ConsultarProprietario(string txtNomeProprietario)
        {
            var q = db.Proprietarios.AsQueryable();

            //if (string.IsNullOrEmpty(txtNomeProprietario))

            q = q.Where(x => x.Nome.Contains(txtNomeProprietario) && x.FlagStatus == "Ativo");
            q = q.OrderBy(x => x.Nome);


            return View(q.ToList());

            // return View(db.Proprietarios.ToList());

        }











        // GET: Proprietario/ DETALHES DO PROPRIETARIO
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proprietario proprietario = db.Proprietarios.Find(id);
            if (proprietario == null)
            {
                return HttpNotFound();
            }
            return View(proprietario);
        }




        // GET: Proprietario/ CRIAR
        public ActionResult Create()
        {

            return View();

        }




        // POST: Proprietario/Create / CRIAR
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UsuarioId,LoginUsuario,SenhaUsuario,StatusUsuario,PerfilUsuario,Nome,CPF,RG,Logradouro,Numero,CEP,TelefoneCelular,Email,TelefoneComercial,Observacao,FlagStatus")] Proprietario proprietario, string DropStatus)
        {

            DropStatus = "1";

            if (DropStatus == "1")
            {
                proprietario.FlagStatus = "Ativo";
            }
            else
            {
                if (DropStatus == "2")
                {
                    proprietario.FlagStatus = "Inativo";
                }

            }

            if (ModelState.IsValid)
            {
                db.Usuarios.Add(proprietario);
                proprietario.LoginUsuario = proprietario.CPF;
                proprietario.SenhaUsuario = "123456";
                proprietario.PerfilUsuario = "Proprietário";                
                proprietario.StatusUsuario = true;
                if (ValidarCPF.ValidaCPF(proprietario.CPF) == false)
                {
                    return View().Mensagem("CPF Informado é Inválido. Favor Verificar.", "Atenção");
                }
                
                db.SaveChanges();
                return RedirectToAction("Create", "Proprietario").Mensagem("Dados Inseridos com Sucesso !!", "Atenção");
            }

            return View(proprietario);
        }




        // GET: Proprietario/Edit/5 / ALTERAR
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proprietario proprietario = db.Proprietarios.Find(id);           
            if (proprietario == null)
            {
                return HttpNotFound();
            }
            return View(proprietario);
        }

                


        // POST: Proprietario/Edit/5 / ALTERAR
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UsuarioId,LoginUsuario,SenhaUsuario,StatusUsuario,PerfilUsuario,Nome,CPF,RG,Logradouro,Numero,CEP,TelefoneCelular,Email,TelefoneComercial,Observacao,FlagStatus")] Proprietario proprietario, string DropStatus)
        {

            string recebeStatus = DropStatus;

            if (recebeStatus == "1")
            {
                proprietario.FlagStatus = "Ativo";
            }
            else
            {
                if (recebeStatus == "2")
                {
                    proprietario.FlagStatus = "Inativo";
                }

            }
            
            if (ModelState.IsValid)
            {
                db.Entry(proprietario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Proprietario").Mensagem("Dados Alterados com Sucesso !!", "Atenção");
            }
            return View(proprietario);
        }

        // METODO QUE DELTAR O REGISTRO // 
        public ActionResult DeletarProprietario(int id)
        {
            Proprietario proprietario = db.Proprietarios.Find(id);

            try
            {
                if (proprietario.FlagStatus == "Ativo")
                {
                    return RedirectToAction("ListarTodosProprietarios", "Proprietario").Mensagem("Proprietario com Status Ativo, não pode ser Excluido !!", "Atenção");
                }

                db.Proprietarios.Remove(proprietario);
                db.SaveChanges();
                return RedirectToAction("ListarTodosProprietarios", "Proprietario").Mensagem("Dados Excluidos com Sucesso !!", "Atenção");
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Home").Mensagem("Não Foi Possivel Excluir este Proprietario. Existem Registros Atrelados ao Mesmo. Contate o Administrador do Sistema.", "Atenção");
            }
        }




        // METODO UTILIZADO PARA INATIVAR UM REGISTRO DE PROPRIETARIO
        public ActionResult InativarProprietario(int id)
        {
            Proprietario proprietario = db.Proprietarios.Find(id);

            if (ModelState.IsValid)
            {
                proprietario.FlagStatus = "Inativo";
                db.Entry(proprietario).State = EntityState.Modified;
                db.SaveChanges();

            }

            return RedirectToAction("Index", "Proprietario").Mensagem("Operação Realizada com Sucesso !!", "Atenção");
        }






        // METODO PARA VERIFICAR O LOGIN
        public Proprietario VerificarLogin(string login)
        {

            Proprietario proprietario = new Proprietario();
            proprietario = db.Proprietarios.FirstOrDefault(x => x.LoginUsuario.Equals(login));

            if (proprietario == null)
            {
                return null;
            }
            else
            {
                return proprietario;
            }
        }



        // METODO PARA GERAR A SENHA DO PROPRIETARIO
        public string GerarSenha()
        {
            int Tamanho = 6; // Numero de digitos da senha

            string senha = string.Empty;

            for (int i = 0; i < Tamanho; i++)
            {
                Random random = new Random();
                int codigo = Convert.ToInt32(random.Next(48, 122).ToString());

                if ((codigo >= 48 && codigo <= 57) || (codigo >= 97 && codigo <= 122))
                {
                    string _char = ((char)codigo).ToString();
                    if (!senha.Contains(_char))
                    {
                        senha += _char;
                    }
                    else
                    {
                        i--;
                    }
                }
                else
                {
                    i--;
                }
            }
            return senha;
        }




        
        
//===============================================================================================================================================//


        /*
             METODOS QUE NÃO SÃO UTILIZADOS, PELOS MENOS POR ENQUANTO.  
        */



        // GET: Condomino/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proprietario condomino = db.Proprietarios.Find(id);
            if (condomino == null)
            {
                return HttpNotFound();
            }
            return View(condomino);
        }

        // POST: Proprietario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proprietario condomino = db.Proprietarios.Find(id);
            db.Usuarios.Remove(condomino);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
