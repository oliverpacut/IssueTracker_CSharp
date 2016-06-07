namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class projectaddmaxlength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "Description", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "Description", c => c.String(maxLength: 140));
        }
    }
}
