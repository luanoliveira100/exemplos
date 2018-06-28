namespace CondominioEstribo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class terceira : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MesAnoPagos",
                c => new
                    {
                        MesAnoPagoId = c.Int(nullable: false, identity: true),
                        mes = c.Int(nullable: false),
                        ano = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MesAnoPagoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MesAnoPagos");
        }
    }
}
