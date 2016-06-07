namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_owner_to_project : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Owner", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "Owner");
        }
    }
}
