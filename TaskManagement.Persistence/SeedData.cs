using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManagment.Domain;
using TaskManagment.Domain.Enums;


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
                context.Database.Migrate();

                if (!context.Users.Any())
                {
                    var user = new UserEntity()
                    {
                        UserName = "pavlo@gmail.com",
                        FirstName = "Pavlo",
                        LastName = "Pavlovych",
                        Email = "pavlo@gmail.com",
                    };
                    userManager.CreateAsync(user, "Palvic123.").GetAwaiter().GetResult();
                    var tasks = new List<TaskEntity>()
                    {
                        new TaskEntity
                        { 
                            Name = "Red a book", 
                            CreatedBy = user,
                            Priority = TaskPriority.Low,
                            Status = Status.NotStarted,
                            CreatedDate = DateTime.Now,
                            
                        },
                        new TaskEntity
                        { 
                            Name = "Do something", 
                            CreatedBy = user,
                            Priority = TaskPriority.Low,
                            Status = Status.NotStarted,
                            CreatedDate = DateTime.Now
                        },
                        new TaskEntity
                        { 
                            Name = "Programing", 
                            CreatedBy= user,
                            Priority = TaskPriority.Low,
                            Status = Status.NotStarted,
                            CreatedDate = DateTime.Now
                        }
                    };
                    context.Tasks.AddRange(tasks);
                    context.SaveChanges();
                };
            }     
       }
    }
}
