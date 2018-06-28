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
    public class CargoController : Controller
    {

        private Context db = new Context();



        // LISTA TODOS OS REGISTROS QUE ESTEJAM ATIVOS //
        public ActionResult Index(string txtNomeCargo)
        {
            var q = db.Cargos.AsQueryable();                      

            return View(q.ToList().Where(x => x.FlagStatus.Equals("Ativo")));

        }


       



        // LISTA TODOS OS REGISTROS DE CARGO E ORDENA POR ORDEM ALFABETICA //
        public ActionResult ListarTodosCargos()
        {

            var q = db.Cargos.AsQueryable();

            return View(q.ToList().Where(x=> x.FlagStatus.Equals("Inativo")));            
        }



        // GET: Cargo/ DETALHES DO REGISTRO DE CARGO
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = db.Cargos.Find(id);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            return View(cargo);
        }


        // GET: Cargo/ GRAVA O REGISTRO
        public ActionResult Create()
        {

            return View();


        }



        // POST: Cargo/ GRAVA O REGISTRO 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CargoId,NomeCargo,Observacao,FlagStatus")] Cargo cargo, string DropStatus)
        {
            string setarDropStatus = "1";
            DropStatus = setarDropStatus;
            string recebeStatus = DropStatus;


            if (recebeStatus == "1")
            {
                cargo.FlagStatus = "Ativo";
            }
            else
            {
                if (recebeStatus == "2")
                {
                    cargo.FlagStatus = "Inativo";
                }

            }



            if (ModelState.IsValid)
            {
                db.Cargos.Add(cargo);
                db.SaveChanges();
                return RedirectToAction("Create", "Cargo").Mensagem("Dados Inseridos com Sucesso !!", "Atenção");
            }

            return View(cargo);
        }



        // GET: Cargo/ ALTERA O REGISTRO
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = db.Cargos.Find(id);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            return View(cargo);
        }



        // POST: Cargo/ ALTERA O REGISTRO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CargoId,NomeCargo,Observacao,FlagStatus")] Cargo cargo, string DropStatus)
        {

            string recebeStatus = DropStatus;


            if (recebeStatus == "1")
            {
                cargo.FlagStatus = "Ativo";
            }
            else
            {
                if (recebeStatus == "2")
                {
                    cargo.FlagStatus = "Inativo";
                }

            }


            if (ModelState.IsValid)
            {
                db.Entry(cargo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Cargo").Mensagem("Dados Alterados com Sucesso !!", "Atenção");
            }
            return View(cargo);
        }


        // METODO UTILIZADO PARA EXCLUIR UM REGISTRO DE CARGO
        public ActionResult DeletarCargo(int id)
        {
            Cargo cargo = db.Cargos.Find(id);

            try
            {
                if (cargo.FlagStatus == "Ativo")
                {
                    return RedirectToAction("ListarTodosCargos", "Cargo").Mensagem("Fornecedor com Status Ativo, não pode ser Excluido !!", "Atenção");
                }

                db.Cargos.Remove(cargo);
                db.SaveChanges();
                return RedirectToAction("ListarTodosCargos", "Cargo").Mensagem("Dados Excluidos com Sucesso !!", "Atenção");
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Home").Mensagem("Não Foi Possivel Excluir este Cargo. Existem Registros Atrelados ao Mesmo. Contate o Administrador do Sistema.", "Atenção");
            }
        }




        // POST: Fornecedor /  METODO UTILIZADO PARA INATIVAR UM REGISTRO (FORNECEDOR)
        public ActionResult InativarCargo(int id)
        {
            Cargo cargo = db.Cargos.Find(id);

            if (ModelState.IsValid)
            {
                cargo.FlagStatus = "Inativo";
                db.Entry(cargo).State = EntityState.Modified;
                db.SaveChanges();

            }

            return RedirectToAction("Index", "Cargo").Mensagem("Operação Realizada com Sucesso !!", "Atenção");
        }







//===========================================================================================================================//

        /*
            METODOS QUE NÃO ESTÃO SENDO UTILIZADOS NO MOMENTO.
         */



        // GET: Cargo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = db.Cargos.Find(id);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            return View(cargo);
        }

        // POST: Cargo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cargo cargo = db.Cargos.Find(id);
            db.Cargos.Remove(cargo);
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
