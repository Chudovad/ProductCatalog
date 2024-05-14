using Microsoft.AspNetCore.Identity;
using ProductCatalogAPI.Models;

namespace ProductCatalogAPI.Data
{
    public class RoleInitializer
    {
        public static void InitializeAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string userEmail = "user@gmail.com";
            string managerEmail = "manager@gmail.com";
            if (roleManager.FindByNameAsync("admin").GetAwaiter().GetResult() == null)
            {
                roleManager.CreateAsync(new IdentityRole("admin")).GetAwaiter().GetResult();
            }
            if (roleManager.FindByNameAsync("user").GetAwaiter().GetResult() == null)
            {
                roleManager.CreateAsync(new IdentityRole("user")).GetAwaiter().GetResult();
            }
            if (roleManager.FindByNameAsync("manager").GetAwaiter().GetResult() == null)
            {
                roleManager.CreateAsync(new IdentityRole("manager")).GetAwaiter().GetResult();
            }
            if (userManager.FindByNameAsync(adminEmail).GetAwaiter().GetResult() == null)
            {
                ApplicationUser admin = new ApplicationUser { Email = adminEmail, UserName = adminEmail, Name = "admin" };
                IdentityResult result = userManager.CreateAsync(admin, "Admin1*").GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(admin, "admin").GetAwaiter().GetResult();
                }
            }
            if (userManager.FindByNameAsync(userEmail).GetAwaiter().GetResult() == null)
            {
                ApplicationUser user = new ApplicationUser { Email = userEmail, UserName = userEmail, Name = "user" };
                IdentityResult result = userManager.CreateAsync(user, "User1*").GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "user").GetAwaiter().GetResult();
                }
            }
            if (userManager.FindByNameAsync(managerEmail).GetAwaiter().GetResult() == null)
            {
                ApplicationUser manager = new ApplicationUser { Email = managerEmail, UserName = managerEmail, Name = "manager" };
                IdentityResult result = userManager.CreateAsync(manager, "Manager1*").GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(manager, "manager").GetAwaiter().GetResult();
                }
            }
        }
    }
}
