namespace VyatkinSchool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VisitsCounterItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AbsoluteUri = c.String(),
                        VisitsCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VisitsCounterItems");
        }
    }
}
