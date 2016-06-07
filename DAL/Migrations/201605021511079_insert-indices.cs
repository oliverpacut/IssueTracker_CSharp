namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class insertindices : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Issues", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Projects", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Issues", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.Issues", "Project_Id", "dbo.Projects");
            DropIndex("dbo.Issues", new[] { "Customer_Id" });
            DropIndex("dbo.Issues", new[] { "Employee_Id" });
            DropIndex("dbo.Issues", new[] { "Project_Id" });
            DropIndex("dbo.Projects", new[] { "Customer_Id" });
            RenameColumn(table: "dbo.Issues", name: "Customer_Id", newName: "CustomerId");
            RenameColumn(table: "dbo.Projects", name: "Customer_Id", newName: "CustomerId");
            RenameColumn(table: "dbo.Issues", name: "Employee_Id", newName: "EmployeeId");
            RenameColumn(table: "dbo.Issues", name: "Project_Id", newName: "ProjectId");
            AlterColumn("dbo.Issues", "CustomerId", c => c.Int(nullable: false));
            AlterColumn("dbo.Issues", "EmployeeId", c => c.Int(nullable: false));
            AlterColumn("dbo.Issues", "ProjectId", c => c.Int(nullable: false));
            AlterColumn("dbo.Projects", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Issues", "ProjectId");
            CreateIndex("dbo.Issues", "CustomerId");
            CreateIndex("dbo.Issues", "EmployeeId");
            CreateIndex("dbo.Projects", "CustomerId");
            AddForeignKey("dbo.Issues", "CustomerId", "dbo.Customers", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Projects", "CustomerId", "dbo.Customers", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Issues", "EmployeeId", "dbo.Employees", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Issues", "ProjectId", "dbo.Projects", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Issues", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Issues", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Projects", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Issues", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Projects", new[] { "CustomerId" });
            DropIndex("dbo.Issues", new[] { "EmployeeId" });
            DropIndex("dbo.Issues", new[] { "CustomerId" });
            DropIndex("dbo.Issues", new[] { "ProjectId" });
            AlterColumn("dbo.Projects", "CustomerId", c => c.Int());
            AlterColumn("dbo.Issues", "ProjectId", c => c.Int());
            AlterColumn("dbo.Issues", "EmployeeId", c => c.Int());
            AlterColumn("dbo.Issues", "CustomerId", c => c.Int());
            RenameColumn(table: "dbo.Issues", name: "ProjectId", newName: "Project_Id");
            RenameColumn(table: "dbo.Issues", name: "EmployeeId", newName: "Employee_Id");
            RenameColumn(table: "dbo.Projects", name: "CustomerId", newName: "Customer_Id");
            RenameColumn(table: "dbo.Issues", name: "CustomerId", newName: "Customer_Id");
            CreateIndex("dbo.Projects", "Customer_Id");
            CreateIndex("dbo.Issues", "Project_Id");
            CreateIndex("dbo.Issues", "Employee_Id");
            CreateIndex("dbo.Issues", "Customer_Id");
            AddForeignKey("dbo.Issues", "Project_Id", "dbo.Projects", "Id");
            AddForeignKey("dbo.Issues", "Employee_Id", "dbo.Employees", "Id");
            AddForeignKey("dbo.Projects", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.Issues", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
