namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class projectforeignkey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Projects", new[] { "CustomerId" });
            RenameColumn(table: "dbo.Projects", name: "CustomerId", newName: "Customer_Id");
            AlterColumn("dbo.Projects", "Customer_Id", c => c.Int());
            CreateIndex("dbo.Projects", "Customer_Id");
            AddForeignKey("dbo.Projects", "Customer_Id", "dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Projects", new[] { "Customer_Id" });
            AlterColumn("dbo.Projects", "Customer_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Projects", name: "Customer_Id", newName: "CustomerId");
            CreateIndex("dbo.Projects", "CustomerId");
            AddForeignKey("dbo.Projects", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
        }
    }
}
