namespace CondominioEstribo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inquilinos", "lote_LoteId", c => c.Int());
            CreateIndex("dbo.Inquilinos", "lote_LoteId");
            AddForeignKey("dbo.Inquilinos", "lote_LoteId", "dbo.Lotes", "LoteId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inquilinos", "lote_LoteId", "dbo.Lotes");
            DropIndex("dbo.Inquilinos", new[] { "lote_LoteId" });
            DropColumn("dbo.Inquilinos", "lote_LoteId");
        }
    }
}
