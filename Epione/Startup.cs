using DATA;
using Domain;
using Epione.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartupAttribute(typeof(Epione.Startup))]
namespace Epione
{
    public partial class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
            AddUsersAndRoles();

        }

 




        private void AddUsersAndRoles()
        {

            EpioneContext context = new EpioneContext();
            var roleManager = new RoleManager<CustomRole, int>(new RoleStore<CustomRole, int, CustomUserRole>(context));
            var UserManager = new UserManager<User, int>(new UserStore<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>(context));

            // In Startup iam creating first Admin Role and creating a default Admin User    
            var role = new CustomRole();
            if (!roleManager.RoleExists("Patient"))
            {


                role.Name = "Patient";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Medecin"))
            {


                role.Name = "Medecin";
                roleManager.Create(role);
            }
            
            if (!roleManager.RoleExists("SuperAdmin"))
            {


                role.Name = "SuperAdmin";
                roleManager.Create(role);
            }
            if (UserManager.FindByName("SuperAdmin") == null)
            {
                var user = new User
                {
                    UserName = "admin@yahoo.com",

                    Email = "admin@yahoo.com",
                    Password = "Administration123555554$"

                };

                var Password = "Administration123555554$"; 
                var chkUser = UserManager.Create(user , Password  );
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "SuperAdmin")
                    ;
                }




            }
        }
    }
}







        
    