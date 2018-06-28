namespace CondominioEstribo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Usuarios", "ProprietarioDentroDeUsuario");
            DropColumn("dbo.Usuarios", "InquilinoDentroDeUsuario");
            DropColumn("dbo.Usuarios", "FuncionarioDentroDeUsuario");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuarios", "FuncionarioDentroDeUsuario", c => c.Int(nullable: false));
            AddColumn("dbo.Usuarios", "InquilinoDentroDeUsuario", c => c.Int(nullable: false));
            AddColumn("dbo.Usuarios", "ProprietarioDentroDeUsuario", c => c.Int(nullable: false));
        }
    }
}
