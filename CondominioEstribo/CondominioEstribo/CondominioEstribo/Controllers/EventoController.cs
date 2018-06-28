using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CondominioEstribo.Models;
using System;

namespace CondominioEstribo.Controllers
{
    [Authorize]
    public class EventoController : Controller
    {
        private Context db = new Context();

        public ActionResult Ver(string txtData)
        {
            return View(db.Eventos.ToList());            
        }

        // GET: Eventos
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Reservar()
        {
            return View();
        }





        // GET: Eventos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Eventos.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // GET: Eventos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventoId,Titulo,DataInicial,DataFinal,DiaTodo")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                db.Eventos.Add(evento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(evento);
        }

        // GET: Eventos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Eventos.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventoId,Titulo,DataInicial,DataFinal,DiaTodo")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(evento);
        }

        // GET: Eventos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Eventos.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evento evento = db.Eventos.Find(id);
            db.Eventos.Remove(evento);
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

        // Método que cria os eventos no calendário
        public ActionResult CriarEvento (string data)
        {
            Evento e = new Evento();
            e.Titulo = "Reservado";
            e.DataInicial = DateTime.Parse(data);
            e.DataFinal = e.DataInicial;
            e.DiaTodo = true;

            if (ModelState.IsValid)
            {
                db.Eventos.Add(e);
                db.SaveChanges();
            }
            return Json("Reserva realizada com sucesso!", JsonRequestBehavior.AllowGet);
        }

        //Método que deleta os eventos no calendário
        public ActionResult DeletarEvento (int id)
        {
            Evento evento = db.Eventos.Find(id);
            db.Eventos.Remove(evento);
            db.SaveChanges();
            return Json("Reserva cancelada com sucesso!", JsonRequestBehavior.AllowGet);
        }

        // Método que lista os eventos no calendário
        public ActionResult ListarEventos()
        {            
            var eventosDb = db.Eventos.ToList();

            var eventos = from e in eventosDb
                          select new
                          {
                              id = e.EventoId,
                              title = e.Titulo,
                              start = e.DataInicial.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                              end = e.DataFinal.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                              allDay = e.DiaTodo
                          };

            return Json(eventos.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}
