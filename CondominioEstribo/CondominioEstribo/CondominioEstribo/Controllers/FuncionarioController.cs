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
    public class FuncionarioController : Controller
    {


        private Context db = new Context();


        // LISTA TODOS OS REGISTROS QUE ESTEJAM ATIVOS //
        public ActionResult Index(string txtNomeFuncionario)
        {
            var q = db.Funcionarios.AsQueryable();
            //if (!string.IsNullOrEmpty(txtNomeFuncionario))
            q = q.Where(x => x.Nome.Contains(txtNomeFuncionario) && x.FlagStatus == "Ativo");
            q = q.OrderBy(x => x.Nome);


            return View(q.ToList());

            // return View(db.Funcionarios.ToList());

        }




        // GET: Funcionario
        public ActionResult ListarTodosFuncionarios(string txtNomeFuncionario)
        {
            var q = db.Funcionarios.AsQueryable();

            return View(q.ToList().Where(X => X.FlagStatus.Equals("Inativo")).OrderBy(x => x.Nome));
        }



        // GET: Funcionario/ DETALHES
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionario funcionario = db.Funcionarios.Find(id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View(funcionario);
        }





        // GET: Funcionario/Create / CRIAR
        public ActionResult Create()
        {

            List<Cargo> ListaDeCargo = db.Cargos.ToList();
            List<SelectListItem> ListaDeCargoDeFuncionario = new List<SelectListItem>();
            foreach (Cargo item in ListaDeCargo)
            {
                ListaDeCargoDeFuncionario.Add(new SelectListItem { Text = item.NomeCargo.ToString(), Value = item.NomeCargo.ToString() });
            }

            ViewBag.CargoFuncionario = ListaDeCargoDeFuncionario;         
            
            return View();
        }






        // POST: Funcionario/Create / CRIAR
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UsuarioId,LoginUsuario,SenhaUsuario,StatusUsuario,PerfilUsuario,Nome,CPF,RG,Logradouro,Numero,CEP,TelefoneCelular,Email,Cargo,NumeroCTPS,SerieCTPS,SalarioBase")] Funcionario funcionario, string DropCargo, string DropStatus)
        {            

            if (DropStatus == "1")
            {
                funcionario.FlagStatus = "Ativo";
            }
            else
            {
                if (DropStatus == "2")
                {
                    funcionario.FlagStatus = "Inativo";
                }

            }
                        
            
            if (ModelState.IsValid)
            {
                
                if (ValidarCPF.ValidaCPF(funcionario.CPF) == false)
                {
                    return View().Mensagem("CPF Informado é Invalido. Favor Verificar.", "Atenção");
                }
                else
                {
                    db.Usuarios.Add(funcionario);
                    funcionario.SenhaUsuario = "123456";
                    funcionario.PerfilUsuario = "Funcionário";
                    funcionario.LoginUsuario = funcionario.CPF;
                    funcionario.StatusUsuario = true;
                    Cargo cargo = db.Cargos.FirstOrDefault(x => x.NomeCargo == DropCargo);
                    funcionario.CargoDentroDeFuncionario = cargo;
                    funcionario.Cargo = DropCargo;
                    
                    db.SaveChanges();
                    return RedirectToAction("Create", "Funcionario").Mensagem("Dados Inseridos com Sucesso !!", "Atenção");
                }

            }


            return View(funcionario);
        }


        

        // GET: Funcionario/Edit/5 / ALTERAR
        public ActionResult Edit(int? id)
        {

            List<Cargo> ListaDeCargo = db.Cargos.ToList();
            List<SelectListItem> ListaDeCargoDeFuncionario = new List<SelectListItem>();
            foreach (Cargo item in ListaDeCargo)
            {
                ListaDeCargoDeFuncionario.Add(new SelectListItem { Text = item.NomeCargo.ToString(), Value = item.NomeCargo.ToString() });
            }

            ViewBag.CargoFuncionario = ListaDeCargoDeFuncionario;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionario funcionario = db.Funcionarios.Find(id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }

            return View(funcionario);


        }
        



        // POST: Funcionario/Edit/5 / ALTERAR
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UsuarioId,LoginUsuario,SenhaUsuario,StatusUsuario,PerfilUsuario,Nome,CPF,RG,Logradouro,Numero,CEP,TelefoneCelular,Email,Cargo,NumeroCTPS,SerieCTPS,SalarioBase")] Funcionario funcionario, string DropCargo, string DropStatus)
        {
            string recebeStatus = DropStatus;

            if (recebeStatus == "1")
            {
                funcionario.FlagStatus = "Ativo";
            }
            else
            {
                if (recebeStatus == "2")
                {
                    funcionario.FlagStatus = "Inativo";
                }

            }


            string cargoFuncionario = DropCargo;
            Cargo cargo = db.Cargos.FirstOrDefault(x => x.NomeCargo == cargoFuncionario);
            funcionario.CargoDentroDeFuncionario = cargo;
            funcionario.Cargo = cargoFuncionario;

            if (ModelState.IsValid)
            {
                db.Entry(funcionario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Funcionario").Mensagem("Dados Alterados com Sucesso !!", "Atenção");
            }

            List<Cargo> ListaDeCargo = db.Cargos.ToList();

            List<SelectListItem> ListaDeCargoDeFuncionario = new List<SelectListItem>();

            foreach (Cargo item in ListaDeCargo)
            {
                ListaDeCargoDeFuncionario.Add(new SelectListItem { Text = item.NomeCargo.ToString(), Value = item.NomeCargo.ToString() });
            }

            ViewBag.CargoFuncionario = ListaDeCargoDeFuncionario;

            return View(funcionario);
        }





        // POST: Fornecedor / EXCLUIR DADOS DO CADASTRO
        public ActionResult DeletarFuncionario(int id)
        {

            Funcionario funcionario = db.Funcionarios.Find(id);


            try
            {
                if (funcionario.FlagStatus == "Ativo")
                {
                    return RedirectToAction("ListarTodosFuncionarios", "Funcionario").Mensagem("Funcionário com Status Ativo, não pode ser Excluido !!", "Atenção");
                }

                db.Funcionarios.Remove(funcionario);
                db.SaveChanges();
                return RedirectToAction("ListarTodosFuncionarios", "Funcionario").Mensagem("Dados Excluidos com Sucesso !!", "Atenção");
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Home").Mensagem("Não Foi Possivel Excluir este Funcionário. Existem Registros Atrelados ao Mesmo. Contate o Administrador do Sistema.", "Atenção");
            }
        }





        // METODO UTILIZADO PARA INATIVAR UM REGISTRO (FUNCIONÁRIO)
        public ActionResult InativarFuncionario(int id)
        {
            Funcionario funcionario = db.Funcionarios.Find(id);

            if (ModelState.IsValid)
            {
                funcionario.FlagStatus = "Inativo";
                db.Entry(funcionario).State = EntityState.Modified;
                db.SaveChanges();

            }

            return RedirectToAction("Index", "Funcionario").Mensagem("Operação Realizada com Sucesso !!", "Atenção");
        }




        





        //===============================================================================================================================================//


        /*
            METODOS QUE NÃO SÃO UTILIZADOS, PELOS MENOS POR ENQUANTO. // 
        */



        // GET: Funcionario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionario funcionario = db.Funcionarios.Find(id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View(funcionario);
        }

        // POST: Funcionario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Funcionario funcionario = db.Funcionarios.Find(id);
            db.Usuarios.Remove(funcionario);
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
