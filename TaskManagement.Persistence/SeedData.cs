using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManagment.Domain;

namespace TaskManagement.Persistence
{
    public class SeedData
    {
       public static void Seed (WebApplication app)
       {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<TaskManagementDbContext>();
                var userManager = services.GetRequiredService<UserManager<UserEntity>>();
                if (!context.Users.Any())
                {
                    var user = new UserEntity()
                    {
                        UserName = "Palvo",
                        Email = "palvo@.com",
                    };
                    userManager.CreateAsync(user, "Palvic123.").GetAwaiter().GetResult();
                };
            }     
       }
    }
}
