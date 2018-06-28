namespace CondominioEstribo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _22 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "OrderID_OrderID", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "OrderID_OrderID" });
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Orders");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.OrderItensID);
            
            CreateIndex("dbo.OrderDetails", "OrderID_OrderID");
            AddForeignKey("dbo.OrderDetails", "OrderID_OrderID", "dbo.Orders", "OrderID");
        }
    }
}
