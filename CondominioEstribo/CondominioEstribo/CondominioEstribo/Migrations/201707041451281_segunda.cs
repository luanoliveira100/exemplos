namespace CondominioEstribo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class segunda : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContasPagars",
                c => new
                    {
                        ContasPagarId = c.Int(nullable: false, identity: true),
                        datatitulo = c.DateTime(nullable: false),
                        datapagamento = c.DateTime(nullable: false),
                        valor = c.Double(nullable: false),
                        TipodaConta_TipoDaContaId = c.Int(),
                    })
                .PrimaryKey(t => t.ContasPagarId)
                .ForeignKey("dbo.TipoDaContas", t => t.TipodaConta_TipoDaContaId)
                .Index(t => t.TipodaConta_TipoDaContaId);
            
            CreateTable(
                "dbo.TipoDaContas",
                c => new
                    {
                        TipoDaContaId = c.Int(nullable: false, identity: true),
                        Tipo = c.String(),
                        Observacao = c.String(),
                    })
                .PrimaryKey(t => t.TipoDaContaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContasPagars", "TipodaConta_TipoDaContaId", "dbo.TipoDaContas");
            DropIndex("dbo.ContasPagars", new[] { "TipodaConta_TipoDaContaId" });
            DropTable("dbo.TipoDaContas");
            DropTable("dbo.ContasPagars");
        }
    }
}
