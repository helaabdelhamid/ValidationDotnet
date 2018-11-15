using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain;
using Domaine;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DATA
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {




        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class EpioneContext : IdentityDbContext<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public EpioneContext()
            : base("name=DefaultConnection")
        {
        }

        public static EpioneContext Create()
        {
            return new EpioneContext();
        }
        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Disscussion> Discussions { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Recomandation> Recomendations { get; set; }

        public DbSet<Repport> Repports { get; set; }

        public DbSet<Treatment> Treatments { get; set; }

        public DbSet<Room> Rooms { get; set; }


    }
}
