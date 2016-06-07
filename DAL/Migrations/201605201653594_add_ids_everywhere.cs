namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_ids_everywhere : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "OwnerId", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "OwnerName", c => c.String());
            AddColumn("dbo.Issues", "ResponsiblePersonId", c => c.Int(nullable: false));
            AddColumn("dbo.Issues", "ResponsiblePersonEmail", c => c.String());
            AddColumn("dbo.Issues", "OwnerId", c => c.Int(nullable: false));
            AddColumn("dbo.Issues", "OwnerEmail", c => c.String());
            AddColumn("dbo.Issues", "ProjectId", c => c.Int(nullable: false));
            AddColumn("dbo.Notifications", "PersonRecipientId", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "OwnerId", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "OwnerEmail", c => c.String());
            DropColumn("dbo.Comments", "PersonId");
            DropColumn("dbo.Comments", "Owner");
            DropColumn("dbo.Issues", "ResponsiblePerson");
            DropColumn("dbo.Issues", "Owner");
            DropColumn("dbo.Notifications", "PersonRecipient");
            DropColumn("dbo.Projects", "Owner");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "Owner", c => c.String());
            AddColumn("dbo.Notifications", "PersonRecipient", c => c.Int(nullable: false));
            AddColumn("dbo.Issues", "Owner", c => c.String());
            AddColumn("dbo.Issues", "ResponsiblePerson", c => c.String());
            AddColumn("dbo.Comments", "Owner", c => c.String());
            AddColumn("dbo.Comments", "PersonId", c => c.Int(nullable: false));
            DropColumn("dbo.Projects", "OwnerEmail");
            DropColumn("dbo.Projects", "OwnerId");
            DropColumn("dbo.Notifications", "PersonRecipientId");
            DropColumn("dbo.Issues", "ProjectId");
            DropColumn("dbo.Issues", "OwnerEmail");
            DropColumn("dbo.Issues", "OwnerId");
            DropColumn("dbo.Issues", "ResponsiblePersonEmail");
            DropColumn("dbo.Issues", "ResponsiblePersonId");
            DropColumn("dbo.Comments", "OwnerName");
            DropColumn("dbo.Comments", "OwnerId");
        }
    }
}
