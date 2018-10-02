using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace VyatkinSchool.Models.IdentityModels
{
    public partial class AddIsDeletedToGalleryItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GalleryItem", "IsDeleted", c => c.Boolean());
        }

        public override void Down()
        {
            DropColumn("dbo.GalleryItem", "IsDeleted");
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<GalleryGroupItem> GalleryGroups { get; set; }
        public virtual DbSet<MessageItem> Messages { get; set; }
        public virtual DbSet<GalleryItem> Gallery { get; set; }
        public virtual DbSet<VideoFileItem> Video { get; set; }
        public virtual DbSet<ImageItem> Images { get; set; }
        public virtual DbSet<VisitsCounterItem> VisitsCounters { get; set; }
        public virtual DbSet<PreviewItem> PreviewItems { get; set; }
    }
}