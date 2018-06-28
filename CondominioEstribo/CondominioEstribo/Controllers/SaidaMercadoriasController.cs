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
    public class SaidaMercadoriasController : Controller
    {
        private Context db = new Context();


        public JsonResult GetProduto(string term)
        {
            // retorna placas
            List<string> produtos;
            produtos = db.Produtos.Where(p => p.NomeProduto.StartsWith(term)).
                 Select(e => e.NomeProduto).Distinct().ToList();

            return Json(produtos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFuncionario(string term)
        {
            // retorna placas
            List<string> funcionarios;
            funcionarios = db.Funcionarios.Where(p => p.Nome.StartsWith(term)).
                 Select(e => e.Nome).Distinct().ToList();

            return Json(funcionarios, JsonRequestBehavior.AllowGet);
        }

        // GET: SaidaMercadorias
        public ActionResult Index()
        {
            return View(db.SaidaMercadorias.ToList());
        }

        // GET: SaidaMercadorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaidaMercadoria saidaMercadoria = db.SaidaMercadorias.Find(id);
            if (saidaMercadoria == null)
            {
                return HttpNotFound();
            }
            return View(saidaMercadoria);
        }

        // GET: SaidaMercadorias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SaidaMercadorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SaidaMercadoriaId,quantidade,data,observacao")] SaidaMercadoria saidaMercadoria, string produto, string funcionario)
        {
            saidaMercadoria.produto = db.Produtos.FirstOrDefault(x => x.NomeProduto.Equals(produto));
            saidaMercadoria.funcionario = db.Funcionarios.FirstOrDefault(x => x.Nome.Equals(funcionario));
            if (saidaMercadoria.produto == null )
            {
                return RedirectToAction("Create", "SaidaMercadorias").Mensagem("Produto não localizado!!");
            }
            else
            {
                if ( saidaMercadoria.funcionario == null)
                {
                    return RedirectToAction("Create", "SaidaMercadorias").Mensagem("Funcionário não localizado!!");
                }
            }
            if (saidaMercadoria.data > DateTime.Now)
            {
                return RedirectToAction("Create", "SaidaMercadorias").Mensagem("Data não pode ser maior que a atual favor verificar!!");
            }

            if (saidaMercadoria.quantidade > saidaMercadoria.produto.QuantidadeAtual)
            {
                return RedirectToAction("Create", "SaidaMercadorias").Mensagem("Quantidade da retirada excede a quantidade em estoque , !!", "Atencão");
            }
            else
            {
                ProdutoController produtocontroller = new ProdutoController();
                if (produtocontroller.SaidaDeProduto(saidaMercadoria))
                {
                    if (ModelState.IsValid)
                    {
                        db.SaidaMercadorias.Add(saidaMercadoria);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    
                }
            }
            return View(saidaMercadoria);

        }

        // GET: SaidaMercadorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaidaMercadoria saidaMercadoria = db.SaidaMercadorias.Find(id);
            if (saidaMercadoria == null)
            {
                return HttpNotFound();
            }
            return View(saidaMercadoria);
        }

        // POST: SaidaMercadorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SaidaMercadoriaId,quantidade,data,observacao")] SaidaMercadoria saidaMercadoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(saidaMercadoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(saidaMercadoria);
        }

        // GET: SaidaMercadorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SaidaMercadoria saidaMercadoria = db.SaidaMercadorias.Find(id);
            if (saidaMercadoria == null)
            {
                return HttpNotFound();
            }
            return View(saidaMercadoria);
        }

        // POST: SaidaMercadorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SaidaMercadoria saidaMercadoria = db.SaidaMercadorias.Find(id);
            db.SaidaMercadorias.Remove(saidaMercadoria);
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
