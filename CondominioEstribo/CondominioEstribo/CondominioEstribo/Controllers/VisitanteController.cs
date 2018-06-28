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
    public class VisitanteController : Controller
    {

        private Context db = new Context();


        // METODO UTILIZADO PARA FAZER A LISTAGEM DOS REGISTROS, POR DATA DE SAÍDA MAIOR OU IGUAL A DATA ATUAL EFETUANDO A BUSCA POR PLACA DO VEICULO DO VISITANTE //        
        public ActionResult Index(string txtPlaca)
        {
            var q = db.Visitantes.AsQueryable();

            //if (!string.IsNullOrEmpty(txtPlaca))
                q = q.Where(x => x.PlacaVeiculoVisitante.Contains(txtPlaca) && x.DataSaida >= DateTime.Now);
                q = q.OrderBy(x => x.NomeVisitante);

            return View(q.ToList());
            
        }



        // METODO UTILIZADO PARA FAZER A LISTAGEM DOS REGISTROS POR ORDEM ALFABETICA //        
        public ActionResult ListarTodosVisitantes(string txtNome)
        {
            var q = db.Visitantes.AsQueryable();

            if (!string.IsNullOrEmpty(txtNome))
                q = q.Where(x => x.NomeVisitante.Contains(txtNome));

            q = q.OrderBy(x => x.NomeVisitante);

            return View(q.ToList());

        }




        // GET: Visitante/ DETALHES DO REGISTRO DE VISITANTE
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitante visitante = db.Visitantes.Find(id);
            if (visitante == null)
            {
                return HttpNotFound();
            }
            return View(visitante);
        }




        // GET: Visitante/Create / CRIAR
        public ActionResult Create()
        {

            return View();

        }




        // POST: Visitante/Create / CRIAR
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VisitanteId,NomeVisitante,DataEntrada,DataSaida,PlacaVeiculoVisitante,NomeDoCondomino,Observacao")] Visitante visitante)
        {                 
                                                          
            if (ModelState.IsValid)
            {
                db.Visitantes.Add(visitante);
                visitante.NomeDoCondomino = Session["Nome"].ToString();

                DateTime dataEntrada = (visitante.DataEntrada).Date;
                DateTime dataSaida = (visitante.DataSaida).Date;
                int resultado = DateTime.Compare(dataEntrada, dataSaida);
                int resultadoAno = dataSaida.Year;

                if (resultado >= 1)
                {
                    return View().Mensagem("Data de Saída informada é menor que a Data de Entrada. Favor Verificar !!", "Atenção");
                }
                if (visitante.DataEntrada < (DateTime.Now.Date))
                {
                    return View().Mensagem("Data de Entrada informada é Anterior a Data de Atual. Favor Verificar !!", "Atenção");
                }
                if (resultadoAno != DateTime.Now.Year)
                {
                    return View().Mensagem("Verifique a Data de Saída", "Atenção");
                }
                                                                                                          
                db.SaveChanges();
                return RedirectToAction("Create", "Visitante").Mensagem("Dados Inseridos com Sucesso !!","Atenção");
            }

            return View(visitante);
        }



        // GET: Visitante/Edit/5 / ALTERAR
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitante visitante = db.Visitantes.Find(id);

            visitante.DataEntrada = visitante.DataEntrada;
            visitante.DataSaida = visitante.DataSaida;

            if (visitante == null)
            {
                return HttpNotFound();
            }
            return View(visitante);
        }



        // POST: Visitante/Edit/5 / ALTERAR      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VisitanteId,NomeVisitante,DataEntrada,DataSaida,PlacaVeiculoVisitante,NomeDoCondomino,Observacao")] Visitante visitante)
        {
            if (ModelState.IsValid)
            {

                DateTime dataEntrada = (visitante.DataEntrada);
                DateTime dataSaida = (visitante.DataSaida);
                int resultado = DateTime.Compare(dataEntrada, dataSaida);

                if (resultado >= 1)
                {
                    return RedirectToAction("Edit", "Visitante").Mensagem("Data de Saída informada é menor que a Data de Entrada. Favor Verificar !!", "Atenção");
                }
                if (dataEntrada < DateTime.Now.Date)
                {
                    return RedirectToAction("Edit", "Visitante").Mensagem("Data de Entrada informada é Anterior a Data de Atual. Favor Verificar !!", "Atenção");
                }


                db.Entry(visitante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Visitante").Mensagem("Dados Alterados com Sucesso !!", "Atenção");
            }
            return View(visitante);
        }

          
        
          
        // METODO UTILIZADO PARA DELETAR UM REGISTRO //
        public ActionResult DeletarVisitante(int id)
        {
            Visitante visitante = db.Visitantes.Find(id);
            db.Visitantes.Remove(visitante);
            db.SaveChanges();
            return RedirectToAction ("Index").Mensagem("Dados Excluidos com Sucesso !!","Atenção");
        }









//==========================================================================================================================================================//
    
        /*
            METODOS QUE NÃO SÃO UTILIZADOS, PELOS MENOS POR ENQUANTO. // 
        */



       

        // GET: Visitante/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitante visitante = db.Visitantes.Find(id);
            if (visitante == null)
            {
                return HttpNotFound();
            }
            return View(visitante);
        }

        // POST: Visitante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Visitante visitante = db.Visitantes.Find(id);
            db.Visitantes.Remove(visitante);
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



