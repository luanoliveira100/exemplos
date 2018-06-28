namespace CondominioEstribo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _14 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lotes", "ProprietarioDentroDeLote_UsuarioId", "dbo.Proprietarios");
            DropIndex("dbo.Lotes", new[] { "ProprietarioDentroDeLote_UsuarioId" });
            DropTable("dbo.Lotes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Lotes",
                c => new
                    {
                        LoteId = c.Int(nullable: false, identity: true),
                        NumeroLote = c.String(),
                        NomeProprietario = c.String(),
                        AreaConstruida = c.String(),
                        Observacao = c.String(),
                        ProprietarioDentroDeLote_UsuarioId = c.Int(),
                    })
                .PrimaryKey(t => t.LoteId);
            
            CreateIndex("dbo.Lotes", "ProprietarioDentroDeLote_UsuarioId");
            AddForeignKey("dbo.Lotes", "ProprietarioDentroDeLote_UsuarioId", "dbo.Proprietarios", "UsuarioId");
        }
    }
}
