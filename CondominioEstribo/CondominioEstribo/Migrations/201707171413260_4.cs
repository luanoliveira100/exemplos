namespace CondominioEstribo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Visitantes", name: "ProprietarioDentroDeVisitante_UsuarioId", newName: "proprietario_UsuarioId");
            RenameIndex(table: "dbo.Visitantes", name: "IX_ProprietarioDentroDeVisitante_UsuarioId", newName: "IX_proprietario_UsuarioId");
            DropColumn("dbo.Visitantes", "NomeDoCondomino");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Visitantes", "NomeDoCondomino", c => c.String());
            RenameIndex(table: "dbo.Visitantes", name: "IX_proprietario_UsuarioId", newName: "IX_ProprietarioDentroDeVisitante_UsuarioId");
            RenameColumn(table: "dbo.Visitantes", name: "proprietario_UsuarioId", newName: "ProprietarioDentroDeVisitante_UsuarioId");
        }
    }
}
