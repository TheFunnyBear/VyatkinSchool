namespace VyatkinSchool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserHostAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastVisit = c.DateTime(nullable: false),
                        HostAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserHostAddresses");
        }
    }
}
