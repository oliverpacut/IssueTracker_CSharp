namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_responsibleperson_to_issue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issues", "ResponsiblePerson", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Issues", "ResponsiblePerson");
        }
    }
}
