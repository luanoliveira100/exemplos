namespace CondominioEstribo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class terceira1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EntradaMercadorias", "ValorTotalDaNF", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EntradaMercadorias", "ValorTotalDaNF", c => c.String());
        }
    }
}
