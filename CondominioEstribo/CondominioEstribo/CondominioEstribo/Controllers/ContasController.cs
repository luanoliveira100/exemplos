using CondominioEstribo.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;




namespace CondominioEstribo.Models
{
    public class ContasController : Controller
    {
        private Context db = new Context();

        //teste para o autocomplete
        public ActionResult teste()
        {
            List<Proprietario> prop = new List<Proprietario>();
            prop = db.Proprietarios.ToList();            
            return View(prop);
        }
        //teste para o autocomplete
        [HttpPost]
        public ActionResult teste(string searchTerm)
        {
            List<Proprietario> proprietarios;
            if (string.IsNullOrEmpty(searchTerm))
            {
                proprietarios = db.Proprietarios.ToList();
            }
            else
            {
                proprietarios = db.Proprietarios.Where(p => p.Nome.StartsWith(searchTerm)).ToList();
            }
            
            return View(proprietarios);

        }

        public JsonResult GetProprietario(string term)
        {
            List<string> proprietarios;
            proprietarios = db.Proprietarios.Where(p => p.Nome.StartsWith(term)).
                 Select(e => e.Nome).Distinct().ToList();
            
            return Json(proprietarios, JsonRequestBehavior.AllowGet);
        }



        public ActionResult relatorioDetalhado()
        {

            return View();
        }
        [HttpPost]
        public ActionResult relatorioDetalhado(string mes)
        {
            int ano = Convert.ToInt16(mes.Substring(0, 4));
            int mes1 = Convert.ToInt16(mes.Substring(5, 2));
            string data = "0"+ mes1 + "/" + ano;

            //referente a lista de contas
            List<Contas> listarContas = new List<Contas>();
            listarContas = buscarContasPormes(mes1, ano);
            ViewBag.listarContas = listarContas;
            double subtotalContas = 0;
            foreach (var con in listarContas)
            {
                subtotalContas += con.valor;
            }
            ViewBag.MensalidadesPagas = subtotalContas;
            ProprietarioController proprietarioControler = new ProprietarioController();
            List<Proprietario> listaProprietarios = new List<Proprietario>();
            listaProprietarios = proprietarioControler.listarTodosOsProprietarios();
            ViewBag.ListaProprietario = listaProprietarios;
                        
            //referente a lista de mercadoria
            subtotalContas = 0;
            List<EntradaMercadoria> Mercadorias = new List<EntradaMercadoria>();
            EntradaMercadoriaController MercadoriaControler = new EntradaMercadoriaController();
            Mercadorias = MercadoriaControler.BuscarMercadoriaMesAno(mes1, ano);
            ViewBag.Mercadorias = Mercadorias;
            foreach (var con in Mercadorias)
            {
                subtotalContas += con.ValorTotalDaNF;
            }
            ViewBag.totalMercadorias = subtotalContas;
            ViewBag.Mes = data;


            //referente a lista de obras
            subtotalContas = 0;
            List<Obra> listaDeObras = new List<Obra>();
            ObraController obraController = new ObraController();
            listaDeObras = obraController.BuscarObraMesAno(mes1, ano);
            ViewBag.listaDeObras = listaDeObras;
            foreach (var con in listaDeObras)
            {
                
                subtotalContas += Convert.ToDouble(con.ValorObra) ;
            }
            ViewBag.totalObras = subtotalContas;

            return View("relatorioDetalhadoRetorno");
        }


        public ActionResult RelatorioFinanceiro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RelatorioFinanceiro(string mes)
        {
            int ano = Convert.ToInt16(mes.Substring(0, 4));
            int mes1 = Convert.ToInt16(mes.Substring(5, 2));
            string data = mes1 + "/" + ano;
            List<Contas> listarContas = new List<Contas>();
            listarContas = buscarContasPormes(mes1, ano);
            double subtotalContas = 0;
            foreach (var con in listarContas)
            {
                subtotalContas += con.valor;
            }
            ViewBag.MensalidadesPagas = subtotalContas;
            List<ContasPagar> contasPagas = new List<ContasPagar>();
            ContasPagarsController contasPagasController = new ContasPagarsController();
            contasPagas = contasPagasController.BuscarContasPagasPorAnoMes(mes1, ano);
            double subtotalContas1 = 0;
            foreach (var con in contasPagas)
            {
                subtotalContas1 += con.valor;
            }
            ViewBag.ContasPagas = subtotalContas1;
            double subtotalContas2 = 0;
            List<EntradaMercadoria> Mercadorias = new List<EntradaMercadoria>();
            EntradaMercadoriaController MercadoriaControler = new EntradaMercadoriaController();
            Mercadorias = MercadoriaControler.BuscarMercadoriaMesAno(mes1, ano);
            foreach (var con in Mercadorias)
            {
                subtotalContas2 += con.ValorTotalDaNF;
            }
            ViewBag.Mercadorias = subtotalContas2;
            ViewBag.Mes = data;
            return View("RelatoriofinanceiroRetorno");
        }


