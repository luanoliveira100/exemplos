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
    public class EntradaMercadoriaController : Controller
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

        public JsonResult GetFornecedor(string term)
        {
            // retorna placas
            List<string> fornecedores;
            fornecedores = db.Fornecedores.Where(p => p.NomeEmpresa.StartsWith(term)).
                 Select(e => e.NomeEmpresa).Distinct().ToList();

            return Json(fornecedores, JsonRequestBehavior.AllowGet);
        }

        public List<EntradaMercadoria> BuscarMercadoriaMesAno(int mes, int ano)
        {
            var Mercadorias = from mercadorias in db.EntradaMercadorias
            where mercadorias.DataDaEntrada.Month == (mes) && mercadorias.DataDaEntrada.Year == (ano)
                              select mercadorias;
            return Mercadorias.ToList();
        }

        // GET: EntradaMercadoria
        public ActionResult ListarDados(string txtData)
        {
            try
            {
                DateTime recebeData = Convert.ToDateTime(txtData);
                var q = db.EntradaMercadorias.AsQueryable();
                q = q.Where(x => x.DataDaEntrada.Equals(recebeData));
               // q = q.OrderBy(x => x.ProdutoEntradaMercadoria);

                return View(q.ToList());
            }
            catch (Exception)
            {
                return RedirectToAction("ListarDados").Mensagem("Favor Informar uma Data !!", "Atenção");
            }
            
        }

        // GET: EntradaMercadoria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntradaMercadoria entradaMercadoria = db.EntradaMercadorias.Find(id);
            if (entradaMercadoria == null)
            {
                return HttpNotFound();
            }
            return View(entradaMercadoria);
        }

        // GET: EntradaMercadoria/Create
        public ActionResult Create()
        {

            List<Produto> ListaDeProduto = db.Produtos.ToList();
            List<SelectListItem> ListaGenericaDeProduto = new List<SelectListItem>();

            foreach (Produto item in ListaDeProduto)
            {

                ListaGenericaDeProduto.Add(new SelectListItem { Text = item.NomeProduto.ToString(), Value = item.NomeProduto.ToString() });
            }
            ViewBag.Produto = ListaGenericaDeProduto;


            List<Fornecedor> ListaDeFornecedor = db.Fornecedores.ToList();
            List<SelectListItem> ListaGenericaDeFornecedor = new List<SelectListItem>();

            foreach (Fornecedor item in ListaDeFornecedor)
            {
                ListaGenericaDeFornecedor.Add(new SelectListItem { Text = item.NomeEmpresa.ToString(), Value = item.NomeEmpresa.ToString() });
            }
            ViewBag.Fornecedor = ListaGenericaDeFornecedor;


            return View();
        }

        // POST: EntradaMercadoria/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EntradaMercadoriaId,QuantidadeInserida,ValorTotalDaNF,DataDaEntrada,Observacao")] EntradaMercadoria entradaMercadoria, string DropProduto, string DropFornecedor)
        {

            string recebeProduto = DropProduto;
            Produto produto = db.Produtos.FirstOrDefault(x => x.NomeProduto == recebeProduto);
            entradaMercadoria.ProdutoDentroDeEntradaMercadoria = produto;
           // entradaMercadoria.ProdutoEntradaMercadoria = recebeProduto;

            string recebeFornecedor = DropFornecedor;
            Fornecedor fornecedor = db.Fornecedores.FirstOrDefault(x => x.NomeEmpresa == recebeFornecedor);
            entradaMercadoria.FornecedorDentroDeEntradaMercadoria = fornecedor;
            //entradaMercadoria.FornecedorEntradaMercadoria = recebeFornecedor;

            produto.QuantidadeAtual = entradaMercadoria.QuantidadeInserida;

            //entradaMercadoria.DataDaEntrada = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.EntradaMercadorias.Add(entradaMercadoria);
                db.SaveChanges();
                return RedirectToAction("Create", "EntradaMercadoria").Mensagem("Dados Inseridos com Sucesso !!", "Atenção");
            }

            return View(entradaMercadoria);
        }

        // GET: EntradaMercadoria/Edit/5
        public ActionResult Edit(int? id)
        {

            List<Produto> ListaDeProduto = db.Produtos.ToList();
            List<SelectListItem> ListaGenericaDeProduto = new List<SelectListItem>();

            foreach (Produto item in ListaDeProduto)
            {
                ListaGenericaDeProduto.Add(new SelectListItem { Text = item.NomeProduto.ToString(), Value = item.NomeProduto.ToString() });
            }

            ViewBag.Produto = ListaGenericaDeProduto;


            List<Fornecedor> ListaDeFornecedor = db.Fornecedores.ToList();
            List<SelectListItem> ListaGenericaDeFornecedor = new List<SelectListItem>();

            foreach (Fornecedor item in ListaDeFornecedor)
            {
                ListaGenericaDeFornecedor.Add(new SelectListItem { Text = item.NomeEmpresa.ToString(), Value = item.NomeEmpresa.ToString() });
            }

            ViewBag.Fornecedor = ListaGenericaDeFornecedor;
            //EntradaMercadoria entrada = new EntradaMercadoria();
            //ViewBag.Fornecedor = (from c in db.EntradaMercadorias select c.FornecedorEntradaMercadoria);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }            
            EntradaMercadoria entradaMercadoria = db.EntradaMercadorias.Find(id);            
            if (entradaMercadoria == null)
            {
                return HttpNotFound();
            }
            return View(entradaMercadoria);
        }

        // POST: EntradaMercadoria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EntradaMercadoriaId,ProdutoEntradaMercadoria,FornecedorEntradaMercadoria,QuantidadeInserida,ValorTotalDaNF,DataDaEntrada,Observacao")] EntradaMercadoria entradaMercadoria, string DropProduto, string DropFornecedor)
        {
            List<Produto> ListaDeProduto = db.Produtos.ToList();
            List<SelectListItem> ListaGenericaDeProduto = new List<SelectListItem>();

            foreach (Produto item in ListaDeProduto)
            {

                ListaGenericaDeProduto.Add(new SelectListItem { Text = item.NomeProduto.ToString(), Value = item.NomeProduto.ToString() });
            }
            ViewBag.Produto = ListaGenericaDeProduto;


            List<Fornecedor> ListaDeFornecedor = db.Fornecedores.ToList();
            List<SelectListItem> ListaGenericaDeFornecedor = new List<SelectListItem>();

            foreach (Fornecedor item in ListaDeFornecedor)
            {
                ListaGenericaDeFornecedor.Add(new SelectListItem { Text = item.NomeEmpresa.ToString(), Value = item.NomeEmpresa.ToString() });
            }
            ViewBag.Fornecedor = ListaGenericaDeFornecedor;

            string recebeProduto = DropProduto;
            Produto produto = db.Produtos.FirstOrDefault(x => x.NomeProduto == recebeProduto);
            entradaMercadoria.ProdutoDentroDeEntradaMercadoria = produto;
          //  entradaMercadoria.ProdutoEntradaMercadoria = recebeProduto;

            string recebeFornecedor = DropFornecedor;
            Fornecedor fornecedor = db.Fornecedores.FirstOrDefault(x => x.NomeEmpresa == recebeFornecedor);
            entradaMercadoria.FornecedorDentroDeEntradaMercadoria = fornecedor;
          //  entradaMercadoria.FornecedorEntradaMercadoria = recebeFornecedor;


            if (ModelState.IsValid)
            {
                db.Entry(entradaMercadoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListarDados", "EntradaMercadoria").Mensagem("Dados Alterados com Sucesso !!", "Atenção");
            }



            return View(entradaMercadoria);
        }



        // EntradaMercadoria/ EXCLUIR OS DADOS 
        public ActionResult DeletarRegistro(int id)
        {
            EntradaMercadoria entradaMercadoria = db.EntradaMercadorias.Find(id);
            db.EntradaMercadorias.Remove(entradaMercadoria);
            db.SaveChanges();
            return RedirectToAction("ListarDados", "EntradaMercadoria").Mensagem("Dados Excluidos com Sucesso !!", "Atenção");
        }






        //==========================================================================================================================================================================//








        // GET: EntradaMercadoria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntradaMercadoria entradaMercadoria = db.EntradaMercadorias.Find(id);
            if (entradaMercadoria == null)
            {
                return HttpNotFound();
            }
            return View(entradaMercadoria);
        }

        // POST: EntradaMercadoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EntradaMercadoria entradaMercadoria = db.EntradaMercadorias.Find(id);
            db.EntradaMercadorias.Remove(entradaMercadoria);
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
