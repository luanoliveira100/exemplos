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
    public class SaidaMercadoriaController : Controller
    {
        private Context db = new Context();


        // LISTA POR DATA 
        public ActionResult Index(string txtData)
        {
            try
            {
                DateTime recebeData = Convert.ToDateTime(txtData);
                var q = db.SaidaMercadorias.AsQueryable();
                q = q.Where(x => x.DataRetirada.Equals(recebeData));
                q = q.OrderBy(x => x.ProdutoSaidaMercadoria);

                return View(q.ToList());
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "SaidaMercadoria").Mensagem("Favor Informar Uma Data !!", "Atenção");
            }
                        
        }





        // GET: SaidaMercadoria/ DETALHES
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






        // GET: SaidaMercadoria/ GRAVA OS DADOS GET
        public ActionResult Create()
        {
            List<Produto> ListaDeProduto = db.Produtos.ToList();
            List<SelectListItem> ListaGenericaDeProduto = new List<SelectListItem>();

            foreach (Produto item in ListaDeProduto)
            {

                ListaGenericaDeProduto.Add(new SelectListItem { Text = item.NomeProduto.ToString(), Value = item.NomeProduto.ToString() });
            }
            ViewBag.Produto = ListaGenericaDeProduto;


            List<Funcionario> ListaDeFuncionario = db.Funcionarios.ToList();
            List<SelectListItem> ListaGenericaDeFuncionario = new List<SelectListItem>();

            foreach (Funcionario item in ListaDeFuncionario)
            {
                ListaGenericaDeFuncionario.Add(new SelectListItem { Text = item.Nome.ToString(), Value = item.Nome.ToString() });
            }
            ViewBag.Funcionario = ListaGenericaDeFuncionario;

            return View();
        }






        // POST: SaidaMercadoria/ GRAVA OS DADOS POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SaidaMercadoriaId,ProdutoSaidaMercadoria,QuantidadeRetirada,DataRetirada,FuncionarioSaidaMercadoria,Observacao")] SaidaMercadoria saidaMercadoria, string DropProduto, string DropFuncionario)
        {

            int FlagQuantidade = 0;

            DateTime recebeDataSaida = saidaMercadoria.DataRetirada.Date;

            string recebeProduto = DropProduto;
            Produto produto = db.Produtos.FirstOrDefault(x => x.NomeProduto == recebeProduto);
            saidaMercadoria.ProdutoDentroSaidaMercadoria = produto;
            saidaMercadoria.ProdutoSaidaMercadoria = recebeProduto;

            string recebeFuncionario = DropFuncionario;
            Funcionario funcionario = db.Funcionarios.FirstOrDefault(x => x.Nome == recebeFuncionario);
            saidaMercadoria.FuncionarioDentroSaidaMercadoria = funcionario;
            saidaMercadoria.FuncionarioSaidaMercadoria = recebeFuncionario;
            
            FlagQuantidade = (produto.QuantidadeAtual - saidaMercadoria.QuantidadeRetirada);           
           

            if (saidaMercadoria.QuantidadeRetirada > produto.QuantidadeAtual)
            {
                return RedirectToAction("Create", "SaidaMercadoria").Mensagem("Quantidade de Produto Insuficiente em Estoque. Favor Verificar !!", "Atenção");
            }

            produto.QuantidadeAtual = FlagQuantidade;

            if (recebeDataSaida < DateTime.Now.Date || recebeDataSaida > DateTime.Now.Date)
            {
                return RedirectToAction("Create", "SaidaMercadoria").Mensagem("Data da Retirada não Pode Ser Diferente da Data Atual. Favor Verificar !!", "Atenção");
            }

            if (produto.QuantidadeAtual < produto.QuantidadeMinima)
            {   
                        
                if (ModelState.IsValid)
                {
                    db.SaidaMercadorias.Add(saidaMercadoria);                    
                    db.SaveChanges();

                    return RedirectToAction("Create", "SaidaMercadoria").Mensagem(" Saldo em Estoque Ficou Abaixo da Quantidade Mínima. Favor Verificar !! ", " Dados Inseridos com Sucesso !!");
                }
            }

            return View(saidaMercadoria);
        }

        

        // GET: SaidaMercadoria/ ALTERAR OS DADOS GET
        public ActionResult Edit(int? id)
        {

            //List<Produto> ListaDeProduto = db.Produtos.ToList();
            //List<SelectListItem> ListaGenericaDeProduto = new List<SelectListItem>();

            //foreach (Produto item in ListaDeProduto)
            //{

            //    ListaGenericaDeProduto.Add(new SelectListItem { Text = item.NomeProduto.ToString(), Value = item.NomeProduto.ToString() });
            //}
            //ViewBag.Produto = ListaGenericaDeProduto;


            //List<Funcionario> ListaDeFuncionario = db.Funcionarios.ToList();
            //List<SelectListItem> ListaGenericaDeFuncionario = new List<SelectListItem>();

            //foreach (Funcionario item in ListaDeFuncionario)
            //{
            //    ListaGenericaDeFuncionario.Add(new SelectListItem { Text = item.Nome.ToString(), Value = item.Nome.ToString() });
            //}
            //ViewBag.Funcionario = ListaGenericaDeFuncionario;


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




        // POST: SaidaMercadoria/ ALTERAR OS DADOS POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SaidaMercadoriaId,ProdutoSaidaMercadoria,QuantidadeRetirada,DataRetirada,FuncionarioSaidaMercadoria,Observacao")] SaidaMercadoria saidaMercadoria, string DropProduto, string DropFuncionario)
        {

            //List<Produto> ListaDeProduto = db.Produtos.ToList();
            //List<SelectListItem> ListaGenericaDeProduto = new List<SelectListItem>();

            //foreach (Produto item in ListaDeProduto)
            //{

            //    ListaGenericaDeProduto.Add(new SelectListItem { Text = item.NomeProduto.ToString(), Value = item.NomeProduto.ToString() });
            //}
            //ViewBag.Produto = ListaGenericaDeProduto;


            //List<Funcionario> ListaDeFuncionario = db.Funcionarios.ToList();
            //List<SelectListItem> ListaGenericaDeFuncionario = new List<SelectListItem>();

            //foreach (Funcionario item in ListaDeFuncionario)
            //{
            //    ListaGenericaDeFuncionario.Add(new SelectListItem { Text = item.Nome.ToString(), Value = item.Nome.ToString() });
            //}
            //ViewBag.Funcionario = ListaGenericaDeFuncionario;


            //string recebeProduto = DropProduto;
            //Produto produto = db.Produtos.FirstOrDefault(x => x.NomeProduto == recebeProduto);
            //saidaMercadoria.ProdutoDentroSaidaMercadoria = produto;
            //saidaMercadoria.ProdutoSaidaMercadoria = recebeProduto;

            //string recebeFuncionario = DropFuncionario;
            //Funcionario funcionario = db.Funcionarios.FirstOrDefault(x => x.Nome == recebeFuncionario);
            //saidaMercadoria.FuncionarioDentroSaidaMercadoria = funcionario;
            //saidaMercadoria.FuncionarioSaidaMercadoria = recebeFuncionario;



            if (ModelState.IsValid)
            {
                db.Entry(saidaMercadoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "SaidaMercadoria").Mensagem("Dados Alterados com Sucesso !!", "Atenção");
            }

            return View(saidaMercadoria);
        }




        // SaidaMercadoria/ EXCLUIR OS DADOS POST
        public ActionResult DeletarRegistro(int id)
        {
            SaidaMercadoria saidaMercadoria = db.SaidaMercadorias.Find(id);
            db.SaidaMercadorias.Remove(saidaMercadoria);
            db.SaveChanges();
            return RedirectToAction("Index", "SaidaMercadoria").Mensagem("Dados Excluídos com Sucesso", "Atenção");
        }










//==================================================================================================================================================================//




        // GET: SaidaMercadoria/ EXCLUIR OS DADOS GET
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

        // POST: SaidaMercadoria/ EXCLUIR OS DADOS POST
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
