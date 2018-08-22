using System.Data.Entity;


namespace VyatkinSchool.Infrastructure
{
    public class DbContextInitializer : DropCreateDatabaseIfModelChanges<VyatkinSchoolIdentityDbContext>
    {

        protected override void Seed(VyatkinSchoolIdentityDbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }

        public void PerformInitialSetup(VyatkinSchoolIdentityDbContext context)
        {
            // remove this, because the OWIN context does not yet exist:
            //var userManager = HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();
            //var roleManager = HttpContext.Current.GetOwinContext().Get<AppRoleManager>();
        }

    }
}