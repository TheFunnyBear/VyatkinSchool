namespace VyatkinSchool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VideoDownloadCounterItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VideoFileId = c.Int(nullable: false),
                        LastDownload = c.DateTime(nullable: false),
                        DownloadCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VideoDownloadCounterItems");
        }
    }
}
