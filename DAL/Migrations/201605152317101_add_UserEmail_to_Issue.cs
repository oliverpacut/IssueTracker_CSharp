namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_UserEmail_to_Issue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "UserEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Issues", "UserEmail");
        }
    }
}
