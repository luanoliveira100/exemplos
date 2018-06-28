namespace CondominioEstribo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _27 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SaidaMercadorias",
                c => new
                    {
                        SaidaMercadoriaId = c.Int(nullable: false, identity: true),
                        quantidade = c.Int(nullable: false),
                        data = c.DateTime(nullable: false),
                        observacao = c.String(),
                        funcionario_UsuarioId = c.Int(),
                        produto_ProdutoId = c.Int(),
                    })
                .PrimaryKey(t => t.SaidaMercadoriaId)
                .ForeignKey("dbo.Funcionarios", t => t.funcionario_UsuarioId)
                .ForeignKey("dbo.Produtos", t => t.produto_ProdutoId)
                .Index(t => t.funcionario_UsuarioId)
                .Index(t => t.produto_ProdutoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SaidaMercadorias", "produto_ProdutoId", "dbo.Produtos");
            DropForeignKey("dbo.SaidaMercadorias", "funcionario_UsuarioId", "dbo.Funcionarios");
            DropIndex("dbo.SaidaMercadorias", new[] { "produto_ProdutoId" });
            DropIndex("dbo.SaidaMercadorias", new[] { "funcionario_UsuarioId" });
            DropTable("dbo.SaidaMercadorias");
        }
    }
}
