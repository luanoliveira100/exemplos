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
    public class LotesController : Controller
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
        // GET: Lotes
        public ActionResult Index()
        {
            return View(db.Lotes.ToList());
        }

        // GET: Lotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lote lote = db.Lotes.Find(id);
            if (lote == null)
            {
                return HttpNotFound();
            }
            return View(lote);
        }

        // GET: Lotes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoteId,NumeroLote,AreaConstruida,Observacao")] Lote lote, string proprietario, string DropArea)
        {
          
            lote.NumeroLote = lote.NumeroLote.ToUpper();
            lote.proprietario = db.Proprietarios.FirstOrDefault(p => p.Nome.Equals(proprietario));
            if (lote.proprietario != null)
            {
                if (DropArea == "1")
                {
                    lote.AreaConstruida = "Sim";
                }
                else
                {
                    lote.AreaConstruida = "Nao";
                }
                if (ModelState.IsValid)
                {
                    db.Lotes.Add(lote);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }


            return View().Mensagem("Proprietário Inválido. Favor Verificar.", "Atenção");
        }

        // GET: Lotes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lote lote = db.Lotes.Find(id);
            if (lote == null)
            {
                return HttpNotFound();
            }
            return View(lote);
        }

        // POST: Lotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoteId,NumeroLote,AreaConstruida,Observacao")] Lote lote)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lote);
        }

        // GET: Lotes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lote lote = db.Lotes.Find(id);
            if (lote == null)
            {
                return HttpNotFound();
            }
            return View(lote);
        }

        // POST: Lotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lote lote = db.Lotes.Find(id);
            db.Lotes.Remove(lote);
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
