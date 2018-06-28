namespace CondominioEstribo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _25 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NotaFiscals", "Valor", c => c.Double(nullable: false));
            AlterColumn("dbo.NotaFiscalItens", "ValorUnitartio", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NotaFiscalItens", "ValorUnitartio", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.NotaFiscals", "Valor", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
