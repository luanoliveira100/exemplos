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
    public class NotaFiscalsController : Controller
    {
        private Context db = new Context();


        public JsonResult GetProduto(string term)
        {
            // retorna placas
            List<string> produtos;
            produtos = db.Produtos.Where(p => p.NomeProduto.StartsWith(term)).
                 Select(e => e.NomeProduto).Distinct().ToList();

            return Json(produtos, JsonRequestBehavior.AllowGet);
        }

        // GET: NotaFiscals
        public ActionResult Index()
        {
            return View(db.NotaFiscal.ToList());
        }

        // GET: NotaFiscals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NotaFiscal notaFiscal = db.NotaFiscal.Find(id);
            if (notaFiscal == null)
            {
                return HttpNotFound();
            }
            return View(notaFiscal);
        }

        // GET: NotaFiscals/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: NotaFiscals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NotaFiscal notaFiscal)
        {
            var nf = db.NotaFiscal.FirstOrDefault(x => x.NumeroDaNota.Equals(notaFiscal.NumeroDaNota));
            if (nf == null)
            {
                if (ModelState.IsValid)
                {
                    db.NotaFiscal.Add(notaFiscal);
                    db.SaveChanges();
                }
                int Resultado = notaFiscal.Id;
                return Json(new { Resultado }, JsonRequestBehavior.AllowGet).Mensagem("Nota Fiscal Inserida com Sucesso");
            }
            //int Resultado = notaFiscal.Id;
            return Json(new { Resultado = 0}, JsonRequestBehavior.AllowGet).Mensagem("Nota Fiscal já inseriva favor verificar");
            // return RedirectToAction("Create", "NotaFiscalItens").Mensagem("Nota fiscal não pode ser criada novamente !!", "Atenção");
        }

        // GET: NotaFiscals/Edit/5
        public ActionResult Edit(int? id)
        {
            var notafisca = db.NotaFiscal.Find(id);
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //NotaFiscal notaFiscal = db.NotaFiscal.Find(id);
            //if (notaFiscal == null)
            //{
            //    return HttpNotFound();
            //}
            return View(notafisca);
        }

        // POST: NotaFiscals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Data,NumeroDaNota,Valor")] NotaFiscal notaFiscal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notaFiscal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            int Resultado = notaFiscal.Id;
            return Json(new { Resultado }, JsonRequestBehavior.AllowGet).Mensagem("Nota Fiscal Alterada com Sucesso");
        }

        // GET: NotaFiscals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NotaFiscal notaFiscal = db.NotaFiscal.Find(id);
            if (notaFiscal == null)
            {
                return HttpNotFound();
            }
            return View(notaFiscal);
        }

        // POST: NotaFiscals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NotaFiscal notaFiscal = db.NotaFiscal.Find(id);
            db.NotaFiscal.Remove(notaFiscal);
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
