namespace CondominioEstribo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contas", "proprietario_UsuarioId", c => c.Int());
            AddColumn("dbo.Inadimplentes", "Proprietario_UsuarioId", c => c.Int());
            CreateIndex("dbo.Contas", "proprietario_UsuarioId");
            CreateIndex("dbo.Inadimplentes", "Proprietario_UsuarioId");
            AddForeignKey("dbo.Contas", "proprietario_UsuarioId", "dbo.Proprietarios", "UsuarioId");
            AddForeignKey("dbo.Inadimplentes", "Proprietario_UsuarioId", "dbo.Proprietarios", "UsuarioId");
            DropColumn("dbo.Contas", "proprietarioid");
            DropColumn("dbo.Inadimplentes", "Proprietarioid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Inadimplentes", "Proprietarioid", c => c.Int(nullable: false));
            AddColumn("dbo.Contas", "proprietarioid", c => c.Int(nullable: false));
            DropForeignKey("dbo.Inadimplentes", "Proprietario_UsuarioId", "dbo.Proprietarios");
            DropForeignKey("dbo.Contas", "proprietario_UsuarioId", "dbo.Proprietarios");
            DropIndex("dbo.Inadimplentes", new[] { "Proprietario_UsuarioId" });
            DropIndex("dbo.Contas", new[] { "proprietario_UsuarioId" });
            DropColumn("dbo.Inadimplentes", "Proprietario_UsuarioId");
            DropColumn("dbo.Contas", "proprietario_UsuarioId");
        }
    }
}
