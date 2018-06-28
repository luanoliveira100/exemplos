namespace CondominioEstribo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderItensID = c.Int(nullable: false, identity: true),
                        ItemName = c.String(),
                        Quantity = c.Int(nullable: false),
                        Rate = c.Double(nullable: false),
                        TotalAmount = c.Double(nullable: false),
                        OrderID_OrderID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderItensID)
                .ForeignKey("dbo.Orders", t => t.OrderID_OrderID)
                .Index(t => t.OrderID_OrderID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        Orderno = c.String(),
                        OrderDate = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.OrderID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "OrderID_OrderID", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "OrderID_OrderID" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
        }
    }
}
