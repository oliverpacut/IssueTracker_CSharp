namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeprojectfromissueaddprojectnametoissue : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Issues", "Project_Id", "dbo.Projects");
            DropIndex("dbo.Issues", new[] { "Project_Id" });
            AddColumn("dbo.Issues", "ProjectName", c => c.String());
            DropColumn("dbo.Issues", "Project_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Issues", "Project_Id", c => c.Int());
            DropColumn("dbo.Issues", "ProjectName");
            CreateIndex("dbo.Issues", "Project_Id");
            AddForeignKey("dbo.Issues", "Project_Id", "dbo.Projects", "Id");
        }
    }
}
