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
    public class OcorrenciaController : Controller
    {
        private Context db = new Context();

        // GET: Ocorrencia
        public ActionResult ListaEmAberto(string DropBusca)
        {                     
            
            var q = db.Ocorrencias.AsQueryable();

            // if (string.IsNullOrEmpty(DropStatus))

            q = q.Where(x => x.Tipo.Equals(DropBusca) && x.Situacao.Equals("Em Aberto"));
            q = q.OrderBy(x => x.DataAberturaOcorrencia);

            return View(q.ToList());
           
        }


        public ActionResult ListaFinalizada(string DropBusca)
        {            

            var q = db.Ocorrencias.AsQueryable();

            // if (string.IsNullOrEmpty(DropStatus))

            q = q.Where(x => x.Tipo.Equals(DropBusca) && x.Situacao.Equals("Finalizada"));
            q = q.OrderBy(x => x.DataAberturaOcorrencia);

            return View(q.ToList());
        }



        // GET: Ocorrencia/Details/5
        public ActionResult DetailsEmAberto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ocorrencia ocorrencia = db.Ocorrencias.Find(id);
            if (ocorrencia == null)
            {
                return HttpNotFound();
            }
            return View(ocorrencia);
        }

        // GET: Ocorrencia/Details/5
        public ActionResult DetailsFinalizada(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ocorrencia ocorrencia = db.Ocorrencias.Find(id);
            if (ocorrencia == null)
            {
                return HttpNotFound();
            }
            return View(ocorrencia);
        }







        // GET: Ocorrencia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ocorrencia/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OcorrenciaId,Descricao,Tipo,Situacao,Desfecho,DataAberturaOcorrencia,DataDesfechoOcorrencia,FlagData,QuemRegistrouOcorrencia")] Ocorrencia ocorrencia, string DropSitucao, string DropTipoObra)
        {
                   
            if (DropTipoObra == "1")
            {
                ocorrencia.Tipo = "Dúvida";
            }
            else
            {
                if (DropTipoObra == "2")
                {
                    ocorrencia.Tipo = "Reclamação";
                }
                else
                {
                    if (ocorrencia.Tipo == "3")
                    {
                        ocorrencia.Tipo = "Sugestão";
                    }
                    else
                    {
                        ocorrencia.Tipo = "Outros";
                    }
                }
            }
                         
         
            if (ModelState.IsValid)
            {
                db.Ocorrencias.Add(ocorrencia);
                ocorrencia.Situacao = "Em Aberto";
                ocorrencia.Desfecho = "Ocorrência está sendo tratada.";
                ocorrencia.DataAberturaOcorrencia = DateTime.Now;
                ocorrencia.DataDesfechoOcorrencia = DateTime.Now;
                ocorrencia.QuemRegistrouOcorrencia = Session["Nome"].ToString();                
                db.SaveChanges();
                return RedirectToAction("Create", "Ocorrencia").Mensagem("Ocorrência Aberta com Sucesso !!", "Atenção");
            }

            return View(ocorrencia);
        }

        // GET: Ocorrencia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ocorrencia ocorrencia = db.Ocorrencias.Find(id);
            ocorrencia.Desfecho = "";
            ocorrencia.DataDesfechoOcorrencia = DateTime.Now;
            if (ocorrencia == null)
            {
                return HttpNotFound();
            }
            return View(ocorrencia);
        }

        // POST: Ocorrencia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OcorrenciaId,Descricao,Tipo,Situacao,Desfecho,DataAberturaOcorrencia,DataDesfechoOcorrencia,FlagData")] Ocorrencia ocorrencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ocorrencia).State = EntityState.Modified;
                ocorrencia.Situacao = "Finalizada";
                ocorrencia.DataDesfechoOcorrencia = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("ListaFinalizada", "Ocorrencia").Mensagem("Ocorrência Finalizada com Sucesso !!", "Atenção");
            }
            return View(ocorrencia);
        }

        // GET: Ocorrencia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ocorrencia ocorrencia = db.Ocorrencias.Find(id);
            if (ocorrencia == null)
            {
                return HttpNotFound();
            }
            return View(ocorrencia);
        }

        // POST: Ocorrencia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ocorrencia ocorrencia = db.Ocorrencias.Find(id);
            db.Ocorrencias.Remove(ocorrencia);
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
