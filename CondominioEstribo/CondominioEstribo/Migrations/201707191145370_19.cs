namespace CondominioEstribo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _19 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "OrderMasterId_OrderMasterId", "dbo.OrderMasters");
            DropForeignKey("dbo.Products", "CategoryId_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.OrderDetails", "ProductId_ProductId", "dbo.Products");
            DropIndex("dbo.OrderDetails", new[] { "OrderMasterId_OrderMasterId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId_ProductId" });
            DropIndex("dbo.Products", new[] { "CategoryId_CategoryId" });
            DropTable("dbo.Categories");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.OrderMasters");
            DropTable("dbo.Products");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        CategoryId_CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId);
            
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
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailId = c.Int(nullable: false, identity: true),
                        value = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        OrderMasterId_OrderMasterId = c.Int(),
                        ProductId_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderDetailId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateIndex("dbo.Products", "CategoryId_CategoryId");
            CreateIndex("dbo.OrderDetails", "ProductId_ProductId");
            CreateIndex("dbo.OrderDetails", "OrderMasterId_OrderMasterId");
            AddForeignKey("dbo.OrderDetails", "ProductId_ProductId", "dbo.Products", "ProductId");
            AddForeignKey("dbo.Products", "CategoryId_CategoryId", "dbo.Categories", "CategoryId");
            AddForeignKey("dbo.OrderDetails", "OrderMasterId_OrderMasterId", "dbo.OrderMasters", "OrderMasterId");
        }
    }
}
