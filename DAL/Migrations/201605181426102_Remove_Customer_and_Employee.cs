namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_Customer_and_Employee : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Issues", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Issues", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.Projects", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Issues", new[] { "Customer_Id" });
            DropIndex("dbo.Issues", new[] { "Employee_Id" });
            DropIndex("dbo.Projects", new[] { "Customer_Id" });
            DropColumn("dbo.Issues", "Customer_Id");
            DropColumn("dbo.Issues", "Employee_Id");
            DropColumn("dbo.Projects", "Customer_Id");
            DropTable("dbo.Customers");
            DropTable("dbo.Employees");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Email = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Email = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Projects", "Customer_Id", c => c.Int());
            AddColumn("dbo.Issues", "Employee_Id", c => c.Int());
            AddColumn("dbo.Issues", "Customer_Id", c => c.Int());
            CreateIndex("dbo.Projects", "Customer_Id");
            CreateIndex("dbo.Issues", "Employee_Id");
            CreateIndex("dbo.Issues", "Customer_Id");
            AddForeignKey("dbo.Projects", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.Issues", "Employee_Id", "dbo.Employees", "Id");
            AddForeignKey("dbo.Issues", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
