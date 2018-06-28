namespace CondominioEstribo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _15 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lotes",
                c => new
                    {
                        LoteId = c.Int(nullable: false, identity: true),
                        NumeroLote = c.String(),
                        AreaConstruida = c.String(),
                        Observacao = c.String(),
                        proprietario_UsuarioId = c.Int(),
                    })
                .PrimaryKey(t => t.LoteId)
                .ForeignKey("dbo.Proprietarios", t => t.proprietario_UsuarioId)
                .Index(t => t.proprietario_UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lotes", "proprietario_UsuarioId", "dbo.Proprietarios");
            DropIndex("dbo.Lotes", new[] { "proprietario_UsuarioId" });
            DropTable("dbo.Lotes");
        }
    }
}
