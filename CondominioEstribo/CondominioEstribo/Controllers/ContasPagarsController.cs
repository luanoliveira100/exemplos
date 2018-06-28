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
    public class ContasPagarsController : Controller
    {
        private Context db = new Context();

        public List<ContasPagar> BuscarContasPagasPorAnoMes(int mes, int ano)
        {            
            var contasPagas = from contas in db.ContasPagar where 
            contas.datapagamento.Month == (mes) && contas.datapagamento.Year == (ano) select contas;
            return contasPagas.ToList();


        }

       


        // GET: ContasPagars
        public ActionResult Index()
        {
            return View(db.ContasPagar.ToList());
        }

        // GET: ContasPagars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContasPagar contasPagar = db.ContasPagar.Find(id);
            if (contasPagar == null)
            {
                return HttpNotFound();
            }
            return View(contasPagar);
        }

        // GET: ContasPagars/Create
        public ActionResult Create()
        {
            List<TipoDaConta> ListaContas = db.TipodaContas.ToList();
            List<SelectListItem> ListaGenericaDeContas = new List<SelectListItem>();

            foreach (TipoDaConta item in ListaContas)
            {

                ListaGenericaDeContas.Add(new SelectListItem { Text = item.Tipo.ToString(), Value = item.Tipo.ToString() });
            }
            ViewBag.listaDeContas = ListaGenericaDeContas;
            return View();
        }

        // POST: ContasPagars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContasPagarId,datatitulo,datapagamento,valor,TipodaConta")] ContasPagar contasPagar, string DropContas)
        {
            string tipoDaConta = DropContas;
            contasPagar.TipodaConta = db.TipodaContas.FirstOrDefault(x => x.Tipo.Equals(tipoDaConta));
            if (ModelState.IsValid)
            {
                db.ContasPagar.Add(contasPagar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contasPagar);
        }

        // GET: ContasPagars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContasPagar contasPagar = db.ContasPagar.Find(id);
            if (contasPagar == null)
            {
                return HttpNotFound();
            }
            return View(contasPagar);
        }

        // POST: ContasPagars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContasPagarId,datatitulo,datapagamento,valor")] ContasPagar contasPagar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contasPagar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contasPagar);
        }

        // GET: ContasPagars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContasPagar contasPagar = db.ContasPagar.Find(id);
            if (contasPagar == null)
            {
                return HttpNotFound();
            }
            return View(contasPagar);
        }

        // POST: ContasPagars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContasPagar contasPagar = db.ContasPagar.Find(id);
            db.ContasPagar.Remove(contasPagar);
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
