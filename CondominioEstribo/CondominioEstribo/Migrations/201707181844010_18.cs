namespace CondominioEstribo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _18 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailId = c.Int(nullable: false, identity: true),
                        value = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        OrderMasterId_OrderMasterId = c.Int(),
                        ProductId_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderDetailId)
                .ForeignKey("dbo.OrderMasters", t => t.OrderMasterId_OrderMasterId)
                .ForeignKey("dbo.Products", t => t.ProductId_ProductId)
                .Index(t => t.OrderMasterId_OrderMasterId)
                .Index(t => t.ProductId_ProductId);
            
            CreateTable(
                "dbo.OrderMasters",
                c => new
                    {
                        OrderMasterId = c.Int(nullable: false, identity: true),
                        OrderNo = c.String(),
                        OrderDate = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.OrderMasterId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        CategoryId_CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Categories", t => t.CategoryId_CategoryId)
                .Index(t => t.CategoryId_CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "ProductId_ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryId_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.OrderDetails", "OrderMasterId_OrderMasterId", "dbo.OrderMasters");
            DropIndex("dbo.Products", new[] { "CategoryId_CategoryId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId_ProductId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderMasterId_OrderMasterId" });
            DropTable("dbo.Products");
            DropTable("dbo.OrderMasters");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Categories");
        }
    }
}
