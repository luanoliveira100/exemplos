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
    public class ObraController : Controller
    {
        private Context db = new Context();

        
        public List<Obra> BuscarObraMesAno(int mes, int ano)
        {
            List<Obra> contasMes = new List<Obra>();
            var ListaDeObras = from obras in db.Obras where obras.DataTerminoObra.Month == (mes) && obras.DataTerminoObra.Year == (ano) select obras;

            return ListaDeObras.ToList();

        }


        // GET: Obra
        public ActionResult ListaObraEmAndamento(string DropBusca)
        {
            var q = db.Obras.AsQueryable();

            // if (string.IsNullOrEmpty(DropStatus))

            q = q.Where(x => x.TipoObra.Equals(DropBusca) && x.StatusObra.Equals("Em Andamento")).OrderBy(x => x.DataInicioObra);         

            return View(q.ToList());

        }


        // GET: Obra
        public ActionResult ListaObraConcluida(string DropBusca)
        {
            var q = db.Obras.AsQueryable();

            // if (string.IsNullOrEmpty(DropStatus))

            q = q.Where(x => x.TipoObra.Equals(DropBusca) && x.StatusObra.Equals("Concluída")).OrderBy(x => x.DataInicioObra);
            
            return View(q.ToList());

        }

        

        // GET: Obra/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Obra obra = db.Obras.Find(id);
            if (obra == null)
            {
                return HttpNotFound();
            }
            return View(obra);
        }





        // GET: Obra/Create
        public ActionResult Create()
        {
            return View();
        }





        // POST: Obra/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ObraId,TipoObra,DescricaoObra,StatusObra,ValorObra,DataInicioObra,DataTerminoObra")] Obra obra, string DropStatus, string DropTipoObra)
        {
                 
            if (DropTipoObra == "1")
            {
                obra.TipoObra = "Construção";
            }
            else
            {
                if (DropTipoObra == "2")
                {
                    obra.TipoObra = "Reforma";
                }
                else
                {
                    if (DropTipoObra == "3")
                    {
                        obra.TipoObra = "Reparo";
                    }
                    else
                    {
                        obra.TipoObra = "Outros";
                    }
                }
            }           


            if (ModelState.IsValid)
            {
                
                DateTime dataInicio = Convert.ToDateTime(obra.DataInicioObra).Date;
                DateTime dataTermino = Convert.ToDateTime(obra.DataTerminoObra).Date;

                int resultado = DateTime.Compare(dataInicio, dataTermino);
                int resultadoAno = dataTermino.Year;

                if (resultado >= 1)
                {
                    return View(obra).Mensagem("Data de Termino da Obra é menor que a Data de Início. Favor Verificar !!", "Atenção");
                }
                if (obra.DataInicioObra < (DateTime.Now.Date))
                {
                    return View(obra).Mensagem("Data de Início da Obra é Anterior a Data de Atual. Favor Verificar !!", "Atenção");
                }
                if (resultadoAno != DateTime.Now.Year)
                {
                    return View(obra).Mensagem("Verifique a Data de Término da Obra", "Atenção");
                }
                obra.StatusObra = "Em Andamento";                                                                       
                db.Obras.Add(obra);
                db.SaveChanges();
                return RedirectToAction("Create", "Obra").Mensagem("Dados Inseridos com Sucesso !!", "Atenção");
            }

            return View(obra);
        }




        // GET: Obra/Edit/5
        public ActionResult AlterarDadosObra(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Obra obra = db.Obras.Find(id);
            if (obra == null)
            {
                return HttpNotFound();
            }
            return View(obra);
        }





        // POST: Obra/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlterarDadosObra([Bind(Include = "ObraId,TipoObra,DescricaoObra,StatusObra,ValorObra,DataInicioObra,DataTerminoObra")] Obra obra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(obra).State = EntityState.Modified;
                obra.StatusObra = "Em Andamento";
                db.SaveChanges();
                return RedirectToAction("ListaObraEmAndamento", "Obra").Mensagem("Dados Alterados com Sucesso !!", "Atenção");
            }
            return View(obra);
        }



        // GET: Obra/FinalizarObra/5
        public ActionResult FinalizarObra(int? id)
        {
            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Obra obra = db.Obras.Find(id);
            obra.DataTerminoObra = DateTime.Now;                            
            if (obra == null)
            {
                return HttpNotFound();
            }
            return View(obra);
        }





        // POST: Obra/FinalizarObra/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinalizarObra([Bind(Include = "ObraId,TipoObra,DescricaoObra,StatusObra,ValorObra,DataInicioObra,DataTerminoObra")] Obra obra, string DropStatus)
        {            
            string recebeStatus = DropStatus;                               
            
            if (ModelState.IsValid)
            {
                db.Entry(obra).State = EntityState.Modified;
                obra.StatusObra = "Concluída";
                db.SaveChanges();
                return RedirectToAction("ListaObraConcluida").Mensagem("Obra Concluída com Sucesso !!", "Informação");
            }
            return View(obra);
        }


        






        // GET: Obra/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Obra obra = db.Obras.Find(id);
            if (obra == null)
            {
                return HttpNotFound();
            }
            return View(obra);
        }

        
        


        // POST: Obra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Obra obra = db.Obras.Find(id);
            db.Obras.Remove(obra);
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
