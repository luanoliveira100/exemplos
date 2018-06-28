using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CondominioEstribo.Models
{
    public class MesAnoPagosController : Controller
    {
        private Context db = new Context();


        public bool verificaMes(string mes)
        {
            int ano = Convert.ToInt16(mes.Substring(0, 4));
            int mes1 = Convert.ToInt16(mes.Substring(5, 2));
            MesAnoPago mesano = new MesAnoPago();
            mesano = db.MesAnoPagos.FirstOrDefault(x => x.mes.Equals(mes1)&& x.ano.Equals(ano));
            if (mesano == null)
            {
                return true;
            }
            return false;
        }
         public void salvar (string mes)
        {
            int ano = Convert.ToInt16(mes.Substring(0, 4));
            int mes1 = Convert.ToInt16(mes.Substring(5, 2));
            MesAnoPago MesAnoPago1 = new MesAnoPago();
            MesAnoPago1.mes = mes1;
            MesAnoPago1.ano = ano;
            if (ModelState.IsValid)
            {              

                db.MesAnoPagos.Add(MesAnoPago1);
                db.SaveChanges();
            }

        }


        // GET: MesAnoPagos
        public ActionResult Index()
        {
            return View(db.MesAnoPagos.ToList());
        }

        // GET: MesAnoPagos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MesAnoPago mesAnoPago = db.MesAnoPagos.Find(id);
            if (mesAnoPago == null)
            {
                return HttpNotFound();
            }
            return View(mesAnoPago);
        }

        // GET: MesAnoPagos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MesAnoPagos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mesanoId,mes,ano")] MesAnoPago mesAnoPago)
        {
            if (ModelState.IsValid)
            {
                db.MesAnoPagos.Add(mesAnoPago);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mesAnoPago);
        }

        // GET: MesAnoPagos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MesAnoPago mesAnoPago = db.MesAnoPagos.Find(id);
            if (mesAnoPago == null)
            {
                return HttpNotFound();
            }
            return View(mesAnoPago);
        }

        // POST: MesAnoPagos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mesanoId,mes,ano")] MesAnoPago mesAnoPago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mesAnoPago).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mesAnoPago);
        }

        // GET: MesAnoPagos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MesAnoPago mesAnoPago = db.MesAnoPagos.Find(id);
            if (mesAnoPago == null)
            {
                return HttpNotFound();
            }
            return View(mesAnoPago);
        }

        // POST: MesAnoPagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MesAnoPago mesAnoPago = db.MesAnoPagos.Find(id);
            db.MesAnoPagos.Remove(mesAnoPago);
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
