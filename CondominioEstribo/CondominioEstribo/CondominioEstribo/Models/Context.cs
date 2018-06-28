using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CondominioEstribo.Models
{
    public class Context : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Pessoa> Pessoas { get; set; }

        public DbSet<Proprietario> Proprietarios { get; set; }
                
        public DbSet<Evento> Eventos { get; set; }

        public DbSet<Visitante> Visitantes { get; set; }                      

        public DbSet<Fornecedor> Fornecedores { get; set; }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Funcionario> Funcionarios { get; set; }

        public DbSet<Lote> Lotes { get; set; }

        public DbSet<Inquilino> Inquilinos { get; set; }

        public DbSet<Cargo> Cargos { get; set; }

        public DbSet<EntradaMercadoria> EntradaMercadorias { get; set; }

        public DbSet<SaidaMercadoria> SaidaMercadorias { get; set; }

        public DbSet<CadastroVeiculoFuncionario> CadastroVeiculoFuncionarios { get; set; }

        public DbSet<CadastroVeiculoInquilino> CadastroVeiculoInquilinos { get; set; }

        public DbSet<CadastroVeiculoProprietario> CadastroVeiculoProprietarios { get; set; }

        public DbSet<GerenciarEntradaPortaria> GerenciarEntradaPortarias { get; set; }

        public DbSet<Ocorrencia> Ocorrencias { get; set; }

        public DbSet<Obra> Obras { get; set; }

        public DbSet<Contas> Contas { get; set; }

        public DbSet<Inadimplentes> Inadimplentes { get; set; }

        public DbSet<MesAnoPago> MesAnoPagos { get; set; }

        public DbSet <TipoDaConta> TipodaContas{ get; set; }

        public DbSet <ContasPagar> ContasPagar { get; set; }

    }
}