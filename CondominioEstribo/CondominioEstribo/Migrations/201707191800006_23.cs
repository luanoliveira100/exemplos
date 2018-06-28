namespace CondominioEstribo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _23 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NotaFiscals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        NumeroDaNota = c.String(),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NotaFiscalItens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantidade = c.Int(nullable: false),
                        ValorUnitartio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        notaFiscal_Id = c.Int(),
                        Produto_ProdutoId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NotaFiscals", t => t.notaFiscal_Id)
                .ForeignKey("dbo.Produtos", t => t.Produto_ProdutoId)
                .Index(t => t.notaFiscal_Id)
                .Index(t => t.Produto_ProdutoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NotaFiscalItens", "Produto_ProdutoId", "dbo.Produtos");
            DropForeignKey("dbo.NotaFiscalItens", "notaFiscal_Id", "dbo.NotaFiscals");
            DropIndex("dbo.NotaFiscalItens", new[] { "Produto_ProdutoId" });
            DropIndex("dbo.NotaFiscalItens", new[] { "notaFiscal_Id" });
            DropTable("dbo.NotaFiscalItens");
            DropTable("dbo.NotaFiscals");
        }
    }
}
