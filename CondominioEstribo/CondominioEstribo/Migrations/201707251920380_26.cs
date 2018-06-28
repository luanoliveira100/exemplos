namespace CondominioEstribo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _26 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SaidaMercadorias", "EntradaDeMercadoriaDentroDeSaidaDeMercadoria_EntradaMercadoriaId", "dbo.EntradaMercadorias");
            DropForeignKey("dbo.SaidaMercadorias", "FuncionarioDentroSaidaMercadoria_UsuarioId", "dbo.Funcionarios");
            DropForeignKey("dbo.SaidaMercadorias", "ProdutoDentroSaidaMercadoria_ProdutoId", "dbo.Produtos");
            DropIndex("dbo.SaidaMercadorias", new[] { "EntradaDeMercadoriaDentroDeSaidaDeMercadoria_EntradaMercadoriaId" });
            DropIndex("dbo.SaidaMercadorias", new[] { "FuncionarioDentroSaidaMercadoria_UsuarioId" });
            DropIndex("dbo.SaidaMercadorias", new[] { "ProdutoDentroSaidaMercadoria_ProdutoId" });
            DropTable("dbo.SaidaMercadorias");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SaidaMercadorias",
                c => new
                    {
                        SaidaMercadoriaId = c.Int(nullable: false, identity: true),
                        ProdutoSaidaMercadoria = c.String(),
                        QuantidadeRetirada = c.Int(nullable: false),
                        DataRetirada = c.DateTime(nullable: false),
                        FuncionarioSaidaMercadoria = c.String(),
                        Observacao = c.String(),
                        EntradaDeMercadoriaDentroDeSaidaDeMercadoria_EntradaMercadoriaId = c.Int(),
                        FuncionarioDentroSaidaMercadoria_UsuarioId = c.Int(),
                        ProdutoDentroSaidaMercadoria_ProdutoId = c.Int(),
                    })
                .PrimaryKey(t => t.SaidaMercadoriaId);
            
            CreateIndex("dbo.SaidaMercadorias", "ProdutoDentroSaidaMercadoria_ProdutoId");
            CreateIndex("dbo.SaidaMercadorias", "FuncionarioDentroSaidaMercadoria_UsuarioId");
            CreateIndex("dbo.SaidaMercadorias", "EntradaDeMercadoriaDentroDeSaidaDeMercadoria_EntradaMercadoriaId");
            AddForeignKey("dbo.SaidaMercadorias", "ProdutoDentroSaidaMercadoria_ProdutoId", "dbo.Produtos", "ProdutoId");
            AddForeignKey("dbo.SaidaMercadorias", "FuncionarioDentroSaidaMercadoria_UsuarioId", "dbo.Funcionarios", "UsuarioId");
            AddForeignKey("dbo.SaidaMercadorias", "EntradaDeMercadoriaDentroDeSaidaDeMercadoria_EntradaMercadoriaId", "dbo.EntradaMercadorias", "EntradaMercadoriaId");
        }
    }
}
