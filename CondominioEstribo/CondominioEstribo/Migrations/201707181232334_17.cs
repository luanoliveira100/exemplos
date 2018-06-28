namespace CondominioEstribo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _17 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.EntradaMercadorias", "ProdutoEntradaMercadoria");
            DropColumn("dbo.EntradaMercadorias", "FornecedorEntradaMercadoria");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EntradaMercadorias", "FornecedorEntradaMercadoria", c => c.String());
            AddColumn("dbo.EntradaMercadorias", "ProdutoEntradaMercadoria", c => c.String());
        }
    }
}
