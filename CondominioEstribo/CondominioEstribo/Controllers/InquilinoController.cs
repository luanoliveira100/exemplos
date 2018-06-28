using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CondominioEstribo.Models;
using CondominioEstribo.Negocio;

namespace CondominioEstribo.Controllers
{
    public class InquilinoController : Controller
    {

        private Context db = new Context();


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

        public JsonResult GetLote(string term)
        {
            // retorna placas
            List<string> lotes;
            lotes = db.Lotes.Where(p => p.NumeroLote.StartsWith(term)).
            Select(e => e.NumeroLote).Distinct().ToList();

            return Json(lotes, JsonRequestBehavior.AllowGet);
        }


        // GET: Inquilino
        public ActionResult Index(string inquilino)
        {
            var q = db.Inquilinos.AsQueryable();
            //if (!string.IsNullOrEmpty(txtNomeInquilino))
            q = q.Where(x => x.NomeInquilino.Contains(inquilino) && x.FlagStatus == "Ativo");
            q = q.OrderBy(x => x.NomeInquilino);


            return View(q.ToList());            

        }



        // GET: Inquilino
        public ActionResult ListarTodosInquilinos()
        {
            var q = db.Inquilinos.AsQueryable();            

            return View(q.ToList().Where(x => x.FlagStatus.Equals("Inativo")).OrderBy(x => x.NomeInquilino));

        }


        public Inquilino BuscarPorNome(string Nome)
        {

            Inquilino inquilino = new Inquilino();

            inquilino = db.Inquilinos.FirstOrDefault(x => x.NomeInquilino.Equals(Nome));

            if (inquilino == null)
            {
                return null;
            }
            else
            {
                return inquilino;
            }


        }
        
        // GET: Inquilino/ DETALHES
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inquilino inquilino = db.Inquilinos.Find(id);
            if (inquilino == null)
            {
                return HttpNotFound();
            }
            return View(inquilino);
        }





        // GET: Inquilino/ GRAVAR 
        public ActionResult Create()
        {

            return View();

        }

        // POST: Inquilino/GRAVA OS DADOS DO INQUILINO
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult Create([Bind(Include = "UsuarioId,LoginUsuario,SenhaUsuario,StatusUsuario,PerfilUsuario,InquilinoId,NomeInquilino,CPFInquilino,RGInquilino,TelefoneInquilino,Observacao,EmailInquilino,FlagStatus,LoteInquilino")] Inquilino inquilino, string DropStatus, string lote)
        {
            Lote l = db.Lotes.FirstOrDefault(x => x.NumeroLote.Equals(lote));

            if (l != null)
            {
                
                inquilino.lote = l;
                inquilino.proprietario = l.proprietario;
                string setarDropStatus = "1";
                DropStatus = setarDropStatus;
                string recebeStatus = DropStatus;


                if (recebeStatus == "1")
                {
                    inquilino.FlagStatus = "Ativo";
                }
                else
                {
                    if (recebeStatus == "2")
                    {
                        inquilino.FlagStatus = "Inativo";
                    }

                }

                if (ModelState.IsValid)
                {
                    db.Usuarios.Add(inquilino);
                    inquilino.SenhaUsuario = "123456";
                    inquilino.PerfilUsuario = "Inquilino";
                    inquilino.StatusUsuario = true;
                    if (inquilino.RGInquilino == null)
                    {
                        inquilino.RGInquilino = "Não Informado.";
                    }
                    if (inquilino.EmailInquilino == null)
                    {
                        inquilino.EmailInquilino = "Não Informado.";
                    }
                    if (ValidarCPF.ValidaCPF(inquilino.CPFInquilino))
                    {
                        inquilino.LoginUsuario = inquilino.CPFInquilino;
                        db.SaveChanges();
                        return RedirectToAction("Create", "Inquilino").Mensagem("Dados Inseridos com Sucesso !!", "Atenção");
                    }
                    else
                    {
                        return View().Mensagem("CPF Informado é Inválido. Favor Verificar.");
                    }

                }
            }


            return View().Mensagem("Lote Inválido. Favor Verificar.", "Atenção");
        }




        // GET: Inquilino/ MOSTRA OS DADOS PARA EDITAR O REGISTRO
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inquilino inquilino = db.Inquilinos.Find(id);
            if (inquilino == null)
            {
                return HttpNotFound();
            }
            return View(inquilino);
        }




        // POST: Inquilino/ ALTERA O REGISTRO DO INQUILINO E SALVA 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UsuarioId,LoginUsuario,SenhaUsuario,StatusUsuario,PerfilUsuario,InquilinoId,NomeInquilino,CPFInquilino,RGInquilino,TelefoneInquilino,Observacao,EmailInquilino,FlagStatus,LoteInquilino")] Inquilino inquilino, string DropStatus)
        {
            
            string recebeStatus = DropStatus;


            if (recebeStatus == "1")
            {
                inquilino.FlagStatus = "Ativo";
            }
            else
            {
                if (recebeStatus == "2")
                {
                    inquilino.FlagStatus = "Inativo";
                }

            }

            if (ModelState.IsValid)
            {
                db.Entry(inquilino).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Inquilino").Mensagem("Dados Alterados com Sucesso !!", "Atenção");
            }
            return View(inquilino);
        }



        // METODO PARA EXCLUIR UM INQUILINO
        public ActionResult DeletarInquilino(int id)
        {
            Inquilino inquilino = db.Inquilinos.Find(id);

            try
            {
                if (inquilino.FlagStatus == "Ativo")
                {
                    return RedirectToAction("ListarTodosInquilinos", "Inquilino").Mensagem("Inquilino com Status Ativo, não pode ser Excluido !!", "Atenção");
                }

                db.Inquilinos.Remove(inquilino);
                db.SaveChanges();
                return RedirectToAction("ListarTodosInquilinos", "Inquilino").Mensagem("Dados Excluidos com Sucesso !!", "Atenção");
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Home").Mensagem("Não Foi Possivel Excluir este Inquilino. Existem Registros Atrelados ao Mesmo. Contate o Administrador do Sistema.", "Atenção");
            }
        }


        // METODO UTILIZADO PARA INATIVAR UM REGISTRO DE INQUILINO
        public ActionResult InativarInquilino(int id)
        {
            Inquilino inquilino = db.Inquilinos.Find(id);

            if (ModelState.IsValid)
            {
                inquilino.FlagStatus = "Inativo";
                db.Entry(inquilino).State = EntityState.Modified;
                db.SaveChanges();

            }

            return RedirectToAction("Index", "Inquilino").Mensagem("Operação Realizada com Sucesso !!", "Atenção");
        }





        










        //====================================================================================================================================//






        // GET: Inquilino/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inquilino inquilino = db.Inquilinos.Find(id);
            if (inquilino == null)
            {
                return HttpNotFound();
            }
            return View(inquilino);
        }

        // POST: Inquilino/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inquilino inquilino = db.Inquilinos.Find(id);
            db.Usuarios.Remove(inquilino);
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
