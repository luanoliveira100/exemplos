using CondominioEstribo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CondominioEstribo.Controllers
{
    public class NotaFiscalItensController : Controller
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
        public ActionResult ListarItens(int id)
        {

            //daqui 
            List<Produto> ListaDeProduto = db.Produtos.ToList();
            List<SelectListItem> ListaGenericaDeProduto = new List<SelectListItem>();

            foreach (Produto item in ListaDeProduto)
            {

                ListaGenericaDeProduto.Add(new SelectListItem { Text = item.NomeProduto.ToString(), Value = item.NomeProduto.ToString() });
            }
            ViewBag.Produto = ListaGenericaDeProduto;
            //até aqui ...



            List<NotaFiscalItens> lista = new List<NotaFiscalItens>();
            lista = db.NotaFiscalItens.Where(x => x.notaFiscal.Id .Equals(id)).ToList();            

            ViewBag.Pedido = id;
            return View(lista);
        }
        [HttpPost]
        public ActionResult Excluir(int id)
        {
                   
            var Result = false;           
            NotaFiscalItens item = db.NotaFiscalItens.Find(id);

            //retirar quantidade de produto no estoque;
            ProdutoController produtoController = new ProdutoController();
            if (produtoController.RetirarQuantidadeProduto(item)) ;
            {
                if (item != null)
                {
                    db.NotaFiscalItens.Remove(item);
                    db.SaveChanges();
                    Result = true;
                }
                return Json(new { Resultado = Result }, JsonRequestBehavior.AllowGet);
            }
           
           
        }

        
       
        public ActionResult SalvarItens(int quantidade
            , string produto
            , double valorunitario
            , int idPedido)
            
        {
            ProdutoController produtoController = new ProdutoController();
            NotaFiscalItens n = new NotaFiscalItens();
            n.Produto = db.Produtos.FirstOrDefault(x => x.NomeProduto.StartsWith(produto));

            //inserir quantidade de produto no estoque;
            produtoController.InserirQuantidadeProduto(n.Produto, quantidade);

            if (n.Produto != null )
            {                
                n.ValorUnitartio = valorunitario;
                n.Quantidade = quantidade;
                n.notaFiscal = db.NotaFiscal.Find(idPedido);
                n.Produto = db.Produtos.FirstOrDefault(x => x.NomeProduto.StartsWith(produto));
                db.NotaFiscalItens.Add(n);
                db.SaveChanges();

                return Json(new { Resultado = n.Id }, JsonRequestBehavior.AllowGet);
            }           
            return Json(new { Resultado = 0 }, JsonRequestBehavior.AllowGet).Mensagem("Produto não Encontrado");

            // }
        }
    }
}