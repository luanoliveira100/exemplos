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
    public class PlacasController : Controller
    {
        private Context db = new Context();

        public JsonResult GetPlaca(string term)
        {
            // retorna placas
            List<string> placas;
            placas = db.Placas.Where(p => p.numeroPlaca.StartsWith(term)).
                 Select(e => e.numeroPlaca).Distinct().ToList();

            return Json(placas, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProprietario(string term)
        {
            // retorna placas
            List<string> proprietarios;
            proprietarios = db.Proprietarios.Where(p => p.Nome.StartsWith(term)).
                 Select(e => e.Nome).Distinct().ToList();

            return Json(proprietarios, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetInquilino(string term)
        {
            // retorna placas
            List<string> inquilinos;
            inquilinos = db.Inquilinos.Where(p => p.NomeInquilino.StartsWith(term)).
                 Select(e => e.NomeInquilino).Distinct().ToList();

            return Json(inquilinos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFuncionario(string term)
        {
            // retorna placas
            List<string> funcionarios;
            funcionarios = db.Funcionarios.Where(p => p.Nome.StartsWith(term)).
                 Select(e => e.Nome).Distinct().ToList();

            return Json(funcionarios, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetVisitante(string term)
        {
            // retorna placas
            List<string> visitantes;
            visitantes = db.Visitantes.Where(p => p.NomeVisitante.StartsWith(term)).
                 Select(e => e. NomeVisitante).Distinct().ToList();

            return Json(visitantes, JsonRequestBehavior.AllowGet);
        }
        // GET: Placas
        public ActionResult Index()
        {
            return View(db.Placas.ToList());
        }

        // GET: Placas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Placa placa = db.Placas.Find(id);
            if (placa == null)
            {
                return HttpNotFound();
            }
            return View(placa);
        }


        public ActionResult BuscarPlaca()
        {
            Placa p = new Placa();
            return View(p);
        }

        [HttpPost]
        public ActionResult BuscarPlaca(string placa)
        {
           Placa p = db.Placas.FirstOrDefault(x => x.numeroPlaca.Equals(placa));
            if (p == null)
            {
                return RedirectToAction("BuscarPlaca", "Placas").Mensagem("Placa não encontrada !!");
            }              
            return View(p);
        }


        public ActionResult CreatePlacaInquilino()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePlacaInquilino([Bind(Include = "placaId,numeroPlaca")] Placa placa, string inquilino)
        {
            placa.numeroPlaca = placa.numeroPlaca.ToUpper();
            placa.inquilino = db.Inquilinos.FirstOrDefault(v => v.NomeInquilino.Equals(inquilino));
            if (ModelState.IsValid)
            {
                db.Placas.Add(placa);
                db.SaveChanges();
                return View();
            }

            return View("Index");
        }



        public ActionResult CreatePlacaFuncionario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePlacaFuncionario([Bind(Include = "placaId,numeroPlaca")] Placa placa, string funcionario)
        {
            placa.numeroPlaca = placa.numeroPlaca.ToUpper();
            placa.funcionario = db.Funcionarios.FirstOrDefault(v => v.Nome.Equals(funcionario));
            if (ModelState.IsValid)
            {
                db.Placas.Add(placa);
                db.SaveChanges();
                return View();
            }

            return View("Index");
        }



        public ActionResult CreatePlacaProprietario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePlacaProprietario([Bind(Include = "placaId,numeroPlaca")] Placa placa, string proprietario)
        {
            placa.numeroPlaca = placa.numeroPlaca.ToUpper();
            placa.proprietario = db.Proprietarios.FirstOrDefault(v => v.Nome.Equals(proprietario));
            if (ModelState.IsValid)
            {
                db.Placas.Add(placa);
                db.SaveChanges();
                return View();
            }

            return View("Index");
        }


        public ActionResult CreatePlacaVisitante()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePlacaVisitante([Bind(Include = "placaId,numeroPlaca")] Placa placa, string visitante)
        {
            placa.numeroPlaca = placa.numeroPlaca.ToUpper();
            placa.visitante = db.Visitantes.FirstOrDefault(v => v.NomeVisitante.Equals(visitante));
            if (ModelState.IsValid)
            {
                db.Placas.Add(placa);
                db.SaveChanges();
                return View();
            }

            return View("Index");
        }



        // GET: Placas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Placas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "placaId,numeroPlaca")] Placa placa)
        {
            if (ModelState.IsValid)
            {
                db.Placas.Add(placa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(placa);
        }

        // GET: Placas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Placa placa = db.Placas.Find(id);
            if (placa == null)
            {
                return HttpNotFound();
            }
            return View(placa);
        }

        // POST: Placas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "placaId,numeroPlaca")] Placa placa)
        {
            placa.numeroPlaca = placa.numeroPlaca.ToUpper();
            if (ModelState.IsValid)
            {
                db.Entry(placa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(placa);
        }

        // GET: Placas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Placa placa = db.Placas.Find(id);
            if (placa == null)
            {
                return HttpNotFound();
            }
            return View(placa);
        }

        // POST: Placas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Placa placa = db.Placas.Find(id);
            db.Placas.Remove(placa);
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
