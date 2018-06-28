namespace CondominioEstribo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _7 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Inquilinos", name: "ProprietarioDentroDeInquilino_UsuarioId", newName: "proprietario_UsuarioId");
            RenameIndex(table: "dbo.Inquilinos", name: "IX_ProprietarioDentroDeInquilino_UsuarioId", newName: "IX_proprietario_UsuarioId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Inquilinos", name: "IX_proprietario_UsuarioId", newName: "IX_ProprietarioDentroDeInquilino_UsuarioId");
            RenameColumn(table: "dbo.Inquilinos", name: "proprietario_UsuarioId", newName: "ProprietarioDentroDeInquilino_UsuarioId");
        }
    }
}
