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
    public class GerenciarEntradaPortariaController : Controller
    {


        private Context db = new Context();




        // GET: GerenciarEntradaPortaria
        public ActionResult Index(string txtPlaca)
        {

            var q = db.GerenciarEntradaPortarias.AsQueryable();
            q = q.Where(x => x.PlacaDoVeiculo.Contains(txtPlaca) && x.FlagParaControle == "Entrada");
            q = q.OrderBy(x => x.DataHoraEntrada);

            return View(q.ToList());

        }




        public ActionResult ListarTodosRegistros(string DropStatus)
        {

            string recebeStatus = DropStatus;

            var q = db.GerenciarEntradaPortarias.AsQueryable();
            q = q.Where(x => x.FlagParaControle.Equals(recebeStatus) || x.FlagParaControle.Equals(recebeStatus)).Take(30);
            q = q.OrderBy(x => x.DataHoraEntrada);

            return View(q.ToList());

        }
        public JsonResult GetPlaca(string term)
        {
            //placa proprietario
            List<string> Placasproprietario;
            Placasproprietario = db.CadastroVeiculoProprietarios.Where(p => p.PlacaVeiculoProprietario.StartsWith(term)).
                 Select(e => e.PlacaVeiculoProprietario).Distinct().ToList();
            if (Placasproprietario.Count != 0)
            {
                return Json(Placasproprietario, JsonRequestBehavior.AllowGet);
            }

            //placa visitante
            List<string> PlacasVisitante;
            PlacasVisitante = db.Visitantes.Where(p => p.PlacaVeiculoVisitante.StartsWith(term)).
                 Select(e => e.PlacaVeiculoVisitante).Distinct().ToList();
            if (PlacasVisitante.Count != 0)
            {
                return Json(PlacasVisitante, JsonRequestBehavior.AllowGet);
            }

            //placa inquilino
            List<string> PlacasInquilino;
            PlacasInquilino = db.CadastroVeiculoInquilinos.Where(p => p.PlacaVeiculoInquilino.StartsWith(term)).
                 Select(e => e.PlacaVeiculoInquilino).Distinct().ToList();
            if (PlacasInquilino.Count != 0)
            {
                return Json(PlacasInquilino, JsonRequestBehavior.AllowGet);
            }
            List<string> PlacasFuncionario;
            PlacasFuncionario = db.CadastroVeiculoFuncionarios.Where(p => p.PlacaVeiculoFuncionario.StartsWith(term)).
                 Select(e => e.PlacaVeiculoFuncionario).Distinct().ToList();
            if (PlacasFuncionario.Count!=0)
            {
                return Json(PlacasFuncionario, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }


        public ActionResult BuscarPlaca(string Placa)
        {
            List<CadastroVeiculoProprietario> placaProprietario = new List<CadastroVeiculoProprietario>();
            placaProprietario = db.CadastroVeiculoProprietarios.Where(x => x.PlacaVeiculoProprietario.Equals(Placa)).ToList();

            List<CadastroVeiculoFuncionario> placaFuncionario = new List<CadastroVeiculoFuncionario>();
            placaFuncionario = db.CadastroVeiculoFuncionarios.Where(x => x.PlacaVeiculoFuncionario.Equals(Placa)).ToList();

            List<CadastroVeiculoInquilino> placaInquilino = new List<CadastroVeiculoInquilino>();
            placaInquilino = db.CadastroVeiculoInquilinos.Where(x => x.PlacaVeiculoInquilino.Equals(Placa)).ToList();

            List<GerenciarEntradaPortaria> placaVisitante = new List<GerenciarEntradaPortaria>();
            placaVisitante = db.GerenciarEntradaPortarias.Where(x => x.PlacaDoVeiculo.Equals(Placa)).ToList();


            if (Placa != null)
            {

                if (placaProprietario.Count != 0)
                {
                    ViewBag.placaProprietario = placaProprietario;
                    return View();
                }
                else
                {
                    if (placaFuncionario.Count != 0)
                    {
                        ViewBag.placaFuncionario = placaFuncionario;
                        return View();
                    }
                    else
                    {
                        if (placaInquilino.Count != 0)
                        {
                            ViewBag.placaInquilino = placaInquilino;
                            return View();
                        }
                        else
                        {
                            if (placaVisitante.Count != 0)
                            {
                                ViewBag.placaVisitaante = placaVisitante;
                                return View();
                            }
                        }
                    }
                }

                return View().Mensagem("Placa Não Encontrada. Favor Verificar !!");
            }

            return View();

        }    





        // GET: GerenciarEntradaPortaria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GerenciarEntradaPortaria gerenciarEntradaPortaria = db.GerenciarEntradaPortarias.Find(id);
            if (gerenciarEntradaPortaria == null)
            {
                return HttpNotFound();
            }
            return View(gerenciarEntradaPortaria);
        }


        public ActionResult DetalheTodosRegistros(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            GerenciarEntradaPortaria gerenciarEntradaPortaria = db.GerenciarEntradaPortarias.Find(id);

            if (gerenciarEntradaPortaria == null )
            {
                return HttpNotFound();
            }                      

            return View(gerenciarEntradaPortaria);
        }





        // GET: GerenciarEntradaPortaria/Create
        public ActionResult Create()
        {
            GerenciarEntradaPortaria gerenciarEntradaPortaria = new GerenciarEntradaPortaria();

            gerenciarEntradaPortaria.DataHoraEntrada = DateTime.Now;

            return View();
        }







        // POST: GerenciarEntradaPortaria/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GerenciarEntradaPortariaId,Nome,RG,DataHoraEntrada,DataHoraSaida,PlacaDoVeiculo,TelefoneContato,FlagParaControle,Observacao")] GerenciarEntradaPortaria gerenciarEntradaPortaria, string DropStatus)
        {   
                      
            if (DropStatus == "1")
            {
                gerenciarEntradaPortaria.FlagParaControle = "Entrada";
            }
            else
            {
                if (DropStatus == "2")
                {
                    gerenciarEntradaPortaria.FlagParaControle = "Saída";
                }

            }

            if (gerenciarEntradaPortaria.PlacaDoVeiculo == null)
            {
                gerenciarEntradaPortaria.PlacaDoVeiculo = "SEMPLACA";
            }

            gerenciarEntradaPortaria.DataHoraEntrada = DateTime.Now;
            gerenciarEntradaPortaria.DataHoraSaida = DateTime.Now;                                                             

            if (ModelState.IsValid)
            {
                db.GerenciarEntradaPortarias.Add(gerenciarEntradaPortaria);                
                db.SaveChanges();
                return RedirectToAction("Index", "Home").Mensagem("Entrada Registrada com Sucesso !!", "Atenção");
            }

            return View(gerenciarEntradaPortaria);
        }





        // GET: GerenciarEntradaPortaria/Edit/5
        public ActionResult Edit(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
                        
            GerenciarEntradaPortaria gerenciarEntradaPortaria = db.GerenciarEntradaPortarias.Find(id);
            //gerenciarEntradaPortaria.DataHoraEntrada = Convert.ToDateTime(gerenciarEntradaPortaria.FlagDataHora);
            gerenciarEntradaPortaria.DataHoraSaida = DateTime.Now;
            gerenciarEntradaPortaria.FlagParaControle = "Saída";

            if (gerenciarEntradaPortaria == null)
            {
                return HttpNotFound();
            }
            return View(gerenciarEntradaPortaria);
        }






        // POST: GerenciarEntradaPortaria/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GerenciarEntradaPortariaId,Nome,RG,DataHoraEntrada,DataHoraSaida,PlacaDoVeiculo,TelefoneContato,FlagParaControle,Observacao,FlagDataHora,Status")] GerenciarEntradaPortaria gerenciarEntradaPortaria, string DropStatus)
        {         
                      

            if (DropStatus == "1")
            {
                gerenciarEntradaPortaria.FlagParaControle = "Entrada";
            }
            else
            {
                if (DropStatus == "2")
                {
                    gerenciarEntradaPortaria.FlagParaControle = "Saída";
                }

            }

            if (ModelState.IsValid)
            {               
                gerenciarEntradaPortaria.DataHoraSaida = DateTime.Now;                                            
                db.Entry(gerenciarEntradaPortaria).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("ListarTodosRegistros", "GerenciarEntradaPortaria").Mensagem("Saída Registrada com Sucesso !!", "Atenção");
            }
            return View(gerenciarEntradaPortaria);
        }





        public ActionResult DeletarRegistro(int id)
        {
            GerenciarEntradaPortaria gerenciarEntradaPortaria = db.GerenciarEntradaPortarias.Find(id);

            if (gerenciarEntradaPortaria.FlagParaControle == "Entrada")
            {
                return RedirectToAction("Index", "Home").Mensagem("Registro Pendente de Saída, não é possivel Excluir. Favor Verificar !!", "Atenção");
            }

            db.GerenciarEntradaPortarias.Remove(gerenciarEntradaPortaria);
            db.SaveChanges();
            return RedirectToAction("Index", "Home").Mensagem("Registro Excluído com Sucesso !!", "Atenção");
        }









//================================================================================================================================================//




        // GET: GerenciarEntradaPortaria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GerenciarEntradaPortaria gerenciarEntradaPortaria = db.GerenciarEntradaPortarias.Find(id);
            if (gerenciarEntradaPortaria == null)
            {
                return HttpNotFound();
            }
            return View(gerenciarEntradaPortaria);
        }

        // POST: GerenciarEntradaPortaria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GerenciarEntradaPortaria gerenciarEntradaPortaria = db.GerenciarEntradaPortarias.Find(id);
            db.GerenciarEntradaPortarias.Remove(gerenciarEntradaPortaria);
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
