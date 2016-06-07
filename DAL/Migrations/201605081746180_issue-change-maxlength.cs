namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class issuechangemaxlength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Issues", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.Issues", "Description", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Issues", "Description", c => c.String(maxLength: 140));
            AlterColumn("dbo.Issues", "Name", c => c.String(maxLength: 20));
        }
    }
}
