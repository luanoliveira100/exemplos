namespace CondominioEstribo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contas",
                c => new
                    {
                        ContasId = c.Int(nullable: false, identity: true),
                        proprietarioid = c.Int(nullable: false),
                        datatitulo = c.DateTime(nullable: false),
                        datapagamento = c.DateTime(nullable: false),
                        valor = c.Double(nullable: false),
                        fonte = c.String(),
                        codigo = c.String(),
                        lote = c.String(),
                        pago = c.String(),
                    })
                .PrimaryKey(t => t.ContasId);
            
            CreateTable(
                "dbo.Inadimplentes",
                c => new
                    {
                        ContasId = c.Int(nullable: false, identity: true),
                        Proprietarioid = c.Int(nullable: false),
                        data = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ContasId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Inadimplentes");
            DropTable("dbo.Contas");
        }
    }
}
