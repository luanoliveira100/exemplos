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
    public class TipoDaContasController : Controller
    {
        private Context db = new Context();

        // GET: TipoDaContas
        public ActionResult Index()
        {
            return View(db.TipodaContas.ToList());
        }

        // GET: TipoDaContas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDaConta tipoDaConta = db.TipodaContas.Find(id);
            if (tipoDaConta == null)
            {
                return HttpNotFound();
            }
            return View(tipoDaConta);
        }

        // GET: TipoDaContas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoDaContas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoDaContaId,Tipo,Observacao")] TipoDaConta tipoDaConta)
        {
            if (ModelState.IsValid)
            {
                db.TipodaContas.Add(tipoDaConta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoDaConta);
        }

        // GET: TipoDaContas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDaConta tipoDaConta = db.TipodaContas.Find(id);
            if (tipoDaConta == null)
            {
                return HttpNotFound();
            }
            return View(tipoDaConta);
        }

        // POST: TipoDaContas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoDaContaId,Tipo,Observacao")] TipoDaConta tipoDaConta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoDaConta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoDaConta);
        }

        // GET: TipoDaContas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDaConta tipoDaConta = db.TipodaContas.Find(id);
            if (tipoDaConta == null)
            {
                return HttpNotFound();
            }
            return View(tipoDaConta);
        }

        // POST: TipoDaContas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoDaConta tipoDaConta = db.TipodaContas.Find(id);
            db.TipodaContas.Remove(tipoDaConta);
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
