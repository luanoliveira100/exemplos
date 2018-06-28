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
    public class CadastroVeiculoInquilinoController : Controller
    {


        private Context db = new Context();



        // LISTA TODOS OS REGISTRO E ORDENA POR ORDEM ALFABETICA //
        public ActionResult Index(string txtNome)
        {
            var q = db.CadastroVeiculoInquilinos.AsQueryable();

            //if (!string.IsNullOrEmpty(txtNome))
                q = q.Where(x => x.NomeDoInquilino.Contains(txtNome));

            q = q.OrderBy(x => x.NomeDoInquilino);

            return View(q.ToList());
        }




        // GET: CadastroVeiculoInquilino/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CadastroVeiculoInquilino cadastroVeiculoInquilino = db.CadastroVeiculoInquilinos.Find(id);
            if (cadastroVeiculoInquilino == null)
            {
                return HttpNotFound();
            }
            return View(cadastroVeiculoInquilino);
        }


        // GET: CadastroVeiculoInquilino/ CRIA O REGISTRO
        public ActionResult Create()
        {

            return View();

        }



        // POST: CadastroVeiculoInquilino/ CRIA O REGISTRO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CadastroVeiculoInquilinosId,NomeDoInquilino,PlacaVeiculoInquilino")] CadastroVeiculoInquilino cadastroVeiculoInquilino)
        {

            InquilinoController inquilinoController = new InquilinoController();
            Inquilino inquilino = inquilinoController.BuscarPorNome(Session["Nome"].ToString());           

            if (inquilino != null)
            {
                if (ModelState.IsValid)
                    cadastroVeiculoInquilino.InquilinoDentroCadastroVeiculo = inquilino.UsuarioId;
                {
                    cadastroVeiculoInquilino.NomeDoInquilino = Session["Nome"].ToString();
                    db.CadastroVeiculoInquilinos.Add(cadastroVeiculoInquilino);
                    db.SaveChanges();
                    return RedirectToAction("Create", "CadastroVeiculoInquilino").Mensagem("Dados Inseridos com Sucesso !!", "Atenção");
                }
            }

            return View().Mensagem("Inquilino Não Encontradado. Favor Verificar.");
        }




        // GET: CadastroVeiculoInquilino/ ALTERA O REGISTRO
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CadastroVeiculoInquilino cadastroVeiculoInquilino = db.CadastroVeiculoInquilinos.Find(id);
            if (cadastroVeiculoInquilino == null)
            {
                return HttpNotFound();
            }
            return View(cadastroVeiculoInquilino);
        }



        // POST: CadastroVeiculoInquilino/ ALTERA O REGISTRO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CadastroVeiculoInquilinosId,NomeDoInquilino,PlacaVeiculoInquilino")] CadastroVeiculoInquilino cadastroVeiculoInquilino)
        {
            if (ModelState.IsValid)
            {
                cadastroVeiculoInquilino.NomeDoInquilino = Session["Nome"].ToString();
                db.Entry(cadastroVeiculoInquilino).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "CadastroVeiculoInquilino").Mensagem("Dados Alterados com Sucesso !!", "Atenção");
            }
            return View(cadastroVeiculoInquilino);
        }


        // EXCLUI O REGISTRO
        public ActionResult DeletarVeiculoInquilino(int id)
        {
            CadastroVeiculoInquilino cadastroVeiculoInquilino = db.CadastroVeiculoInquilinos.Find(id);
            db.CadastroVeiculoInquilinos.Remove(cadastroVeiculoInquilino);
            db.SaveChanges();
            return RedirectToAction("Index", "CadastroVeiculoInquilino").Mensagem("Dados Excluidos com Sucesso !!", "Atenção");
        }





//==============================================================================================================================================//





       

        


        // GET: CadastroVeiculoInquilino/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CadastroVeiculoInquilino cadastroVeiculoInquilino = db.CadastroVeiculoInquilinos.Find(id);
            if (cadastroVeiculoInquilino == null)
            {
                return HttpNotFound();
            }
            return View(cadastroVeiculoInquilino);
        }

        // POST: CadastroVeiculoInquilino/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CadastroVeiculoInquilino cadastroVeiculoInquilino = db.CadastroVeiculoInquilinos.Find(id);
            db.CadastroVeiculoInquilinos.Remove(cadastroVeiculoInquilino);
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