        public List<Proprietario> listaDeInadimplentes(List<Proprietario> p)
        {
            List<Proprietario> listaInadimplentes = new List<Proprietario>();
            List<Proprietario> listadetodosproprietarios = new List<Proprietario>();
            listadetodosproprietarios = db.Proprietarios.ToList();
            List<string> nomes1 = new List<string>();
            List<string> nomes2 = new List<string>();


            foreach (Proprietario p1 in p)
            {
                nomes1.Add(p1.Nome);
            }


            foreach (Proprietario p1 in listadetodosproprietarios)
            {
                nomes2.Add(p1.Nome);
            }


            var lista2 = nomes2.Except(nomes1).ToList();
            List<string> diferencaProprietarios = new List<string>();
            diferencaProprietarios = lista2;
            foreach (string diferenca in diferencaProprietarios)
            {
                foreach (Proprietario p1 in listadetodosproprietarios)
                {
                    if (diferenca == p1.Nome)
                    {
                        listaInadimplentes.Add(p1);
                    }
                }
            }



            return listaInadimplentes;
        }

        public ActionResult buscar()
        {
            return View();

        }
        public ActionResult buscarPorProprietario()
        {
           // List<Proprietario> ListaProprietarios = db.Proprietarios.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult buscarPorProprietario(string searchTerm, string ano)
        {

            Proprietario proprietario = new Proprietario();
            proprietario = db.Proprietarios.FirstOrDefault(x => x.Nome.Equals(searchTerm));
            List<Contas> listarContas = new List<Contas>();
            listarContas = buscarContasPorProprietario(proprietario, ano);
            double subtotal = 0;
            foreach (var con in listarContas)
            {
                subtotal += con.valor;
            }

            List<Proprietario> listaProprietarios = new List<Proprietario>();

            ProprietarioController proprietarioControler = new ProprietarioController();
            listaProprietarios = proprietarioControler.listarTodosOsProprietarios();

            ViewBag.Subtotal = subtotal;
            ViewBag.ListaProprietario = listaProprietarios;
            ViewBag.listarContas = listarContas;
            return View("ListaPorData");
        }

        public List<Contas> buscarContasPorProprietario(Proprietario proprietario, string ano)
        {
            int ano1 = Convert.ToInt16(ano.Substring(0, 4));
            List<Contas> contasMes = new List<Contas>();
            var ListaDeContas = from contas in db.Contas where contas.proprietarioid == (proprietario.UsuarioId) && contas.datapagamento.Year == (ano1) select contas;

            return ListaDeContas.ToList();
        }





        [HttpPost]
        public ActionResult buscar(string mes)
        {
            int ano = Convert.ToInt16(mes.Substring(0, 4));
            int mes1 = Convert.ToInt16(mes.Substring(5, 2));
            List<Contas> listarContas = new List<Contas>();
            listarContas = buscarContasPormes(mes1, ano);
            double subtotal = 0;
            foreach (var con in listarContas)
            {
                subtotal += con.valor;
            }

            List<Proprietario> listaProprietarios = new List<Proprietario>();

            ProprietarioController proprietarioControler = new ProprietarioController();
            ViewBag.data = Convert.ToDateTime(mes);
            listaProprietarios = proprietarioControler.listarTodosOsProprietarios();

            ViewBag.Subtotoal = subtotal;
            ViewBag.ListaProprietario = listaProprietarios;
            ViewBag.listarContas = listarContas;
            return View("ListaPorData");
        }



        public List<Contas> buscarContasPormes(int mes, int ano)
        {
            List<Contas> contasMes = new List<Contas>();
            var ListaDeContas = from contas in db.Contas where contas.datapagamento.Month == (mes) && contas.datapagamento.Year == (ano) select contas;

            return ListaDeContas.ToList();
        }




        public List<Contas> buscarContasPordia(DateTime dataDaPesquisa)
        {
            string data = dataDaPesquisa.ToString();
            int mes = Convert.ToInt16(data.Substring(3, 2));
            int ano = Convert.ToInt16(data.Substring(6, 4));
            int dia = Convert.ToInt16(data.Substring(0, 2));
            List<Contas> contasMes = new List<Contas>();
            var ListaDeContas = from contas in db.Contas where contas.datapagamento.Month == (mes) && contas.datapagamento.Year == (ano) && contas.datapagamento.Day == (dia) select contas;
            //ListaDeContas.OrderByDescending(x => x.);


            return ListaDeContas.ToList();
        }




        public Contas buscarContas(Proprietario p, Contas c)
        {
            Contas conta = new Contas();
            conta = db.Contas.FirstOrDefault(x => x.datapagamento.Equals(c.datapagamento) && x.datatitulo.Equals(c.datatitulo) && x.proprietarioid.Equals(c.proprietarioid));

            if (conta == null)
            {
                return null;
            }
            else
            {
                return conta;
            }
        }





        [HttpPost]
        public ActionResult Import(HttpPostedFileBase excelfile, string mes, string ano)
        {
            double subtotal = 0.0;
            string mes1 = mes;
            MesAnoPagosController MesAnoPago = new MesAnoPagosController();
            ContasController contaController = new ContasController();
            InadimplentesController inadimplentesController = new InadimplentesController();
            List<Inadimplentes> listaInadimplentes = new List<Inadimplentes>();
            List<Contas> contasDoMes = new List<Contas>();
            ProprietarioController proprietarioControler = new ProprietarioController();
            Proprietario prop = new Proprietario();
            List<Proprietario> listaproprietario = new List<Proprietario>();


            int contador = 0;
            if (MesAnoPago.verificaMes(mes1))
            {


                if (excelfile == null || excelfile.ContentLength == 0)
                {

                    ViewBag.Error = "Por favor insira um arquivo .pdf<br>";
                    return View("Index");
                }
                else
                {
                    if (excelfile.FileName.EndsWith("xls") || (excelfile.FileName.EndsWith("xlsx")))
                    {
                        string path = Server.MapPath("~/Content/" + excelfile.FileName);
                        if (System.IO.File.Exists(path))
                            System.IO.File.Delete(path);
                        excelfile.SaveAs(path);
                        Excel.Application application = new Excel.Application();
                        Excel.Workbook workbook = application.Workbooks.Open(path);
                        Excel.Worksheet worksheet = workbook.ActiveSheet;
                        Excel.Range range = worksheet.UsedRange;
                        List<Contas> listaContas = new List<Contas>();

                        for (int row = 1; row < range.Rows.Count + 1; row++)
                        {
                            Inadimplentes inadimplente = new Inadimplentes();
                            Contas c = new Contas();
                            string nome = ((Excel.Range)range.Cells[row, 1]).Text;


                            if (nome != null)
                            {
                                Proprietario p = new Proprietario();
                                p = (Proprietario)proprietarioControler.BuscarPorNome(nome);
                                if (p != null)
                                {
                                    c.proprietarioid = p.UsuarioId;
                                    c.datatitulo = Convert.ToDateTime(((Excel.Range)range.Cells[row, 2]).Text);
                                    c.datapagamento = Convert.ToDateTime(((Excel.Range)range.Cells[row, 3]).Text);
                                    c.valor = Convert.ToDouble(((Excel.Range)range.Cells[row, 4]).Text);
                                    c.fonte = ((Excel.Range)range.Cells[row, 5]).Text;
                                    c.codigo = ((Excel.Range)range.Cells[row, 6]).Text;
                                    c.lote = ((Excel.Range)range.Cells[row, 7]).Text;
                                    c.pago = ((Excel.Range)range.Cells[row, 8]).Text;
                                    // if (buscarContas(p, c) == null)
                                    // {




                                    if (ModelState.IsValid)
                                    {
                                        listaContas.Add(c);
                                        subtotal = subtotal + c.valor;
                                        listaproprietario.Add(p);
                                        inadimplentesController.deletarInadimplentes(c);
                                        db.Contas.Add(c);
                                        db.SaveChanges();
                                        contador++;
                                    }
                                    //}
                                }
                            }
                        }

                        List<Proprietario> listaProprietarios = new List<Proprietario>();

                        listaProprietarios = proprietarioControler.listarTodosOsProprietarios();

                        List<Proprietario> ListaDeInadimplentes = new List<Proprietario>();

                        // List<Proprietario> ListaDeProprietario = new List<Proprietario>();

                        listaProprietarios = listaDeInadimplentes(listaproprietario);

                        ListaDeInadimplentes = inadimplentesController.salvar(listaProprietarios, mes1);


                        ViewBag.subtotal = subtotal;
                        ViewBag.ListaProprietario = listaproprietario;
                        ViewBag.contador = contador;
                        ViewBag.listarContas = listaContas;
                        ViewBag.listaInadimplentes = ListaDeInadimplentes;
                        MesAnoPago.salvar(mes1);
                        return View("Success");
                    }
                    else
                    {
                        ViewBag.Error = "Inserir aquivos apenas EXCEL  <br>";
                        return View("Index");
                    }

                }
            }
            ViewBag.Error = "Mês já inserido  <br>";
            return View("Index");
        }



        // GET: Contas
        public ActionResult Index()
        {
            return View(db.Contas.ToList());
        }

        // GET: Contas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contas contas = db.Contas.Find(id);
            if (contas == null)
            {
                return HttpNotFound();
            }
            return View(contas);
        }

        // GET: Contas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContasId,proprietarioid,datatitulo,datapagamento,valor,fonte,codigo,lote,pago")] Contas contas)
        {
            if (ModelState.IsValid)
            {
                db.Contas.Add(contas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contas);
        }

        // GET: Contas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contas contas = db.Contas.Find(id);
            if (contas == null)
            {
                return HttpNotFound();
            }
            return View(contas);
        }

        // POST: Contas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContasId,proprietarioid,datatitulo,datapagamento,valor,fonte,codigo,lote,pago")] Contas contas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contas);
        }

        // GET: Contas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contas contas = db.Contas.Find(id);
            if (contas == null)
            {
                return HttpNotFound();
            }
            return View(contas);
        }

        // POST: Contas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contas contas = db.Contas.Find(id);
            db.Contas.Remove(contas);
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
