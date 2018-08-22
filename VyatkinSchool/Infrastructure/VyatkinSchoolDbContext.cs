using Microsoft.EntityFrameworkCore;
using VyatkinSchool.Models;

namespace VyatkinSchool.Infrastructure
{
    public partial class VyatkinSchoolDbContext : DbContext
    {
        public VyatkinSchoolDbContext()
        {
        }

        public VyatkinSchoolDbContext(DbContextOptions<VyatkinSchoolDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer("data source=u0535220.plsk.regruhosting.ru;initial catalog=u0535220_VyatkinSchool;persist security info=True;user id=u0535220_u0535220;password=Kx%j8h49;MultipleActiveResultSets=True;App=EntityFramework");
            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public virtual DbSet<GalleryData> Gallery { get; set; }
        public virtual DbSet<GalleryGroup> GalleryGroup { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
    }
}