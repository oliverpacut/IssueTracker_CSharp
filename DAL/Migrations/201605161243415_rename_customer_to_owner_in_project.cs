namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rename_customer_to_owner_in_project : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "Owner", c => c.String());
            DropColumn("dbo.Issues", "UserEmail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Issues", "UserEmail", c => c.String());
            DropColumn("dbo.Issues", "Owner");
        }
    }
}
