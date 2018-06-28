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
    public class CadastroVeiculoProprietarioController : Controller
    {


        private Context db = new Context();



        // GET: CadastroVeiculoProprietarios
        public ActionResult Index(string txtNome)
        {

            var q = db.CadastroVeiculoProprietarios.AsQueryable();

            //if (!string.IsNullOrEmpty(txtNome))
                q = q.Where(x => x.NomeDoProprietario.Contains(txtNome));

            q = q.OrderBy(x => x.NomeDoProprietario);

            return View(q.ToList());         
            
        }




        // GET: CadastroVeiculoProprietarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CadastroVeiculoProprietario cadastroVeiculoProprietario = db.CadastroVeiculoProprietarios.Find(id);
            if (cadastroVeiculoProprietario == null)
            {
                return HttpNotFound();
            }
            return View(cadastroVeiculoProprietario);
        }




        // GET: CadastroVeiculoProprietarios/Create
        public ActionResult Create()
        {
           return View();
        }



        // POST: CadastroVeiculoProprietarios/ GRAVA O REGISTRO 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CadastroVeiculoProprietarioId,NomeDoProprietario,PlacaVeiculoProprietario")] CadastroVeiculoProprietario cadastroVeiculoProprietario)
        {
            ProprietarioController proprietarioController = new ProprietarioController();
            Proprietario proprietario = new Proprietario();
            proprietario = proprietarioController.BuscarPorNome(Session["Nome"].ToString());

            if (proprietario != null)
            {
                proprietario = proprietarioController.BuscarPorNome(Session["Nome"].ToString());

                if (ModelState.IsValid)
                    cadastroVeiculoProprietario.ProprietarioDentroCadastroVeiculo = proprietario.UsuarioId;
                {
                    cadastroVeiculoProprietario.NomeDoProprietario = Session["Nome"].ToString();
                    db.CadastroVeiculoProprietarios.Add(cadastroVeiculoProprietario);
                    db.SaveChanges();
                    
                }
            }

            return RedirectToAction("Create", "CadastroVeiculoProprietario").Mensagem("Dados Inseridos com Sucesso !!", "Atenção");

        }




        // GET: CadastroVeiculoProprietarios/ ALTERA DADOS DO REGISTRO
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CadastroVeiculoProprietario cadastroVeiculoProprietario = db.CadastroVeiculoProprietarios.Find(id);
            if (cadastroVeiculoProprietario == null)
            {
                return HttpNotFound();
            }
            return View(cadastroVeiculoProprietario);
        }




        // POST: CadastroVeiculoProprietarios/ ALTERA DADOS DO REGISTRO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CadastroVeiculoProprietarioId,NomeDoProprietario,PlacaVeiculoProprietario")] CadastroVeiculoProprietario cadastroVeiculoProprietario)
        {
            if (ModelState.IsValid)
            {
                cadastroVeiculoProprietario.NomeDoProprietario = Session["Nome"].ToString();
                db.Entry(cadastroVeiculoProprietario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "CadastroVeiculoProprietario").Mensagem("Dados Alterados com Sucesso !!", "Atenção");
            }
            return View(cadastroVeiculoProprietario);
        }




        // DELETA O REGISTRO
        public ActionResult DeletarVeiculoProprietario(int id)
        {
            CadastroVeiculoProprietario cadastroVeiculoProprietario = db.CadastroVeiculoProprietarios.Find(id);
            db.CadastroVeiculoProprietarios.Remove(cadastroVeiculoProprietario);
            db.SaveChanges();
            return RedirectToAction("Index", "CadastroVeiculoProprietario").Mensagem("Dados Excluidos com Sucesso !!", "Atenção");
        }



//===============================================================================================================================================//

        /*
            
            A PRINCIPIO POR ENQUANTO NAO ESTA SENDO UTILIZADO.
             
        */

            
        




        // GET: CadastroVeiculoProprietarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CadastroVeiculoProprietario cadastroVeiculoProprietario = db.CadastroVeiculoProprietarios.Find(id);
            if (cadastroVeiculoProprietario == null)
            {
                return HttpNotFound();
            }
            return View(cadastroVeiculoProprietario);
        }

        // POST: CadastroVeiculoProprietarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CadastroVeiculoProprietario cadastroVeiculoProprietario = db.CadastroVeiculoProprietarios.Find(id);
            db.CadastroVeiculoProprietarios.Remove(cadastroVeiculoProprietario);
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
