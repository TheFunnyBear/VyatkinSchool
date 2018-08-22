using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;


namespace VyatkinSchool.Infrastructure
{
    public partial class VyatkinSchoolIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public VyatkinSchoolIdentityDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public VyatkinSchoolIdentityDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Database.SetInitializer<VyatkinSchoolIdentityDbContext>(new DbContextInitializer());
        }

        public static VyatkinSchoolIdentityDbContext Create()
        {
            return new VyatkinSchoolIdentityDbContext(GetConnectionString());
        }

        private static string GetConnectionString()
        {
            SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
            sqlBuilder.DataSource = "u0535220.plsk.regruhosting.ru";
            sqlBuilder.InitialCatalog = "u0535220_VyatkinSchool";
            sqlBuilder.UserID = "u0535220_u0535220";
            sqlBuilder.Password = "Kx%j8h49";
            sqlBuilder.PersistSecurityInfo = true;
            sqlBuilder.IntegratedSecurity = false;
            sqlBuilder.MultipleActiveResultSets = true;
            sqlBuilder.ApplicationName = "EntityFramework";

            EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
            entityBuilder.ProviderConnectionString = sqlBuilder.ToString();
            entityBuilder.Metadata = "res://*";
            //entityBuilder.Metadata = "res://*/Model.CompanyModel.csdl|res://*/Model.CompanyModel.ssdl|res://*/Model.CompanyModel.msl";
            entityBuilder.Provider = "System.Data.SqlClient";

            return entityBuilder.ToString();
        }
    }
}