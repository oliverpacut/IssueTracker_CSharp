namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conform : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Issues", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Projects", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Issues", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Issues", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Issues", new[] { "ProjectId" });
            DropIndex("dbo.Issues", new[] { "CustomerId" });
            DropIndex("dbo.Issues", new[] { "EmployeeId" });
            DropIndex("dbo.Projects", new[] { "CustomerId" });
            RenameColumn(table: "dbo.Issues", name: "CustomerId", newName: "Customer_Id");
            RenameColumn(table: "dbo.Projects", name: "CustomerId", newName: "Customer_Id");
            RenameColumn(table: "dbo.Issues", name: "EmployeeId", newName: "Employee_Id");
            RenameColumn(table: "dbo.Issues", name: "ProjectId", newName: "Project_Id");
            AlterColumn("dbo.Issues", "Project_Id", c => c.Int());
            AlterColumn("dbo.Issues", "Customer_Id", c => c.Int());
            AlterColumn("dbo.Issues", "Employee_Id", c => c.Int());
            AlterColumn("dbo.Projects", "Customer_Id", c => c.Int());
            CreateIndex("dbo.Issues", "Customer_Id");
            CreateIndex("dbo.Issues", "Employee_Id");
            CreateIndex("dbo.Issues", "Project_Id");
            CreateIndex("dbo.Projects", "Customer_Id");
            AddForeignKey("dbo.Issues", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.Projects", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.Issues", "Employee_Id", "dbo.Employees", "Id");
            AddForeignKey("dbo.Issues", "Project_Id", "dbo.Projects", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Issues", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.Issues", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.Projects", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Issues", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Projects", new[] { "Customer_Id" });
            DropIndex("dbo.Issues", new[] { "Project_Id" });
            DropIndex("dbo.Issues", new[] { "Employee_Id" });
            DropIndex("dbo.Issues", new[] { "Customer_Id" });
            AlterColumn("dbo.Projects", "Customer_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Issues", "Employee_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Issues", "Customer_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Issues", "Project_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Issues", name: "Project_Id", newName: "ProjectId");
            RenameColumn(table: "dbo.Issues", name: "Employee_Id", newName: "EmployeeId");
            RenameColumn(table: "dbo.Projects", name: "Customer_Id", newName: "CustomerId");
            RenameColumn(table: "dbo.Issues", name: "Customer_Id", newName: "CustomerId");
            CreateIndex("dbo.Projects", "CustomerId");
            CreateIndex("dbo.Issues", "EmployeeId");
            CreateIndex("dbo.Issues", "CustomerId");
            CreateIndex("dbo.Issues", "ProjectId");
            AddForeignKey("dbo.Issues", "ProjectId", "dbo.Projects", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Issues", "EmployeeId", "dbo.Employees", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Projects", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Issues", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
        }
    }
}
