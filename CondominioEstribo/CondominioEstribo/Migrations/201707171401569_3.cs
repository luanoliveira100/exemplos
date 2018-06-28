namespace CondominioEstribo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GerenciarEntradaPortarias", "FuncionarioDentroPortaria_CadastroVeiculoFuncionarioId", "dbo.CadastroVeiculoFuncionarios");
            DropForeignKey("dbo.GerenciarEntradaPortarias", "InquilinoDentroPortaria_CadastroVeiculoInquilinosId", "dbo.CadastroVeiculoInquilinos");
            DropForeignKey("dbo.GerenciarEntradaPortarias", "ProprietarioDentroPortaria_CadastroVeiculoProprietarioId", "dbo.CadastroVeiculoProprietarios");
            DropForeignKey("dbo.GerenciarEntradaPortarias", "VisitanteDentroPortaria_VisitanteId", "dbo.Visitantes");
            DropIndex("dbo.GerenciarEntradaPortarias", new[] { "FuncionarioDentroPortaria_CadastroVeiculoFuncionarioId" });
            DropIndex("dbo.GerenciarEntradaPortarias", new[] { "InquilinoDentroPortaria_CadastroVeiculoInquilinosId" });
            DropIndex("dbo.GerenciarEntradaPortarias", new[] { "ProprietarioDentroPortaria_CadastroVeiculoProprietarioId" });
            DropIndex("dbo.GerenciarEntradaPortarias", new[] { "VisitanteDentroPortaria_VisitanteId" });
            CreateTable(
                "dbo.Placas",
                c => new
                    {
                        placaId = c.Int(nullable: false, identity: true),
                        numeroPlaca = c.String(),
                        funcionario_UsuarioId = c.Int(),
                        inquilino_UsuarioId = c.Int(),
                        proprietario_UsuarioId = c.Int(),
                        visitante_VisitanteId = c.Int(),
                    })
                .PrimaryKey(t => t.placaId)
                .ForeignKey("dbo.Funcionarios", t => t.funcionario_UsuarioId)
                .ForeignKey("dbo.Inquilinos", t => t.inquilino_UsuarioId)
                .ForeignKey("dbo.Proprietarios", t => t.proprietario_UsuarioId)
                .ForeignKey("dbo.Visitantes", t => t.visitante_VisitanteId)
                .Index(t => t.funcionario_UsuarioId)
                .Index(t => t.inquilino_UsuarioId)
                .Index(t => t.proprietario_UsuarioId)
                .Index(t => t.visitante_VisitanteId);
            
            DropColumn("dbo.Visitantes", "PlacaVeiculoVisitante");
            DropTable("dbo.CadastroVeiculoFuncionarios");
            DropTable("dbo.CadastroVeiculoInquilinos");
            DropTable("dbo.CadastroVeiculoProprietarios");
            DropTable("dbo.GerenciarEntradaPortarias");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GerenciarEntradaPortarias",
                c => new
                    {
                        GerenciarEntradaPortariaId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        RG = c.String(),
                        DataHoraEntrada = c.DateTime(nullable: false),
                        DataHoraSaida = c.DateTime(nullable: false),
                        PlacaDoVeiculo = c.String(),
                        TelefoneContato = c.String(),
                        FlagParaControle = c.String(),
                        Observacao = c.String(),
                        FuncionarioDentroPortaria_CadastroVeiculoFuncionarioId = c.Int(),
                        InquilinoDentroPortaria_CadastroVeiculoInquilinosId = c.Int(),
                        ProprietarioDentroPortaria_CadastroVeiculoProprietarioId = c.Int(),
                        VisitanteDentroPortaria_VisitanteId = c.Int(),
                    })
                .PrimaryKey(t => t.GerenciarEntradaPortariaId);
            
            CreateTable(
                "dbo.CadastroVeiculoProprietarios",
                c => new
                    {
                        CadastroVeiculoProprietarioId = c.Int(nullable: false, identity: true),
                        NomeDoProprietario = c.String(),
                        PlacaVeiculoProprietario = c.String(),
                        ProprietarioDentroCadastroVeiculo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CadastroVeiculoProprietarioId);
            
            CreateTable(
                "dbo.CadastroVeiculoInquilinos",
                c => new
                    {
                        CadastroVeiculoInquilinosId = c.Int(nullable: false, identity: true),
                        NomeDoInquilino = c.String(),
                        PlacaVeiculoInquilino = c.String(),
                        InquilinoDentroCadastroVeiculo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CadastroVeiculoInquilinosId);
            
            CreateTable(
                "dbo.CadastroVeiculoFuncionarios",
                c => new
                    {
                        CadastroVeiculoFuncionarioId = c.Int(nullable: false, identity: true),
                        NomeDoFuncionario = c.String(),
                        PlacaVeiculoFuncionario = c.String(),
                        FuncionarioDentroCadastroVeiculo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CadastroVeiculoFuncionarioId);
            
            AddColumn("dbo.Visitantes", "PlacaVeiculoVisitante", c => c.String());
            DropForeignKey("dbo.Placas", "visitante_VisitanteId", "dbo.Visitantes");
            DropForeignKey("dbo.Placas", "proprietario_UsuarioId", "dbo.Proprietarios");
            DropForeignKey("dbo.Placas", "inquilino_UsuarioId", "dbo.Inquilinos");
            DropForeignKey("dbo.Placas", "funcionario_UsuarioId", "dbo.Funcionarios");
            DropIndex("dbo.Placas", new[] { "visitante_VisitanteId" });
            DropIndex("dbo.Placas", new[] { "proprietario_UsuarioId" });
            DropIndex("dbo.Placas", new[] { "inquilino_UsuarioId" });
            DropIndex("dbo.Placas", new[] { "funcionario_UsuarioId" });
            DropTable("dbo.Placas");
            CreateIndex("dbo.GerenciarEntradaPortarias", "VisitanteDentroPortaria_VisitanteId");
            CreateIndex("dbo.GerenciarEntradaPortarias", "ProprietarioDentroPortaria_CadastroVeiculoProprietarioId");
            CreateIndex("dbo.GerenciarEntradaPortarias", "InquilinoDentroPortaria_CadastroVeiculoInquilinosId");
            CreateIndex("dbo.GerenciarEntradaPortarias", "FuncionarioDentroPortaria_CadastroVeiculoFuncionarioId");
            AddForeignKey("dbo.GerenciarEntradaPortarias", "VisitanteDentroPortaria_VisitanteId", "dbo.Visitantes", "VisitanteId");
            AddForeignKey("dbo.GerenciarEntradaPortarias", "ProprietarioDentroPortaria_CadastroVeiculoProprietarioId", "dbo.CadastroVeiculoProprietarios", "CadastroVeiculoProprietarioId");
            AddForeignKey("dbo.GerenciarEntradaPortarias", "InquilinoDentroPortaria_CadastroVeiculoInquilinosId", "dbo.CadastroVeiculoInquilinos", "CadastroVeiculoInquilinosId");
            AddForeignKey("dbo.GerenciarEntradaPortarias", "FuncionarioDentroPortaria_CadastroVeiculoFuncionarioId", "dbo.CadastroVeiculoFuncionarios", "CadastroVeiculoFuncionarioId");
        }
    }
}
