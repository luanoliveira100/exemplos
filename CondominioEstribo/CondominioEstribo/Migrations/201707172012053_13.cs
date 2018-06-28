namespace CondominioEstribo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _13 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Inquilinos", "lote_LoteId", "dbo.Lotes");
            DropIndex("dbo.Inquilinos", new[] { "lote_LoteId" });
            DropColumn("dbo.Inquilinos", "lote_LoteId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Inquilinos", "lote_LoteId", c => c.Int());
            CreateIndex("dbo.Inquilinos", "lote_LoteId");
            AddForeignKey("dbo.Inquilinos", "lote_LoteId", "dbo.Lotes", "LoteId");
        }
    }
}
