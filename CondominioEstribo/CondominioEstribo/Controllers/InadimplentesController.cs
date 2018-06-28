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
    public class InadimplentesController : Controller
    {
        private Context db = new Context();


        public void deletarInadimplentes(Contas c)
        {
            Inadimplentes inadimplente = new Inadimplentes();
            inadimplente.Proprietario = c.proprietario;
            DateTime date = c.datatitulo;
            inadimplente = db.Inadimplentes.FirstOrDefault(x => x.data.Month.Equals(date.Month) && x.Proprietario.UsuarioId.Equals(c.proprietario.UsuarioId));
            if (inadimplente != null)
            {
                db.Inadimplentes.Remove(inadimplente);
                db.SaveChanges();

            }
        }


        public Inadimplentes buscarInadimplente(Proprietario p, string mes)
        {

            int ano = Convert.ToInt16(mes.Substring(0, 4));
            int mes1 = Convert.ToInt16(mes.Substring(5, 2));

            int mesInteiro = Convert.ToInt16(mes1);


            Inadimplentes inadimplentes = new Inadimplentes();
            inadimplentes = db.Inadimplentes.FirstOrDefault(x => x.data.Month.Equals(mesInteiro) && x.Proprietario.UsuarioId.Equals(p.UsuarioId) && x.data.Year.Equals(ano));
            if (inadimplentes == null)
            {
                return null;
            }
            else
            {
                return inadimplentes;

            }
        }


        public List<Inadimplentes> salvar  (List<Proprietario> proprietario, string mes)
        {

            DateTime data = Convert.ToDateTime(mes);
            Inadimplentes inadimplente = new Inadimplentes();
            List<Inadimplentes> listaProprietariosInadimplente = new List<Inadimplentes>();
            //listaInadimplentes = db.Inadimplentes.ToList();

            foreach (var p1 in proprietario)
            {

                //inadimplente.Proprietario = p1;
                inadimplente.Proprietario = db.Proprietarios.FirstOrDefault(x => x.UsuarioId.Equals(p1.UsuarioId));
                inadimplente.data = data;
                //InadimplentesController inadimplenteController = new InadimplentesController();
                ////if (inadimplenteController.buscarInadimplente(p1, mes) == null)
                ////{
                //    int ano = Convert.ToInt16(mes.Substring(0, 4));
                //    int mes1 = Convert.ToInt16(mes.Substring(5, 2));
                //    inadimplentes = db.Inadimplentes.FirstOrDefault(x => x.data.Month.Equals(mes1) && x.Proprietario.UsuarioId.Equals(p1.UsuarioId) && x.data.Year.Equals(ano));
               
                if (inadimplente != null)
                {
                   
                    if (ModelState.IsValid)
                    {
                        db.Inadimplentes.Add(inadimplente);
                        db.SaveChanges();
                        listaProprietariosInadimplente.Add(inadimplente);
                    }
                }
                    
                //}




            }
            return listaProprietariosInadimplente;


        }







        // GET: Inadimplentes
        public ActionResult Index()
        {
            return View(db.Inadimplentes.ToList());
        }

        // GET: Inadimplentes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inadimplentes inadimplentes = db.Inadimplentes.Find(id);
            if (inadimplentes == null)
            {
                return HttpNotFound();
            }
            return View(inadimplentes);
        }

        // GET: Inadimplentes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inadimplentes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContasId,Proprietarioid,data")] Inadimplentes inadimplentes)
        {
            if (ModelState.IsValid)
            {
                db.Inadimplentes.Add(inadimplentes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(inadimplentes);
        }

        // GET: Inadimplentes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inadimplentes inadimplentes = db.Inadimplentes.Find(id);
            if (inadimplentes == null)
            {
                return HttpNotFound();
            }
            return View(inadimplentes);
        }

        // POST: Inadimplentes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContasId,Proprietarioid,data")] Inadimplentes inadimplentes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inadimplentes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inadimplentes);
        }

        // GET: Inadimplentes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inadimplentes inadimplentes = db.Inadimplentes.Find(id);
            if (inadimplentes == null)
            {
                return HttpNotFound();
            }
            return View(inadimplentes);
        }

        // POST: Inadimplentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inadimplentes inadimplentes = db.Inadimplentes.Find(id);
            db.Inadimplentes.Remove(inadimplentes);
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
