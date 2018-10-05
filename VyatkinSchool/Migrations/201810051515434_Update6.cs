namespace VyatkinSchool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserSearchRequestItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SearchRequest = c.String(nullable: false),
                        SearchInNews = c.Boolean(nullable: false),
                        SearchInPhoto = c.Boolean(nullable: false),
                        SearchInVideo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserSearchRequestItems");
        }
    }
}
