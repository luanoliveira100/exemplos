using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CondominioEstribo.Models;

namespace CondominioEstribo.Controllers
{
    public class CadastroVeiculoFuncionarioController : Controller
    {
        private Context db = new Context();

        // LISTA TODOS OS REGISTRO E ORDENA EM ORDEM ALFABETICA
        public ActionResult Index(string txtNome)
        {
            var q = db.CadastroVeiculoFuncionarios.AsQueryable();

            //if (!string.IsNullOrEmpty(txtNome))
                q = q.Where(x => x.NomeDoFuncionario.Contains(txtNome));

            q = q.OrderBy(x => x.NomeDoFuncionario);

            return View(q.ToList());
        }


        // GET: CadastroVeiculoFuncionario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CadastroVeiculoFuncionario cadastroVeiculoFuncionario = db.CadastroVeiculoFuncionarios.Find(id);
            if (cadastroVeiculoFuncionario == null)
            {
                return HttpNotFound();
            }
            return View(cadastroVeiculoFuncionario);
        }



        // GET: CadastroVeiculoFuncionario/ GRAVA OS DADOS DO REGISTRO
        public ActionResult Create()
        {
            List<Funcionario> ListaDeFuncionario = db.Funcionarios.ToList();

            List<SelectListItem> ListaDeFuncionarioParaCarro = new List<SelectListItem>();

            foreach (Funcionario item in ListaDeFuncionario)
            {
                ListaDeFuncionarioParaCarro.Add(new SelectListItem { Text = item.Nome.ToString(), Value = item.Nome.ToString() });
            }

            ViewBag.nomeFuncionario = ListaDeFuncionarioParaCarro;

            return View();

        }




        // POST: CadastroVeiculoFuncionario/ GRAVA OS DADOS DO REGISTRO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CadastroVeiculoFuncionarioId,NomeDoFuncionario,PlacaVeiculoFuncionario")] CadastroVeiculoFuncionario cadastroVeiculoFuncionario, string DropNomeFuncionario)
        {
            
            Funcionario funcionario = db.Funcionarios.FirstOrDefault(x => x.Nome == DropNomeFuncionario);
            cadastroVeiculoFuncionario.NomeDoFuncionario = DropNomeFuncionario;

            if (ModelState.IsValid)
                cadastroVeiculoFuncionario.FuncionarioDentroCadastroVeiculo = funcionario.UsuarioId;
            {
                db.CadastroVeiculoFuncionarios.Add(cadastroVeiculoFuncionario);
                db.SaveChanges();
                
            }

            return RedirectToAction("Create", "CadastroVeiculoFuncionario").Mensagem("Dados Inseridos com Sucesso !!", "Atenção");
        }




        // GET: CadastroVeiculoFuncionario/ ALTERA OS DADOS DO REGISTRO
        public ActionResult Edit(int? id)
        {            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CadastroVeiculoFuncionario cadastroVeiculoFuncionario = db.CadastroVeiculoFuncionarios.Find(id);
            if (cadastroVeiculoFuncionario == null)
            {
                return HttpNotFound();
            }
            return View(cadastroVeiculoFuncionario);
        }





        // POST: CadastroVeiculoFuncionario/ ALTERA OS DADOS DO REGISTRO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CadastroVeiculoFuncionarioId,NomeDoFuncionario,PlacaVeiculoFuncionario")] CadastroVeiculoFuncionario cadastroVeiculoFuncionario)
        {
            Funcionario funcionario = db.Funcionarios.FirstOrDefault(x => x.Nome == cadastroVeiculoFuncionario.NomeDoFuncionario);

            if (ModelState.IsValid)
                cadastroVeiculoFuncionario.FuncionarioDentroCadastroVeiculo = funcionario.UsuarioId;
            {              
                
                db.Entry(cadastroVeiculoFuncionario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "CadastroVeiculoFuncionario").Mensagem("Dados Alterados com Sucesso !!", "Atenção");

            }

            return View(cadastroVeiculoFuncionario);
        }




        // EXCLUI O REGISTRO
        public ActionResult DeletarVeiculoFuncionario(int id)
        {
            CadastroVeiculoFuncionario cadastroVeiculoFuncionario = db.CadastroVeiculoFuncionarios.Find(id);
            db.CadastroVeiculoFuncionarios.Remove(cadastroVeiculoFuncionario);
            db.SaveChanges();
            return RedirectToAction("Index", "CadastroVeiculoFuncionario").Mensagem("Dados Excluidos com Sucesso !!", "Atenção");
        }




//=========================================================================================================================================================//






       




        // GET: CadastroVeiculoFuncionario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CadastroVeiculoFuncionario cadastroVeiculoFuncionario = db.CadastroVeiculoFuncionarios.Find(id);
            if (cadastroVeiculoFuncionario == null)
            {
                return HttpNotFound();
            }
           

            return View(cadastroVeiculoFuncionario);
        }

        // POST: CadastroVeiculoFuncionario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CadastroVeiculoFuncionario cadastroVeiculoFuncionario = db.CadastroVeiculoFuncionarios.Find(id);
            db.CadastroVeiculoFuncionarios.Remove(cadastroVeiculoFuncionario);
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
