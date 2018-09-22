namespace VyatkinSchool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.VisitsCounterItems", "AbsolutePath", c => c.String());
            DropColumn("dbo.VisitsCounterItems", "AbsoluteUri");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VisitsCounterItems", "AbsoluteUri", c => c.String());
            DropColumn("dbo.VisitsCounterItems", "AbsolutePath");
        }
    }
}
