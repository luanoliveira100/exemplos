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
    public class FornecedorController : Controller
    {


        private Context db = new Context();


        public JsonResult GetFornecedor(string term)
        {
            // retorna placas
            List<string> fornecedores;
            fornecedores = db.Fornecedores.Where(p => p.NomeEmpresa.StartsWith(term)).
                 Select(e => e.NomeEmpresa).Distinct().ToList();

            return Json(fornecedores, JsonRequestBehavior.AllowGet);
        }


        // LISTA TODOS OS REGISTROS QUE ESTEJAM ATIVOS //
        public ActionResult Index(string fornecedor)
        {
            var q = db.Fornecedores.AsQueryable();

            //if (!string.IsNullOrEmpty(txtFornecedor))

                q = q.Where(x => x.NomeEmpresa.Contains(fornecedor) && x.FlagStatus == "Ativo");
                q = q.OrderBy(x => x.NomeEmpresa);                         
                                  

            return View(q.ToList());          

        }


        // LISTA TODOS OS REGISTROS E ORDENA POR ORDEM ALFABETICA //
        public ActionResult ListarTodosFornecedores()
        {
            var q = db.Fornecedores.AsQueryable();
                      
            return View(q.ToList().Where(x => x.FlagStatus.Equals("Inativo")));
           
        }




        // GET: Fornecedor/ LISTA OS DETALHES DO REGISTRO 
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornecedor fornecedor = db.Fornecedores.Find(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }






        // GET: Fornecedor/ GRAVAR DADOS DO CADASTRO 
        public ActionResult Create()
        {

            return View();

        }




        // POST: Fornecedor/ GRAVAR DADOS DO CADASTRO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FornecedorId,NomeEmpresa,CNPJ_CPF,TelefoneFixo,TelefoneCelular,Endereco,Numero,CEP,Bairro,Cidade,Estado,EmailDoContato,NomeDoContato,FlagStatus,Observacao")] Fornecedor fornecedor, string DropStatus, string DropEstados)
        {
            string setarDropStatus = "1";
            DropStatus = setarDropStatus;
            string recebeStatus = DropStatus;
            string recebeEstados = DropEstados;            

            switch (recebeEstados)
            {
                case "1":
                    fornecedor.Estado = "AC";
                    break;
                case "2":
                    fornecedor.Estado = "AL";
                    break;
                case "3":
                    fornecedor.Estado = "AP";
                    break;
                case "4":
                    fornecedor.Estado = "AM";
                    break;
                case "5":
                    fornecedor.Estado = "BA";
                    break;
                case "6":
                    fornecedor.Estado = "CE";
                    break;
                case "7":
                    fornecedor.Estado = "DF";
                    break;
                case "8":
                    fornecedor.Estado = "ES";
                    break;
                case "9":
                    fornecedor.Estado = "GO";
                    break;
                case "10":
                    fornecedor.Estado = "MA";
                    break;
                case "11":
                    fornecedor.Estado = "MT";
                    break;
                case "12":
                    fornecedor.Estado = "MS";
                    break;
                case "13":
                    fornecedor.Estado = "MG";
                    break;
                case "14":
                    fornecedor.Estado = "PA";
                    break;
                case "15":
                    fornecedor.Estado = "PB";
                    break;
                case "16":
                    fornecedor.Estado = "PR";
                    break;
                case "17":
                    fornecedor.Estado = "PE";
                    break;
                case "18":
                    fornecedor.Estado = "PI";
                    break;
                case "19":
                    fornecedor.Estado = "RJ";
                    break;
                case "20":
                    fornecedor.Estado = "RN";
                    break;
                case "21":
                    fornecedor.Estado = "RS";
                    break;
                case "22":
                    fornecedor.Estado = "RO";
                    break;
                case "23":
                    fornecedor.Estado = "RR";
                    break;
                case "24":
                    fornecedor.Estado = "SC";
                    break;
                case "25":
                    fornecedor.Estado = "SP";
                    break;
                case "26":
                    fornecedor.Estado = "SE";
                    break;
                case "27":
                    fornecedor.Estado = "TO";
                    break;

                default:
                    fornecedor.Estado = "SP";
                    break;
            }
            

            if (recebeStatus == "1")
            {
                fornecedor.FlagStatus = "Ativo";
            }
            else
            {
                if (recebeStatus == "2")
                {
                    fornecedor.FlagStatus = "Inativo";
                }

            }


            if (fornecedor.TelefoneCelular == null)
            {
                fornecedor.TelefoneCelular = "(00) 0000-0000";
            }
            if (fornecedor.EmailDoContato == null)
            {
                fornecedor.EmailDoContato = "Email não Informado.";
            }
            if (fornecedor.NomeDoContato == null)
            {
                fornecedor.NomeDoContato = "Nome não Informado.";
            }
            
            if (ModelState.IsValid)
            {
                db.Fornecedores.Add(fornecedor);
                db.SaveChanges();
                return RedirectToAction("Create", "Fornecedor").Mensagem("Dados Inseridos com Sucesso !!", "Atencão");
            }

            return View(fornecedor);
        }




        // GET: Fornecedor/ ALTERAR DADOS DO CADASTRO
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornecedor fornecedor = db.Fornecedores.Find(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }




        // POST: Fornecedor/ ALTERAR DADOS DO CADASTRO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FornecedorId,NomeEmpresa,CNPJ_CPF,TelefoneFixo,TelefoneCelular,Endereco,Numero,CEP,Bairro,Cidade,Estado,EmailDoContato,NomeDoContato,FlagStatus,Observacao")] Fornecedor fornecedor, string DropStatus, string DropEstados)
        {

            string recebeStatus = DropStatus;
            

            if (recebeStatus == "1")
            {
                fornecedor.FlagStatus = "Ativo";
            }
            else
            {
                if (recebeStatus == "2")
                {
                    fornecedor.FlagStatus = "Inativo";
                }

            }
            
            if (ModelState.IsValid)
            {
                db.Entry(fornecedor).State = EntityState.Modified;                

                db.SaveChanges();

                return RedirectToAction("Index", "Fornecedor").Mensagem("Dados Alterados com Sucesso !!", "Atenção");
            }
            return View(fornecedor);
        }




        // POST: Fornecedor / EXCLUIR DADOS DO CADASTRO
        public ActionResult DeletarFornecedor(int id)
        {            
            Fornecedor fornecedor = db.Fornecedores.Find(id);


            try
            {
                if (fornecedor.FlagStatus == "Ativo")
                {
                    return RedirectToAction("ListarTodosFornecedores", "Fornecedor").Mensagem("Fornecedor com Status Ativo, não pode ser Excluido !!", "Atenção");
                }

                db.Fornecedores.Remove(fornecedor);
                db.SaveChanges();
                return RedirectToAction("ListarTodosFornecedores", "Fornecedor").Mensagem("Dados Excluidos com Sucesso !!", "Atenção");
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Home").Mensagem("Não Foi Possivel Excluir este Fornecedor. Existem Registros Atrelados ao Mesmo. Contate o Administrador do Sistema.", "Atenção");
            }     
            
        }




        // POST: Fornecedor /  METODO UTILIZADO PARA INATIVAR UM REGISTRO (FORNECEDOR)
        public ActionResult InativarFornecedor(int id)
        {
            Fornecedor fornecedor = db.Fornecedores.Find(id);

            if (ModelState.IsValid)
            {
                fornecedor.FlagStatus = "Inativo";
                db.Entry(fornecedor).State = EntityState.Modified;
                db.SaveChanges();

            }

            return RedirectToAction("Index", "Fornecedor").Mensagem("Operação Realizada com Sucesso !!", "Atenção");
        }






//===========================================================================================================================================================//

        /*
            NÃO USADO POR ENQUANTO 
         */
                
        // GET: Fornecedor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornecedor fornecedor = db.Fornecedores.Find(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }

        // POST: Fornecedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fornecedor fornecedor = db.Fornecedores.Find(id);
            db.Fornecedores.Remove(fornecedor);
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
