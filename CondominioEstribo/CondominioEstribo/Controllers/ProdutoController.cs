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
    public class ProdutoController : Controller
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

        public bool InserirQuantidadeProduto(Produto produto , int quantidade)
        {
            produto.QuantidadeAtual += quantidade;
            if (ModelState.IsValid)
            {
                db.Entry(produto).State = EntityState.Modified;
                db.SaveChanges();
            }

            return true;
        }

        public bool SaidaDeProduto(SaidaMercadoria saidaMercadoria)
        {
            Produto p = saidaMercadoria.produto;
            p.QuantidadeAtual -= saidaMercadoria.quantidade;
            if (ModelState.IsValid)
            {
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
            }

            return true;
        }

        public bool RetirarQuantidadeProduto(NotaFiscalItens nfItems)
        {
            Produto p =  db.Produtos.FirstOrDefault(x => x.ProdutoId.Equals(nfItems.Produto.ProdutoId));
            p.QuantidadeAtual -= nfItems.Quantidade;
            if (ModelState.IsValid)
            {
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            return false;

           
        }

        // METODO UTILIZADO PARA FAZER A LISTAGEM DOS REGISTROS POR ORDEM ALFABETICA //        
        public ActionResult Index(string produto)
        {
            var q = db.Produtos.AsQueryable();

            //if (!string.IsNullOrEmpty(txtProduto))
                q = q.Where(x => x.NomeProduto.Contains(produto));

            q = q.OrderBy(x => x.NomeProduto);

            return View(q.ToList());
            
        }




        // GET: Produto/ DETALHES DO REGISTRO
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }




        // GET: Produto/Create / CRIAR
        public ActionResult Create()
        {
                      
            return View();

        }

        // POST: Produto/Create / CRIAR
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProdutoId,NomeProduto,Descricao,QuantidadeMinima,QuantidadeAtual,Observacao")] Produto produto)
        {

            produto.QuantidadeAtual = 0;

            if (ModelState.IsValid)
            {
                db.Produtos.Add(produto);
                db.SaveChanges();
                return RedirectToAction("Create", "Produto").Mensagem("Dados Inseridos com Sucesso !!", "Atenção");
            }


            return View(produto);
        }






        // GET: Produto/Edit/5 / ALTERAR
        public ActionResult Edit(int? id)
        {                               
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }


            return View(produto);
        }







        // POST: Produto/Edit/5 / ALTERAR
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProdutoId,NomeProduto,Descricao,QuantidadeMinima,QuantidadeAtual,Observacao")] Produto produto)
        {  
                      
            if (ModelState.IsValid)
            {
                db.Entry(produto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Produto").Mensagem("Dados Alterados com Sucesso !!", "Atenção");
            }

            
            return View(produto);
        }



        // METODO QUE EXCLUI O REGISTRO
        public ActionResult DeletarProduto(int id)
        {
            Produto produto = db.Produtos.Find(id);
            db.Produtos.Remove(produto);
            db.SaveChanges();
            return RedirectToAction("Index", "Produto").Mensagem("Dados Excluidos com Sucesso !!", "Atenção");
        }







//==================================================================================================================================//

          /*
                METODOS QUE NÃO SÃO UTILIZADOS, PELOS MENOS POR ENQUANTO. // 
          */


       




        // GET: Produto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // POST: Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produto produto = db.Produtos.Find(id);
            db.Produtos.Remove(produto);
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
