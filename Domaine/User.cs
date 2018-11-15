using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
 public class User :IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public String LastName { get; set; }
        public String FirstName { get; set; }
        public String Role { get; set; }
        public override String Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [MinLength(8)]
        public String Password { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {
            // Note the authenticationType must match the one defined in
            // CookieAuthenticationOptions.AuthenticationType 
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here 
            return userIdentity;
        }
    }
}
public class CustomUserLogin : IdentityUserLogin<int>
{
    public int Id { get; set; }

}
public class CustomUserRole : IdentityUserRole<int>
{
    public int Id { get; set; }
}
public class CustomUserClaim : IdentityUserClaim<int>
{

}
public class CustomRole : IdentityRole<int, CustomUserRole>
{
    public CustomRole() { }
    public CustomRole(string name) { Name = name; }
}

