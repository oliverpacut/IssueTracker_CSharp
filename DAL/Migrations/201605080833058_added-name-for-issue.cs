namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addednameforissue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "Name", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Issues", "Name");
        }
    }
}
